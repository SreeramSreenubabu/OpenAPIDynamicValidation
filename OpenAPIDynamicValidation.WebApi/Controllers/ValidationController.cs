using Microsoft.AspNetCore.Mvc;
using OpenAPIDynamicValidation.BAL;
using OpenAPIDynamicValidation.DAL.Models;

namespace OpenAPIDynamicValidation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidationController : ControllerBase
    {
        private readonly ValidationService _validationService;

        public ValidationController(ValidationService validationService)
        {
            _validationService = validationService;
        }

        [HttpPost]
        public IActionResult ValidateModel([FromBody] ValidationModel model)
        {
            // Call the validation function
            var validationMessages = _validationService.ValidateModel(model);

            // If no errors, return a success message
            if (validationMessages.Count == 0)
            {
                return Ok("Model is valid.");
            }

            // If there are validation errors, return them in the response
            return BadRequest(new { errors = validationMessages });
        }
    }
}
