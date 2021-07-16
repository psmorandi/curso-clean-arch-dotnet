namespace CleanArch.School.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Exceptions;
    using Application.UseCase.Data;
    using Domain.Entity;
    using Domain.Exceptions;
    using TypeExtensions;
    using Xunit;

    public class EnrollStudentTests : BaseEnrollmentTests
    {
        [Fact]
        public async Task Should_not_enroll_without_valid_student_name()
        {
            var enrollmentRequest = new EnrollStudentInputData
                                    {
                                        StudentName = "Ana"
                                    };
            await Assert.ThrowsAsync<InvalidNameException>(() => this.enrollStudent.Execute(enrollmentRequest, new DateOnly().UtcNow()));
        }

        [Theory]
        [MemberData(nameof(GenerateInvalidCpfData))]
        public async Task Should_not_enroll_without_valid_student_cpf(EnrollStudentInputData enrollmentRequest)
        {
            await Assert.ThrowsAsync<InvalidCpfException>(() => this.enrollStudent.Execute(enrollmentRequest, new DateOnly().UtcNow()));
        }

        [Fact]
        public async Task Should_not_enroll_duplicated_student()
        {
            var refDate = new DateOnly().UtcNow();
            await this.levelRepository.Save(new Level("EM", "Ensino Médio"));
            await this.moduleRepository.Save(new Module("EM", "3", "3o Ano", 17, 17000));
            await this.classroomRepository.Save(new Classroom("EM", "3", "A", 5, refDate, refDate.AddMonths(6)));
            var enrollmentRequest = await this.CreateEnrollmentRequest("755.525.774-26", "EM", "3", "A");
            await this.enrollStudent.Execute(enrollmentRequest, refDate);
            await Assert.ThrowsAsync<StudentAlreadyEnrolledException>(() => this.enrollStudent.Execute(enrollmentRequest, refDate));
        }

        [Fact]
        public async Task Should_generate_enrollment_code()
        {
            var refDate = new DateOnly().UtcNow();
            await this.levelRepository.Save(new Level("EM", "Ensino Médio"));
            await this.moduleRepository.Save(new Module("EM", "3", "3o Ano", 17, 17000));
            await this.classroomRepository.Save(new Classroom("EM", "3", "A", 5, refDate, refDate.AddMonths(6)));
            var enrollmentRequest = await this.CreateEnrollmentRequest("755.525.774-26", "EM", "3", "A");
            var enrollResult = await this.enrollStudent.Execute(enrollmentRequest, refDate);
            var expectedEnrollCode =
                $"{DateTime.Now.Year}{enrollmentRequest.Level}{enrollmentRequest.Module}{enrollmentRequest.Class}0001";
            Assert.Equal(expectedEnrollCode, enrollResult.Code);
        }

        [Fact]
        public async Task Should_not_enroll_student_below_minimum_age()
        {
            var refDate = new DateOnly().UtcNow();
            await this.levelRepository.Save(new Level("EM", "Ensino Médio"));
            await this.moduleRepository.Save(new Module("EM", "3", "3o Ano", 17, 17000));
            await this.classroomRepository.Save(new Classroom("EM", "3", "A", 5, refDate, refDate.AddMonths(6)));
            var enrollmentRequest = await this.CreateEnrollmentRequest("755.525.774-26", "EM", "3", "A");
            enrollmentRequest.StudentBirthday = enrollmentRequest.StudentBirthday.AddYears(2);
            await Assert.ThrowsAsync<StudentBelowMinimumAgeException>(() => this.enrollStudent.Execute(enrollmentRequest, refDate));
        }

        [Fact]
        public async Task Should_not_enroll_student_over_class_capacity()
        {
            var refDate = new DateOnly().UtcNow();
            await this.levelRepository.Save(new Level("EM", "Ensino Médio"));
            await this.moduleRepository.Save(new Module("EM", "3", "3o Ano", 17, 17000));
            await this.classroomRepository.Save(new Classroom("EM", "3", "A", 2, refDate, refDate.AddMonths(6)));
            var enrollmentRequest1 = await this.CreateEnrollmentRequest("755.525.774-26", "EM", "3", "A");
            var enrollmentRequest2 = await this.CreateEnrollmentRequest("832.081.519-34", "EM", "3", "A");
            var enrollmentRequest3 = await this.CreateEnrollmentRequest("046.934.190-44", "EM", "3", "A");
            await this.enrollStudent.Execute(enrollmentRequest1, refDate);
            await this.enrollStudent.Execute(enrollmentRequest2, refDate);
            await Assert.ThrowsAsync<ClassroomOverCapacityException>(() => this.enrollStudent.Execute(enrollmentRequest3, refDate));
        }

        [Fact]
        public async Task Should_not_enroll_after_the_end_of_the_class()
        {
            var refDate = new DateOnly().UtcNow();
            await this.levelRepository.Save(new Level("EM", "Ensino Médio"));
            await this.moduleRepository.Save(new Module("EM", "3", "3o Ano", 17, 17000));
            await this.classroomRepository.Save(new Classroom("EM", "3", "B", 5, refDate.AddDays(-30), refDate.AddDays(-2)));
            var enrollmentRequest = await this.CreateEnrollmentRequest("755.525.774-26", "EM", "3", "B");
            await Assert.ThrowsAsync<ClassroomAlreadyFinishedException>(() => this.enrollStudent.Execute(enrollmentRequest, refDate));
        }

        [Fact]
        public async Task Should_not_enroll_after_25_percent_of_the_start_of_the_class()
        {
            var refDate = new DateOnly().UtcNow();
            await this.levelRepository.Save(new Level("EM", "Ensino Médio"));
            await this.moduleRepository.Save(new Module("EM", "3", "3o Ano", 17, 17000));
            await this.classroomRepository.Save(new Classroom("EM", "3", "C", 5, refDate.AddDays(-50), refDate.AddDays(50)));
            var enrollmentRequest = await this.CreateEnrollmentRequest("755.525.774-26", "EM", "3", "C");
            await Assert.ThrowsAsync<ClassroomAlreadyStartedException>(() => this.enrollStudent.Execute(enrollmentRequest, refDate));
        }

        [Fact]
        public async Task Should_generate_the_invoices_based_on_the_number_of_installments()
        {
            var refDate = new DateOnly().UtcNow();
            await this.levelRepository.Save(new Level("EM", "Ensino Médio"));
            await this.moduleRepository.Save(new Module("EM", "3", "3o Ano", 17, 17000));
            await this.classroomRepository.Save(new Classroom("EM", "3", "C", 2, refDate, refDate.AddMonths(6)));
            var enrollmentRequest = await this.CreateEnrollmentRequest("755.525.774-26", "EM", "3", "C");
            enrollmentRequest.Installments = 12;
            var enrollment = await this.enrollStudent.Execute(enrollmentRequest, refDate);
            Assert.True(enrollment.Invoices.Count == 12);
            Assert.True(enrollment.Invoices.ElementAt(0).Amount == new decimal(1416.66));
            Assert.True(enrollment.Invoices.ElementAt(11).Amount == new decimal(1416.74));
            Assert.True(enrollment.Invoices.Sum(_ => _.Amount) == 17000);
        }

        private static IEnumerable<object[]> GenerateInvalidCpfData()
        {
            yield return new object[]
                         {
                             new EnrollStudentInputData
                             {
                                 StudentName = "Ana Silva",
                                 StudentCpf = "123.456.789-99"
                             }
                         };
            yield return new object[]
                         {
                             new EnrollStudentInputData
                             {
                                 StudentName = "Ana Silva",
                                 StudentCpf = "111.111.111-11"
                             }
                         };
            yield return new object[]
                         {
                             new EnrollStudentInputData
                             {
                                 StudentName = "Ana Silva",
                                 StudentCpf = "000.000.000-00"
                             }
                         };
            yield return new object[]
                         {
                             new EnrollStudentInputData
                             {
                                 StudentName = "Ana Silva",
                                 StudentCpf = "00000000"
                             }
                         };
            yield return new object[]
                         {
                             new EnrollStudentInputData
                             {
                                 StudentName = "Ana Silva",
                                 StudentCpf = ""
                             }
                         };
            yield return new object[]
                         {
                             new EnrollStudentInputData
                             {
                                 StudentName = "Ana Silva"
                             }
                         };
        }
    }
}