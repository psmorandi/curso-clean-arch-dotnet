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
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public string Level { get; }
        public string Module { get; }
        public string Code { get; }
        public int Capacity { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public void SetCapacity(int capacity)
        {
            if (capacity < 0) throw new Exception("Invalid capacity.");
            this.Capacity = capacity;
        }

        public void SetClassPeriod(DateTime start, DateTime end)
        {
            if (start.After(end)) throw new Exception("Start date can't be after the end.");
            this.StartDate = start;
            this.EndDate = end;
        }
    }
}