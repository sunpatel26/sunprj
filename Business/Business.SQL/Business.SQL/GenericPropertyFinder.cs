using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Business.SQL
{
    public class GenericPropertyFinder<TModel> where TModel : class
    {
        public DataTable PrintTModelPropertyAndValue(TModel tmodelObj, List<string> execludes)
        {
            try
            {
                //Getting Type of Generic Class Model
                Type tModelType = tmodelObj.GetType();

                //We will be defining a PropertyInfo Object which contains details about the class property 
                PropertyInfo[] arrayPropertyInfos = tModelType.GetProperties();

                DataTable table = new DataTable();

                table.Columns.Add("KeyText", typeof(string));
                table.Columns.Add("KeyValue", typeof(string));

                //Now we will loop in all properties one by one to get value
                foreach (PropertyInfo property in arrayPropertyInfos)
                {
                    if (execludes.Contains(property.Name))
                        continue;
                    var s = property.GetValue(tmodelObj,null);
                    if (s == null)
                    {
                        table.Rows.Add(property.Name, string.Empty);
                    }
                    else
                    {
                        table.Rows.Add(property.Name, property.GetValue(tmodelObj,null).ToString());
                    }
                }
                //Console.WriteLine("Name of Property is\t:\t" + property.Name);
                //Console.WriteLine("Value of Property is\t:\t" + property.GetValue(tmodelObj).ToString());
                //Console.WriteLine(Environment.NewLine);
                return table;

            }
            catch 
            {
                throw;
            }
        }
    }
}
