using System.Collections.Generic;
using System.Data;
using LocalVibes.Models;
using Microsoft.Data.SqlClient;

namespace LocalVibes.DALs
{
    public class InstrumentDAL
    {
        private readonly string _connectionString = "Server=85.208.21.117,54321;Database=AbelAlexiaDavidJoelLocalVibes;User Id=sa;Password=Sql#123456789;TrustServerCertificate=True;";

        /// <summary>
        /// Obtiene los instrumentos asociados a un miembro específico.
        /// </summary>
        /// <param name="memberId">El ID del miembro.</param>
        /// <returns>Lista de instrumentos asociados al miembro.</returns>
        public List<Instrument> GetMemberInstrumentsByMemberId(int memberId)
        {
            var memberInstruments = new List<Instrument>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
            SELECT i.IdInstrument, i.NameInstrument
            FROM MemberInstrument mi
            JOIN Instrument i ON mi.IdInstrument = i.IdInstrument
            WHERE mi.IdMember = @memberId";

                    command.Parameters.Add(new SqlParameter("@memberId", System.Data.SqlDbType.Int) { Value = memberId });

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            memberInstruments.Add(new Instrument
                            {
                                IdInstrument = (int)reader["IdInstrument"],
                                NameInstrument = (string)reader["NameInstrument"]
                            });
                        }
                    }
                }
            }

            return memberInstruments;
        }


    }
}
