using System;
using System.Collections.Generic;
using ConsoleApp1.Model;
using ConsoleApp1.Service.QuotationSystem;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //SNIP - collect input (risk data from the user)

            var request = new PriceRequest()
            {
                RiskData = new RiskData() //hardcoded here, but would normally be from user input above
                {
                    DOB = DateTime.Parse("1980-01-01"),
                    FirstName = "John",
                    LastName = "Smith",
                    Make = "Cool New Phone",
                    Value = 500
                }
            };

            decimal tax = 0;
            string insurer = "";
            string error = "";

            List<IQuoteInterface> quoteInterfaces = new List<IQuoteInterface> {
                new QuotationSystem1("http://quote-system-1.com", "1235"),
                new QuotationSystem2("http://quote-system-2.com", "1235"),
                new QuotationSystem3("http://quote-system-3.com", "1235")
            };

            var priceEngine = new PriceEngine(quoteInterfaces);
            var price = priceEngine.GetPrice(request, out tax, out insurer, out error);

            if (price == -1)
            {
                Console.WriteLine(String.Format("There was an error - {0}", error));
            }
            else
            {
                Console.WriteLine(String.Format("You price is {0}, from insurer: {1}. This includes tax of {2}", price, insurer, tax));
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
