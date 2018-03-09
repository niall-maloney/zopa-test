using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ZopaTest.Contracts;
using ZopaTest.Model;

namespace ZopaTest.Calculator
{
    public class QuoteCalculator : IQuoteCalculator
    {
        private readonly int _loanDurationInMonth = 36;
        private ILogger<QuoteCalculator> _logger;


        public QuoteCalculator(IConfiguration config, ILogger<QuoteCalculator> logger) : this(logger)
        {
            _loanDurationInMonth = config.GetValue<int>("loanDuration");
        }


        public QuoteCalculator(ILogger<QuoteCalculator> logger)
        {
            _logger = logger;
        }


        public Quote CalculateQuote(decimal loanRequest, IList<Offer> offers)
        {
            var rate = CalculateRate(loanRequest, offers);
            var totalRepayment = loanRequest * (decimal) Math.Pow((double) (1 + rate / 12), _loanDurationInMonth);
            var monthlyRepayment = totalRepayment / _loanDurationInMonth;

            return new Quote(loanRequest, rate, monthlyRepayment, totalRepayment);
        }


        private static decimal CalculateRate(decimal loanAmount, IList<Offer> offers)
        {
            var totalBorrowed = 0.0m;
            var rate = 0.0m;
            var lenders = 0;

            foreach (var offer in offers)
            {
                decimal borrow;

                if (loanAmount < totalBorrowed + offer.Available)
                    borrow = loanAmount - totalBorrowed;
                else
                    borrow = offer.Available;

                rate += offer.Rate * (borrow/loanAmount);
                
                lenders++;

                if ((totalBorrowed += borrow) >= loanAmount) break;
            }

            return rate;
        }
    }
}