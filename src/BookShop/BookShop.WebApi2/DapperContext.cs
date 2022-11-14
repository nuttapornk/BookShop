﻿using Microsoft.Data.SqlClient;
using System.Data;

namespace BookShop.WebApi2
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("AppDbContext");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
