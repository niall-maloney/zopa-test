using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ZopaTest.Contracts;
using ZopaTest.Model;

namespace ZopaTest.App
{
    public class ArgumentValidator : IArgumentValidator
    {
        private readonly ILogger _logger;
        private readonly IOffersReader _offersReader;
        private readonly int _loanIncrement = 100;
        private readonly int _maxLoanAmount = 15000;
        private readonly int _minLoanAmount = 1000;


        public ArgumentValidator(IConfiguration config, ILogger<ArgumentValidator> logger, IOffersReader offersReader) : this(logger, offersReader)
        {
            _minLoanAmount = config.GetValue<int>("minLoanAmount");
            _maxLoanAmount = config.GetValue<int>("maxLoanAmount");
            _loanIncrement = config.GetValue<int>("loanIncrement");
        }


        public ArgumentValidator(ILogger<ArgumentValidator> logger, IOffersReader offersReader)
        {
            _logger = logger;
            _offersReader = offersReader;
        }


        public bool TryParseLoanRequest(IList<string> arguments, out int loanAmount)
        {
            loanAmount = 0;

            if (!ValidateArguments(arguments)) return false;

            if (!int.TryParse(arguments[1], out loanAmount))
            {
                _logger.LogError("Unable to parse loan amount parameter! Integer expected.");

                return false;
            }

            if (loanAmount < _minLoanAmount)
            {
                _logger.LogError("Loan amount outside of accepted boundaries! Loan amount too small.");

                return false;
            }

            if (loanAmount > _maxLoanAmount)
            {
                _logger.LogError("Loan amount outside of accepted boundaries! Loan amount too large.");

                return false;
            }

            if (loanAmount % _loanIncrement != 0)
            {
                _logger.LogError("Loan amount outside of accepted boundaries! Loan amount must be divisible by 100.");

                return false;
            }

            return true;
        }


        public bool TryParseOffers(IList<string> arguments, out List<Offer> offers)
        {
            offers = new List<Offer>();

            var filePath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + arguments[0];

            if (!File.Exists(filePath))
            {
                _logger.LogError($"Unable to parse market data parameter! File {filePath} does not exist!.");

                return false;
            }

            offers = _offersReader.GetOffers(arguments[0]);

            if (offers == null) return false;

            return true;
        }


        private bool ValidateArguments(ICollection<string> arguments)
        {
            if (arguments.Count != 2)
            {
                _logger.LogError("Invalid number of input parameters!");

                return false;
            }

            return true;
        }
    }
}