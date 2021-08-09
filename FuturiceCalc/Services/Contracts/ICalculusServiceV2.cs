namespace FuturiceCalc.Services.Contracts
{
    /// <summary>
    /// V2 interface to hold functions related to calculus
    /// </summary>
    public interface ICalculusServiceV2
    {
        /// <summary>
        /// using stacks to evaluate expression
        /// </summary>
        /// <param name="expression">simplified calculus expression</param>
        /// <returns>evaluated response in decimal</returns>
        public decimal EvaluateExpression(string expression);
    }
}
