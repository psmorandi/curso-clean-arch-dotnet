﻿namespace CleanArch.School.Application.Adapter.Repository.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Entity;
    using Domain.Repository;

    public class EnrollmentRepositoryMemory : IEnrollmentRepository
    {
        private readonly ICollection<Enrollment> enrollments;

        public EnrollmentRepositoryMemory() => this.enrollments = new List<Enrollment>();

        public void Save(Enrollment enrollment)
        {
            this.enrollments.Add(enrollment);
        }

        public ICollection<Enrollment> FindAllByClass(string level, string module, string classroom) =>
            this.enrollments.Where(e => e.Level.Code == level && e.Module.Code == module && e.Class.Code == classroom).ToList();

        public Enrollment? FindByCpf(string cpf) => this.enrollments.SingleOrDefault(_ => _.Student.Cpf.Value == cpf);

        public Enrollment FindByCode(string code) =>
            this.enrollments.SingleOrDefault(_ => _.Code.Value == code) ?? throw new Exception("Enrollment not found.");

        public int Count() => this.enrollments.Count;
    }
}