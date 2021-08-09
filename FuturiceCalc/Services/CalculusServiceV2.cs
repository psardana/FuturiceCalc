using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FuturiceCalc.Services.Contracts;

namespace FuturiceCalc.Services
{
    /// <summary>
    /// v2 service that implements functions related to calculus
    /// </summary>
    public class CalculusServiceV2 : ICalculusServiceV2
    {
        /// <summary>
        /// using stacks to evaluate expression
        /// </summary>
        /// <param name="expression">simplified calculus expression</param>
        /// <returns>evaluated response in decimal</returns>
        public decimal EvaluateExpression(string expression)
        {
            expression = expression.Replace(" ", "");
            List<string> listToHoldExpressionAndResult = new List<string>();
            string currentValue = string.Empty;

            for (int i = 0; i < expression.Length; i++)
            {
                string stringWithFirstChar = expression.Substring(i, 1);
                char firstCharOfExpression = stringWithFirstChar.ToCharArray().First();

                if (!char.IsDigit(firstCharOfExpression) && firstCharOfExpression != '.' && currentValue != string.Empty)
                {
                    listToHoldExpressionAndResult.Add(currentValue);
                    currentValue = string.Empty;
                }
                if (firstCharOfExpression == '(')
                {
                    i = ResolveBrackets(expression, i, listToHoldExpressionAndResult);
                }
                else if (firstCharOfExpression == '+' || firstCharOfExpression == '-' || firstCharOfExpression == '*' ||
                         firstCharOfExpression == '/')
                {
                    listToHoldExpressionAndResult.Add(stringWithFirstChar);
                }
                else if (char.IsDigit(firstCharOfExpression) || firstCharOfExpression == '.')
                {
                    currentValue += stringWithFirstChar;
                    if (i == expression.Length - 1)
                    {
                        listToHoldExpressionAndResult.Add(currentValue);
                    }
                }
            }

            return EvaluateValuesFromList(listToHoldExpressionAndResult);
        }

        /// <summary>
        /// evaluates the stack if the items are more and equal to three
        /// </summary>
        /// <param name="listToHoldExpressionAndResult"></param>
        private static decimal EvaluateValuesFromList(List<string> listToHoldExpressionAndResult)
        {
            for (int i = listToHoldExpressionAndResult.Count - 2; i >= 0; i--)
            {
                if (listToHoldExpressionAndResult[i] == "/")
                {
                    listToHoldExpressionAndResult[i] = (decimal.Parse(listToHoldExpressionAndResult[i - 1], CultureInfo.InvariantCulture) /
                                                        decimal.Parse(listToHoldExpressionAndResult[i + 1], CultureInfo.InvariantCulture))
                        .ToString(CultureInfo.InvariantCulture);

                    listToHoldExpressionAndResult.RemoveAt(i + 1);
                    listToHoldExpressionAndResult.RemoveAt(i - 1);
                    i -= 2;
                }
            }

            for (int i = listToHoldExpressionAndResult.Count - 2; i >= 0; i--)
            {
                if (listToHoldExpressionAndResult[i] == "*")
                {
                    listToHoldExpressionAndResult[i] = (decimal.Parse(listToHoldExpressionAndResult[i + 1], CultureInfo.InvariantCulture) *
                                                        decimal.Parse(listToHoldExpressionAndResult[i - 1], CultureInfo.InvariantCulture))
                        .ToString(CultureInfo.InvariantCulture);

                    listToHoldExpressionAndResult.RemoveAt(i + 1);
                    listToHoldExpressionAndResult.RemoveAt(i - 1);
                    i -= 2;
                }
            }

            for (int i = listToHoldExpressionAndResult.Count - 2; i >= 0; i--)
            {
                if (listToHoldExpressionAndResult[i] == "+")
                {
                    listToHoldExpressionAndResult[i] = (decimal.Parse(listToHoldExpressionAndResult[i + 1], CultureInfo.InvariantCulture) +
                                                        decimal.Parse(listToHoldExpressionAndResult[i - 1], CultureInfo.InvariantCulture))
                        .ToString(CultureInfo.InvariantCulture);

                    listToHoldExpressionAndResult.RemoveAt(i + 1);
                    listToHoldExpressionAndResult.RemoveAt(i - 1);
                    i -= 2;
                }
            }

            for (int i = listToHoldExpressionAndResult.Count - 2; i >= 0; i--)
            {
                if (listToHoldExpressionAndResult[i] == "-")
                {
                    listToHoldExpressionAndResult[i] = (decimal.Parse(listToHoldExpressionAndResult[i - 1], CultureInfo.InvariantCulture) -
                                                        decimal.Parse(listToHoldExpressionAndResult[i + 1], CultureInfo.InvariantCulture))
                        .ToString(CultureInfo.InvariantCulture);

                    listToHoldExpressionAndResult.RemoveAt(i + 1);
                    listToHoldExpressionAndResult.RemoveAt(i - 1);
                    i -= 2;
                }
            }
            return decimal.Parse(listToHoldExpressionAndResult.First(), CultureInfo.InvariantCulture);
        }


        /// <summary>
        /// based on the occurrence of brackets, resolves them and add the bracket expression to the stack
        /// </summary>
        /// <param name="expression">expression to be evaluated</param>
        /// <param name="i">current index</param>
        /// <param name="listToHoldExpressionAndResult"></param>
        /// <returns></returns>
        private int ResolveBrackets(string expression, int i,
            List<string> listToHoldExpressionAndResult)
        {
            string innerExpression = string.Empty;
            i++;
            int brackets = 0;
            for (; i < expression.Length; i++)
            {
               char firstCharOfExpression = expression.Substring(i, 1).ToCharArray().First();
                if (firstCharOfExpression == '(')
                {
                    brackets++;
                }

                if (firstCharOfExpression == ')')
                {
                    if (brackets == 0)
                    {
                        break;
                    }

                    brackets--;
                }

                innerExpression += firstCharOfExpression;
            }

            listToHoldExpressionAndResult.Add(EvaluateExpression(innerExpression).ToString(CultureInfo.InvariantCulture));

            return i;
        }
    }
}
