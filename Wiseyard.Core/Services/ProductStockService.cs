using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Wiseyard.Core.Models;
using Wiseyard.Core.Services.DbUtils;

namespace Wiseyard.Core.Services
{
    public class ProductStockService
    {
        public static int CreateProductStock(ProductStock productStock)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("CreateProductStock", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ProductId", productStock.ProductId);
                        cmd.Parameters.AddWithValue("@Amount", productStock.Amount);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }

        public static ICollection<ProductStock> GetAllProductStocks()
        {
            List<ProductStock> productStocks = new List<ProductStock>();

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetAllProductStocks", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int chemicalId = Convert.ToInt32(reader["ProductId"]);
                                double amount = Convert.ToDouble(reader["Amount"]);

                                ProductStock productStock = new ProductStock
                                {
                                    ProductId = chemicalId,
                                    Amount = amount
                                };

                                productStocks.Add(productStock);
                            }
                        }
                    }
                }
            }

            return productStocks;
        }

        public static ProductStock GetProductStockByProductId(int chemicalId)
        {
            ProductStock productStock = null;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetProductStockByProductId", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ProductId", chemicalId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                chemicalId = Convert.ToInt32(reader["ProductId"]);
                                double amount = Convert.ToDouble(reader["Amount"]);

                                productStock = new ProductStock
                                {
                                    ProductId = chemicalId,
                                    Amount = amount
                                };
                            }
                        }
                    }
                }
            }

            return productStock;
        }

        public static int UpdateProductStock(ProductStock productStock)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("UpdateProductStock", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ProductId", productStock.ProductId);
                        cmd.Parameters.AddWithValue("@Amount", productStock.Amount);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }

        public static int DeleteProductStock(int chemicalId)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("DeleteProductStock", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ProductId", chemicalId);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }
    }
}
