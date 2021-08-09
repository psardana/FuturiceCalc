using System;
using FluentAssertions;
using FuturiceCalc.Services;
using FuturiceCalc.Services.Contracts;
using NUnit.Framework;

namespace FuturiceCalc.tests.UnitTests
{
    public class CalculusServiceTests
    {
        private ICalculusService _calculusService;


        [SetUp]
        public void Setup()
        {
            _calculusService = new CalculusService();
        }

        [Test]
        [TestCase("2*2", 4)]
        [TestCase("2*2-2", 2)]
        [TestCase("2*2-(2*5)", -6)]
        [TestCase("(3/(5-2))-(2*5)", -9)]
        public void ShouldReturnCorrectResultForExpression(string expression, decimal expectedResult)
        {
            //Arrange
            //Act
            var evaluatedExpressionValue = _calculusService.EvaluateExpression(expression);

            //Assert
            evaluatedExpressionValue.Should().Be(expectedResult);
        }

        [Test]
        public void ShouldThrowExceptionForAlphaNumericExpression()
        {
            //Arrange
            //Act
            Action act = () =>  _calculusService.EvaluateExpression("(x/(5-2))-(2*5)");

            //Assert
            act.Should().Throw<ArgumentException>();
        }
    }
}
