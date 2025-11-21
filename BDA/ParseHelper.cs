using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDA
{

    public class ParseHelper 
    {
        public T? GetFieldValue<T>(SqlDataReader reader, string columnName)
        {
            return reader.IsDBNull(reader.GetOrdinal(columnName)) ? default : reader.GetFieldValue<T>(reader.GetOrdinal(columnName));
        }

        public List<T> DataReaderMapToList<T>(IDataReader reader)
        {
            List<T> list = new List<T>();
            T obj = default(T);

            while (reader.Read())
            {
                obj = Activator.CreateInstance<T>();

                foreach (System.Reflection.PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (HasColumn(reader, prop.Name))
                    {
                        object value = reader[prop.Name];

                        if (!DBNull.Value.Equals(value))
                        {
                            if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                Type nullableType = Nullable.GetUnderlyingType(prop.PropertyType);
                                prop.SetValue(obj, Convert.ChangeType(value, nullableType), null);
                            }
                            else
                            {
                                prop.SetValue(obj, Convert.ChangeType(value, prop.PropertyType), null);
                            }
                        }
                    }
                }
                list.Add(obj);
            }
            return list;
        }
        public bool HasColumn(IDataReader dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
