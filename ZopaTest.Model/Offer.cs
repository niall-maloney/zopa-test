namespace ZopaTest.Model
{
    public class Offer
    {
        public Offer(string lender, decimal rate, decimal available)
        {
            Lender = lender;
            Rate = rate;
            Available = available;
        }


        public string Lender { get; }

        public decimal Rate { get; }

        public decimal Available { get; }
    }
}