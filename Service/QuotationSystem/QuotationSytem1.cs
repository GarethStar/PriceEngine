using System.Dynamic;
using ConsoleApp1.Model;

namespace ConsoleApp1.Service.QuotationSystem
{
    public class QuotationSystem1 : IQuoteInterface
    {
        public QuotationSystem1(string url, string port)
        {

        }

        public dynamic GetPrice(PriceRequest request)
        {
            //makes a call to an external service - SNIP
            //var response = _someExternalService.PostHttpRequest(requestData);

            dynamic response = new ExpandoObject();
            response.Price = 123.45M;
            response.IsSuccess = true;
            response.Name = "Test Name";
            response.Tax = 123.45M * 0.12M;

            return response;
        }
    }
}
