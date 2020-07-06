using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Wiseyard.Core.Models;
using Wiseyard.Core.Services.DbUtils;

namespace Wiseyard.Core.Services
{
    public class ChemicalStockService
    {
        public static int CreateChemicalStock(ChemicalStock chemicalStock)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("CreateChemicalStock", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ChemicalId", chemicalStock.ChemicalId);
                        cmd.Parameters.AddWithValue("@Amount", chemicalStock.Amount);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }

        public static ICollection<ChemicalStock> GetAllChemicalStocks()
        {
            List<ChemicalStock> chemicalStocks = new List<ChemicalStock>();

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetAllChemicalStocks", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int chemicalId = Convert.ToInt32(reader["ChemicalId"]);
                                double amount = Convert.ToDouble(reader["Amount"]);

                                ChemicalStock chemicalStock = new ChemicalStock
                                {
                                    ChemicalId = chemicalId,
                                    Amount = amount
                                };

                                chemicalStocks.Add(chemicalStock);
                            }
                        }
                    }
                }
            }

            return chemicalStocks;
        }

        public static ChemicalStock GetChemicalStockByChemicalId(int chemicalId)
        {
            ChemicalStock chemicalStock = null;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetChemicalStockByChemicalId", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ChemicalId", chemicalId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                chemicalId = Convert.ToInt32(reader["ChemicalId"]);
                                double amount = Convert.ToDouble(reader["Amount"]);

                                chemicalStock = new ChemicalStock
                                {
                                    ChemicalId = chemicalId,
                                    Amount = amount
                                };
                            }
                        }
                    }
                }
            }

            return chemicalStock;
        }

        public static int UpdateChemicalStock(ChemicalStock chemicalStock)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("UpdateChemicalStock", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ChemicalId", chemicalStock.ChemicalId);
                        cmd.Parameters.AddWithValue("@Amount", chemicalStock.Amount);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }

        public static int DeleteChemicalStock(int chemicalId)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("DeleteChemicalStock", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ChemicalId", chemicalId);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }
    }
}