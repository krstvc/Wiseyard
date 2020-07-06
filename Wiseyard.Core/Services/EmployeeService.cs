using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Wiseyard.Core.Models;
using Wiseyard.Core.Services.DbUtils;

namespace Wiseyard.Core.Services
{
    public class EmployeeService
    {
        public static int CreateEmployee(Employee employee)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("CreateEmployee", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                        cmd.Parameters.AddWithValue("@DateEmployed", employee.DateEmployed);
                        cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int));
                        cmd.Parameters["@Id"].Direction = System.Data.ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        result = Convert.ToInt32(cmd.Parameters["@Id"].Value);
                    }
                }
            }

            return result;
        }

        public static ICollection<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetAllEmployees", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["Id"]);
                                string firstName = reader["FirstName"] as string;
                                string lastName = reader["LastName"] as string;
                                DateTime dateEmployed = Convert.ToDateTime(reader["DateEmployed"]);

                                Employee employee = new Employee
                                {
                                    Id = id,
                                    FirstName = firstName,
                                    LastName = lastName,
                                    DateEmployed = dateEmployed
                                };

                                employees.Add(employee);
                            }
                        }
                    }
                }
            }

            return employees;
        }

        public static Employee GetEmployeeById(int id)
        {
            Employee employee = null;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetEmployeeById", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                id = Convert.ToInt32(reader["Id"]);
                                string firstName = reader["FirstName"] as string;
                                string lastName = reader["LastName"] as string;
                                DateTime dateEmployed = Convert.ToDateTime(reader["DateEmployed"]);

                                employee = new Employee
                                {
                                    Id = id,
                                    FirstName = firstName,
                                    LastName = lastName,
                                    DateEmployed = dateEmployed
                                };
                            }
                        }
                    }
                }
            }

            return employee;
        }

        public static int UpdateEmployee(Employee employee)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("UpdateEmployee", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", employee.Id);
                        cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                        cmd.Parameters.AddWithValue("@DateEmployed", employee.DateEmployed);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }

        public static int DeleteEmployee(int id)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("DeleteEmployee", conn))
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
