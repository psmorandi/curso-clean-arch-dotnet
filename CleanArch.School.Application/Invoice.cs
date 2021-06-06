namespace CleanArch.School.Application
{
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;

    public class Invoice
    {
        public Invoice(int installments, decimal moduleValue)
        {
            var installmentValue = (moduleValue / installments).Truncate(2);
            this.Installments = installmentValue.Repeat(installments - 1);
            var lastInstallment = installmentValue + (moduleValue - installmentValue * installments);
            this.Installments.Add(lastInstallment);
        }

        public ICollection<decimal> Installments { get; }
        public decimal Total => this.Installments.Sum();
    }
}