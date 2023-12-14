using ExchangeRate.Controllers;
using ExchangeRate.DataAccess;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRate.BusinessEntities
{
    public class ExchangeRateManager
    {
        private readonly ILogger<ExchangeRateManager> _logger;
        public ExchangeRateManager() { }

        public bool UpdateConversionFactor(string fromCurrency, string toCurrency, decimal updateExchangeRate)
        {
            try
            {
                bool result = false;
                SQLExchangeDAO sqlExchangeDAO = new SQLExchangeDAO();
                result= sqlExchangeDAO.UpdateConversionFactor(fromCurrency, toCurrency, updateExchangeRate);
                return result;
            }
            catch(Exception ex)
            {

                _logger.LogError(ex.Message, "");
                throw ex;
            }
        }
    }
}
