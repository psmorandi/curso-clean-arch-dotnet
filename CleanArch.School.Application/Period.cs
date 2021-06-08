namespace CleanArch.School.Application
{
    using System;
    using Extensions;

    public class Period
    {
        public Period(DateTime startDate, DateTime endDate)
        {
            if (startDate.After(endDate)) throw new Exception("Start date can't be after the end.");
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public int Days => (this.EndDate - this.StartDate).Days;
    }
}