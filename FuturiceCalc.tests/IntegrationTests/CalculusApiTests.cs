using System.Net;
using System.Net.Http;
using System.Text.Json;
using FluentAssertions;
using FuturiceCalc.Infrastructure.Models;
using FuturiceCalc.Models;
using FuturiceCalc.tests.IntegrationTests.Helpers;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace FuturiceCalc.tests.IntegrationTests
{
    public class CalculusApiTests
    {
        private IHost _host;
        private IHostBuilder _hostBuilder;
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            _hostBuilder = TestServerHelper.GetHost();
            _host = _hostBuilder.Start();
            _client = _host.GetTestClient();
        }

        [Test]
        [TestCase("62hhs7")]
        [TestCase("MSsxKzErMQ=")]
        public void ShouldThrowExceptionWhenInvalidBase64Expression(string encodedExpression)
        {
            //Arrange
            //Act
            var response = TestServerHelper.GetCalculusResponse(_client, encodedExpression);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        
        [Test]
        [TestCase("62hhs7")]
        [TestCase("MSsxKzErMQ=")]
        public void ShouldHaveValidatorErrorWhenInvalidBase64Expression(string encodedExpression)
        {
            //Arrange
            //Act
            var response = TestServerHelper.GetCalculusResponse(_client, encodedExpression);
            var content = response.Content.ReadAsStringAsync().Result;
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(content);

            //Assert
            errorResponse.Should().NotBeNull();
            errorResponse.Message.Should().Be("Expression is not valid base 64 expression");
        }

        [Test]
        [TestCase("MSsxKzErMQ==", 4)]
        [TestCase("MSsxKygzLzMpICogKDErMSk=", 4)]
        public void ShouldReturnCorrectResponseForValidBase64Expression(string encodedExpression, decimal expectedResponse)
        {
            //Arrange
            //Act
            var response = TestServerHelper.GetCalculusResponse(_client, encodedExpression);
            var content = response.Content.ReadAsStringAsync().Result;
            var evaluatedExpressionResponse = JsonSerializer.Deserialize<EvaluatedExpressionResponse>(content);

            //Assert
            evaluatedExpressionResponse.Should().NotBeNull();
            evaluatedExpressionResponse.Result.Should().Be(expectedResponse);
        }

        [Test]
        [TestCase("MSsxKzErMQ==")]
        [TestCase("MSsxKygzLzMpICogKDErMSk=")]
        public void ShouldHaveSuccessStatusCodeForValidBase64Expression(string encodedExpression)
        {
            //Arrange
            //Act
            var response = TestServerHelper.GetCalculusResponse(_client, encodedExpression);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
