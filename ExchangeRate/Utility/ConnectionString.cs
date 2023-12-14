namespace ExchangeRate.Utility
{
    public static class ConnectionString
    {
        private static string connectionName = "Data Source=LAPTOP-GT3N053R\\SQLEXPRESS;  Initial Catalog=CurrnecyExchangeFactor;Integrated Security=True;";        
        public static string CName { get => connectionName; }
    }
}
