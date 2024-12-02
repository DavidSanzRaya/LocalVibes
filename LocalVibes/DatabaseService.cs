using Microsoft.Extensions.Configuration;

namespace LocalVibes
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new ArgumentNullException("DefaultConnection", "Connection string cannot be null.");
        }


        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
