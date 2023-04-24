namespace QuizplusApi.ViewModels.Payment
{
    public class PaymentSettings
    {
        public string StripeSecretKey { get; set; }
        public string ClientUrl { get; set; }
        public int AddedBy { get; set; }
    }
}