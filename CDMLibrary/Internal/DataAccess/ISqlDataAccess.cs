using System.Collections.Generic;

namespace CDMLibrary.Internal.DataAccess
{
    public interface ISqlDataAccess
    {
        void CommitTransaction();
        //void Dispose();
        string GetConnectionString(string name);
        List<T> LoadData<T, U>(string sqlStatement, U parameter, string connectionStringName, bool isStoredProcedure);
        List<T> LoadDataInTransAction<T, U>(string storedProcedure, T parameter);
        void RollbackTransaction();
        void SaveData<T>(string sqlStatement, T parameter, string connectionStringName, bool isStoredProcedure);
        int SaveDataAndReturnIdInTransaction<T>(string storedProcedure, T parameter);
        void SaveDataInTransaction<T>(string storedProcedure, T parameter);
        int SaveDataWithId<T>(string sqlStatement, T parameter, string connectionStringName, bool isStoredProcedure);
        void StartTransaction(string connectionStringName);
    }
}