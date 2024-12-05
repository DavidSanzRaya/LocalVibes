using LocalVibes.Models;
using Microsoft.Data.SqlClient;

namespace LocalVibes.DALs
{
    public class ProjectDAL : DAL<Project>
    {
        public ProjectDAL() { }

        protected override string TableName => "Project";

        // Mapeo del lector de datos al modelo Project
        protected override Project MapReaderToEntity(SqlDataReader reader)
        {
            return new Project
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
            };
        }
    }
}
