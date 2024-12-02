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
                IdUser = (int)reader["IdUsers"],
                UserName = (string)reader["UserName"],
                FirstName = (string)reader["FirstName"],
                LastName = reader["LastName"] != DBNull.Value 
                            ? (string)reader["LastName"]
                            : null,
                UserEmail = (string)reader["UserEmail"],
                Phone = reader["Phone"] != DBNull.Value
                            ? (string)reader["Phone"]
                            : null,
                PasswordHash = (byte[])reader["PasswordHash"],
                PasswordSalt = (byte[])reader["PasswordSalt"],
                BirthDate = reader["Birthdate"] != DBNull.Value
                            ? (DateTime)reader["Birthdate"]
                            : null,
                ProfileImage = reader["ProfileImage"] != DBNull.Value
                            ? (byte[])reader["ProfileImage"]
                            : null,
                DocumentNumber = reader["DocumentNumber"] != DBNull.Value
                            ? (string)reader["DocumentNumber"]
                            : null,
                UserPoints = (int)reader["UserPoints"],
                RegisterDate = (DateTime)reader["DateRegister"],
                FKDocumentType = reader["IdDocumentType"] != DBNull.Value
                            ? (int)reader["IdDocumentType"]
                            : null,
                FKTier = (int)reader["IdTier"]
            };
        }
    }
}
