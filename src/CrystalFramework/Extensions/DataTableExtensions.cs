using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CrystalFramework.Extensions
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// Convert DataTable to List
        /// </summary>
        /// <typeparam name="T">List datatype</typeparam>
        /// <param name="source">Source table</param>
        public static List<T> ToList<T>(this DataTable source) where T : new()
        {
            List<T> list = new List<T>();

            var typeProperties = typeof(T).GetProperties().Select(propertyInfo => new
            {
                PropertyInfo = propertyInfo,
                Type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType
            }).ToList();

            foreach (var row in source.Rows.Cast<DataRow>())
            {
                T obj = new T();

                foreach (var typeProperty in typeProperties)
                {
                    object value = row[typeProperty.PropertyInfo.Name];

                    object safeValue = value == null || DBNull.Value.Equals(value)
                        ? null
                        : Convert.ChangeType(value, typeProperty.Type);

                    typeProperty.PropertyInfo.SetValue(obj, safeValue, null);
                }

                list.Add(obj);
            }

            return list;
        }

        /// <summary>
        /// Gently rename column if it exists
        /// </summary>
        /// <param name="source">Таблица</param>
        /// <param name="oldName">Current column name</param>
        /// <param name="newName">New column name</param>
        public static DataTable RenameColumn(this DataTable source, string oldName, string newName)
        {
            if (source.Columns.Contains(oldName))
                source.Columns[oldName].ColumnName = newName;

            return source;
        }

        /// <summary>
        /// Gently remove column if it exists
        /// </summary>
        /// <param name="source">Source table</param>
        /// <param name="columnName">Column name for removing</param>
        public static DataTable RemoveColumn(this DataTable source, string columnName)
        {
            if (source.Columns.Contains(columnName))
                source.Columns.Remove(columnName);

            return source;
        }
    }
}
