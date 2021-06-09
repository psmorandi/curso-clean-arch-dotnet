namespace CleanArch.School.Application
{
    using System;
    using Extensions;

    public class Classroom
    {
        public Classroom(string level, string module, string code, int capacity, DateTime startDate, DateTime endDate)
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

        public bool IsFinished(DateTime currentDate) => currentDate.Date.After(this.Period.EndDate);

        public int GetProgress(DateTime currentDate)
        {
            var numberOfDaysOfClass = this.Period.Days;
            var remainingDays = (currentDate - this.Period.StartDate).Days;
            return remainingDays * 100 / numberOfDaysOfClass;
        }
    }
}