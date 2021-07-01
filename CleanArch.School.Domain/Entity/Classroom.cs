namespace CleanArch.School.Domain.Entity
{
    using System;
    using TypeExtensions;

    public class Classroom
    {
        public Classroom(string level, string module, string code, int capacity, DateOnly startDate, DateOnly endDate)
        {
            this.Level = level;
            this.Module = module;
            this.Code = code;
            this.Capacity = capacity;
            this.Period = new Period(startDate, endDate);
        }

        public string Level { get; }
        public string Module { get; }
        public string Code { get; }
        public int Capacity { get; }
        public Period Period { get; }

        public bool IsFinished(DateOnly currentDate) => currentDate > this.Period.EndDate;

        public int GetProgress(DateOnly currentDate)
        {
            var numberOfDaysOfClass = this.Period.Days;
            var remainingDays = (currentDate.ToDateTime() - this.Period.StartDate.ToDateTime()).Days;
            return remainingDays * 100 / numberOfDaysOfClass;
        }
    }
}