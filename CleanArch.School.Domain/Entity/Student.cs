namespace CleanArch.School.Domain.Entity
{
    using System;
    using TypeExtensions;

    public class Student
    {
        public Student(string name, string cpf, DateTime birthday)
        {
            this.Name = new Name(name);
            this.Cpf = new Cpf(cpf);
            this.Birthday = birthday.ToDateOnly();
        }

        public Name Name { get; }
        public Cpf Cpf { get; }
        public DateOnly Birthday { get; }

        public int Age
        {
            get
            {
                var today = DateTime.Now.ToDateOnly();
                var age = today.Year - this.Birthday.Year;
                return today.Month < this.Birthday.Month || today.Month == this.Birthday.Month && today.Day < this.Birthday.Day ? age - 1 : age;
            }
        }
    }
}