using CDMLibrary.Internal.DataAccess;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDMLibrary.DataAccess
{
    public class UserData : IUserData
    {
        private const string _connectionStringName = "CDMDataConnection";
          private readonly ISqlDataAccess _db;

        public UserData(ISqlDataAccess db)
        {
            _db = db;
        }
        public List<UserModel> GetUserById(string id)
        {
             return _db.LoadData<UserModel, dynamic>("dbo.spUserLookup",
                                             new { id },
                                             _connectionStringName,
                                             true);
        }
    }
}
