using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Wiseyard.Core.Models;
using Wiseyard.Core.Services.DbUtils;

namespace Wiseyard.Core.Services
{
    public class SeasonalProductionService
    {
        public static int CreateSeasonalProduction(SeasonalProduction seasonalProduction)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("CreateSeasonalProduction", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@SeasonId", seasonalProduction.SeasonId);
                        cmd.Parameters.AddWithValue("@ProductId", seasonalProduction.ProductId);
                        cmd.Parameters.AddWithValue("@Amount", seasonalProduction.Amount);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }

        public static ICollection<SeasonalProduction> GetAllSeasonalProductions()
        {
            List<SeasonalProduction> seasonalProductions = new List<SeasonalProduction>();

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetAllSeasonalProductions", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int seasonId = Convert.ToInt32(cmd.Parameters["SeasonId"]);
                                int productId = Convert.ToInt32(cmd.Parameters["ProductId"]);
                                double amount = Convert.ToDouble(cmd.Parameters["Amount"]);

                                SeasonalProduction seasonalProduction = new SeasonalProduction
                                {
                                    SeasonId = seasonId,
                                    ProductId = productId,
                                    Amount = amount
                                };

                                seasonalProductions.Add(seasonalProduction);
                            }
                        }
                    }
                }
            }

            return seasonalProductions;
        }

        public static ICollection<SeasonalProduction> GetSeasonalProductionByProductId(int productId)
        {
            List<SeasonalProduction> seasonalProductions = new List<SeasonalProduction>();

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetSeasonalProductionByProductId", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ProductId", productId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int seasonId = Convert.ToInt32(cmd.Parameters["SeasonId"]);
                                productId = Convert.ToInt32(cmd.Parameters["ProductId"]);
                                double amount = Convert.ToDouble(cmd.Parameters["Amount"]);

                                SeasonalProduction seasonalProduction = new SeasonalProduction
                                {
                                    SeasonId = seasonId,
                                    ProductId = productId,
                                    Amount = amount
                                };

                                seasonalProductions.Add(seasonalProduction);
                            }
                        }
                    }
                }
            }

            return seasonalProductions;
        }

        public static ICollection<SeasonalProduction> GetSeasonalProductionBySeasonId(int seasonId)
        {
            List<SeasonalProduction> seasonalProductions = new List<SeasonalProduction>();

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetSeasonalProductionBySeasonId", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@SeasonId", seasonId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                seasonId = Convert.ToInt32(cmd.Parameters["SeasonId"]);
                                int productId = Convert.ToInt32(cmd.Parameters["ProductId"]);
                                double amount = Convert.ToDouble(cmd.Parameters["Amount"]);

                                SeasonalProduction seasonalProduction = new SeasonalProduction
                                {
                                    SeasonId = seasonId,
                                    ProductId = productId,
                                    Amount = amount
                                };

                                seasonalProductions.Add(seasonalProduction);
                            }
                        }
                    }
                }
            }

            return seasonalProductions;
        }

        public static int UpdateSeasonalProduction(SeasonalProduction seasonalProduction)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("UpdateSeasonalProduction", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@SeasonId", seasonalProduction.SeasonId);
                        cmd.Parameters.AddWithValue("@ProductId", seasonalProduction.ProductId);
                        cmd.Parameters.AddWithValue("@Amount", seasonalProduction.Amount);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }

        public static int DeleteSeasonalProduction(int seasonId, int productId)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("DeleteSeasonalProduction", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@SeasonId", seasonId);
                        cmd.Parameters.AddWithValue("@ProductId", productId);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }
    }
}
