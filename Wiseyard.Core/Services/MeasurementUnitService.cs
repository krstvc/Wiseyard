using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Wiseyard.Core.Models;
using Wiseyard.Core.Services.DbUtils;

namespace Wiseyard.Core.Services
{
    public class MeasurementUnitService
    {
        public static int CreateMeasurementUnit(MeasurementUnit measurementUnit)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("CreateMeasurementUnit", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Unit", measurementUnit.Unit);
                        cmd.Parameters.AddWithValue("@Abbreviation", measurementUnit.Abbreviation);
                        cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output);

                        cmd.ExecuteNonQuery();

                        result = Convert.ToInt32(cmd.Parameters["@Id"].Value);
                    }
                }
            }

            return result;
        }

        public static ICollection<MeasurementUnit> GetAllMeasurementUnits()
        {
            List<MeasurementUnit> measurementUnits = new List<MeasurementUnit>();

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetAllMeasurementUnits", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["Id"]);
                                string unit = reader["Unit"] as string;
                                string abbreviation = reader["Abbreviaton"] as string;

                                MeasurementUnit measurementUnit = new MeasurementUnit
                                {
                                    Id = id,
                                    Unit = unit,
                                    Abbreviation = abbreviation
                                };

                                measurementUnits.Add(measurementUnit);
                            }
                        }
                    }
                }
            }

            return measurementUnits;
        }

        public static MeasurementUnit GetMeasurementUnitById(int id)
        {
            MeasurementUnit measurementUnit = null;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetMeasurementUnitById", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                id = Convert.ToInt32(reader["Id"]);
                                string unit = reader["Unit"] as string;
                                string abbreviation = reader["Abbreviaton"] as string;

                                measurementUnit = new MeasurementUnit
                                {
                                    Id = id,
                                    Unit = unit,
                                    Abbreviation = abbreviation
                                };
                            }
                        }
                    }
                }
            }

            return measurementUnit;
        }

        public static int UpdateMeasurementUnit(MeasurementUnit measurementUnit)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("UpdateMeasurementUnit", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", measurementUnit.Id);
                        cmd.Parameters.AddWithValue("@Unit", measurementUnit.Unit);
                        cmd.Parameters.AddWithValue("@Abbreviation", measurementUnit.Abbreviation);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }

        public static int DeleteMeasurementUnit(int id)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("DeleteMeasurementUnit", conn))
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
