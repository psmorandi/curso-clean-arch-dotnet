namespace CleanArch.School.API.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application.UseCase;
    using Application.UseCase.Data;
    using AutoMapper;
    using Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using TypeExtensions;

    [ApiController]
    [Route("[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollStudent enrollStudentUseCase;
        private readonly IGetEnrollment getEnrollmentUseCase;
        private readonly ICancelEnrollment cancelEnrollmentUseCase;
        private readonly IMapper mapper;

        public EnrollmentsController(
            IEnrollStudent enrollStudentUseCase,
            IGetEnrollment getEnrollmentUseCase,
            ICancelEnrollment cancelEnrollmentUseCase,
            IMapper mapper)
        {
            this.enrollStudentUseCase = enrollStudentUseCase;
            this.getEnrollmentUseCase = getEnrollmentUseCase;
            this.cancelEnrollmentUseCase = cancelEnrollmentUseCase;
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
        public async Task<IActionResult> ChangeEnrollmentStatus(string code)
        {
            await this.cancelEnrollmentUseCase.Execute(code);
            return this.Ok();
        }
    }
}