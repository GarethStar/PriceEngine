using ConsoleApp1.Model;

namespace ConsoleApp1.Service.QuotationSystem
{
    public interface IQuoteInterface
    {
        dynamic GetPrice(PriceRequest request);
    }
}
