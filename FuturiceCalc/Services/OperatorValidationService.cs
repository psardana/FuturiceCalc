using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FuturiceCalc.Services.Contracts;
using Microsoft.Extensions.Configuration;

namespace FuturiceCalc.Services
{
    /// <summary>
    /// class that implements OperatorValidationService interface
    /// </summary>
    public class OperatorValidationService : IOperatorValidationService
    {
        private readonly IConfiguration _configuration;

        public OperatorValidationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// checks for validation for given operators in a given string
        /// </summary>
        /// <param name="expressionToBeValidated">expression to be validated for given operators</param>
        /// <returns>if the validation succeeded or failed</returns>
        public bool CheckForValidOperatorValidation(string expressionToBeValidated)
        {
            char[] allowedOperators = GetAllowedOperators();
            string allowedCharsRegularExpression = GetAllowedCharExpression(allowedOperators);
            string completeRegularExpression = $@"^(?:\d{allowedCharsRegularExpression}| )*$";
            return Regex.IsMatch(expressionToBeValidated, completeRegularExpression) && CheckForValidBrackets(expressionToBeValidated);
        }

        /// <summary>
        /// checks for brackets validation for given operators in a given string
        /// </summary>
        /// <param name="expressionToBeValidated">expression to be validated for given operators</param>
        /// <returns>if the validation succeeded or failed</returns>
        private bool CheckForValidBrackets(string expressionToBeValidated)
        {
            return expressionToBeValidated.Count(l => l == '(') == expressionToBeValidated.Count(l => l == ')');
        }

        private char[] GetAllowedOperators()
        {
            char[] allowedOperators = Array.Empty<char>();
            string allowedOperatorsFromConfig = _configuration["AllowedOperators"];
            if (!string.IsNullOrWhiteSpace(allowedOperatorsFromConfig))
            {
                allowedOperators = allowedOperatorsFromConfig.ToCharArray();
            }

            return allowedOperators;
        }

        private string GetAllowedCharExpression(char[] allowedOperators)
        {
            StringBuilder allowedCharsRegularExpression = new StringBuilder();

            foreach (var allowedOperator in allowedOperators)
            {
                allowedCharsRegularExpression.Append($@"|\{allowedOperator}");
            }

            return allowedCharsRegularExpression.ToString();
        }
    }
}
