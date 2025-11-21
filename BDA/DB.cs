using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDA
{
    public static class DB
    {
        private static string? _connectionString;

        public static void Initialize(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public static string ConnectionString
        {
            get
            {
                if (_connectionString == null)
                    throw new Exception("Connection string not initialized. Call DB.Initialize() in Program.cs");

                return _connectionString;
            }
        }
    }
}
