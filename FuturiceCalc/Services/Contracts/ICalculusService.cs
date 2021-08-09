namespace FuturiceCalc.Services.Contracts
{
    /// <summary>
    /// interface to hold functions related to calculus
    /// </summary>
    public interface ICalculusService
    {
        /// <summary>
        /// use ncalc library to evaluate expression
        /// </summary>
        /// <param name="expression">simplified calculus expression</param>
        /// <returns>evaluated response in decimal</returns>
        public decimal EvaluateExpression(string expression);
    }
}
