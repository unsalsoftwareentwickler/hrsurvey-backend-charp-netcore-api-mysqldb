namespace QuizplusApi.ViewModels.Payment
{
    public class BillRegister
    {
        public string TransactionEmail { get; set; }
        public string UserEmail { get; set; }
        public string TransactionDetail { get; set; }
        public string StripeSessionId { get; set; }
        public string PaymentMode { get; set; }
        public int Price { get; set; }
        public int AddedBy { get; set; }
        public int BillingPlanId { get; set; }

    }
}