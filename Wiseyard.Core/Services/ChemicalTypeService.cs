using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Wiseyard.Core.Models;
using Wiseyard.Core.Services.DbUtils;

namespace Wiseyard.Core.Services
{
    public class ChemicalTypeService
    {
        public static int CreateChemicalType(ChemicalType chemicalType)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("CreateChemicalType", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Type", chemicalType.Type);
                        cmd.Parameters.AddWithValue("@Description", chemicalType.Description);
                        cmd.Parameters.AddWithValue("@MeasurementUnitId", chemicalType.MeasurementUnitId);
                        cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output);

                        cmd.ExecuteNonQuery();

                        result = Convert.ToInt32(cmd.Parameters["@Id"].Value);
                    }
                }
            }

            return result;
        }

        public static ICollection<ChemicalType> GetAllChemicalTypes()
        {
            List<ChemicalType> chemicalTypes = new List<ChemicalType>();

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetAllChemicalTypes", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["Id"]);
                                string type = reader["Type"] as string;
                                string description = reader["Description"] as string;
                                int measurementUnitId = Convert.ToInt32(reader["MeasurementUnitId"]);

                                ChemicalType chemicalType = new ChemicalType
                                {
                                    Id = id,
                                    Type = type,
                                    Description = description,
                                    MeasurementUnitId = measurementUnitId
                                };

                                chemicalTypes.Add(chemicalType);
                            }
                        }
                    }
                }
            }

            return chemicalTypes;
        }

        public static ChemicalType GetChemicalTypeById(int id)
        {
            ChemicalType chemicalType = null;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetChemicalTypeById", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                id = Convert.ToInt32(reader["Id"]);
                                string type = reader["Type"] as string;
                                string description = reader["Description"] as string;
                                int measurementUnitId = Convert.ToInt32(reader["MeasurementUnitId"]);

                                chemicalType = new ChemicalType
                                {
                                    Id = id,
                                    Type = type,
                                    Description = description,
                                    MeasurementUnitId = measurementUnitId
                                };
                            }
                        }
                    }
                }
            }

            return chemicalType;
        }

        public static int UpdateChemicalType(ChemicalType chemicalType)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("UpdateChemicalType", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", chemicalType.Id);
                        cmd.Parameters.AddWithValue("@Type", chemicalType.Type);
                        cmd.Parameters.AddWithValue("@Description", chemicalType.Description);
                        cmd.Parameters.AddWithValue("@MeasurementUnitId", chemicalType.MeasurementUnitId);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }

        public static int DeleteChemicalType(int id)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("DeleteChemicalType", conn))
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