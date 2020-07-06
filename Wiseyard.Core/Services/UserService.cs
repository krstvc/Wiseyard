using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Wiseyard.Core.Models;
using Wiseyard.Core.Services.DbUtils;

namespace Wiseyard.Core.Services
{
    public class UserService
    {
        public static int CreateUser(User user)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("CreateUser", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                        cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int));
                        cmd.Parameters["@Id"].Direction = System.Data.ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        result = Convert.ToInt32(cmd.Parameters["@Id"].Value);
                    }
                }
            }

            return result;
        }

        public static ICollection<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetAllUsers", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["Id"]);
                                string username = reader["Username"] as string;
                                string passwordHash = reader["PasswordHash"] as string;

                                User user = new User
                                {
                                    Id = id,
                                    Username = username,
                                    PasswordHash = passwordHash
                                };

                                users.Add(user);
                            }
                        }
                    }
                }
            }

            return users;
        }

        public static User GetUserById(int id)
        {
            User user = null;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetUserById", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                id = Convert.ToInt32(reader["Id"]);
                                string username = reader["Username"] as string;
                                string passwordHash = reader["PasswordHash"] as string;

                                user = new User
                                {
                                    Id = id,
                                    Username = username,
                                    PasswordHash = passwordHash
                                };
                            }
                        }
                    }
                }
            }

            return user;
        }

        public static User GetUserByUsername(string username)
        {
            User user = null;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetUserByUsername", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Username", username);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["Id"]);
                                username = reader["Username"] as string;
                                string passwordHash = reader["PasswordHash"] as string;

                                user = new User
                                {
                                    Id = id,
                                    Username = username,
                                    PasswordHash = passwordHash
                                };
                            }
                        }
                    }
                }
            }

            return user;
        }

        public static int UpdateUser(User user)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("UpdateUser", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", user.Id);
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }

        public static int DeleteUser(int id)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("DeleteUser", conn))
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
