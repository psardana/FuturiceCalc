using System;
using FuturiceCalc.Services.Contracts;
using NCalc;

namespace FuturiceCalc.Services
{
    /// <summary>
    /// service that implements functions related to calculus
    /// </summary>
    public class CalculusService : ICalculusService
    {
        /// <summary>
        /// using ncalc library to evaluate expression
        /// </summary>
        /// <param name="expression">simplified calculus expression</param>
        /// <returns>evaluated response in decimal</returns>
        public decimal EvaluateExpression(string expression)
        {
            var evaluatedExpression = new Expression(expression).Evaluate();
            return Convert.ToDecimal(evaluatedExpression);
        }
    }
}
