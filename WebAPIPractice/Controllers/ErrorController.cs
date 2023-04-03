using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIPractice.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        [Route("/local-development-error")]
        public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if(webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException("This is not a Development Environment!!");
            }
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message);
        }

        [HttpGet]
        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}
