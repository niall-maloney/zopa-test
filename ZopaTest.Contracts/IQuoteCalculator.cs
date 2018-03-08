using System.Collections.Generic;
using ZopaTest.Model;

namespace ZopaTest.Contracts
{
    public interface IQuoteCalculator
    {
        Quote CalculateQuote(decimal loanRequest, IList<Offer> offers);
    }
}