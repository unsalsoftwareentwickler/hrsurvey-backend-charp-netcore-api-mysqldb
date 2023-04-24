using System;
using System.Collections.Generic;
using System.Linq;
using QuizplusApi.Models;
using QuizplusApi.Models.Others;
using QuizplusApi.ViewModels.Payment;

namespace QuizplusApi.Services
{
    public class PaymentService
    {
        private readonly AppDbContext _context;
        public PaymentService(AppDbContext context)
        {
            _context=context;
        }
        public int InsertBillingPayment(BillRegister model)
        {
            int paymentId=0;
            var objBilling=_context.BillingPlans.FirstOrDefault(q=>q.Price==model.Price);
            var objDuplicatePaymentCheck=_context.BillingPayments.FirstOrDefault(q=>q.StripeSessionId==model.StripeSessionId && q.PaymentMode=="Online");
            if(objDuplicatePaymentCheck==null)
            {
                var objPayment=new BillingPayment();
                objPayment.Title=objBilling.Title;
                objPayment.Price=objBilling.Price;
                objPayment.Interval=objBilling.Interval;
                objPayment.AssessmentCount=objBilling.AssessmentCount;
                objPayment.QuestionPerAssessmentCount=objBilling.QuestionPerAssessmentCount;
                objPayment.ResponsePerAssessmentCount=objBilling.ResponsePerAssessmentCount;
                if(model.PaymentMode=="Online")
                {
                    objPayment.PaymentMode=model.PaymentMode;
                    objPayment.TransactionEmail=model.TransactionEmail;
                    objPayment.UserEmail=model.UserEmail;
                    objPayment.StripeSessionId=model.StripeSessionId;
                    objPayment.TransactionDetail=model.TransactionDetail;
                }
                else if(model.PaymentMode=="Offline")
                {
                    objPayment.PaymentMode=model.PaymentMode;
                    objPayment.StripeSessionId="";
                    objPayment.TransactionEmail=model.TransactionEmail;
                    objPayment.UserEmail=model.TransactionEmail;
                    objPayment.AddedBy=model.AddedBy;
                    objPayment.TransactionDetail=model.TransactionDetail;
                }
                else
                {
                    objPayment.PaymentMode="N/A";
                    objPayment.StripeSessionId="";
                    objPayment.TransactionEmail="";
                    objPayment.UserEmail=model.TransactionEmail;
                    objPayment.AddedBy=model.AddedBy;
                    objPayment.TransactionDetail=model.TransactionDetail;
                }
                
                objPayment.IsActive=true;
                objPayment.DateAdded=DateTime.Now;
                if(objPayment.Interval=="Monthly")
                {
                    objPayment.StartDate=DateTime.Now;
                    objPayment.EndDate=DateTime.Now.AddMonths(1);
                }
                else if(objPayment.Interval=="Yearly")
                {
                    objPayment.StartDate=DateTime.Now;
                    objPayment.EndDate=DateTime.Now.AddYears(1);
                }
                _context.BillingPayments.Add(objPayment);
                _context.SaveChanges();
                paymentId=objPayment.BillingPaymentId;
            }
            return paymentId;
        }
    }
}