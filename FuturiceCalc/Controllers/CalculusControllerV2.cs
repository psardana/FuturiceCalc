using FuturiceCalc.Infrastructure.Attributes;
using FuturiceCalc.Models;
using FuturiceCalc.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FuturiceCalc.Controllers
{
    /// <summary>
    /// this provides end points for calculus 
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [ApiVersion("2.0")]
    public class CalculusControllerV2 : ControllerBase
    {
        private readonly ICalculusServiceV2 _calculusService;
        private readonly IOperatorValidationService _operatorValidationService;

        public CalculusControllerV2(ICalculusServiceV2 calculusService,
            IOperatorValidationService operatorValidationService)
        {
            _calculusService = calculusService;
            _operatorValidationService = operatorValidationService;
        }


        /// <summary>
        /// Evaluates a given mathematical expression for the allowed operations + - * / ( ) 
        /// </summary>
        /// <param name="calculusParam">base 64 encoded expression</param>
        /// <returns> decimal response of the mathematical expression</returns>
        [HttpGet]
        [Route("api/evaluate-expression/{calculusParam}")]
        [QueryBase64Decode]
        public IActionResult EvaluateExpression(string calculusParam)
        {
            if (_operatorValidationService.CheckForValidOperatorValidation(calculusParam))
            {
                decimal evaluatedExpression = _calculusService.EvaluateExpression(calculusParam);
                return Ok(new EvaluatedExpressionResponse
                {
                    Error = false,
                    Result = evaluatedExpression
                });
            }

            return BadRequest("Expression has one or more invalid operators. Allowed Operators are + - * / ( )");
        }
    }
}
