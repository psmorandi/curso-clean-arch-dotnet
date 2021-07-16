namespace CleanArch.School.Domain.Entity
{
    using System;
    using Exceptions;
    using TypeExtensions;

    public class Period
    {
        public Period(DateOnly startDate, DateOnly endDate)
        {
            if (startDate > endDate) throw new InvalidPeriodException($"Start date ({startDate}) can't be after the end ({endDate}).");
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public DateOnly StartDate { get; }
        public DateOnly EndDate { get; }

        public int Days => (this.EndDate.ToDateTime() - this.StartDate.ToDateTime()).Days;
    }
}