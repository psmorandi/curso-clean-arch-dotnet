namespace CleanArch.School.Application.UseCase
{
    using System.Threading.Tasks;
    using Data;

    public interface IPayInvoice
    {
        Task Execute(PayInvoiceInputData request);
    }
}