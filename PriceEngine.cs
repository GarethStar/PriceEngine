using System;
using System.Collections.Generic;
using ConsoleApp1.Model;
using ConsoleApp1.Service.QuotationSystem;

namespace ConsoleApp1
{

    public class PriceEngine
    {
        private List<IQuoteInterface> _quoteInterfaces;
        /// <summary>
        /// Constructor of the quote systems should enable greater enhance abiltiy
        /// and flexibitly for injecting new services through interface
        /// Also will be handy for testing
        /// </summary>
        /// <param name="quoteInterfaces"></param>
        public PriceEngine(List<IQuoteInterface> quoteInterfaces)
        {
            _quoteInterfaces = quoteInterfaces;
        }

        //pass request with risk data with details of a gadget, return the best price retrieved from 3 external quotation engines
        public decimal GetPrice(PriceRequest request, out decimal tax, out string insurerName, out string errorMessage)
        {
            //initialise return variables
            tax = 0;
            insurerName = "";

            //validation
            if (!validate(request, out errorMessage)) { return -1; }

            //now call 3 external system and get the best price
            decimal price = 0;

            foreach (var quoteInterface in _quoteInterfaces) {
                dynamic systemResponse = quoteInterface.GetPrice(request);
                setPriceUpdate(ref price, ref insurerName, ref tax, systemResponse);
            }

            if (price == 0)
            {
                errorMessage = "Price should not be 0";
                price = -1;
            }

            return price;
        }

        /// <summary>
        /// Might be over doing it but moving this logic to a new function.
        /// I feel makes it clear what all the encapsulated is intended to do
        /// </summary>
        /// <param name="price"></param>
        /// <param name="insurerName"></param>
        /// <param name="tax"></param>
        /// <param name="systemResponse"></param>
        private static void setPriceUpdate(ref decimal price, ref string insurerName, ref decimal tax,
            dynamic systemResponse)
        {
            bool isLowestPrice = (systemResponse.IsSuccess & (price == 0) )||
                    (systemResponse.IsSuccess & systemResponse.Price < price);

            if (isLowestPrice)
            {
                price = systemResponse.Price;
                insurerName = systemResponse.Name;
                tax = systemResponse.Tax;
            }

        }

        /// <summary>
        /// Separating the consern of validation, makes sections alot easier to read.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool validate(PriceRequest request, out string errorMessage)
        {
            errorMessage = "";

            if (request.RiskData == null)
            {
                errorMessage = "Risk Data is missing";
                return false;
            }

            if (String.IsNullOrEmpty(request.RiskData.FirstName))
            {
                errorMessage = "First name is required";
                return false;
            }

            if (String.IsNullOrEmpty(request.RiskData.LastName))
            {
                errorMessage = "Surname is required";
                return false;
            }

            if (request.RiskData.Value == 0)
            {
                errorMessage = "Value is required";

                return false;
            }
            return true;
        }
    }
}
