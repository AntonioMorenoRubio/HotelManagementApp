using System.Data;
using System.Data.SqlClient;
using Dapper;
using HotelManagementLibrary.Interfaces;
using Microsoft.Extensions.Configuration;

namespace HotelManagementLibrary.Databases
{
    public class SQLServerDataAccess : ISQLDataAccess
    {
        public List<T> LoadData<T, U>(string query, U parameters, string connectionStringName, bool isStoredProcedure)
        {
            CommandType commandType = CommandType.Text;

            if (isStoredProcedure == true)
                commandType = CommandType.StoredProcedure;

            using (IDbConnection connection = new SqlConnection(connectionStringName))
            {
                return connection.Query<T>(query, parameters, commandType: commandType).ToList();
            }
        }

        public void SaveData<T, U>(string query, U parameters, string connectionStringName, bool isStoredProcedure)
        {
            CommandType commandType = CommandType.Text;

            if (isStoredProcedure == true)
                commandType = CommandType.StoredProcedure;

            using (IDbConnection connection = new SqlConnection(connectionStringName))
            {
                connection.Execute(query, parameters, commandType: commandType);
            }
        }
    }
}
