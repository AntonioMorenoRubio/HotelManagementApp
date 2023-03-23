using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementLibrary.Interfaces
{
    public interface ISQLDataAccess
    {
        List<T> LoadData<T, U>(string query,
                               U parameters,
                               string connectionStringName,
                               bool isStoredProcedure);
        void SaveData<T, U>(string query,
                            U parameters,
                            string connectionStringName,
                             bool isStoredProcedure);
    }
}
