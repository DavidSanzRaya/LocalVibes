using LocalVibes.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LocalVibes.DALs
{
    public class GenereMusicDAL : DAL<GenereMusic>
    {
        public GenereMusicDAL() { }

        private readonly string _connectionString = "Server=85.208.21.117,54321;Database=AbelAlexiaDavidJoelLocalVibes;User Id=sa;Password=Sql#123456789;TrustServerCertificate=True;";

        protected override string TableName => "GenereMusic";

        protected override GenereMusic MapReaderToEntity(SqlDataReader reader)
        {
            return new GenereMusic
            {
                IdGenereMusic = (int)reader["IdGenereMusic"],
                GenereMusicName = (string)reader["GenereMusicName"]
            };
        }

        public List<GenereMusic> GetGenresByUserId(int userId)
        {
            var generesMusic = new List<GenereMusic>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                            SELECT gm.IdGenereMusic, gm.GenereMusicName
                            FROM UsersGenereMusic pg
                            INNER JOIN GenereMusic gm ON pg.IdGenereMusic = gm.IdGenereMusic
                            WHERE pg.IdUsers = @userId";

                    command.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int) { Value = userId });

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            generesMusic.Add(new GenereMusic
                            {
                                IdGenereMusic = (int)reader["IdGenereMusic"],
                                GenereMusicName = (string)reader["GenereMusicName"]
                            });
                        }
                    }
                }
            }
            return generesMusic;
        }

        public List<GenereMusic> GetGenresByProjectId(int projectId)
        {
            var generesMusic = new List<GenereMusic>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                            SELECT gm.IdGenereMusic, gm.GenereMusicName
                            FROM ProjectGenereMusic pg
                            INNER JOIN GenereMusic gm ON pg.IdGenereMusic = gm.IdGenereMusic
                            WHERE pg.IdProject = @ProjectId";

                    command.Parameters.Add(new SqlParameter("@projectId", SqlDbType.Int) { Value = projectId });

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            generesMusic.Add(new GenereMusic
                            {
                                IdGenereMusic = (int)reader["IdGenereMusic"],
                                GenereMusicName = (string)reader["GenereMusicName"]
                            });
                        }
                    }
                }
            }
            return generesMusic;
        }
    }
}
