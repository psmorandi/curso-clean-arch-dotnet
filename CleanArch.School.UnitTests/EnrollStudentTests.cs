namespace CleanArch.School.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Domain.Entity;
    using Application.Domain.UseCase;
    using Application.Domain.UseCase.Data;
    using Application.Extensions;
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
            var exception = await Assert.ThrowsAsync<Exception>(() => this.enrollStudent.Execute(enrollmentRequest, new DateOnly().UtcNow()));
            Assert.Equal("Invalid name.", exception.Message);
        }

        [Theory]
        [MemberData(nameof(GenerateInvalidCpfData))]
        public async Task Should_not_enroll_without_valid_student_cpf(EnrollStudentInputData enrollmentRequest)
        {
            var exception = await Assert.ThrowsAsync<Exception>(() => this.enrollStudent.Execute(enrollmentRequest, new DateOnly().UtcNow()));
            Assert.Equal("Invalid cpf.", exception.Message);
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
            var exception = await Assert.ThrowsAsync<Exception>(() => this.enrollStudent.Execute(enrollmentRequest, refDate));
            Assert.Equal("Enrollment with duplicated student is not allowed.", exception.Message);
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
            var exception = await Assert.ThrowsAsync<Exception>(() => this.enrollStudent.Execute(enrollmentRequest, refDate));
            Assert.Equal("Student below minimum age.", exception.Message);
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
            var exception = await Assert.ThrowsAsync<Exception>(() => this.enrollStudent.Execute(enrollmentRequest3, refDate));
            Assert.Equal("Class is over capacity.", exception.Message);
        }

        [Fact]
        public async Task Should_not_enroll_after_the_end_of_the_class()
        {
            var refDate = new DateOnly().UtcNow();
            await this.levelRepository.Save(new Level("EM", "Ensino Médio"));
            await this.moduleRepository.Save(new Module("EM", "3", "3o Ano", 17, 17000));
            await this.classroomRepository.Save(new Classroom("EM", "3", "B", 5, refDate.AddDays(-30), refDate.AddDays(-2)));
            var enrollmentRequest = await this.CreateEnrollmentRequest("755.525.774-26", "EM", "3", "B");
            var exception = await Assert.ThrowsAsync<Exception>(() => this.enrollStudent.Execute(enrollmentRequest, refDate));
            Assert.Equal("Class is already finished.", exception.Message);
        }

        [Fact]
        public async Task Should_not_enroll_after_25_percent_of_the_start_of_the_class()
        {
            var refDate = new DateOnly().UtcNow();
            await this.levelRepository.Save(new Level("EM", "Ensino Médio"));
            await this.moduleRepository.Save(new Module("EM", "3", "3o Ano", 17, 17000));
            await this.classroomRepository.Save(new Classroom("EM", "3", "C", 5, refDate.AddDays(-50), refDate.AddDays(50)));
            var enrollmentRequest = await this.CreateEnrollmentRequest("755.525.774-26", "EM", "3", "C");
            var exception = await Assert.ThrowsAsync<Exception>(() => this.enrollStudent.Execute(enrollmentRequest, refDate));
            Assert.Equal("Class is already started.", exception.Message);
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