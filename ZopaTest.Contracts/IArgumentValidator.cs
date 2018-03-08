using System.Collections.Generic;
using ZopaTest.Model;

namespace ZopaTest.Contracts
{
    public interface IArgumentValidator
    {
        bool TryParseLoanRequest(IList<string> arguments, out int loanRequest);

        bool TryParseOffers(IList<string> arguments, out List<Offer> offers);
    }
}