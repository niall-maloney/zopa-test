using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ZopaTest.App;
using ZopaTest.Contracts;
using ZopaTest.Model;

namespace ZopaTest.Tests
{
    public class ArgumentValidatorTests
    {
        [TestCase((object) new[] {"unused", "1000"})]
        [TestCase((object) new[] {"unused", "2500"})]
        [TestCase((object) new[] {"unused", "14900"})]
        [TestCase((object) new[] {"unused", "15000"})]
        public void TryParseLoanAmountWithValidArgs_Should_ReturnTrueAndParsedInteger(string[] args)
        {
            var mockLogger = new Mock<ILogger<ArgumentValidator>>();
            var mockReader = new Mock<IOffersReader>();

            var argsValidator = new ArgumentValidator(mockLogger.Object, mockReader.Object);

            Assert.IsTrue(argsValidator.TryParseLoanRequest(args, out var loanRequest));
            Assert.IsInstanceOf<int>(loanRequest);
        }


        [TestCase((object) new[] {"unused", "100"})]
        [TestCase((object) new[] {"unused", "1001"})]
        [TestCase((object) new[] {"unused", "100000"})]
        [TestCase((object) new[] {"unused", "10000.00"})]
        [TestCase((object) new[] {"unused", "some string"})]
        public void TryParseLoanAmountWithInvalidArgs_Should_ReturnFalse(string[] args)
        {
            var mockLogger = new Mock<ILogger<ArgumentValidator>>();
            var mockReader = new Mock<IOffersReader>();

            var argsValidator = new ArgumentValidator(mockLogger.Object, mockReader.Object);

            Assert.IsFalse(argsValidator.TryParseLoanRequest(args, out var loanRequest));
        }


        [TestCase((object) new[] {"market-data.csv", "unused"})]
        public void TryParseOffersWithValidArgs_Should_ReturnTrueAndOffers(string[] args)
        {
            var mockLogger = new Mock<ILogger<ArgumentValidator>>();
            var mockReader = new Mock<IOffersReader>();
            mockReader.Setup(r => r.GetOffers(args[0])).Returns(new List<Offer>());

            var argsValidator = new ArgumentValidator(mockLogger.Object, mockReader.Object);

            Assert.IsTrue(argsValidator.TryParseOffers(args, out var offers));
        }


        [TestCase((object) new[] {"non-exist.csv", "unused"})]
        public void TryParseOffersWithInvalidArgs_Should_ReturnFalse(string[] args)
        {
            var mockLogger = new Mock<ILogger<ArgumentValidator>>();
            var mockReader = new Mock<IOffersReader>();
            mockReader.Setup(r => r.GetOffers(args[0])).Returns(new List<Offer>());

            var argsValidator = new ArgumentValidator(mockLogger.Object, mockReader.Object);

            Assert.IsFalse(argsValidator.TryParseOffers(args, out var offers));
        }
    }
}