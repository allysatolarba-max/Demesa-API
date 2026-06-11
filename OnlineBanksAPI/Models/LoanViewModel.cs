namespace OnlineBanksAPI.Models
{
    public class LoanViewModel
    {
        //public Guid LoanId { get; set; }

        public int LoanAmount { get; set; }

        public int LoanPeriod { get; set; }

        public double MonthlyPayment { get; set; }
    }
}
