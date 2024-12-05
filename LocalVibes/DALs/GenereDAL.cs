using LocalVibes.Models;
using LocalVibes.Tools;
using Microsoft.Data.SqlClient;

namespace LocalVibes.DALs
{
    public class GenereDAL : DAL<Genere>
    {
        protected override string TableName => "Genere";


        protected override Genere MapReaderToEntity(SqlDataReader reader)
        {
            return new Genere
            {
                IdGenere = (int)reader["IdGenere"],
                GenereName = (string)reader["GenereName"],
            };
        }

    }
}
