using LocalVibes.Models;
using Microsoft.Data.SqlClient;

namespace LocalVibes.DALs
{
    public class EventProjectDAL : DAL<EventProject>
    {
        public EventProjectDAL() { }

        protected override string TableName => "EventProject";

        protected override EventProject MapReaderToEntity(SqlDataReader reader)
        {
            return new EventProject
            {
                IdEvent = (int)reader["IdEvent"],
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
    }
}
