using LocalVibes.Models;
using Microsoft.Data.SqlClient;

namespace LocalVibes.DALs
{
    public class UserDAL : DAL<User>
    {
        protected override string TableName => "Users";

        protected override string IdName => "IdUsers";

        protected override User MapReaderToEntity(SqlDataReader reader)
        {
            return new User
            {

            }
        }
    }
}
