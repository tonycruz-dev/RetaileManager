using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDMLibrary.Internal.DataAccess
{
    public class SqlDataAccess : IDisposable, ISqlDataAccess
    {
        private readonly IConfiguration _config;
        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }
        public string GetConnectionString(string name)
        {
            return _config.GetConnectionString(name);
            // return @"Server=(localdb)\mssqllocaldb;Database=CDMData;Trusted_Connection=True;MultipleActiveResultSets=true"; //ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
        public List<T> LoadData<T, U>(string sqlStatement,
                                      U parameter,
                                      string connectionStringName,
                                      bool isStoredProcedure)
        {
            string connectionString = GetConnectionString(connectionStringName);

            CommandType commandType = CommandType.Text;
            if (isStoredProcedure == true)
            {
                commandType = CommandType.StoredProcedure;
            }
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> Rows = connection.Query<T>(sqlStatement, parameter, commandType: commandType).ToList();
                return Rows;
            }
        }
        public void SaveData<T>(string sqlStatement,
                                      T parameter,
                                      string connectionStringName,
                                      bool isStoredProcedure)
        {
            string connectionString = GetConnectionString(connectionStringName);
            CommandType commandType = CommandType.Text;
            if (isStoredProcedure == true)
            {
                commandType = CommandType.StoredProcedure;
            }
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(sqlStatement, parameter, commandType: commandType);

            }
        }
        public int SaveDataWithId<T>(string sqlStatement,
                                      T parameter,
                                      string connectionStringName,
                                      bool isStoredProcedure)
        {
            string connectionString = GetConnectionString(connectionStringName);
            CommandType commandType = CommandType.Text;
            if (isStoredProcedure == true)
            {
                commandType = CommandType.StoredProcedure;
            }
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<int>(sqlStatement, parameter, commandType: commandType).SingleOrDefault();

            }
        }
        private IDbConnection _connection;
        private IDbTransaction _dbTransaction;
        public List<T> LoadDataInTransAction<T, U>(string storedProcedure, T parameter)
        {

            List<T> Rows = _connection.Query<T>(
                storedProcedure,
                parameter,
                transaction: _dbTransaction).ToList();
            return Rows;


        }

        public int SaveDataAndReturnIdInTransaction<T>(string storedProcedure, T parameter)
        {
            return _connection.Query<int>(
                 storedProcedure,
                 parameter,
                 commandType: CommandType.StoredProcedure,
                 transaction: _dbTransaction).SingleOrDefault();


        }

        public void SaveDataInTransaction<T>(string storedProcedure, T parameter)
        {
            _connection.Execute(
                storedProcedure,
                parameter,
                commandType: CommandType.StoredProcedure,
                transaction: _dbTransaction);


        }


        public void StartTransaction(string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _dbTransaction = _connection.BeginTransaction();
            isClose = false;

        }
        private bool isClose = false;


        public void CommitTransaction()
        {
            _dbTransaction?.Commit();
            _connection?.Close();
            isClose = true;
        }
        public void RollbackTransaction()
        {
            _dbTransaction?.Rollback();
            _connection?.Close();
            isClose = true;
        }

        public void Dispose()
        {
            if (isClose == false)
            {
                try
                {
                    CommitTransaction();
                }
                catch
                {
                }

            }
            _dbTransaction = null;
            _connection = null;

        }
    }
}
