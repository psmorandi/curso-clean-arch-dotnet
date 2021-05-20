namespace CleanArch.School.Application.Validators
{
    using System;
    using System.Linq;
    using Extensions;

    // ReSharper disable InconsistentNaming
    public class CpfValidator : ICpfValidator
    {
        private const int FACTOR_DIGIT_1 = 10;
        private const int FACTOR_DIGIT_2 = 11;
        private const int MAX_DIGITS_1 = 9;
        private const int MAX_DIGITS_2 = 10;

        bool ICpfValidator.IsValid(string cpf)
        {
            var cpfOnlyNumbers = cpf.OnlyNumbers();
            if (IsInvalidLength(cpfOnlyNumbers) || IsBlockedCpf(cpfOnlyNumbers)) return false;
            var firstVerificationDigit = CalculateVerificationDigit(cpfOnlyNumbers, FACTOR_DIGIT_1, MAX_DIGITS_1);
            var secondVerificationDigit = CalculateVerificationDigit(cpfOnlyNumbers, FACTOR_DIGIT_2, MAX_DIGITS_2);
            return GetVerificationDigit(cpfOnlyNumbers) == $"{firstVerificationDigit}{secondVerificationDigit}";
        }

        private static bool IsInvalidLength(string cpf) => cpf.Length != 11;

        private static bool IsBlockedCpf(string cpf)
        {
            var cpfNumbers = cpf.ToList();
            return cpfNumbers.TrueForAll(c => c == cpf[0]);
        }

        private static int CalculateVerificationDigit(string cpf, int factor, int max)
        {
            var multiplierFactor = factor;
            var total = cpf.ToArray().Take(max).Sum(digit => int.Parse(digit.ToString()) * multiplierFactor--);

            var resultModule11 = total % 11;

            return (resultModule11 < 2) ? 0 : (11 - resultModule11);
        }

        private static string GetVerificationDigit(string cpf) => new string(cpf.Skip(9).ToArray());
    }
}