﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Business.SQL
{
    public static class DataTableConvert
    {
        public static int TotalRecords(this DataTable table, string columnName)
        {
            DataColumnCollection columns = table.Columns;

            if (columns.Contains(columnName))
            {
                if (table.Rows.Count > 0)
                {
                    return Convert.ToInt32(table.Rows[0][columnName]);
                }
            }
            else
            {
                return table.Rows.Count;
            }
            return 0;

        }
        public static PagedDataTable<T> ToPagedDataTableList<T>(this IEnumerable<T> queryOver)
        {
            var dataList = new PagedDataTable<T>();
            dataList.AddRange(queryOver);
            return dataList;
        }
        public static PagedDataTable<T> ToPagedDataTableList<T>(this IEnumerable<T> queryOver, int PageNo, int PageSize, int Total, string SearchText)
        {
            var dataList = new PagedDataTable<T>(PageNo, PageSize, Total, SearchText);
            dataList.AddRange(queryOver);
            return dataList;
        }
        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        public static List<T> ToList<T>(this DataTable dataTable) where T : new()
        {
            var dataList = new List<T>();

            //Define what attributes to be read from the class
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            //Read Attribute Names and Types
            var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
                Select(item => new
                {
                    Name = item.Name,
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();

            //Read Datatable column names and types
            var dtlFieldNames = dataTable.Columns.Cast<DataColumn>().
                Select(item => new
                {
                    Name = item.ColumnName,
                    Type = item.DataType
                }).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var classObj = new T();

                foreach (var dtField in dtlFieldNames)
                {
                    PropertyInfo propertyInfos = classObj.GetType().GetProperty(dtField.Name);

                    var field = objFieldNames.Find(x => x.Name == dtField.Name);

                    if (field != null)
                    {

                        if (propertyInfos.PropertyType == typeof(DateTime))
                        {
                            propertyInfos.SetValue
                            (classObj, convertToDateTime(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(int))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToInt(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(long))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToLong(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(decimal))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToDecimal(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(String))
                        {
                            if (dataRow[dtField.Name].GetType() == typeof(DateTime))
                            {
                                propertyInfos.SetValue
                                (classObj, convertToDateTimeString(dataRow[dtField.Name]), null);
                            }
                            else
                            {
                                propertyInfos.SetValue
                                (classObj, ConvertToString(dataRow[dtField.Name]), null);
                            }
                        }
                        else
                        {
                            if (DBNull.Value != dataRow[dtField.Name])
                                propertyInfos.SetValue
                                   (classObj, dataRow[dtField.Name], null);
                        }
                    }
                }
                dataList.Add(classObj);
            }
            return dataList;
        }

        public static PagedDataTable<T> ToPagedDataTableList<T>(this DataTable dataTable) where T : new()
        {
            var dataList = new PagedDataTable<T>();

            //Define what attributes to be read from the class
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            //Read Attribute Names and Types
            var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
                Select(item => new
                {
                    Name = item.Name,
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();

            //Read Datatable column names and types
            var dtlFieldNames = dataTable.Columns.Cast<DataColumn>().
                Select(item => new
                {
                    Name = item.ColumnName,
                    Type = item.DataType
                }).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var classObj = new T();

                foreach (var dtField in dtlFieldNames)
                {
                    PropertyInfo propertyInfos = classObj.GetType().GetProperty(dtField.Name);

                    var field = objFieldNames.Find(x => x.Name == dtField.Name);
                    if (field != null)
                    {

                        if (propertyInfos.PropertyType == typeof(DateTime))
                        {
                            propertyInfos.SetValue
                            (classObj, convertToDateTime(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(int))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToInt(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(uint))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToInt(dataRow[dtField.Name]), null);
                        }

                        else if (propertyInfos.PropertyType == typeof(long))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToLong(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(decimal))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToDecimal(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(Boolean))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToBoolean(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(String))
                        {
                            if (dataRow[dtField.Name].GetType() == typeof(DateTime))
                            {
                                propertyInfos.SetValue
                                (classObj, convertToDateTimeString(dataRow[dtField.Name]), null);
                            }
                            else
                            {
                                propertyInfos.SetValue
                                (classObj, ConvertToString(dataRow[dtField.Name]), null);
                            }
                        }
                        else
                        {
                            if (DBNull.Value != dataRow[dtField.Name])
                                propertyInfos.SetValue
                                   (classObj, dataRow[dtField.Name], null);
                        }
                    }
                }
                dataList.Add(classObj);
            }
            return dataList;
        }

        public static PagedDataTable<T> ToPagedDataTableList<T>(this DataTable dataTable, int PageNo, int PageSize, int Total) where T : new()
        {
            var dataList = new PagedDataTable<T>(PageNo, PageSize, Total);

            //Define what attributes to be read from the class
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            //Read Attribute Names and Types
            var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
                Select(item => new
                {
                    Name = item.Name,
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();

            //Read Datatable column names and types
            var dtlFieldNames = dataTable.Columns.Cast<DataColumn>().
                Select(item => new
                {
                    Name = item.ColumnName,
                    Type = item.DataType
                }).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var classObj = new T();

                foreach (var dtField in dtlFieldNames)
                {
                    PropertyInfo propertyInfos = classObj.GetType().GetProperty(dtField.Name);

                    var field = objFieldNames.Find(x => x.Name == dtField.Name);

                    if (field != null)
                    {
                        if (propertyInfos.PropertyType == typeof(DateTime))
                        {
                            propertyInfos.SetValue
                            (classObj, convertToDateTime(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(int))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToInt(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(uint))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToInt(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(long))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToLong(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(decimal))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToDecimal(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(Boolean))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToBoolean(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(String))
                        {
                            if (dataRow[dtField.Name].GetType() == typeof(DateTime))
                            {
                                propertyInfos.SetValue
                                (classObj, convertToDateTimeString(dataRow[dtField.Name]), null);
                            }
                            else
                            {
                                propertyInfos.SetValue
                                (classObj, ConvertToString(dataRow[dtField.Name]), null);
                            }
                        }
                        else
                        {
                            if (DBNull.Value != dataRow[dtField.Name])
                                propertyInfos.SetValue
                                   (classObj, dataRow[dtField.Name], null);
                        }
                    }
                }
                dataList.Add(classObj);
            }
            return dataList;
        }

        public static PagedDataTable<T> ToPagedDataTableList<T>(this DataTable dataTable, int PageNo, int PageSize, int Total, string SearchText) where T : new()
        {
            var dataList = new PagedDataTable<T>(PageNo, PageSize, Total, SearchText);

            //Define what attributes to be read from the class
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            //Read Attribute Names and Types
            var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
                Select(item => new
                {
                    Name = item.Name,
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();

            //Read Datatable column names and types
            var dtlFieldNames = dataTable.Columns.Cast<DataColumn>().
                Select(item => new
                {
                    Name = item.ColumnName,
                    Type = item.DataType
                }).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var classObj = new T();

                foreach (var dtField in dtlFieldNames)
                {
                    PropertyInfo propertyInfos = classObj.GetType().GetProperty(dtField.Name);

                    var field = objFieldNames.Find(x => x.Name == dtField.Name);

                    if (field != null)
                    {

                        if (propertyInfos.PropertyType == typeof(DateTime))
                        {
                            propertyInfos.SetValue
                            (classObj, convertToDateTime(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(int))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToInt(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(uint))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToInt(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(long))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToLong(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(decimal))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToDecimal(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(Boolean))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToBoolean(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(String))
                        {
                            if (dataRow[dtField.Name].GetType() == typeof(DateTime))
                            {
                                propertyInfos.SetValue
                                (classObj, convertToDateTimeString(dataRow[dtField.Name]), null);
                            }
                            else
                            {
                                propertyInfos.SetValue
                                (classObj, ConvertToString(dataRow[dtField.Name]), null);
                            }
                        }
                        else
                        {
                            if (DBNull.Value != dataRow[dtField.Name])
                                propertyInfos.SetValue
                                   (classObj, dataRow[dtField.Name], null);
                        }
                    }
                }
                dataList.Add(classObj);
            }
            return dataList;
        }

        public static PagedDataTable<T> ToPagedDataTableList<T>(this DataTable dataTable, int PageNo, int PageSize, int Total, string SearchText, string orderBy, string sortBy) where T : new()
        {
            var dataList = new PagedDataTable<T>(PageNo, PageSize, Total, SearchText, orderBy, sortBy);

            //Define what attributes to be read from the class
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            //Read Attribute Names and Types
            var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
                Select(item => new
                {
                    Name = item.Name,
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();

            //Read Datatable column names and types
            var dtlFieldNames = dataTable.Columns.Cast<DataColumn>().
                Select(item => new
                {
                    Name = item.ColumnName,
                    Type = item.DataType
                }).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var classObj = new T();

                foreach (var dtField in dtlFieldNames)
                {
                    PropertyInfo propertyInfos = classObj.GetType().GetProperty(dtField.Name);

                    var field = objFieldNames.Find(x => x.Name == dtField.Name);

                    if (field != null)
                    {

                        if (propertyInfos.PropertyType == typeof(DateTime))
                        {
                            propertyInfos.SetValue
                            (classObj, convertToDateTime(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(int))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToInt(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(uint))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToInt(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(long))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToLong(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(decimal))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToDecimal(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(String))
                        {
                            if (dataRow[dtField.Name].GetType() == typeof(DateTime))
                            {
                                propertyInfos.SetValue
                                (classObj, convertToDateTimeString(dataRow[dtField.Name]), null);
                            }
                            else
                            {
                                propertyInfos.SetValue
                                (classObj, ConvertToString(dataRow[dtField.Name]), null);
                            }
                        }
                        else
                        {
                            if (DBNull.Value != dataRow[dtField.Name])
                                propertyInfos.SetValue
                                   (classObj, dataRow[dtField.Name], null);
                        }
                    }
                }
                dataList.Add(classObj);
            }
            return dataList;
        }
        public static PagedDataTable<T> ToPagedDataTableList<T>(this DataTable dataTable, int PageNo, int PageSize, int Total, string SearchText, string orderBy, string sortBy, string fromDate, string toDate) where T : new()
        {
            var dataList = new PagedDataTable<T>(PageNo, PageSize, Total, SearchText, orderBy, sortBy, fromDate, toDate);

            //Define what attributes to be read from the class
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            //Read Attribute Names and Types
            var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
                Select(item => new
                {
                    Name = item.Name,
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();

            //Read Datatable column names and types
            var dtlFieldNames = dataTable.Columns.Cast<DataColumn>().
                Select(item => new
                {
                    Name = item.ColumnName,
                    Type = item.DataType
                }).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var classObj = new T();

                foreach (var dtField in dtlFieldNames)
                {
                    PropertyInfo propertyInfos = classObj.GetType().GetProperty(dtField.Name);

                    var field = objFieldNames.Find(x => x.Name == dtField.Name);

                    if (field != null)
                    {

                        if (propertyInfos.PropertyType == typeof(DateTime))
                        {
                            propertyInfos.SetValue
                            (classObj, convertToDateTime(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(int))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToInt(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(uint))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToInt(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(long))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToLong(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(decimal))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToDecimal(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(String))
                        {
                            if (dataRow[dtField.Name].GetType() == typeof(DateTime))
                            {
                                propertyInfos.SetValue
                                (classObj, convertToDateTimeString(dataRow[dtField.Name]), null);
                            }
                            else
                            {
                                propertyInfos.SetValue
                                (classObj, ConvertToString(dataRow[dtField.Name]), null);
                            }
                        }
                        else
                        {
                            if (DBNull.Value != dataRow[dtField.Name])
                                propertyInfos.SetValue
                                   (classObj, dataRow[dtField.Name], null);
                        }
                    }
                }
                dataList.Add(classObj);
            }
            return dataList;
        }

        public static T ToPagedDataTableList<T>(this DataRow tableRow) where T : new()
        {
            // Create a new type of the entity I want
            Type t = typeof(T);
            T classObj = new T();


            foreach (DataColumn col in tableRow.Table.Columns)
            {
                string colName = col.ColumnName;

                // Look for the object's property with the columns name, ignore case
                PropertyInfo propertyInfos = classObj.GetType().GetProperty(colName);

                // did we find the property ?
                if (propertyInfos != null)
                {
                    object val = tableRow[colName];

                    if (propertyInfos.PropertyType == typeof(DateTime))
                    {
                        propertyInfos.SetValue
                        (classObj, convertToDateTime(val), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(int))
                    {
                        propertyInfos.SetValue
                        (classObj, ConvertToInt(val), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(uint))
                    {
                        propertyInfos.SetValue
                        (classObj, ConvertToInt(val), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(long))
                    {
                        propertyInfos.SetValue
                        (classObj, ConvertToLong(val), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(decimal))
                    {
                        propertyInfos.SetValue
                        (classObj, ConvertToDecimal(val), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(Boolean))
                    {
                        propertyInfos.SetValue
                        (classObj, ConvertToBoolean(val), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(String))
                    {

                        if (tableRow[colName].GetType() == typeof(DateTime))
                        {
                            propertyInfos.SetValue
                            (classObj, convertToDateTimeString(tableRow[colName]), null);
                        }
                        else
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToString(tableRow[colName]), null);
                        }
                    }
                    else
                    {
                        if (DBNull.Value != val)
                            propertyInfos.SetValue
                               (classObj, val, null);
                    }
                }
            }
            return classObj;
        }

        public static T ToFromTableList<T>(this DataTable table) where T : new()
        {
            // Create a new type of the entity I want
            Type t = typeof(T);
            T classObj = new T();


            foreach (DataRow dataRow in table.AsEnumerable().ToList())
            {
                string colName = dataRow["KeyText"].ToString();

                // Look for the object's property with the columns name, ignore case
                PropertyInfo propertyInfos = classObj.GetType().GetProperty(colName);

                // did we find the property ?
                if (propertyInfos != null)
                {
                    object val = dataRow["KeyValue"];
                    if (propertyInfos.PropertyType == typeof(Nullable<int>))
                    {
                        propertyInfos.SetValue
                      (classObj, ConvertToIntNullable(val), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(DateTime))
                    {
                        propertyInfos.SetValue
                        (classObj, convertToDateTime(val), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(int))
                    {
                        propertyInfos.SetValue
                        (classObj, ConvertToInt(val), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(uint))
                    {
                        propertyInfos.SetValue
                        (classObj, ConvertToInt(val), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(long))
                    {
                        propertyInfos.SetValue
                        (classObj, ConvertToLong(val), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(decimal))
                    {
                        propertyInfos.SetValue
                        (classObj, ConvertToDecimal(val), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(Boolean))
                    {
                        propertyInfos.SetValue
                        (classObj, ConvertToBoolean(val), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(String))
                    {
                        propertyInfos.SetValue
                        (classObj, ConvertToString(val), null);

                    }
                    else
                    {
                        if (DBNull.Value != val)
                            propertyInfos.SetValue
                               (classObj, val, null);
                    }
                }
            }
            return classObj;
        }
        public static DataTable GenerateTransposedTable(this DataTable inputTable)
        {
            DataTable outputTable = new DataTable();

            // Add columns by looping rows

            // Header row's first column is same as in inputTable
            outputTable.Columns.Add(inputTable.Columns[0].ColumnName.ToString());

            // Header row's second column onwards, 'inputTable's first column taken
            foreach (DataRow inRow in inputTable.Rows)
            {
                string newColName = inRow[0].ToString();
                outputTable.Columns.Add(newColName);
            }

            // Add rows by looping columns        
            for (int rCount = 1; rCount <= inputTable.Columns.Count - 1; rCount++)
            {
                DataRow newRow = outputTable.NewRow();

                // First column is inputTable's Header row's second column
                newRow[0] = inputTable.Columns[rCount].ColumnName.ToString();
                for (int cCount = 0; cCount <= inputTable.Rows.Count - 1; cCount++)
                {
                    string colValue = inputTable.Rows[cCount][rCount].ToString();
                    newRow[cCount + 1] = colValue;
                }
                outputTable.Rows.Add(newRow);
            }

            return outputTable;
        }
        public static DataTable GenerateTransposedTable(this DataTable inputTable, bool Sequenace)
        {
            DataTable outputTable = new DataTable();

            // Add columns by looping rows

            // Header row's first column is same as in inputTable
            outputTable.Columns.Add(inputTable.Columns[0].ColumnName.ToString());

            // Header row's second column onwards, 'inputTable's first column taken
            foreach (DataRow inRow in inputTable.Rows)
            {
                string newColName = inRow[0].ToString();
                outputTable.Columns.Add(newColName);
            }

            // Add rows by looping columns        
            for (int rCount = 1; rCount <= inputTable.Columns.Count - 1; rCount++)
            {
                DataRow newRow = outputTable.NewRow();

                // First column is inputTable's Header row's second column
                newRow[0] = inputTable.Columns[rCount].ColumnName.ToString();
                for (int cCount = 0; cCount <= inputTable.Rows.Count - 1; cCount++)
                {
                    string colValue = inputTable.Rows[cCount][rCount].ToString();
                    newRow[cCount + 1] = colValue;
                }
                outputTable.Rows.Add(newRow);
            }
            DataTable ResultTable = new DataTable();
            if (Sequenace)
            {
                DataColumn AutoNumberColumn = new DataColumn();

                AutoNumberColumn.ColumnName = "RowNum";

                AutoNumberColumn.DataType = typeof(int);

                AutoNumberColumn.AutoIncrement = true;

                AutoNumberColumn.AutoIncrementSeed = 1;

                AutoNumberColumn.AutoIncrementStep = 1;

                ResultTable.Columns.Add(AutoNumberColumn);

                ResultTable.Merge(outputTable);
            }
            return ResultTable;
            // return outputTable;
        }

        public static DataTable ToDataTable<T>(this PagedDataTable<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public static DataTable SelectRows(this DataTable dt, string whereExpression, string orderByExpression)
        {
            dt.DefaultView.RowFilter = whereExpression;
            dt.DefaultView.Sort = orderByExpression;
            return dt.DefaultView.ToTable();
        }

        public static DataTable MergeRows(this DataTable tblIn, string WhereColumnName, string ValueColumnName, string UpdateColumnName, int Value, decimal totalSaleAmount)
        {

          //  DataTable tblTender = dtTender.MergeRows("TenderTypeID", "Amount", "TenderAmount", tenderTypeID, transInfo.TotalSaleAmount);

            DataTable dtResult = tblIn.SelectRows(string.Format("{0}<>{1}", WhereColumnName, Value), WhereColumnName);
            DataTable dtcompare = tblIn.SelectRows(string.Format("{0}={1}", WhereColumnName, Value), WhereColumnName);
            decimal totalCredit = 0;
            if (dtResult?.Rows?.Count > 0)
            {
                totalCredit = dtResult.AsEnumerable().Sum(row => ConvertToDecimal(row.Field<string>(ValueColumnName)));
            }

            if (dtcompare?.Rows?.Count > 0)
            {
                decimal total = tblIn.AsEnumerable().Sum(row => ConvertToDecimal(row.Field<string>(ValueColumnName)));
                decimal totalCash = dtcompare.AsEnumerable().Sum(row => ConvertToDecimal(row.Field<string>(ValueColumnName)));

                DataRow dr = dtcompare.Rows[0];
                dr[ValueColumnName] = totalCash;
                dr[UpdateColumnName] = totalSaleAmount - totalCredit;
                dtResult.ImportRow(dr);
            }
            return dtResult;
        }
        public static DataTable MergeRowRewards(this DataTable tblIn, string WhereColumnName, string ValueColumnName, string UpdateColumnName, int Value, decimal totalSaleAmount)
        {

            //  DataTable tblTender = dtTender.MergeRows("TenderTypeID", "Amount", "TenderAmount", tenderTypeID, transInfo.TotalSaleAmount);

            DataTable dtResult = tblIn.SelectRows(string.Format("{0}<>{1}", WhereColumnName, Value), WhereColumnName);
            DataTable dtcompare = tblIn.SelectRows(string.Format("{0}={1}", WhereColumnName, Value), WhereColumnName);
            decimal totalCredit = 0;
            if (dtResult?.Rows?.Count > 0)
            {
                totalCredit = dtResult.AsEnumerable().Sum(row => ConvertToDecimal(row.Field<string>(ValueColumnName)));
            }

            if (dtcompare?.Rows?.Count > 0)
            {
                decimal total = tblIn.AsEnumerable().Sum(row => ConvertToDecimal(row.Field<string>(ValueColumnName)));
                decimal totalCash = dtcompare.AsEnumerable().Sum(row => ConvertToDecimal(row.Field<string>(ValueColumnName)));

                DataRow dr = dtcompare.Rows[0];
                dr[ValueColumnName] = totalCash;
                dr[UpdateColumnName] = totalCash;
                dtResult.ImportRow(dr);
            }
            return dtResult;
        }
        private static string ConvertToString(object value)
        {
            return Convert.ToString(HelperFunctions.ReturnEmptyIfNull(value));
        }
        private static int ConvertToInt(object value)
        {
            return Convert.ToInt32(HelperFunctions.ReturnZeroIfNull(value));
        }
        private static Nullable<int> ConvertToIntNullable(object value)
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
        private static long ConvertToLong(object value)
        {
            return Convert.ToInt64(HelperFunctions.ReturnZeroIfNull(value));
        }
        private static decimal ConvertToDecimal(object value)
        {
            return Convert.ToDecimal(HelperFunctions.ReturnZeroIfNull(value));
        }
        private static bool ConvertToBoolean(object value)
        {
            return Convert.ToBoolean(HelperFunctions.ReturnFalseIfNull(value));
        }
        private static DateTime convertToDateTime(object date)
        {
            return Convert.ToDateTime(HelperFunctions.ReturnDateTimeMinIfNull(date));
        }
        private static string convertToDateTimeString(object date)
        {
            try
            {
                if (date == DBNull.Value)
                    return string.Empty;
                if (date == null)
                    return string.Empty;
                return Convert.ToDateTime(date).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

            }
            catch
            {
                return string.Empty;
            }


            //return Convert.ToDateTime(HelperFunctions.ReturnDateTimeMinIfNull(date));
        }

    }
}
