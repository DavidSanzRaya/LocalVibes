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
                IdGenereMusic = (int)reader["IdGenereMusic"],
                GenereMusicName = (string)reader["GenereMusicName"]
            };
        }

        public List<GenereMusic> GetGenresByUserId(int userId)
        {
            string query = @"
                            SELECT gm.IdGenereMusic, gm.GenereMusicName
                            FROM UsersGenereMusic ug
                            INNER JOIN GenereMusic gm ON ug.IdGenereMusic = gm.IdGenereMusic
                            WHERE ug.IdUsers = @UserId";

            return Query(
                query,
                reader => MapReaderToEntity((SqlDataReader)reader),
                new SqlParameter("@UserId", userId)
            );
        }

        public List<GenereMusic> GetGenresByProjectId(int projectId)
        {
            string query = @"
                            SELECT gm.IdGenereMusic, gm.GenereMusicName
                            FROM ProjectGenereMusic pg
                            INNER JOIN GenereMusic gm ON pg.IdGenereMusic = gm.IdGenereMusic
                            WHERE pg.IdProject = @ProjectId";

            return Query(
                query,
                reader => MapReaderToEntity((SqlDataReader)reader),
                new SqlParameter("@ProjectId", projectId)
            );
        }
    }
}
