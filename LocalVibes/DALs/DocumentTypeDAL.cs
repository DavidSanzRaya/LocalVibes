using LocalVibes.Models;
using LocalVibes.Tools;
using Microsoft.Data.SqlClient;

namespace LocalVibes.DALs
{
    public class DocumentTypeDAL : DAL<DocumentType>
    {
        protected override string TableName => "DocumentType";


        protected override DocumentType MapReaderToEntity(SqlDataReader reader)
        {
            return new DocumentType
            {
                IdDocumentType = (int)reader["IdDocumentType"],
                NameDocumentType = (string)reader["NameDocumentType"],
            };
        }

    }
}
