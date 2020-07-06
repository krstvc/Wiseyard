using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Wiseyard.Core.Services.DbUtils;

namespace Wiseyard.Core.Services
{
    public class UserRoleService
    {
        public static int AddUserToRole(int userId, int roleId)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("AddUserToRole", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@RoleId", roleId);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }

        public static int RemoveUserFromRole(int userId, int roleId)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("RemoveUserFromRole", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@RoleId", roleId);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }
    }
}
