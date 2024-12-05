using LocalVibes.Models;
using Microsoft.Data.SqlClient;

namespace LocalVibes.DALs
{
    public class LocationsDAL : DAL<Locations>
    {
        protected override string TableName => "Locations";

        protected override Locations MapReaderToEntity(SqlDataReader reader)
        {
            return new Locations
            {
                IdLocation = (int)reader["IdLocation"],
                Latitude = (double)reader["Latitude"],
                Longitude = (double)reader["Longitude"],
                NameLocation = (string)reader["NameLocation"]
            };
        }
    }
}
