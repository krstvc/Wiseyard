using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Wiseyard.Core.Models;
using Wiseyard.Core.Services.DbUtils;

namespace Wiseyard.Core.Services
{
    public class JobService
    {
        public static int CreateJob(Job job)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("CreateJob", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Date", job.Date);
                        cmd.Parameters.AddWithValue("@Description", job.Description);
                        cmd.Parameters.AddWithValue("@JobTypeId", job.JobTypeId);
                        cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int));
                        cmd.Parameters["@Id"].Direction = System.Data.ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        result = Convert.ToInt32(cmd.Parameters["@Id"].Value);
                    }
                }
            }

            return result;
        }

        public static ICollection<Job> GetAllJobs()
        {
            List<Job> jobs = new List<Job>();

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetAllJobs", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["Id"]);
                                DateTime date = Convert.ToDateTime(reader["Date"]);
                                string description = reader["Description"] as string;
                                int jobTypeId = Convert.ToInt32(reader["JobTypeId"]);

                                Job job = new Job
                                {
                                    Id = id,
                                    Date = date,
                                    Description = description,
                                    JobTypeId = jobTypeId
                                };

                                jobs.Add(job);
                            }
                        }
                    }
                }
            }

            return jobs;
        }

        public static Job GetJobById(int id)
        {
            Job job = null;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("GetJobById", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                id = Convert.ToInt32(reader["Id"]);
                                DateTime date = Convert.ToDateTime(reader["Date"]);
                                string description = reader["Description"] as string;
                                int jobTypeId = Convert.ToInt32(reader["JobTypeId"]);

                                job = new Job
                                {
                                    Id = id,
                                    Date = date,
                                    Description = description,
                                    JobTypeId = jobTypeId
                                };
                            }
                        }
                    }
                }
            }

            return job;
        }

        public static int UpdateJob(Job job)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("UpdateJob", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", job.Id);
                        cmd.Parameters.AddWithValue("@Date", job.Date);
                        cmd.Parameters.AddWithValue("@Description", job.Description);
                        cmd.Parameters.AddWithValue("@JobTypeId", job.JobTypeId);

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }

            return result;
        }

        public static int DeleteJob(int id)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DbConnectionService.GetConnectionString()))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("DeleteJob", conn))
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
