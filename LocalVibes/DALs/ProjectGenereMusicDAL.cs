using LocalVibes.Models;
using Microsoft.Data.SqlClient;

namespace LocalVibes.DALs
{
    public class ProjectGenereMusicDAL : DAL<ProjectGenereMusic>
    {
        protected override string TableName => "ProjectGenereMusic";


        protected override ProjectGenereMusic MapReaderToEntity(SqlDataReader reader)
        {
            return new ProjectGenereMusic
            {
                IdProject = (int)reader["IdProject"],
                IdGenereMusic = (int)reader["IdGenereMusic"]
            };
        }
    }
}
