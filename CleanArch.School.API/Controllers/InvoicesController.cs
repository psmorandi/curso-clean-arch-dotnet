namespace CleanArch.School.API.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application.UseCase;
    using Application.UseCase.Data;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using TypeExtensions;

    [ApiController]
    [Route("[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IPayInvoice payInvoice;

        public InvoicesController(IPayInvoice payInvoice) => this.payInvoice = payInvoice;

        [HttpPost]
        [Route("~/enrollments/{code}/invoices/{year:int}/{month:int}")]
        public async Task<IActionResult> PayInvoice(string code, int year, int month, [FromBody] PayInvoiceRequest payInvoiceRequest)
        {
            await this.payInvoice.Execute(
                new PayInvoiceInputData
                {
                    Code = code,
                    Year = year,
                    Month = month,
                    Amount = payInvoiceRequest.Amount!.Value,
                    PaymentDate = DateTime.UtcNow.ToDateOnly()
                });

            return this.Ok();
        }
    }
}