using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Wiseyard.Core.Models;
using Wiseyard.Core.Services.DbUtils;

namespace Wiseyard.Core.Services
{
    public class UtilStockService
    {
        public static int CreateUtilStock(UtilStock utilStock)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("CreateUtilStock", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UtilId", utilStock.UtilId);
                        cmd.Parameters.AddWithValue("@Amount", utilStock.Amount);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }

        public static ICollection<UtilStock> GetAllUtilStocks()
        {
            List<UtilStock> utilStocks = new List<UtilStock>();

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetAllUtilStocks", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int utilId = Convert.ToInt32(reader["UtilId"]);
                                int amount = Convert.ToInt32(reader["Amount"]);

                                UtilStock utilStock = new UtilStock
                                {
                                    UtilId = utilId,
                                    Amount = amount
                                };

                                utilStocks.Add(utilStock);
                            }
                        }
                    }
                }
            }

            return utilStocks;
        }

        public static UtilStock GetUtilStockByUtilId(int utilId)
        {
            UtilStock utilStock = null;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetUtilStockByUtilId", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UtilId", utilId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                utilId = Convert.ToInt32(reader["UtilId"]);
                                int amount = Convert.ToInt32(reader["Amount"]);

                                utilStock = new UtilStock
                                {
                                    UtilId = utilId,
                                    Amount = amount
                                };
                            }
                        }
                    }
                }
            }

            return utilStock;
        }

        public static int UpdateUtilStock(UtilStock utilStock)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("UpdateUtilStock", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UtilId", utilStock.UtilId);
                        cmd.Parameters.AddWithValue("@Amount", utilStock.Amount);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }

        public static int DeleteUtilStock(int utilId)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("DeleteUtilStock", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UtilId", utilId);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }
    }
}
