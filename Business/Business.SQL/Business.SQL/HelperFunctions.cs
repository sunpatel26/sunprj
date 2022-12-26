using System;
using System.Data;
using System.Text.RegularExpressions;

namespace Business.SQL
{
    public static class HelperFunctions
    {
        public static bool ContainColumn(this DataTable table, string columnName)
        {
            if (table == null)
                return false;
            DataColumnCollection columns = table.Columns;
            if (columns.Contains(columnName))
            {
                return true;
            }
            return false;
        }

        public static object ReturnZeroIfNull(this object value)
        {
            if (value == DBNull.Value)
                return 0;
            if (value == null)
                return 0;
            return value;
        }

        public static object ReturnEmptyIfNull(this object value)
        {
            if (value == DBNull.Value)
                return string.Empty;
            if (value == null)
                return string.Empty;
            return value;
        }

        public static object ReturnFalseIfNull(this object value)
        {
            if (value == DBNull.Value)
                return false;
            if (value == null)
                return false;
            return value;
        }

        public static object ReturnDateTimeMinIfNull(this object value)
        {
            if (value == DBNull.Value)
                return DateTime.MinValue;
            if (value == null)
                return DateTime.MinValue;
            return value;
        }

        public static object ReturnNullIfDbNull(this object value)
        {
            if (value == DBNull.Value)
                return '\0';
            if (value == null)
                return '\0';
            return value;
        }
        public static object ReturnNullable(this object value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return null;
            }
        }


        public static bool IsValidEmail(this string emailAddress)
        {
            const string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            return Regex.IsMatch(emailAddress, pattern);
        }

        /// <summary>
        /// Convert DateTime to string
        /// </summary>
        /// <param name="datetTime"></param>
        /// <param name="excludeHoursAndMinutes">if true it will execlude time from datetime string. Default is false</param>
        /// <returns></returns>
        public static string ConvertDate(this DateTime datetTime, bool excludeHoursAndMinutes = false)
        {
            if (datetTime != DateTime.MinValue)
            {
                if (excludeHoursAndMinutes)
                    return datetTime.ToString("yyyy-MM-dd");
                return datetTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
            return null;
        }
        public static DataTable ToAddSequence(this DataTable dttable)
        {
            try
            {
                //Now we have Data Table  
                DataColumn dc = new DataColumn("RowNumber");
                dc.AutoIncrement = true;
                dc.AutoIncrementSeed = 1;
                dc.AutoIncrementStep = 1;
                dttable.Columns.Add(dc);
                dc.SetOrdinal(0);

                //Set values for existing rows  
                for (int i = 0; i <= dttable.Rows.Count - 1; i++)
                {
                    dttable.Rows[i]["RowNumber"] = i + 1;
                }
                return dttable;
            }
            catch
            {
                return dttable;
            }
        }
        public static DataTable ToAddBoolean(this DataTable dttable)
        {
            try
            {
                //Now we have Data Table  
                DataColumn dc = new DataColumn("Check", typeof(bool));
                dttable.Columns.Add(dc);
                dc.SetOrdinal(0);

                //Set values for existing rows  
                for (int i = 0; i <= dttable.Rows.Count - 1; i++)
                {
                    dttable.Rows[i]["Check"] = false;
                }
                return dttable;
            }
            catch
            {
                return dttable;
            }
        }
        public static DataTable ToRemoveColumnName(this DataTable dttable, string colName)
        {
            try
            {
                if (dttable.Columns.Contains(colName))
                    dttable.Columns.Remove(colName);
                return dttable;
            }
            catch
            {
                return dttable;
            }
        }
        public static DataTable ToRemoveColumnList(this DataTable dttable, string[] colName)
        {
            try
            {
                for (int i = 0; i < colName.Length; i++)
                {
                    string Name = colName[i];
                    if (dttable.Columns.Contains(Name))
                        dttable.Columns.Remove(Name);
                }
                return dttable;
            }
            catch
            {
                return dttable;
            }
        }
    }
}
