using LocalVibes.Models;
using Microsoft.Data.SqlClient;

namespace LocalVibes.DALs
{
    public class ReviewDAL : DAL<Review>
    {
        public ReviewDAL(string connectionString) : base(connectionString)
        {
        }

        protected override string TableName => "Review";

        protected override Review MapReaderToEntity(SqlDataReader reader)
        {
            return new Review
            {
                IdReview = (int)reader["IdReview"],
                ReviewText = reader["ReviewText"] != DBNull.Value
                                ? (string)reader["ReviewText"]
                                : null,
                ReviewDate = (DateTime)reader["ReviewDate"],
                Rating = (int)reader["Rating"],
                IdUser = (int)reader["IdUsers"],
                IdProject = (int)reader["IdProject"]
            };
        }
    }
}
