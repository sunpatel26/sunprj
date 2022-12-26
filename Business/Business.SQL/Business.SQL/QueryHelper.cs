using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.SQL
{
    public class QueryHelper
    {
        public static int Save(string connection,string query, DynamicParameters param)
        {
            try
            {
                using (var conn = CommonDataOperations.GetConnection(connection))
                {
                    return conn.Query<int>(query, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw (ex.InnerException == null ? ex : ex.InnerException);
            }
        }

        public static List<T> GetList<T>(string connection, string query, DynamicParameters param)
        {
            try
            {
                using (var conn = CommonDataOperations.GetConnection(connection))
                {
                    return conn.Query<T>(query, param, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                throw (ex.InnerException == null ? ex : ex.InnerException);
            }
        }

        public static T GetTableDetail<T>(string connection, string query, DynamicParameters param, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                using (var conn = CommonDataOperations.GetConnection(connection))
                {
                    return conn.Query<T>(query, param, commandType: commandType).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw (ex.InnerException == null ? ex : ex.InnerException);
            }
        }

        public static List<T> GetList<T>(string connection, string query)
        {
            try
            {
                using (var conn = CommonDataOperations.GetConnection(connection))
                {
                    return conn.Query<T>(query, commandType: CommandType.Text).ToList();
                }
            }
            catch (Exception ex)
            {
                throw (ex.InnerException == null ? ex : ex.InnerException);
            }
        }

        public static bool Delete(string connection, string query, DynamicParameters param)
        {
            try
            {
                using (var conn = CommonDataOperations.GetConnection(connection))
                {
                    var result = conn.Query(query, param, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw (ex.InnerException == null ? ex : ex.InnerException);
            }
        }
    }
}
