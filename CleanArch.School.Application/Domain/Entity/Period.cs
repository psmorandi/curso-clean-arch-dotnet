namespace CleanArch.School.Application.Domain.Entity
{
    using System;
    using Extensions;

    public class Period
    {
        public Period(DateOnly startDate, DateOnly endDate)
        {
            if (startDate > endDate) throw new Exception("Start date can't be after the end.");
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public DateOnly StartDate { get; }
        public DateOnly EndDate { get; }

        public int Days => (this.EndDate.ToDateTime() - this.StartDate.ToDateTime()).Days;
    }
}