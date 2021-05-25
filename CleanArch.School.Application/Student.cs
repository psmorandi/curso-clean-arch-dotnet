namespace CleanArch.School.Application
{
    public class Student
    {
        public Student(string name, string cpf)
        {
            this.Name = new Name(name);
            this.Cpf = new Cpf(cpf);
        }

        public Name Name { get; }
        public Cpf Cpf { get; }
    }
}