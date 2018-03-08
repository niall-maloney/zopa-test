using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ZopaTest.Calculator;
using ZopaTest.Model;

namespace ZopaTest.Tests
{
    public class QuoteCalculatorTests
    {
        [Test]
        public void CalculateQuoteWithValidInputs_Should_ReturnValidQuotes()
        {
            var mockLogger = new Mock<ILogger<QuoteCalculator>>();

            var quoteCalculator = new QuoteCalculator(mockLogger.Object);
            var loanRequest = 1000.00m;
            var offers = new List<Offer>
            {
                new Offer ("Bob", 0.075m, 640.00m),
                new Offer("Jane",0.069m,480.00m),
                new Offer("Fred",0.071m,520.00m),
                new Offer("Mary",0.104m,170.00m),
                new Offer("John",0.081m,320.00m),
                new Offer("Dave",0.074m,140.00m),
                new Offer("Angela",0.071m,60.00m)
            };

            var quote = quoteCalculator.CalculateQuote(loanRequest, offers);

            Assert.AreEqual(0.072m, quote.Rate);
            Assert.AreEqual(34.452822378738333333333333333m, quote.MonthlyRepayment);
            Assert.AreEqual(1240.30160563458m, quote.TotalRepayment);
        }
    }
}