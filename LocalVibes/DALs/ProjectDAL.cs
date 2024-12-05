using LocalVibes.Models;
using Microsoft.Data.SqlClient;

namespace LocalVibes.DALs
{
    public class ProjectDAL : DAL<Project>
    {
        private readonly string _connectionString = "Server=85.208.21.117,54321;Database=AbelAlexiaDavidJoelLocalVibes;User Id=sa;Password=Sql#123456789;TrustServerCertificate=True;";

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
                IdUserAdmin = (int)reader["IdUserAdmin"]
            };
        }

        // Método para buscar un proyecto por nombre
        public Project? GetProjectByName(string projectName)
        {
            string query = $"SELECT * FROM {TableName} WHERE ProjectName = @ProjectName";

            return QuerySingle
            (
                query,
                reader => MapReaderToEntity((SqlDataReader)reader),
                new SqlParameter("@ProjectName", projectName)
            );
        }
    }
}
