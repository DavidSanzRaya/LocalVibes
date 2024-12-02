using LocalVibes.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LocalVibes.DALs
{
    public class ProjectDAL : DAL<Project>
    {
        public ProjectDAL(string connectionString) : base(connectionString)
        {
        }

        protected override string TableName => "Project";

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
                            ? (DateOnly)reader["FormationDate"]
                            : null,
                ProjectImage = reader["ProjectImage"] != DBNull.Value
                            ? (byte[])reader["ProjectImage"]
                            : null,
                IdUserAdmin = (int)reader["IdUsersAdmin"]
            };
        }
    }
}
