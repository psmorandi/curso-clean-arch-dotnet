namespace CleanArch.School.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application;
    using Application.Extensions;
    using Xunit;

    public class EnrollStudentTests
    {
        [Fact]
        public void Should_not_enroll_without_valid_student_name()
        {
            var enrollmentRequest = new EnrollmentRequest
                                    {
                                        StudentName = "Ana"
                                    };
            var enrollStudent = CreateEnrollStudent(new Storage());
            var exception = Assert.Throws<Exception>(() => enrollStudent.Execute(enrollmentRequest));
            Assert.Equal("Invalid name.", exception.Message);
        }

        [Theory]
        [MemberData(nameof(GenerateInvalidCpfData))]
        public void Should_not_enroll_without_valid_student_cpf(EnrollmentRequest enrollmentRequest)
        {
            var enrollStudent = CreateEnrollStudent(new Storage());
            var exception = Assert.Throws<Exception>(() => enrollStudent.Execute(enrollmentRequest));
            Assert.Equal("Invalid cpf.", exception.Message);
        }

        [Fact]
        public void Should_not_enroll_duplicated_student()
        {
            var storage = new Storage();
            var @class = FindClass(storage, "A", "3", "EM");
            var enrollmentRequest = CreateEnrollmentRequest("755.525.774-26", @class);
            var enrollStudent = CreateEnrollStudent(storage);
            enrollStudent.Execute(enrollmentRequest);
            var exception = Assert.Throws<Exception>(() => enrollStudent.Execute(enrollmentRequest));
            Assert.Equal("Enrollment with duplicated student is not allowed.", exception.Message);
        }

        [Fact]
        public void Should_generate_enrollment_code()
        {
            var storage = new Storage();
            var @class = FindClass(storage, "A", "3", "EM");
            var enrollmentRequest = CreateEnrollmentRequest("755.525.774-26", @class);
            var enrollStudent = CreateEnrollStudent(storage);
            var enrollResult = enrollStudent.Execute(enrollmentRequest);
            var expectedEnrollCode =
                $"{DateTime.Now.Year}{enrollmentRequest.Level}{enrollmentRequest.Module}{enrollmentRequest.Class}0001";
            Assert.Equal(expectedEnrollCode, enrollResult.EnrollmentCode);
        }

        [Fact]
        public void Should_not_enroll_student_below_minimum_age()
        {
            var storage = new Storage();
            var @class = FindClass(storage, "A", "3", "EM");
            var enrollmentRequest = CreateEnrollmentRequest("755.525.774-26", @class);
            enrollmentRequest.Birthday = enrollmentRequest.Birthday.AddYears(2);
            var enrollStudent = CreateEnrollStudent(storage);
            var exception = Assert.Throws<Exception>(() => enrollStudent.Execute(enrollmentRequest));
            Assert.Equal("Student below minimum age.", exception.Message);
        }

        [Fact]
        public void Should_not_enroll_student_over_class_capacity()
        {
            var storage = new Storage();
            var @class = FindClass(storage, "A", "3", "EM");
            @class.Capacity = 2;
            var enrollmentRequest1 = CreateEnrollmentRequest("755.525.774-26", @class);
            var enrollmentRequest2 = CreateEnrollmentRequest("832.081.519-34", @class);
            var enrollmentRequest3 = CreateEnrollmentRequest("046.934.190-44", @class);
            var enrollStudent = CreateEnrollStudent(storage);
            enrollStudent.Execute(enrollmentRequest1);
            enrollStudent.Execute(enrollmentRequest2);
            var exception = Assert.Throws<Exception>(() => enrollStudent.Execute(enrollmentRequest3));
            Assert.Equal("Class is over capacity.", exception.Message);
        }

        private static EnrollStudent CreateEnrollStudent(Storage storage) => new EnrollStudent(storage);

        private static ClassTable FindClass(Storage storage, string @class, string module, string level) =>
            storage.Data.Classes.SingleOrDefault(c => c.Level.Code == level && c.Module.Code == module && c.Code == @class) ?? throw new Exception("Not found");

        private static EnrollmentRequest CreateEnrollmentRequest(string cpf, ClassTable @class) =>
            new EnrollmentRequest
            {
                StudentName = $"{StringExtensions.GenerateRandomString(5)} {StringExtensions.GenerateRandomString(7)}",
                Cpf = cpf,
                Birthday = DateTime.Now.Date.AddYears(-@class.Module.MinimumAge),
                Level = @class.Level.Code,
                Module = @class.Module.Code,
                Class = @class.Code
            };

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