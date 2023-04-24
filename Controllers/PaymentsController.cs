using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuizplusApi.Models;
using QuizplusApi.Models.Others;
using QuizplusApi.Models.Quiz;
using Stripe;
using Stripe.Checkout;

namespace QuizplusApi.Controllers
{
    public class PaymentsController:Controller
    {
        private AppDbContext _context;
        private IStripeClient client;
        private readonly ISqlRepository<QuizTopic> _quizTopicRepo;
        private readonly ISqlRepository<BillingPlan> _billingPlan;
        SiteSettings objSettings;
        public PaymentsController(
                                AppDbContext context,                           
                                ISqlRepository<QuizTopic> quizTopicRepo,
                                ISqlRepository<BillingPlan> billingPlan
                                )
        {
            _context = context;
            _quizTopicRepo=quizTopicRepo;
            _billingPlan=billingPlan;
        }

        [HttpGet("checkout-session")]
        public async Task<Session> GetCheckoutSession(string sessionId)
        {
            objSettings=_context.SiteSettings.OrderBy(q=>q.DateAdded).FirstOrDefault();
            this.client=new StripeClient(objSettings.StripeSecretKey);
            var service=new SessionService(this.client);
            var session = await service.GetAsync(sessionId);
            return session;
        }

        [HttpGet("checkout-session-quiz-pay")]
        public async Task<Session> GetCheckoutSessionQuizPay(string sessionId,int adminId)
        {
            objSettings=_context.SiteSettings.SingleOrDefault(q=>q.AddedBy==adminId);
            this.client=new StripeClient(objSettings.StripeSecretKey);
            var service=new SessionService(this.client);
            var session = await service.GetAsync(sessionId);
            return session;
        }

        [HttpPost("create-checkout-session-plan")]
        public ActionResult CreateCheckoutPlan(int billingId)
        {
            objSettings=_context.SiteSettings.OrderBy(q=>q.DateAdded).FirstOrDefault();
            StripeConfiguration.ApiKey=objSettings.StripeSecretKey;
            var objBilling=_billingPlan.SelectById(billingId);
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = objBilling.Price*100,
                            Currency = objSettings.Currency.ToLower(),
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Registration Price",
                            },

                        },
                        Quantity = 1,                                          
                    },
                },
                Mode = "payment",
                SuccessUrl = objSettings.ClientUrl+"/signIn?session_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = objSettings.ClientUrl,               
            };

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        [HttpPost("create-checkout-renew-plan")]
        public ActionResult CreateCheckoutRenew(int billingId)
        {
            objSettings=_context.SiteSettings.OrderBy(q=>q.DateAdded).FirstOrDefault();
            StripeConfiguration.ApiKey=objSettings.StripeSecretKey;
            var objBilling=_billingPlan.SelectById(billingId);
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = objBilling.Price*100,
                            Currency = objSettings.Currency.ToLower(),
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Renewal Price",
                            },

                        },
                        Quantity = 1,                                          
                    },
                },
                Mode = "payment",
                SuccessUrl = objSettings.ClientUrl+"/dashboard?renew_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = objSettings.ClientUrl+"/dashboard",               
            };

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        [HttpPost("pay-for-quiz")]
        public ActionResult PayforQuiz(int quizTopicId,int adminId)
        {
            objSettings=_context.SiteSettings.SingleOrDefault(q=>q.AddedBy==adminId);
            StripeConfiguration.ApiKey=objSettings.StripeSecretKey;
            int price=_quizTopicRepo.SelectById(quizTopicId).QuizPrice;
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = price*100,
                            Currency = objSettings.Currency.ToLower(),
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Assessment Price",
                            },

                        },
                        Quantity = 1,                                          
                    },
                },
                Mode = "payment",
                SuccessUrl = objSettings.ClientUrl+"/dashboard?session_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = objSettings.ClientUrl+"/dashboard",            
            };

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
    }
}