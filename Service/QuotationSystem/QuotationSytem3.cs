using System.Dynamic;
using ConsoleApp1.Model;

namespace ConsoleApp1.Service.QuotationSystem
{
    public class QuotationSystem3 : IQuoteInterface
    {
        public QuotationSystem3(string url, string port)
        {

        }

        public dynamic GetPrice(PriceRequest request)
        {
            //makes a call to an external service - SNIP
            //var response = _someExternalService.PostHttpRequest(requestData);

            dynamic response = new ExpandoObject();
            response.Price = 92.67M;
            response.IsSuccess = true;
            response.Name = "zxcvbnm";
            response.Tax = 92.67M * 0.12M;

            return response;
        }
    }
}
