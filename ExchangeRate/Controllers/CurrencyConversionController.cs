using ExchangeRate.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting.Server;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using Microsoft.Extensions.Logging;
using System.Reflection;
using ExchangeRate.BusinessEntities;

namespace ExchangeRate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyConversionController : ControllerBase
    {
        public readonly ILogger<CurrencyConversionController> _logger;
        public CurrencyConversionController(ILogger<CurrencyConversionController> logger) {
            _logger = logger;
        }

        [Route("api/GetConversionDetails")]
        [HttpGet]
        public IActionResult GetConversionDetails(string fromCurrency, string toCurrency, decimal Amount)
        {
            try
            {
                object[] param = { fromCurrency, toCurrency, Amount };
                _logger.LogInformation("Inside the Method" + MethodBase.GetCurrentMethod(), param);
                string sourceCurrency = fromCurrency.ToUpper();
                string targetCurrency = toCurrency.ToUpper();
                if ((string.IsNullOrEmpty(sourceCurrency) || string.IsNullOrEmpty(targetCurrency)))// || (int.Parse(sourceCurrency) >= 0 || int.Parse(targetCurrency) >= 0))
                {
                    return new JsonResult("CurrencyCode should not be Empty or any NumericValues " + sourceCurrency);
                }
                if (Amount < 0)
                    return new JsonResult("Amount Should not be Negative values : " + Amount);

                string jsonFilePath = @"../ExchangeRate/Properties/Exchangerate.json";
                string jsonString = System.IO.File.ReadAllText(jsonFilePath);
                List<ConversionRate> serailObj = System.Text.Json.JsonSerializer.Deserialize<List<ConversionRate>>(jsonString);

                var matchingRates = serailObj.FindAll(rate =>
                                     rate.fromCurrency.Equals(fromCurrency, StringComparison.OrdinalIgnoreCase) &&
                                     rate.toCurrency.Equals(toCurrency, StringComparison.OrdinalIgnoreCase));
                if (matchingRates.Count > 0)
                {
                    decimal conversionamount = matchingRates[0].rate * Amount;
                    var resultData = matchingRates.Select(rate => new
                    {
                        //rate.fromCurrency,
                        //rate.toCurrency,
                        rate.rate,
                        conversionamount,
                        Amount
                    });
                    return new JsonResult(resultData);
                }

                return new JsonResult("No Mapping for Exchange Rate [{SourceCurrency : " + sourceCurrency + " , TargetCurrency  : " + targetCurrency + "}]");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "");
                throw ex;


            }

        }
        [Route("api/UpdateConversionFactor")]
        [HttpPost]
        public bool UpdateConversionFactor(string fromCurrency, string toCurrency, decimal Amount)
        {
            object[] param = { fromCurrency, toCurrency, Amount };
            try
            {
                if (string.IsNullOrEmpty(fromCurrency) || string.IsNullOrEmpty(toCurrency)) {
                    _logger.LogError("Currencies should not be Empty for UpdateConversionFactor Method in CurrencyConversionController.cs" + param);
                    return false;
                }
                if (Amount < 0)
                {
                    _logger.LogError("Amount Should not be in -ve Values for UpdateConversionFactor Method in CurrencyConversionController.cs" +param);
                    return false;
                }
                ExchangeRateManager exmngr = new ExchangeRateManager();
               bool result= exmngr.UpdateConversionFactor(fromCurrency, toCurrency, Amount);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured on UpdateConversionFactor Method in CurrencyConversionController.cs" + param);
                throw ex;
            }
            
           
        }
    }
}
