﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.Common;
using Dapper;
namespace MBO
{
    public static class DBManager<TEntity> where TEntity : class
    {
        private static readonly string connectionString =
            ConfigurationManager.ConnectionStrings["cnnString"].ConnectionString;

        public static IDbConnection GetOpenConnection()
        {
            var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            var connection = factory.CreateConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
            return connection;
        }

        public static List<TEntity> ExecuteReader(string sql, object param = null, CommandType type = CommandType.StoredProcedure)
        {
            using (IDbConnection db = GetOpenConnection())
            {
                return db.Query<TEntity>(sql, param, commandType: type).AsList();
            }
        }


        public static TEntity FindById(string sql, int id, CommandType type = CommandType.StoredProcedure)
        {
            using (IDbConnection db = GetOpenConnection())
            {
                return db.Query<TEntity>(sql, new { id }, commandType: type).SingleOrDefault();
            }
        }

        public static int Execute(string sql, object param = null, CommandType type = CommandType.StoredProcedure)
        {
            using (IDbConnection db = GetOpenConnection())
            {
                return db.Execute(sql, param, commandType: type);
            }
        }

        public static int ExecuteMultiple(string sql, object[] param = null, CommandType type = CommandType.StoredProcedure)
        {
            using (IDbConnection db = GetOpenConnection())
            {
                return db.Execute(sql, param, commandType: type);
            }
        }

        public static object ExecuteScalar(string sql, object param = null, CommandType type = CommandType.StoredProcedure)
        {
            using (IDbConnection db = GetOpenConnection())
            {
                return db.ExecuteScalar(sql, param, commandType: type);
            }
        }
    }
}