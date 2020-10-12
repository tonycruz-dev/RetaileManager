using CDMLibrary.Internal.DataAccess;
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
        public UserData()
        {

        }
        public List<UserModel> GetUserById(string id)
        {

            SqlDataAccess _db = new SqlDataAccess();

           return _db.LoadData<UserModel, dynamic>("dbo.spUserLookup",
                                            new { id },
                                            _connectionStringName,
                                            true);
        }
    }
}
