using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Microsoft.Extensions.Logging;
using ZopaTest.Contracts;
using ZopaTest.Model;

namespace ZopaTest.Readers
{
    public class CsvOfferReader : IOffersReader
    {
        private readonly ILogger<CsvOfferReader> _logger;


        public CsvOfferReader(ILogger<CsvOfferReader> logger)
        {
            _logger = logger;
        }


        public List<Offer> GetOffers(string filePath)
        {
            try
            {
                using (var streamReader = File.OpenText(filePath))
                using (var csvReader = new CsvReader(streamReader))
                {
                    return csvReader.GetRecords<CsvRow>().Select(r => new Offer(r.Lender, r.Rate, r.Available)).OrderBy(o => o.Rate).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to read and parse csv : {filePath}", ex);
                return null;
            }
        }
    }
}