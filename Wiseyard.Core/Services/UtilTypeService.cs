using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Wiseyard.Core.Models;
using Wiseyard.Core.Services.DbUtils;

namespace Wiseyard.Core.Services
{
    public class UtilTypeService
    {
        public static int CreateUtilType(UtilType utilType)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("CreateUtilType", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Type", utilType.Type);
                        cmd.Parameters.AddWithValue("@Description", utilType.Description);
                        cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output);

                        cmd.ExecuteNonQuery();

                        result = Convert.ToInt32(cmd.Parameters["@Id"].Value);
                    }
                }
            }

            return result;
        }

        public static ICollection<UtilType> GetAllUtilTypes()
        {
            List<UtilType> utilTypes = new List<UtilType>();

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetAllUtilTypes", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["Id"]);
                                string type = reader["Type"] as string;
                                string description = reader["Description"] as string;

                                UtilType utilType = new UtilType
                                {
                                    Id = id,
                                    Type = type,
                                    Description = description
                                };

                                utilTypes.Add(utilType);
                            }
                        }
                    }
                }
            }

            return utilTypes;
        }

        public static UtilType GetUtilTypeById(int id)
        {
            UtilType utilType = null;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetUtilTypeById", conn))
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

                                utilType = new UtilType
                                {
                                    Id = id,
                                    Type = type,
                                    Description = description
                                };
                            }
                        }
                    }
                }
            }

            return utilType;
        }

        public static int UpdateUtilType(UtilType utilType)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("UpdateUtilType", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", utilType.Id);
                        cmd.Parameters.AddWithValue("@Type", utilType.Type);
                        cmd.Parameters.AddWithValue("@Description", utilType.Description);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }

        public static int DeleteUtilType(int id)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("DeleteUtilType", conn))
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
