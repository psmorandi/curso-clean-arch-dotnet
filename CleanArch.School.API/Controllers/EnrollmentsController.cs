namespace CleanArch.School.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.UseCase;
    using Application.UseCase.Data;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using TypeExtensions;
    using WebApp.Shared.Data;

    [ApiController]
    [Route("[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollStudent enrollStudentUseCase;
        private readonly IGetEnrollment getEnrollmentUseCase;
        private readonly ICancelEnrollment cancelEnrollmentUseCase;
        private readonly IPayInvoice payInvoice;
        private readonly IGetAllEnrollments getAllEnrollmentsUseCase;
        private readonly IMapper mapper;

        public EnrollmentsController(
            IEnrollStudent enrollStudentUseCase,
            IGetEnrollment getEnrollmentUseCase,
            ICancelEnrollment cancelEnrollmentUseCase,
            IPayInvoice payInvoice,
            IGetAllEnrollments getAllEnrollmentsUseCase,
            IMapper mapper)
        {
            this.enrollStudentUseCase = enrollStudentUseCase;
            this.getEnrollmentUseCase = getEnrollmentUseCase;
            this.cancelEnrollmentUseCase = cancelEnrollmentUseCase;
            this.payInvoice = payInvoice;
            this.getAllEnrollmentsUseCase = getAllEnrollmentsUseCase;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("{code}")]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EnrollmentResponse), StatusCodes.Status200OK)]
        public async Task<EnrollmentResponse> Get(string code)
        {
            var outputData = await this.getEnrollmentUseCase.Execute(code, DateTime.UtcNow.ToDateOnly());
            return this.mapper.Map<EnrollmentResponse>(outputData);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<EnrollmentResponse>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<EnrollmentResponse>> Get()
        {
            var outputData = await this.getAllEnrollmentsUseCase.Execute(DateTime.UtcNow.ToDateOnly());
            return this.mapper.Map<IEnumerable<EnrollmentResponse>>(outputData);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EnrollStudentResponse), StatusCodes.Status200OK)]
        public async Task<EnrollStudentResponse> Post([FromBody] EnrollStudentRequest enrollRequest)
        {
            var inputData = this.mapper.Map<EnrollStudentInputData>(enrollRequest);
            var outputData = await this.enrollStudentUseCase.Execute(inputData, DateTime.UtcNow.ToDateOnly());
            return this.mapper.Map<EnrollStudentResponse>(outputData);
        }

        [HttpPut]
        [Route("{code}/cancel")]
        public async Task<IActionResult> CancelEnrollment(string code)
        {
            await this.cancelEnrollmentUseCase.Execute(code);
            return this.Ok();
        }

        [HttpPut]
        [Route("{code}/invoices/{year:int}/{month:int}")]
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