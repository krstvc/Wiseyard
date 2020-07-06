using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Wiseyard.Core.Models;
using Wiseyard.Core.Services.DbUtils;

namespace Wiseyard.Core.Services
{
    public class ProductTypeService
    {
        public static int CreateProductType(ProductType productType)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("CreateProductType", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Type", productType.Type);
                        cmd.Parameters.AddWithValue("@Description", productType.Description);
                        cmd.Parameters.AddWithValue("@MeasurementUnitId", productType.MeasurementUnitId);
                        cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output);

                        cmd.ExecuteNonQuery();

                        result = Convert.ToInt32(cmd.Parameters["@Id"].Value);
                    }
                }
            }

            return result;
        }

        public static ICollection<ProductType> GetAllProductTypes()
        {
            List<ProductType> productTypes = new List<ProductType>();

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetAllProductTypes", conn))
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

                                ProductType productType = new ProductType
                                {
                                    Id = id,
                                    Type = type,
                                    Description = description,
                                    MeasurementUnitId = measurementUnitId
                                };

                                productTypes.Add(productType);
                            }
                        }
                    }
                }
            }

            return productTypes;
        }

        public static ProductType GetProductTypeById(int id)
        {
            ProductType productType = null;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetProductTypeById", conn))
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

                                productType = new ProductType
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

            return productType;
        }

        public static int UpdateProductType(ProductType productType)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("UpdateProductType", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", productType.Id);
                        cmd.Parameters.AddWithValue("@Type", productType.Type);
                        cmd.Parameters.AddWithValue("@Description", productType.Description);
                        cmd.Parameters.AddWithValue("@MeasurementUnitId", productType.MeasurementUnitId);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }

        public static int DeleteProductType(int id)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("DeleteProductType", conn))
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
