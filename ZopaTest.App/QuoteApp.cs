using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using ZopaTest.Contracts;

namespace ZopaTest.App
{
    public class QuoteApp : IQuoteApp
    {
        private readonly IArgumentValidator _argumentValidator;
        private readonly ILogger<QuoteApp> _logger;
        private readonly IQuoteCalculator _quoteCalculator;


        public QuoteApp(ILogger<QuoteApp> logger, IArgumentValidator argumentValidator, IQuoteCalculator quoteCalculator)
        {
            _logger = logger;
            _argumentValidator = argumentValidator;
            _quoteCalculator = quoteCalculator;
        }


        public void Start(IList<string> arguments)
        {
            if (_argumentValidator.TryParseLoanRequest(arguments, out var loanRequest) &&
                _argumentValidator.TryParseOffers(arguments, out var offers))
            {
                var quote = _quoteCalculator.CalculateQuote(loanRequest, offers);

                Console.WriteLine($"Requested amount: {quote.RequestedAmount:c0}");
                Console.WriteLine($"Rate: {quote.Rate:p1}");
                Console.WriteLine($"Monthly repayment: {quote.MonthlyRepayment:c2}");
                Console.WriteLine($"Total repayment: {quote.TotalRepayment:c2}");
            }
            else
            {
                Console.WriteLine("Invalid arguments.");
            }
        }
    }
}