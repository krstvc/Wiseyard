using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Wiseyard.Core.Services.DbUtils;

namespace Wiseyard.Core.Services
{
    public class EmployeeJobService
    {
        public static int AddEmployeeToJob(int employeeId, int jobId)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("AddEmployeeToJob", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                        cmd.Parameters.AddWithValue("@JobId", jobId);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }

        public static int RemoveEmployeesFromJob(int employeeId, int jobId)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("RemoveEmployeesFromJob", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                        cmd.Parameters.AddWithValue("@JobId", jobId);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }
    }
}