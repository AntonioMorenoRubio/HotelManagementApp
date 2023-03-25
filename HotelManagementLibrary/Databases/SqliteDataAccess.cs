using System.Data;
using Dapper;
using HotelLibrary.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace HotelLibrary.Databases
{
    public class SqliteDataAccess : ISqliteDataAccess
    {
        private readonly IConfiguration config;

        public SqliteDataAccess(IConfiguration config)
        {
            this.config = config;
        }

        public List<T> LoadData<T, U>(string query, U parameters, string connectionStringName)
        {
            string connectionString = config.GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqliteConnection(connectionString))
            {
                return connection.Query<T>(query, parameters, commandType: CommandType.Text).ToList();
            }
        }

        public void SaveData<T, U>(string query, U parameters, string connectionStringName)
        {
            string connectionString = config.GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqliteConnection(connectionString))
            {
                connection.Execute(query, parameters, commandType: CommandType.Text);
            }
        }
    }
}
