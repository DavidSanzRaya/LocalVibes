using LocalVibes.Models;
using LocalVibes.Tools;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LocalVibes.DALs
{
    public class UserDAL : DAL<Users>
    {
        protected override string TableName => "Users";

        private readonly string _connectionString = "Server=85.208.21.117,54321;Database=AbelAlexiaDavidJoelLocalVibes;User Id=sa;Password=Sql#123456789;TrustServerCertificate=True;";

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

        public List<EventProject> GetAssistEventsByUserId(int userId)
        {
            var events = new List<EventProject>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                            SELECT e.IdEvent, e.EventTitle, e.EventDescription, e.EventImage, e.Capacity, e.IsSoldOut, e.Sales, e.EventDate, e.IdLocation
                                    FROM EventProject e
                                    JOIN Ticket t ON e.IdEvent = t.IdEvent
                                    WHERE t.IdUsers = @userId";

                    command.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int) { Value = userId });
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            events.Add(new EventProject
                            {
                                IdEvent = (int)reader["IdEvent"],
                                EventTitle = (string)reader["EventTitle"],
                                EventDescription = (string)reader["EventDescription"],
                                EventImage = reader["EventImage"] != DBNull.Value ? (byte[])reader["EventImage"] : null,
                                Capacity = reader["Capacity"] != DBNull.Value ? (int?)reader["Capacity"] : null,
                                IsSoldOut = (bool)reader["IsSoldOut"],
                                Sales = reader["Sales"] != DBNull.Value ? (int?)reader["Sales"] : null,
                                EventDate = (DateTime)reader["EventDate"],
                                IdLocation = (int)reader["IdLocation"]
                            });
                        }
                    }
                }
            }
            return events;
        }

        public List<Project> GetFavoriteProjectsByUserId(int userId)
        {
            var projectsFav = new List<Project>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                SELECT p.IdProject, p.ProjectName, p.Biography, p.FormationDate, p.ProjectImage, p.IdUsersAdmin
                        FROM Project p
                        JOIN UsersFavoriteProject usp ON p.IdProject = usp.IdProject
                        WHERE usp.IdUsers = @userId";

                    command.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int) { Value = userId });

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            projectsFav.Add(new Project
                            {
                                IdProject = (int)reader["IdProject"],
                                ProjectName = (string)reader["ProjectName"],
                                Biography = reader["Biography"] != DBNull.Value
                                    ? (string)reader["Biography"]
                                    : null,
                                FormationDate = reader["FormationDate"] != DBNull.Value
                                    ? (DateTime)reader["FormationDate"]
                                    : null,
                                ProjectImage = reader["ProjectImage"] != DBNull.Value
                                    ? (byte[])reader["ProjectImage"]
                                    : null,
                                IdUsersAdmin = (int)reader["IdUsersAdmin"]
                            });
                        }
                    }
                }
            }
            return projectsFav;
        }
    }
}

