
using Microsoft.Data.SqlClient;

namespace LocalVibes.DALs
{
    public abstract class DAL<T> where T : class
    {
        private readonly string _connectionString;

        protected DAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected abstract string TableName { get; }
        protected abstract string IdName { get; }
        protected abstract T MapReaderToEntity(SqlDataReader reader);

        private void GetIdName()
        {
            string query = "";

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IdName = reader[0].ToString();
                    }
                }
            }
        }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {
            List<T> entities = new List<T>();

            string query = $"SELECT * FROM {TableName}";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        entities.Add(MapReaderToEntity(reader));
                    }
                }
            }

            return entities;
        }

        public T GetById(int id)
        {
            T entity = null;

            string query = $"SELECT * FROM {TableName} WHERE {IdName} = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();

                using(SqlDataReader reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        entity = MapReaderToEntity(reader);
                    }
                }
            }

            return entity;
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
