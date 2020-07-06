using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Wiseyard.Core.Models;
using Wiseyard.Core.Services.DbUtils;

namespace Wiseyard.Core.Services
{
    public class ChemicalService
    {
        public static int CreateChemical(Chemical chemical)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("CreateChemical", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Name", chemical.Name);
                        cmd.Parameters.AddWithValue("@Description", chemical.Description);
                        cmd.Parameters.AddWithValue("@ChemicalTypeId", chemical.ChemicalTypeId);
                        cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output);

                        cmd.ExecuteNonQuery();

                        result = Convert.ToInt32(cmd.Parameters["@Id"].Value);
                    }
                }
            }

            return result;
        }

        public static ICollection<Chemical> GetAllChemicals()
        {
            List<Chemical> chemicals = new List<Chemical>();

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetAllChemicals", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["Id"]);
                                string name = reader["Name"] as string;
                                string description = reader["Description"] as string;
                                int chemicalTypeId = Convert.ToInt32(reader["ChemicalTypeId"]);

                                Chemical chemical = new Chemical
                                {
                                    Id = id,
                                    Name = name,
                                    Description = description,
                                    ChemicalTypeId = chemicalTypeId
                                };

                                chemicals.Add(chemical);
                            }
                        }
                    }
                }
            }

            return chemicals;
        }

        public static Chemical GetChemicalById(int id)
        {
            Chemical chemical = null;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetChemicalById", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                id = Convert.ToInt32(reader["Id"]);
                                string name = reader["Name"] as string;
                                string description = reader["Description"] as string;
                                int chemicalTypeId = Convert.ToInt32(reader["ChemicalTypeId"]);

                                chemical = new Chemical
                                {
                                    Id = id,
                                    Name = name,
                                    Description = description,
                                    ChemicalTypeId = chemicalTypeId
                                };
                            }
                        }
                    }
                }
            }

            return chemical;
        }

        public static int UpdateChemical(Chemical chemical)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("UpdateChemical", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", chemical.Id);
                        cmd.Parameters.AddWithValue("@Name", chemical.Name);
                        cmd.Parameters.AddWithValue("@Description", chemical.Description);
                        cmd.Parameters.AddWithValue("@ChemicalTypeId", chemical.ChemicalTypeId);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }

        public static int DeleteChemical(int id)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("DeleteChemical", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", id);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }
    }
}
