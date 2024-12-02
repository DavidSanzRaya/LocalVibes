using LocalVibes.Models;
using Microsoft.Data.SqlClient;

namespace LocalVibes.DALs
{
    public class UserDAL : DAL<User>
    {
        public UserDAL(string connectionString) : base(connectionString)
        {
        }

        protected override string TableName => "Users";

        protected override User MapReaderToEntity(SqlDataReader reader)
        {
            return new User
            {

            }
        }
    }
}
