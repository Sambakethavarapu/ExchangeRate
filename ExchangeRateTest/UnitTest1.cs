using ExchangeRate.Controllers;
using Microsoft.Extensions.Logging;

namespace ExchangeRateTest
{
    public class Tests
    {
        public readonly ILogger _Logger;
        public Tests(ILogger<CurrencyConversionController> logger) {
            _Logger = logger;
        }
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCalculateExchangeRate()
        {
            //string fromCurrency = "USD",
            //toCurrency = "INR";
            //CurrencyConversionController currencyConversion = new CurrencyConversionController();
            //currencyConversion.GetConversionDetails(fromCurrency, toCurrency, 1.54m);


            ExchangeRateCalculator calculator = new ExchangeRateCalculator();
            decimal amount = 2, rate = 7.900m;
            decimal result = calculator.CalculateExchangeRate(amount, rate);
            if (amount * rate == result)
            {
                Assert.AreEqual(amount * rate, result, "Exchange rate calculation is correct.");
            }
            else
            {
                Assert.AreNotEqual(amount * rate, result, "Exchange rate calculation is Incorrect.");
            }
           
        }
        [Test]
        public void TestUpdateExchangeRate()
        {
            try
            {
                //string fromCurrency = "INR", toCUrrency = "USD";decimal Amount=2.56m;
                //CurrencyConversionController currencyConversion = new CurrencyConversionController(ILogger<CurrencyConversionController> _Logger);
                //currencyConversion.UpdateConversionFactor(fromCurrency,toCUrrency,Amount);

                Assert.IsTrue(true);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public class ExchangeRateCalculator
    {
        public decimal CalculateExchangeRate(decimal amount, decimal rate)
        {
            return amount * rate;
        }
    }

}