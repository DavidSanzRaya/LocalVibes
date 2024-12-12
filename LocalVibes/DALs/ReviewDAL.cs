using LocalVibes.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LocalVibes.DALs
{
    public class ReviewDAL : DAL<Review>
    {
        public ReviewDAL() { }

        protected override string TableName => "Review";

        private readonly string _connectionString = "Server=85.208.21.117,54321;Database=AbelAlexiaDavidJoelLocalVibes;User Id=sa;Password=Sql#123456789;TrustServerCertificate=True;";

        protected override Review MapReaderToEntity(SqlDataReader reader)
        {
            var idUser = (int)reader["IdUsers"];
            return new Review
            {
                IdReview = (int)reader["IdReview"],
                ReviewText = reader["ReviewText"] != DBNull.Value ? (string)reader["ReviewText"] : null,
                ReviewDate = (DateTime)reader["ReviewDate"],
                Rating = (int)reader["Rating"],
                IdUser = idUser,
                User = GetUserById(idUser),
                IdProject = (int)reader["IdProject"]
            };
        }

        public List<Review> GetReviewsByProjectId(int IdProject)
        {
            var projectReview = new List<Review>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
            SELECT 
                r.IdReview, r.ReviewText, r.ReviewDate, r.Rating, r.IdUsers, r.IdProject
            FROM Review r
            WHERE r.IdProject = @IdProject";

                    command.Parameters.Add(new SqlParameter("@IdProject", System.Data.SqlDbType.Int) { Value = IdProject });

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            projectReview.Add(MapReaderToEntity(reader));
                        }
                    }
                }
            }

            return projectReview;
        }


        private Users GetUserById(int idUser)
        {
            Users user = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                SELECT IdUsers, UserName, UserEmail
                FROM Users
                WHERE IdUsers = @IdUsers";

                    command.Parameters.Add(new SqlParameter("@IdUsers", System.Data.SqlDbType.Int) { Value = idUser });

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new Users
                            {
                                IdUsers = (int)reader["IdUsers"],
                                UserName = reader["UserName"]?.ToString(),
                                UserEmail = reader["UserEmail"]?.ToString()
                            };
                        }
                    }
                }
            }

            return user;
        }

        public void Insert(Review review)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand checkCommand = connection.CreateCommand())
                {
                    checkCommand.CommandText = "SELECT COUNT(1) FROM Project WHERE IdProject = @IdProject";
                    checkCommand.Parameters.Add(new SqlParameter("@IdProject", SqlDbType.Int) { Value = review.IdProject });

                    int projectExists = (int)checkCommand.ExecuteScalar();
                    if (projectExists == 0)
                    {
                        throw new Exception($"El IdProject {review.IdProject} no existe en la tabla Project.");
                    }
                }


                
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                INSERT INTO Review (ReviewText, ReviewDate, Rating, IdUsers, IdProject)
                VALUES (@ReviewText, @ReviewDate, @Rating, @IdUsers, @IdProject)";

                    command.Parameters.Add(new SqlParameter("@ReviewText", SqlDbType.NVarChar) { Value = review.ReviewText });
                    command.Parameters.Add(new SqlParameter("@ReviewDate", SqlDbType.DateTime) { Value = review.ReviewDate });
                    command.Parameters.Add(new SqlParameter("@Rating", SqlDbType.Int) { Value = review.Rating });
                    command.Parameters.Add(new SqlParameter("@IdUsers", SqlDbType.Int) { Value = review.IdUser });
                    command.Parameters.Add(new SqlParameter("@IdProject", SqlDbType.Int) { Value = review.IdProject });

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
