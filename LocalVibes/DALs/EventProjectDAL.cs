using LocalVibes.Models;
using Microsoft.Data.SqlClient;

namespace LocalVibes.DALs
{
    public class EventProjectDAL : DAL<EventProject>
    {
        public EventProjectDAL() { }
        private readonly string _connectionString = "Server=85.208.21.117,54321;Database=AbelAlexiaDavidJoelLocalVibes;User Id=sa;Password=Sql#123456789;TrustServerCertificate=True;";
        protected override string TableName => "EventProject";

        protected override EventProject MapReaderToEntity(SqlDataReader reader)
        {
            return new EventProject
            {
                IdEvent = (int)reader["IdEvent"],
                EventTitle = (string)reader["EventTitle"],
                EventDescription = (string)reader["EventDescription"],
                EventImage = (byte[])reader["EventImage"],
                Capacity = reader["Capacity"] != DBNull.Value
                            ? (int)reader["Capacity"]
                            : null,
                IsSoldOut = (bool)reader["IsSoldOut"],
                Sales = reader["Sales"] != DBNull.Value
                        ? (int)reader["Sales"]
                        : null,
                EventDate = (DateTime)reader["EventDate"],
                IdProject = (int)reader["IdProject"],
                IdLocation = (int)reader["IdLocation"]
            };
        }

        public void InsertEvent(EventProject eventProject)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                // Asegúrate de tener configurado el connectionString adecuado
                connection.Open();

                // Usamos un procedimiento almacenado o una consulta SQL simple para insertar el evento
                var command = new SqlCommand("INSERT INTO EventProject (EventTitle, EventDescription, EventImage, Capacity, IsSoldOut, Sales, EventDate, IdProject, IdLocation) " +
                                            "VALUES (@EventTitle, @EventDescription, NULL, @Capacity, @IsSoldOut, 0, @EventDate, @IdProject, @IdLocation)", connection);

                command.Parameters.AddWithValue("@EventTitle", eventProject.EventTitle ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@EventDescription", eventProject.EventDescription ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Capacity", eventProject.Capacity ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@IsSoldOut", eventProject.IsSoldOut);
                command.Parameters.AddWithValue("@EventDate", eventProject.EventDate);
                command.Parameters.AddWithValue("@IdProject", eventProject.IdProject);
                command.Parameters.AddWithValue("@IdLocation", eventProject.IdLocation);

                // Ejecutamos la consulta
                command.ExecuteNonQuery();
            }
        }
    }
}
