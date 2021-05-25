namespace CleanArch.School.UnitTests
{
    using System;
    using Application;
    using Xunit;

    public class CpfValidatorTests
    {
        [Theory]
        [InlineData("00000000000")]
        [InlineData("11111111111")]
        [InlineData("22222222222")]
        [InlineData("33333333333")]
        [InlineData("44444444444")]
        [InlineData("444.444.444-44")]
        public void Should_Return_Invalid_To_Cpf_With_Repeated_Digits(string cpf)
        {
            Assert.Throws<Exception>(() => new Cpf(cpf));
        }

        [Theory]
        [InlineData("86446422799")]
        [InlineData("864.464.227-99")]
        [InlineData("")]
        public void Should_Return_Invalid_To_Invalid_Verification_Digits(string cpf)
        {
            Assert.Throws<Exception>(() => new Cpf(cpf));
        }

        [Theory]
        [InlineData("86446422784")]
        [InlineData("864.464.227-84")]
        [InlineData("91720489726")]
        [InlineData("832.081.519-34")]
        public void Should_Return_Valid_To_Valid_Cpf(string cpf)
        {
            Assert.False(new Cpf(cpf).Value == string.Empty);
        }
    }
}