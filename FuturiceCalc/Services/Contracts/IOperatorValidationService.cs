namespace FuturiceCalc.Services.Contracts
{
    /// <summary>
    /// interface for operator validation
    /// </summary>
    public interface IOperatorValidationService
    {
        /// <summary>
        /// checks for validation for given operators in a given string
        /// </summary>
        /// <param name="expressionToBeValidated">expression to be validated for given operators</param>
        /// <returns>if the validation succeeded or failed</returns>
        bool CheckForValidOperatorValidation(string expressionToBeValidated);
    }
}
