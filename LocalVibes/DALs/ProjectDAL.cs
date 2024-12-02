using LocalVibes.Models;
using Microsoft.Data.SqlClient;

namespace LocalVibes.DALs
{
    public class ProjectDAL : DAL<Project>
    {
        public ProjectDAL() { }

        protected override string TableName => "Project";

        protected override Project MapReaderToEntity(SqlDataReader reader)
        {
            return new Project
            {
                IdProject = reader.GetInt32(reader.GetOrdinal("IdProject")),
                ProjectName = reader.GetString(reader.GetOrdinal("ProjectName")),
                Biography = reader.IsDBNull(reader.GetOrdinal("Biography"))
                ? null
                : reader.GetString(reader.GetOrdinal("Biography")),
                FormationDate = reader.IsDBNull(reader.GetOrdinal("FormationDate"))
                ? null
                : DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("FormationDate"))),
                ProjectImage = reader.IsDBNull(reader.GetOrdinal("ProjectImage"))
                ? null
                : (byte[])reader["ProjectImage"],
                UserAdmin = reader.IsDBNull(reader.GetOrdinal("UserAdmin"))
                ? null
                : reader.GetInt32(reader.GetOrdinal("UserAdmin"))
            };
        }

    }
}
