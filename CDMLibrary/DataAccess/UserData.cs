using CDMLibrary.Internal.DataAccess;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDMLibrary.DataAccess
{
    public class UserData
    {
        private const string _connectionStringName = "CDMDataConnection";
        private readonly IConfiguration _config;

        public UserData(IConfiguration config)
        {
            _config = config;
        }
        public List<UserModel> GetUserById(string id)
        {

            SqlDataAccess _db = new SqlDataAccess(_config);

           return _db.LoadData<UserModel, dynamic>("dbo.spUserLookup",
                                            new { id },
                                            _connectionStringName,
                                            true);
        }
    }
}
