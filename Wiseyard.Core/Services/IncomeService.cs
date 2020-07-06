using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Wiseyard.Core.Models;
using Wiseyard.Core.Services.DbUtils;

namespace Wiseyard.Core.Services
{
    public class IncomeService
    {
        public static int CreateIncome(Income income)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("CreateIncome", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ResourceId", income.ResourceId);
                        cmd.Parameters.AddWithValue("@Amount", income.Amount);
                        cmd.Parameters.AddWithValue("@Price", income.Price);
                        cmd.Parameters.AddWithValue("@Date", income.Date);
                        cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int));
                        cmd.Parameters["@Id"].Direction = System.Data.ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        result = Convert.ToInt32(cmd.Parameters["@Id"].Value);
                    }
                }
            }

            return result;
        }

        public static ICollection<Income> GetAllIncomes()
        {
            List<Income> incomes = new List<Income>();

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetAllIncomes", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["Id"]);
                                int resourceId = Convert.ToInt32(reader["ResourceId"]);
                                double amount = Convert.ToDouble(reader["Amount"]);
                                double price = Convert.ToDouble(reader["Price"]);
                                DateTime date = Convert.ToDateTime(reader["Date"]);

                                Income income = new Income
                                {
                                    Id = id,
                                    ResourceId = resourceId,
                                    Amount = amount,
                                    Price = price,
                                    Date = date
                                };

                                incomes.Add(income);
                            }
                        }
                    }
                }
            }

            return incomes;
        }

        public static Income GetIncomeById(int id)
        {
            Income income = null;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetIncomeById", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                id = Convert.ToInt32(reader["Id"]);
                                int resourceId = Convert.ToInt32(reader["ResourceId"]);
                                double amount = Convert.ToDouble(reader["Amount"]);
                                double price = Convert.ToDouble(reader["Price"]);
                                DateTime date = Convert.ToDateTime(reader["Date"]);

                                income = new Income
                                {
                                    Id = id,
                                    ResourceId = resourceId,
                                    Amount = amount,
                                    Price = price,
                                    Date = date
                                };
                            }
                        }
                    }
                }
            }

            return income;
        }

        public static int UpdateIncome(Income income)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("UpdateIncome", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", income.Id);
                        cmd.Parameters.AddWithValue("@ResourceId", income.ResourceId);
                        cmd.Parameters.AddWithValue("@Amount", income.Amount);
                        cmd.Parameters.AddWithValue("@Price", income.Price);
                        cmd.Parameters.AddWithValue("@Date", income.Date);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }

        public static int DeleteIncome(int id)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("DeleteIncome", conn))
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
