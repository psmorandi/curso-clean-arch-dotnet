namespace CleanArch.School.API.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application.Adapter.Controller;
    using Application.UseCase.Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentController enrollmentController;

        public EnrollmentsController(IEnrollmentController enrollmentController) => this.enrollmentController = enrollmentController;

        [HttpGet]
        [Route("~/enrollments/{code}")]
        [ProducesResponseType(typeof(EnrollStudentOutputData), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<object> Get(string code)
        {
            var response = await this.enrollmentController.GetEnrollment(code, DateTime.UtcNow);
            if (response.StatusCode != StatusCodes.Status200OK) return this.Problem(response.Data.ToString(), statusCode: response.StatusCode);
            return response.Data;
        }

        [HttpPost]
        [Route("~/enrollments")]
        [ProducesResponseType(typeof(GetEnrollmentOutputData), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<object> Get([FromBody] EnrollStudentInputData enrollRequest)
        {
            //create enroll dto request and response
            var response = await this.enrollmentController.EnrollStudent(enrollRequest, DateTime.UtcNow);
            if (response.StatusCode != StatusCodes.Status200OK) return this.Problem(response.Data.ToString(), statusCode: response.StatusCode);
            return response.Data;
        }
    }
}