namespace HotelLibrary.Interfaces
{
    public interface ISqliteDataAccess
    {
        List<T> LoadData<T, U>(string query, U parameters, string connectionStringName);
        void SaveData<T, U>(string query, U parameters, string connectionStringName);
    }
}