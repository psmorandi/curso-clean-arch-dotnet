namespace CleanArch.School.Application
{
    using System;
    using System.Text.RegularExpressions;

    public class Name
    {
        private readonly Regex nameValidationRegex = new Regex("^([A-Za-z]+ )+([A-Za-z])+$", RegexOptions.Compiled);

        public Name(string value)
        {
            if (!this.nameValidationRegex.IsMatch(value)) throw new Exception("Invalid name.");
            this.Value = value;
        }

        public string Value { get; }
    }
}