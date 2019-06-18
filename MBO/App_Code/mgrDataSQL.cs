using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MBO
{
    public class mgrDataSQL
    {
        public static string connStr = ConfigurationManager.ConnectionStrings["cnnString"].ConnectionString;


        // store procedure
       

        public static DataTable ExecuteStoreReader(string storename, Dictionary<string, object> param = null)
        {
            SqlConnection connect = new SqlConnection(connStr);
            DataTable dtb = null;

            if (dtb == null)
            {
                try
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand(storename, connect);
                    command.CommandType = CommandType.StoredProcedure;
                    if (param != null)
                    {
                        foreach (var item in param)
                        {
                            command.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                        }
                    }

                    SqlDataReader reader = command.ExecuteReader();
                    dtb = new DataTable();
                    dtb.Load(reader);
                 
                    return dtb;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    connect.Dispose();
                }
            }
            else
            {
                //get cache
                return dtb;
            }
        }
        public static int ExecuteStoreNonQuery(string storename, Dictionary<string, object> param = null)
        {
            using (SqlConnection connect = new SqlConnection(connStr))
            {
                try
                {
                    connect.Open();
                    using (SqlCommand command = new SqlCommand(storename, connect))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                      
                        if (param != null)
                        {
                            foreach (var item in param)
                            {
                                command.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                            }
                        }

                        int count = command.ExecuteNonQuery();
                       
                        return count;
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    connect.Dispose();
                }
            }
        }
        public static object ExecuteStoreScalar(string storename, Dictionary<string, object> param = null)
        {
            using (SqlConnection connect = new SqlConnection(connStr))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(storename, connect))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (param != null)
                        {
                            foreach (var item in param)
                            {
                                command.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                            }
                        }
                        connect.Open();
                        object result = command.ExecuteScalar();
                        return result;
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    connect.Dispose();
                }
            }
        }
    }
}