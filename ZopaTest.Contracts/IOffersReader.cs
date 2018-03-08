using System.Collections.Generic;
using ZopaTest.Model;

namespace ZopaTest.Contracts
{
    public interface IOffersReader
    {
        List<Offer> GetOffers(string filePath);
    }
}