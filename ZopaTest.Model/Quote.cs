namespace ZopaTest.Model
{
    public class Quote
    {
        public Quote(decimal requestAmount, decimal rate, decimal monthlyRepayment, decimal totalRepayment)
        {
            RequestedAmount = requestAmount;
            Rate = rate;
            MonthlyRepayment = monthlyRepayment;
            TotalRepayment = totalRepayment;
        }


        public decimal RequestedAmount { get; }
        public decimal Rate { get; }
        public decimal MonthlyRepayment { get; }
        public decimal TotalRepayment { get; }
    }
}