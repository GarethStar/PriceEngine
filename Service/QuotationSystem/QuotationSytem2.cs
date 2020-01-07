using System.Dynamic;
using ConsoleApp1.Model;

namespace ConsoleApp1.Service.QuotationSystem
{
    public class QuotationSystem2 : IQuoteInterface
    {
        public QuotationSystem2(string url, string port)
        {
        }

        public dynamic GetPrice(PriceRequest request)
        {
            dynamic response = new ExpandoObject();
            if (request.RiskData.Make == "examplemake1" || request.RiskData.Make == "examplemake2" ||
                request.RiskData.Make == "examplemake3")
            {
                //makes a call to an external service - SNIP
                //var response = _someExternalService.PostHttpRequest(requestData);

                response.Price = 234.56M;
                //would have this dynamic response morphed into an object with a standed success property
                response.IsSuccess = true;
                response.Name = "qewtrywrh";
                response.Tax = 234.56M * 0.12M;

                return response;
            }
            else
            {
                response.IsSuccess = false;
                response.Price = 0;
                return response;
            }
        }
    }
}
