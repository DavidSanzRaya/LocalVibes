using LocalVibes.Models;
using Microsoft.Data.SqlClient;

namespace LocalVibes.DALs
{
    public class UserDAL : DAL<Users>
    {
        public UserDAL(string connectionString) : base(connectionString)
        {
        }

        protected override string TableName => "Users";

        protected override Users MapReaderToEntity(SqlDataReader reader)
        {
            return new Users
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
                Birthdate = reader["Birthdate"] != DBNull.Value
                            ? (DateOnly)reader["Birthdate"]
                            : null,
                ProfileImage = reader["ProfileImage"] != DBNull.Value
                            ? (byte[])reader["ProfileImage"]
                            : null,
                DocumentNumber = reader["DocumentNumber"] != DBNull.Value
                            ? (string)reader["DocumentNumber"]
                            : null,
                UserPoints = (int)reader["UserPoints"],
                DateRegister = (DateTime)reader["DateRegister"],
                IdDocumentType = reader["IdDocumentType"] != DBNull.Value
                            ? (int)reader["IdDocumentType"]
                            : null,
                IdTier = (int)reader["IdTier"]
            };
        }
    }
}
