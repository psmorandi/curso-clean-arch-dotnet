namespace CleanArch.School.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application;
    using Xunit;

    public class EnrollStudentTests : BaseEnrollmentTests
    {
        [Fact]
        public void Should_not_enroll_without_valid_student_name()
        {
            var enrollmentRequest = new EnrollStudentInputData
                                    {
                                        StudentName = "Ana"
                                    };
            var exception = Assert.Throws<Exception>(() => this.enrollStudent.Execute(enrollmentRequest));
            Assert.Equal("Invalid name.", exception.Message);
        }

        [Theory]
        [MemberData(nameof(GenerateInvalidCpfData))]
        public void Should_not_enroll_without_valid_student_cpf(EnrollStudentInputData enrollmentRequest)
        {
            var exception = Assert.Throws<Exception>(() => this.enrollStudent.Execute(enrollmentRequest));
            Assert.Equal("Invalid cpf.", exception.Message);
        }

        [Fact]
        public void Should_not_enroll_duplicated_student()
        {
            this.levelRepository.Save(new Level("EM", "Ensino Médio"));
            this.moduleRepository.Save(new Module("EM", "3", "3o Ano", 17, 17000));
            this.classroomRepository.Save(new Classroom("EM", "3", "A", 5, DateTime.Now.Date, DateTime.Now.Date.AddMonths(6)));
            var enrollmentRequest = this.CreateEnrollmentRequest("755.525.774-26", "EM", "3", "A");
            this.enrollStudent.Execute(enrollmentRequest);
            var exception = Assert.Throws<Exception>(() => this.enrollStudent.Execute(enrollmentRequest));
            Assert.Equal("Enrollment with duplicated student is not allowed.", exception.Message);
        }

        [Fact]
        public void Should_generate_enrollment_code()
        {
            this.levelRepository.Save(new Level("EM", "Ensino Médio"));
            this.moduleRepository.Save(new Module("EM", "3", "3o Ano", 17, 17000));
            this.classroomRepository.Save(new Classroom("EM", "3", "A", 5, DateTime.Now.Date, DateTime.Now.Date.AddMonths(6)));
            var enrollmentRequest = this.CreateEnrollmentRequest("755.525.774-26", "EM", "3", "A");
            var enrollResult = this.enrollStudent.Execute(enrollmentRequest);
            var expectedEnrollCode =
                $"{DateTime.Now.Year}{enrollmentRequest.Level}{enrollmentRequest.Module}{enrollmentRequest.Class}0001";
            Assert.Equal(expectedEnrollCode, enrollResult.Code);
        }

        [Fact]
        public void Should_not_enroll_student_below_minimum_age()
        {
            this.levelRepository.Save(new Level("EM", "Ensino Médio"));
            this.moduleRepository.Save(new Module("EM", "3", "3o Ano", 17, 17000));
            this.classroomRepository.Save(new Classroom("EM", "3", "A", 5, DateTime.Now.Date, DateTime.Now.Date.AddMonths(6)));
            var enrollmentRequest = this.CreateEnrollmentRequest("755.525.774-26", "EM", "3", "A");
            enrollmentRequest.StudentBirthday = enrollmentRequest.StudentBirthday.AddYears(2);
            var exception = Assert.Throws<Exception>(() => this.enrollStudent.Execute(enrollmentRequest));
            Assert.Equal("Student below minimum age.", exception.Message);
        }

        [Fact]
        public void Should_not_enroll_student_over_class_capacity()
        {
            this.levelRepository.Save(new Level("EM", "Ensino Médio"));
            this.moduleRepository.Save(new Module("EM", "3", "3o Ano", 17, 17000));
            this.classroomRepository.Save(new Classroom("EM", "3", "A", 2, DateTime.Now.Date, DateTime.Now.Date.AddMonths(6)));
            var enrollmentRequest1 = this.CreateEnrollmentRequest("755.525.774-26", "EM", "3", "A");
            var enrollmentRequest2 = this.CreateEnrollmentRequest("832.081.519-34", "EM", "3", "A");
            var enrollmentRequest3 = this.CreateEnrollmentRequest("046.934.190-44", "EM", "3", "A");
            this.enrollStudent.Execute(enrollmentRequest1);
            this.enrollStudent.Execute(enrollmentRequest2);
            var exception = Assert.Throws<Exception>(() => this.enrollStudent.Execute(enrollmentRequest3));
            Assert.Equal("Class is over capacity.", exception.Message);
        }

        [Fact]
        public void Should_not_enroll_after_the_end_of_the_class()
        {
            this.levelRepository.Save(new Level("EM", "Ensino Médio"));
            this.moduleRepository.Save(new Module("EM", "3", "3o Ano", 17, 17000));
            this.classroomRepository.Save(new Classroom("EM", "3", "B", 5, DateTime.Now.Date.AddDays(-30), DateTime.Now.Date.AddDays(-2)));
            var enrollmentRequest = this.CreateEnrollmentRequest("755.525.774-26", "EM", "3", "B");
            var exception = Assert.Throws<Exception>(() => this.enrollStudent.Execute(enrollmentRequest));
            Assert.Equal("Class is already finished.", exception.Message);
        }

        [Fact]
        public void Should_not_enroll_after_25_percent_of_the_start_of_the_class()
        {
            this.levelRepository.Save(new Level("EM", "Ensino Médio"));
            this.moduleRepository.Save(new Module("EM", "3", "3o Ano", 17, 17000));
            this.classroomRepository.Save(new Classroom("EM", "3", "C", 5, DateTime.Now.Date.AddDays(-50), DateTime.Now.Date.AddDays(50)));
            var enrollmentRequest = this.CreateEnrollmentRequest("755.525.774-26", "EM", "3", "C");
            var exception = Assert.Throws<Exception>(() => this.enrollStudent.Execute(enrollmentRequest));
            Assert.Equal("Class is already started.", exception.Message);
        }

        [Fact]
        public void Should_generate_the_invoices_based_on_the_number_of_installments()
        {
            this.levelRepository.Save(new Level("EM", "Ensino Médio"));
            this.moduleRepository.Save(new Module("EM", "3", "3o Ano", 17, 17000));
            this.classroomRepository.Save(new Classroom("EM", "3", "C", 2, DateTime.Now.Date, DateTime.Now.Date.AddMonths(6)));
            var enrollmentRequest = this.CreateEnrollmentRequest("755.525.774-26", "EM", "3", "C");
            enrollmentRequest.Installments = 12;
            var enrollment = this.enrollStudent.Execute(enrollmentRequest);
            Assert.True(enrollment.Invoices.Count == 12);
            Assert.True(enrollment.Invoices.Single(i => i.Month == 1).Amount == new decimal(1416.66));
            Assert.True(enrollment.Invoices.Single(i => i.Month == 12).Amount == new decimal(1416.74));
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