using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Wiseyard.Core.Models;
using Wiseyard.Core.Services.DbUtils;

namespace Wiseyard.Core.Services
{
    public class UtilService
    {
        public static int CreateUtil(Util util)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("CreateUtil", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Name", util.Name);
                        cmd.Parameters.AddWithValue("@Description", util.Description);
                        cmd.Parameters.AddWithValue("@UtilTypeId", util.UtilTypeId);
                        cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output);

                        cmd.ExecuteNonQuery();

                        result = Convert.ToInt32(cmd.Parameters["@Id"].Value);
                    }
                }
            }

            return result;
        }

        public static ICollection<Util> GetAllUtils()
        {
            List<Util> utils = new List<Util>();

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetAllUtils", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["Id"]);
                                string name = reader["Name"] as string;
                                string description = reader["Description"] as string;
                                int utilTypeId = Convert.ToInt32(reader["UtilTypeId"]);

                                Util util = new Util
                                {
                                    Id = id,
                                    Name = name,
                                    Description = description,
                                    UtilTypeId = utilTypeId
                                };

                                utils.Add(util);
                            }
                        }
                    }
                }
            }

            return utils;
        }

        public static Util GetUtilById(int id)
        {
            Util util = null;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetUtilById", conn))
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
                                int utilTypeId = Convert.ToInt32(reader["UtilTypeId"]);

                                util = new Util
                                {
                                    Id = id,
                                    Name = name,
                                    Description = description,
                                    UtilTypeId = utilTypeId
                                };
                            }
                        }
                    }
                }
            }

            return util;
        }

        public static int UpdateUtil(Util util)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("UpdateUtil", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", util.Id);
                        cmd.Parameters.AddWithValue("@Name", util.Name);
                        cmd.Parameters.AddWithValue("@Description", util.Description);
                        cmd.Parameters.AddWithValue("@UtilTypeId", util.UtilTypeId);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }

        public static int DeleteUtil(int id)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("DeleteUtil", conn))
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
