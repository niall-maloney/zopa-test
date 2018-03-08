using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ZopaTest.Readers;

namespace ZopaTest.Tests
{
    public class CsvOfferReaderTests
    {
        [TestCase("market-data.csv")]
        public void TryParseOffersWithValidArgs_Should_ReturnOffers(string filePath)
        {
            var mockLogger = new Mock<ILogger<CsvOfferReader>>();

            var csvReader = new CsvOfferReader(mockLogger.Object);

            var offers = csvReader.GetOffers(filePath);

            Assert.AreEqual(7, offers.Count);
        }


        [TestCase("mangled-data.csv")]
        public void TryParseOffersWithInvalidArgs_Should_ReturnFalse(string filePath)
        {
            var mockLogger = new Mock<ILogger<CsvOfferReader>>();

            var csvReader = new CsvOfferReader(mockLogger.Object);

            var offers = csvReader.GetOffers(filePath);

            Assert.IsNull(offers);
        }
    }
}