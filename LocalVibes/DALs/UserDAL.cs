using LocalVibes.Models;
using LocalVibes.Tools;
using Microsoft.Data.SqlClient;

namespace LocalVibes.DALs
{
    public class UserDAL : DAL<Users>
    {

        private readonly string _connectionString = "Server=85.208.21.117,54321;Database=AbelAlexiaDavidJoelLocalVibes;User Id=sa;Password=Sql#123456789;TrustServerCertificate=True;";


        protected override string TableName => "Users";

        protected override Users MapReaderToEntity(SqlDataReader reader)
        {
            return new Users
            {
                IdUsers = (int)reader["IdUsers"],
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
                            ? (DateTime)reader["Birthdate"]
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
                IdTier = (int)reader["IdTier"],
            };
        }

        // Método para obtener el usuario por login
        public Users GetUserByUsername(string userName)
        {
            string query = @"
        SELECT IdUsers, UserName, FirstName, LastName, UserEmail, Phone, 
               PasswordHash, PasswordSalt, Birthdate, ProfileImage, 
               DocumentNumber, UserPoints, DateRegister, IdDocumentType, 
               IdGenere, IdTier
        FROM Users
        WHERE UserName = @UserName";

            return QuerySingle
            (
                query,
                reader => MapReaderToEntity((SqlDataReader)reader),
                new SqlParameter("@UserName", userName)
            );
        }
    }
}

