using LocalVibes.Models;
using Microsoft.Data.SqlClient;

namespace LocalVibes.DALs
{
    public class GenereMusicDAL : DAL<GenereMusic>
    {
        public GenereMusicDAL() { }

        protected override string TableName => "GenereMusic";

        protected override GenereMusic MapReaderToEntity(SqlDataReader reader)
        {
            return new GenereMusic
            {
                IdGenereMusic = reader.GetInt32(reader.GetOrdinal("IdGenereMusic")),
                GenereMusicName = reader.GetString(reader.GetOrdinal("GenereMusicName"))
            };
        }

        public List<GenereMusic> GetGenresByUserId(int userId)
        {
            //var genres = new List<GenereMusic>();
            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    connection.Open();
            //    using (var command = new SqlCommand(
            //        @"SELECT gm.IdGenereMusic, gm.GenereMusicName
            //          FROM UsersGenereMusic ug
            //          INNER JOIN GenereMusic gm ON ug.idGenereMusic = gm.IdGenereMusic
            //          WHERE ug.idUsers = @UserId", connection))
            //    {
            //        command.Parameters.AddWithValue("@UserId", userId);
            //        using (var reader = command.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                genres.Add(new GenereMusic
            //                {
            //                    IdGenereMusic = (int)reader["IdGenereMusic"],
            //                    GenereMusicName = (string)reader["GenereMusicName"]
            //                });
            //            }
            //        }
            //    }
            //}
            //return genres;
            string query = @"
                            SELECT gm.IdGenereMusic, gm.GenereMusicName
                            FROM UsersGenereMusic ug
                            INNER JOIN GenereMusic gm ON ug.idGenereMusic = gm.IdGenereMusic
                            WHERE ug.idUsers = @UserId";

            return Query(
                query,
                reader => new GenereMusic
                {
                    IdGenereMusic = (int)reader["IdGenereMusic"],
                    GenereMusicName = (string)reader["GenereMusicName"]
                },
                new SqlParameter("@UserId", userId)
            );
        }

    }
}
