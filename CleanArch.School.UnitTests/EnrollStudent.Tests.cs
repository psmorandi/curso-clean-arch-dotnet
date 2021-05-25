namespace CleanArch.School.UnitTests
{
    using System;
    using System.Collections.Generic;
    using Application;
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
            var enrollStudent = new EnrollStudent();
            var exception = Assert.Throws<Exception>(() => enrollStudent.Execute(enrollmentRequest));
            Assert.Equal("Invalid name.", exception.Message);
        }

        [Theory]
        [MemberData(nameof(GenerateInvalidCpfData))]
        public void Should_not_enroll_without_valid_student_cpf(EnrollmentRequest enrollmentRequest)
        {
            var enrollStudent = new EnrollStudent();
            var exception = Assert.Throws<Exception>(() => enrollStudent.Execute(enrollmentRequest));
            Assert.Equal("Invalid cpf.", exception.Message);
        }

        [Fact]
        public void Should_not_enroll_duplicated_student()
        {
            var enrollmentRequest = new EnrollmentRequest
                                    {
                                        StudentName = "Ana Silva",
                                        Cpf = "832.081.519-34"
                                    };
            var enrollStudent = new EnrollStudent();
            enrollStudent.Execute(enrollmentRequest);
            var exception = Assert.Throws<Exception>(() => enrollStudent.Execute(enrollmentRequest));
            Assert.Equal("Enrollment with duplicated student is not allowed.", exception.Message);
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