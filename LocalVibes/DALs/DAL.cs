﻿
using Microsoft.Data.SqlClient;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using LocalVibes.Tools;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LocalVibes.DALs
{
    public abstract class DAL<T> : IDal<T> where T : class
    {
        private readonly string _connectionString = "Server=85.208.21.117,54321;Database=AbelAlexiaDavidJoelLocalVibes;User Id=sa;Password=Sql#123456789;TrustServerCertificate=True;";

        protected DAL() { }

        protected abstract string TableName { get; }
        protected string IdName { get; set; } = "";
        protected abstract T MapReaderToEntity(SqlDataReader reader);

        private void GetIdName()
        {
            string query = @$"SELECT c.name AS column_name
                                FROM sys.indexes i
                                JOIN sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
                                JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
                                WHERE i.is_primary_key = 1 AND i.object_id = OBJECT_ID('{TableName}');";

            using (SqlConnection connection = new SqlConnection(_connectionString))
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
            var properties = typeof(T).GetProperties()
                              .Where(p => !Attribute.IsDefined(p, typeof(NotMappedAttribute)) // Excluir propiedades no mapeadas
                                          && (!typeof(IEnumerable).IsAssignableFrom(p.PropertyType) || p.PropertyType.IsArray || p.PropertyType == typeof(string)) // Excluir colecciones
                                          && !Attribute.IsDefined(p, typeof(KeyAttribute))) // Excluir la clave primaria
                              .ToList();

            // Generar las columnas y valores para la consulta
            string columns = string.Join(", ", properties.Select(p => p.Name));
            string values = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            // Crear la consulta de inserción
            string query = $"INSERT INTO {TableName} ({columns}) VALUES ({values})";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Asignar los valores de las propiedades a los parámetros
                foreach (var property in properties)
                {
                    var value = property.GetValue(entity);

                    if (property.PropertyType == typeof(byte[]))
                    {
                        var parameter = command.Parameters.Add($"@{property.Name}", SqlDbType.VarBinary);
                        parameter.Value = value ?? DBNull.Value;
                    }
                    else
                    {
                        command.Parameters.AddWithValue($"@{property.Name}", value ?? DBNull.Value);
                    }


                }

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

		public void Delete(Dictionary<string, object> primaryKeyValues)
		{
			if (primaryKeyValues == null || primaryKeyValues.Count == 0)
			{
				throw new ArgumentException("Debe proporcionar valores para las claves primarias.");
			}

			// Construye la cláusula WHERE dinámicamente
			string whereClause = string.Join(" AND ", primaryKeyValues.Keys.Select(key => $"{key} = @{key}"));

			// Construye la consulta SQL
			string query = $"DELETE FROM {TableName} WHERE {whereClause}";

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				SqlCommand command = new SqlCommand(query, connection);

				// Agregar parámetros para las claves primarias
				foreach (var kvp in primaryKeyValues)
				{
					command.Parameters.AddWithValue($"@{kvp.Key}", kvp.Value ?? DBNull.Value);
				}

				connection.Open();
				command.ExecuteNonQuery();
			}
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
            GetIdName();

            T? entity = null;

            string query = $"SELECT * FROM {TableName} WHERE {IdName} = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        entity = MapReaderToEntity(reader);
                    }
                }
            }

            return entity;
        }

        public T GetByName(string value)
        {
            string columnName = GetColumnName();

            string query = $"SELECT * FROM {TableName} WHERE {columnName} = @Value";

            return QuerySingle
            (
                query,
                 reader => MapReaderToEntity((SqlDataReader)reader),
                 new SqlParameter("@Value", value)
            );
        }

		public void Update(T entity)
		{
			// Asegúrate de que el nombre del ID esté definido
			GetIdName();

			// Obtiene las propiedades que se pueden mapear a columnas
			var properties = typeof(T).GetProperties()
									  .Where(p => !Attribute.IsDefined(p, typeof(NotMappedAttribute)) // Excluir propiedades no mapeadas
												  && (!typeof(IEnumerable).IsAssignableFrom(p.PropertyType) || p.PropertyType.IsArray || p.PropertyType == typeof(string)) // Excluir colecciones
												  && !Attribute.IsDefined(p, typeof(KeyAttribute))) // Excluir la clave primaria
									  .ToList();

			// Generar las columnas para la consulta
			string setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));

			// Crear la consulta de actualización
			string query = $"UPDATE {TableName} SET {setClause} WHERE {IdName} = @IdName";

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				SqlCommand command = new SqlCommand(query, connection);

				// Asignar los valores de las propiedades a los parámetros
				foreach (var property in properties)
				{
					var value = property.GetValue(entity);

					if (property.PropertyType == typeof(byte[]))
					{
						var parameter = command.Parameters.Add($"@{property.Name}", SqlDbType.VarBinary);
						parameter.Value = value ?? DBNull.Value;
					}
					else
					{
						command.Parameters.AddWithValue($"@{property.Name}", value ?? DBNull.Value);
					}
				}

				// Asignar el valor del identificador (ID) a los parámetros
				var idProperty = typeof(T).GetProperties().FirstOrDefault(p => Attribute.IsDefined(p, typeof(KeyAttribute)) || p.Name.Equals(IdName, StringComparison.OrdinalIgnoreCase));
				if (idProperty != null)
				{
					var idValue = idProperty.GetValue(entity);
					command.Parameters.AddWithValue($"@IdName", idValue);
				}

				connection.Open();
				command.ExecuteNonQuery();
			}
		}


		protected List<T> Query(string query, Func<IDataReader, T> map, params SqlParameter[] parameters)
        {
            List<T> entities = new List<T>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        entities.Add(map(reader));
                    }
                }
            }

            return entities;
        }

        protected T QuerySingle(string query, Func<IDataReader, T> map, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return map(reader);
                    }
                }
            }

            return null;
        }

        private string GetColumnName()
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                if (Attribute.IsDefined(property, typeof(NameAttribute)))
                {
                    return property.Name;
                }
            }

            throw new InvalidOperationException($"No searchable property found in {typeof(T).Name}");
        }

        public IEnumerable<SelectListItem> GetAllEnum(Func<T, string> textSelector, Func<T, string> valueSelector)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            string query = $"SELECT * FROM {TableName}";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T entity = MapReaderToEntity(reader);
                        string text = textSelector(entity);
                        string value = valueSelector(entity);

                        listItems.Add(new SelectListItem
                        {
                            Text = text,
                            Value = value
                        });
                    }
                }
            }

            return listItems;
        }


    }
}
