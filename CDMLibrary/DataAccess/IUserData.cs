using System.Collections.Generic;

namespace CDMLibrary.DataAccess
{
    public interface IUserData
    {
        List<UserModel> GetUserById(string id);
    }
}