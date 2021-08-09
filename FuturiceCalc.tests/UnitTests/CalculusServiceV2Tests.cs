using System;
using FluentAssertions;
using FuturiceCalc.Services;
using FuturiceCalc.Services.Contracts;
using NUnit.Framework;

namespace FuturiceCalc.tests.UnitTests
{
    public class CalculusServiceV2Tests
    {
        private ICalculusServiceV2 _calculusServiceV2;


        [SetUp]
        public void Setup()
        {
            _calculusServiceV2 = new CalculusServiceV2();
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
            var evaluatedExpressionValue = _calculusServiceV2.EvaluateExpression(expression);

            //Assert
            evaluatedExpressionValue.Should().Be(expectedResult);
        }
    }
}
