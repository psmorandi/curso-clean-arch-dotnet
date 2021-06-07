namespace CleanArch.School.UnitTests
{
    using System;
    using System.Collections.Generic;
    using Application;
    using Application.Extensions;
    using Xunit;

    public class EnrollStudentTests : IDisposable
    {
        private readonly IModuleRepository moduleRepository;
        private readonly IClassroomRepository classRepository;
        private readonly EnrollStudent enrollStudent;

        public EnrollStudentTests()
        {
            this.moduleRepository = new ModuleRepositoryMemory();
            this.classRepository = new ClassroomRepositoryMemory();
            this.enrollStudent = new EnrollStudent(new EnrollmentRepositoryMemory(), new LevelRepositoryMemory(), this.moduleRepository, this.classRepository);
        }

        public void Dispose() { }

        [Fact]
        public void Should_not_enroll_without_valid_student_name()
        {
            var enrollmentRequest = new EnrollmentRequest
                                    {
                                        StudentName = "Ana"
                                    };
            var exception = Assert.Throws<Exception>(() => this.enrollStudent.Execute(enrollmentRequest));
            Assert.Equal("Invalid name.", exception.Message);
        }

        [Theory]
        [MemberData(nameof(GenerateInvalidCpfData))]
        public void Should_not_enroll_without_valid_student_cpf(EnrollmentRequest enrollmentRequest)
        {
            var exception = Assert.Throws<Exception>(() => this.enrollStudent.Execute(enrollmentRequest));
            Assert.Equal("Invalid cpf.", exception.Message);
        }

        [Fact]
        public void Should_not_enroll_duplicated_student()
        {
            var enrollmentRequest = this.CreateEnrollmentRequest("755.525.774-26", "EM", "3", "A");
            this.enrollStudent.Execute(enrollmentRequest);
            var exception = Assert.Throws<Exception>(() => this.enrollStudent.Execute(enrollmentRequest));
            Assert.Equal("Enrollment with duplicated student is not allowed.", exception.Message);
        }

        [Fact]
        public void Should_generate_enrollment_code()
        {
            var enrollmentRequest = this.CreateEnrollmentRequest("755.525.774-26", "EM", "3", "A");
            var enrollResult = this.enrollStudent.Execute(enrollmentRequest);
            var expectedEnrollCode =
                $"{DateTime.Now.Year}{enrollmentRequest.Level}{enrollmentRequest.Module}{enrollmentRequest.Class}0001";
            Assert.Equal(expectedEnrollCode, enrollResult.Code.Value);
        }

        [Fact]
        public void Should_not_enroll_student_below_minimum_age()
        {
            var enrollmentRequest = this.CreateEnrollmentRequest("755.525.774-26", "EM", "3", "A");
            enrollmentRequest.Birthday = enrollmentRequest.Birthday.AddYears(2);
            var exception = Assert.Throws<Exception>(() => this.enrollStudent.Execute(enrollmentRequest));
            Assert.Equal("Student below minimum age.", exception.Message);
        }

        [Fact]
        public void Should_not_enroll_student_over_class_capacity()
        {
            var classroom = this.classRepository.FindByCode("EM", "3", "A");
            classroom.SetCapacity(2);
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
            var classroom = this.classRepository.FindByCode("EM", "3", "B");
            classroom.SetClassPeriod(DateTime.Now.Date.AddDays(-30), DateTime.Now.Date.AddDays(-2));
            var enrollmentRequest = this.CreateEnrollmentRequest("755.525.774-26", "EM", "3", "B");
            var exception = Assert.Throws<Exception>(() => this.enrollStudent.Execute(enrollmentRequest));
            Assert.Equal("Class is already finished.", exception.Message);
        }

        [Fact]
        public void Should_not_enroll_after_25_percent_of_the_start_of_the_class()
        {
            var classroom = this.classRepository.FindByCode("EM", "3", "C");
            classroom.SetClassPeriod(DateTime.Now.Date.AddDays(-50), DateTime.Now.Date.AddDays(50));
            var enrollmentRequest = this.CreateEnrollmentRequest("755.525.774-26", "EM", "3", "C");
            var exception = Assert.Throws<Exception>(() => this.enrollStudent.Execute(enrollmentRequest));
            Assert.Equal("Class is already started.", exception.Message);
        }

        [Fact]
        public void Should_generate_the_invoices_based_on_the_number_of_installments()
        {
            var module = this.moduleRepository.FindByCode("EM", "3");
            var enrollmentRequest = this.CreateEnrollmentRequest("755.525.774-26", "EM", "3", "C");
            enrollmentRequest.Installments = 12;
            var enrollment = this.enrollStudent.Execute(enrollmentRequest);
            Assert.True(enrollment.Invoice.Installments.Count == 12);
            Assert.True(enrollment.Invoice.Total == module.Price);
        }

        private EnrollmentRequest CreateEnrollmentRequest(string cpf, string level, string module, string classroom)
        {
            var minimumAge = this.moduleRepository.FindByCode(level, module).MinimumAge;
            var classCode = this.classRepository.FindByCode(level, module, classroom).Code;
            return new EnrollmentRequest
                   {
                       StudentName = $"{StringExtensions.GenerateRandomString(5)} {StringExtensions.GenerateRandomString(7)}",
                       Cpf = cpf,
                       Birthday = DateTime.Now.Date.AddYears(-minimumAge),
                       Level = level,
                       Module = module,
                       Class = classCode
                   };
        }

        private static IEnumerable<object[]> GenerateInvalidCpfData()
        {
            yield return new object[]
                         {
                             new EnrollmentRequest
                             {
                                 StudentName = "Ana Silva",
                                 Cpf = "123.456.789-99"
                             }
                         };
            yield return new object[]
                         {
                             new EnrollmentRequest
                             {
                                 StudentName = "Ana Silva",
                                 Cpf = "111.111.111-11"
                             }
                         };
            yield return new object[]
                         {
                             new EnrollmentRequest
                             {
                                 StudentName = "Ana Silva",
                                 Cpf = "000.000.000-00"
                             }
                         };
            yield return new object[]
                         {
                             new EnrollmentRequest
                             {
                                 StudentName = "Ana Silva",
                                 Cpf = "00000000"
                             }
                         };
            yield return new object[]
                         {
                             new EnrollmentRequest
                             {
                                 StudentName = "Ana Silva",
                                 Cpf = ""
                             }
                         };
            yield return new object[]
                         {
                             new EnrollmentRequest
                             {
                                 StudentName = "Ana Silva"
                             }
                         };
        }
    }
}