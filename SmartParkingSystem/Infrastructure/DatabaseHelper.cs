using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Connections;
using System.Data.Common;



namespace SmartParkingSystem.Infrastructure
{
    /// <summary>
    /// Implimenting Connectionfactory for establishing connection with databsase
    /// </summary>
    public class DatabaseHelper : IDatabaseHelper
    {
        private static IConfiguration _configuration;
        /// <summary>
        /// configaring with connectionfactory
        /// </summary>
        /// <param name="configuration"></param>

        public DatabaseHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Database Connection
        /// </summary>

        public IDbConnection GetConnection
        {
            get
            {
                {
                    DbProviderFactories.RegisterFactory("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);
                    var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                    var conn = factory.CreateConnection();
                    if (conn == null) return null;
                    conn.ConnectionString = _configuration["ConnectionStrings:IdentityDBLocal"];
                    conn.Open();
                    return conn;
                }
            }
        }
    }
}
