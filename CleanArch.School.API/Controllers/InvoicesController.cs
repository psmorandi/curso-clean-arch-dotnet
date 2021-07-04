namespace CleanArch.School.API.Controllers
{
    using AutoMapper;
    using CleanArch.School.Application.UseCase;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Application.UseCase.Data;
    using Data;
    using System;
    using CleanArch.School.TypeExtensions;

    [ApiController]
    [Route("[controller]")]
    public class InvoicesController: ControllerBase
    {
        private readonly IPayInvoice payInvoice;
        private readonly IMapper mapper;

        public InvoicesController(IPayInvoice payInvoice, IMapper mapper)
        {
            this.payInvoice = payInvoice;
            this.mapper = mapper;
        }

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