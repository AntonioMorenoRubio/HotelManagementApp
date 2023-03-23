using System.Data;
using System.Data.SqlClient;
using Dapper;
using HotelManagementLibrary.Interfaces;
using Microsoft.Extensions.Configuration;

namespace HotelManagementLibrary.Databases
{
    public class SqlServerDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration config;

        public SqlServerDataAccess(IConfiguration config) {
            this.config = config;
        }

        public List<T> LoadData<T, U>(string query, U parameters, string connectionStringName, bool isStoredProcedure)
        {
            string connectionString = config.GetConnectionString(connectionStringName);

            CommandType commandType = CommandType.Text;

            if (isStoredProcedure == true)
                commandType = CommandType.StoredProcedure;

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<T>(query, parameters, commandType: commandType).ToList();
            }
        }

        public void SaveData<T, U>(string query, U parameters, string connectionStringName, bool isStoredProcedure)
        {
            string connectionString = config.GetConnectionString(connectionStringName);
            CommandType commandType = CommandType.Text;

            if (isStoredProcedure == true)
                commandType = CommandType.StoredProcedure;

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, parameters, commandType: commandType);
            }
        }
    }
}
