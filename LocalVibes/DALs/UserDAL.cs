using LocalVibes.Models;
using LocalVibes.Tools;
using Microsoft.Data.SqlClient;

namespace LocalVibes.DALs
{
    public class UserDAL : DAL<Users>
    {

        private readonly string _connectionString = "Server=85.208.21.117,54321;Database=AbelAlexiaDavidJoelLocalVibes;User Id=sa;Password=Sql#123456789;TrustServerCertificate=True;";

        public UserDAL() { }

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
        public Users GetUsuarioByUsername(string userName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(@"
            SELECT IdUsers, UserName, FirstName, LastName, UserEmail, Phone, 
                   PasswordHash, PasswordSalt, Birthdate, ProfileImage, 
                   DocumentNumber, UserPoints, DateRegister, IdDocumentType, 
                   IdGenere, IdTier
            FROM Users
            WHERE UserName = @UserName", connection);

                command.Parameters.AddWithValue("@UserName", userName);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Users
                        {
                            IdUser = (int)reader["IdUsers"],
                            UserName = (string)reader["UserName"],
                            FirstName = reader["FirstName"] as string,
                            LastName = reader["LastName"] as string,
                            UserEmail = (string)reader["UserEmail"],
                            Phone = reader["Phone"] as string,
                            PasswordHash = reader["PasswordHash"] as byte[],
                            PasswordSalt = reader["PasswordSalt"] as byte[],
                            Birthdate = reader["Birthdate"] as DateTime?,
                            ProfileImage = reader["ProfileImage"] as byte[],
                            DocumentNumber = reader["DocumentNumber"] as string,
                            UserPoints = reader["UserPoints"] as int?,
                            DateRegister = (DateTime)reader["DateRegister"],
                            IdDocumentType = reader["IdDocumentType"] as int?,
                            IdGenere = (int)reader["IdGenere"],
                            IdTier = (int)reader["IdTier"]
                        };
                    }
                }
            }
            return null; // Si no se encuentra el usuario
        }



        // Método para crear un nuevo usuario
        public void CreateUsuario(Users usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(@"
            INSERT INTO Users (UserName, FirstName, LastName, UserEmail, Phone, 
                               Birthdate, PasswordHash, PasswordSalt, DateRegister, 
                               IdGenere, IdTier, UserPoints)
            VALUES (@UserName, @FirstName, @LastName, @UserEmail, @Phone, 
                    @Birthdate, @PasswordHash, @PasswordSalt, @DateRegister, 
                    @IdGenere, @IdTier, @UserPoints)", connection);

                command.Parameters.AddWithValue("@UserName", usuario.UserName);
                command.Parameters.AddWithValue("@FirstName", usuario.FirstName);
                command.Parameters.AddWithValue("@LastName", usuario.LastName);
                command.Parameters.AddWithValue("@UserEmail", usuario.UserEmail);
                command.Parameters.AddWithValue("@Phone", usuario.Phone);
                command.Parameters.AddWithValue("@Birthdate", usuario.Birthdate);
                command.Parameters.AddWithValue("@PasswordHash", usuario.PasswordHash);
                command.Parameters.AddWithValue("@PasswordSalt", usuario.PasswordSalt);
                command.Parameters.AddWithValue("@DateRegister", usuario.DateRegister);
                command.Parameters.AddWithValue("@IdGenere", usuario.IdGenere);
                command.Parameters.AddWithValue("@IdTier", usuario.IdTier);
                command.Parameters.AddWithValue("@UserPoints", usuario.UserPoints);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}

