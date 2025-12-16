using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using System.Globalization;

namespace HotelManagement.Services
{
    public class PayPalService
    {
        private readonly IConfiguration _config;

        public PayPalService(IConfiguration config)
        {
            _config = config;
        }
        private PayPalHttpClient GetClient()
        {
            PayPalEnvironment env;

            if (_config["PayPal:Mode"] == "live")
            {
                env = new LiveEnvironment(
                    _config["PayPal:ClientId"],
                    _config["PayPal:Secret"]
                );
            }
            else
            {
                env = new SandboxEnvironment(
                    _config["PayPal:ClientId"],
                    _config["PayPal:Secret"]
                );
            }

            return new PayPalHttpClient(env);
        }

        public async Task<string> CreatePayment(decimal amount, string returnUrl, string cancelUrl)
        {
            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");

            request.RequestBody(new OrderRequest
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>
            {
                new PurchaseUnitRequest
                {
                    AmountWithBreakdown = new AmountWithBreakdown
                    {
                        CurrencyCode = "USD",
                        Value = amount.ToString("F2", CultureInfo.InvariantCulture)
                    }
                }
            },
                ApplicationContext = new ApplicationContext
                {
                    ReturnUrl = returnUrl,
                    CancelUrl = cancelUrl
                }
            });

            var response = await GetClient().Execute(request);
            var result = response.Result<Order>();

            return result.Links.First(x => x.Rel == "approve").Href;
        }
    }
}
