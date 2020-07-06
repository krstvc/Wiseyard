using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Wiseyard.Core.Models;
using Wiseyard.Core.Services.DbUtils;

namespace Wiseyard.Core.Services
{
    public class ExpenseService
    {
        public static int CreateExpense(Expense expense)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("CreateExpense", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ResourceId", expense.ResourceId);
                        cmd.Parameters.AddWithValue("@Amount", expense.Amount);
                        cmd.Parameters.AddWithValue("@Price", expense.Price);
                        cmd.Parameters.AddWithValue("@Date", expense.Date);
                        cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int));
                        cmd.Parameters["@Id"].Direction = System.Data.ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        result = Convert.ToInt32(cmd.Parameters["@Id"].Value);
                    }
                }
            }

            return result;
        }

        public static ICollection<Expense> GetAllExpenses()
        {
            List<Expense> expenses = new List<Expense>();

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetAllExpenses", conn))
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

                                Expense expense = new Expense
                                {
                                    Id = id,
                                    ResourceId = resourceId,
                                    Amount = amount,
                                    Price = price,
                                    Date = date
                                };

                                expenses.Add(expense);
                            }
                        }
                    }
                }
            }

            return expenses;
        }

        public static Expense GetExpenseById(int id)
        {
            Expense expense = null;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetExpenseById", conn))
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

                                expense = new Expense
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

            return expense;
        }

        public static int UpdateExpense(Expense expense)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("UpdateExpense", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", expense.Id);
                        cmd.Parameters.AddWithValue("@ResourceId", expense.ResourceId);
                        cmd.Parameters.AddWithValue("@Amount", expense.Amount);
                        cmd.Parameters.AddWithValue("@Price", expense.Price);
                        cmd.Parameters.AddWithValue("@Date", expense.Date);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }

        public static int DeleteExpense(int id)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("DeleteExpense", conn))
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
