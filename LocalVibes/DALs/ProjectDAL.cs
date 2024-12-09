using System.Collections.Generic;
using System.Data;
using LocalVibes.Models;
using Microsoft.Data.SqlClient;

namespace LocalVibes.DALs
{
    public class ProjectDAL : DAL<Project>
    {
        public ProjectDAL() { }

        private readonly string _connectionString = "Server=85.208.21.117,54321;Database=AbelAlexiaDavidJoelLocalVibes;User Id=sa;Password=Sql#123456789;TrustServerCertificate=True;";

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

        public List<MemberOfProject> GetMembersByProjectId(int projectId)
        {
            var members = new List<MemberOfProject>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT m.IdMember, m.MemberName, m.ArtisticName, m.MemberImage
                        FROM MemberOfProject m
                        JOIN ProjectMember pm ON m.IdMember = pm.IdMember
                        WHERE pm.IdProject = @projectId";

                    command.Parameters.Add(new SqlParameter("@projectId", SqlDbType.Int) { Value = projectId });

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            members.Add(new MemberOfProject
                            {
                                IdMember = (int)reader["IdMember"],
                                MemberName = (string)reader["MemberName"],
                                ArtisticName = (string)reader["ArtisticName"],
                                MemberImage = reader["MemberImage"] != DBNull.Value
                                            ? (byte[])reader["ImageUrl"]
                                            : null
                            });
                        }
                    }
                }
            }

            return members;
        }
    }
}
