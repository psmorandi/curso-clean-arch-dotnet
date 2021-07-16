namespace CleanArch.School.Domain.Entity
{
    using System.Text.RegularExpressions;
    using Exceptions;

    public class Name
    {
        private readonly Regex nameValidationRegex = new("^([A-Za-z]+ )+([A-Za-z])+$", RegexOptions.Compiled);

        public Name(string value)
        {
            if (!this.nameValidationRegex.IsMatch(value)) throw new InvalidNameException($"Name '{value}' is invalid.");
            this.Value = value;
        }

        public string Value { get; }
    }
}