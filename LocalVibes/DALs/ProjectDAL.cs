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
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM {TableName} WHERE ProjectName = @ProjectName";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProjectName", projectName);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapReaderToEntity(reader);
                        }
                    }
                }
            }
            return null;
        }

        // Método para agregar un nuevo proyecto
        public void CreateProject(Project project)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(@"
                    INSERT INTO Project
                    (ProjectName, Biography, FormationDate, ProjectImage, IdUserAdmin) 
                    VALUES 
                    (@ProjectName, @Biography, @FormationDate, @ProjectImage, @IdUserAdmin)", connection);

                command.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                command.Parameters.AddWithValue("@Biography", (object?)project.Biography ?? DBNull.Value);
                command.Parameters.AddWithValue("@FormationDate", (object?)project.FormationDate ?? DBNull.Value);
                command.Parameters.AddWithValue("@ProjectImage", (object?)project.ProjectImage ?? DBNull.Value);
                command.Parameters.AddWithValue("@IdUserAdmin", project.IdUserAdmin);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
