namespace ExchangeRate.Entities
{
    public class ConversionRate
    {
        public string fromCurrency { get; set; }
        public string toCurrency { get; set; }

        public decimal rate { get; set; }
        
        public decimal ConvertedAmount { get; set; }


    }
}
