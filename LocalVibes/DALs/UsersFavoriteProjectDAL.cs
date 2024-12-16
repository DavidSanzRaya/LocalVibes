using LocalVibes.Models;
using Microsoft.Data.SqlClient;

namespace LocalVibes.DALs
{
	public class UsersFavoriteProjectDAL : DAL<UserFavoriteProject>
	{
		protected override string TableName => "UsersFavoriteProject";

		private readonly string _connectionString = "Server=85.208.21.117,54321;Database=AbelAlexiaDavidJoelLocalVibes;User Id=sa;Password=Sql#123456789;TrustServerCertificate=True;";

		protected override UserFavoriteProject MapReaderToEntity(SqlDataReader reader)
		{
			return new UserFavoriteProject
			{
				IdUsers = (int)reader["IdUsers"],
				IdProject = (int)reader["IdProject"]
			};
		}
	}
}
