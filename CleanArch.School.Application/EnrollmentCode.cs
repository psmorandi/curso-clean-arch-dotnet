namespace CleanArch.School.Application
{
    using System;

    public class EnrollmentCode
    {
        public EnrollmentCode(Level level, Module module, Classroom classroom, int sequence, DateOnly issueDate) =>
            this.Value = $"{issueDate.Year:0000}{level.Code}{module.Code}{classroom.Code}{sequence:0000}";

        public string Value { get; }
    }
}