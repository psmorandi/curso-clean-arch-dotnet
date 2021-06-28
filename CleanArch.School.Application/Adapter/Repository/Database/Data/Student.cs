namespace CleanArch.School.Application.Adapter.Repository.Database.Data
{
    using System;

    public class Student
    {
        public string Cpf { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }
}