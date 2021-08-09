using FluentAssertions;
using FuturiceCalc.Services;
using FuturiceCalc.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace FuturiceCalc.tests.UnitTests
{
    public class OperatorValidationServiceTests
    {
        private Mock<IConfiguration> _configuration;
        private IOperatorValidationService _operatorValidationService;


        [SetUp]
        public void Setup()
        {
            _configuration = new Mock<IConfiguration>();
            _operatorValidationService = new OperatorValidationService(_configuration.Object);
        }

        [Test]
        public void ShouldReturnFalseWhenExpressionHasMoreThanAllowedOperators()
        {
            //Arrange
            _configuration.SetupGet(m => m[It.Is<string>(s => s == "AllowedOperators")]).Returns("-+");

            //Act
            var isValidExpression = _operatorValidationService.CheckForValidOperatorValidation("2*2");

            //Assert
            isValidExpression.Should().BeFalse();
        }

        [Test]
        public void ShouldReturnTrueWhenExpressionHasAllowedOperators()
        {
            //Arrange
            _configuration.SetupGet(m => m[It.Is<string>(s => s == "AllowedOperators")]).Returns("-+*");

            //Act
            var isValidExpression = _operatorValidationService.CheckForValidOperatorValidation("2*2");

            //Assert
            isValidExpression.Should().BeTrue();
        }
    }
}