using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FuturiceCalc.Infrastructure.Attributes
{
    /// <summary>
    /// attribute for decoding base 64 input string
    /// </summary>
    public class QueryBase64DecodeAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            string calculusParam = context.ActionArguments["calculusParam"] as string;

            if (!string.IsNullOrWhiteSpace(calculusParam) && IsBase64String(calculusParam))
            {
                context.ActionArguments["calculusParam"] =
                    Encoding.UTF8.GetString(
                        Convert.FromBase64String(calculusParam));
            }
            else
            {
                throw new ValidationException("Expression is not valid base 64 expression");
            }

            await next();
        }

        /// <summary>
        /// checks if the expression is valid base 64
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }
    }
}
