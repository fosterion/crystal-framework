using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalFramework.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Convert to table. Version 1
        /// </summary>
        /// <typeparam name="T">Datatype</typeparam>
        /// <param name="source">Collection</param>
        /// <returns></returns>
        public static DataTable ToDataTable1<T>(this IList<T> source)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();

            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            object[] values = new object[props.Count];

            foreach (T item in source)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }

                table.Rows.Add(values);
            }

            return table;
        }

        /// <summary>
        /// Convert to table. Version 2
        /// </summary>
        /// <typeparam name="T">Datatype</typeparam>
        /// <param name="source">Collection</param>
        /// <returns></returns>
        public static DataTable ToDataTable2<T>(this IList<T> source)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in source)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }

            return table;
        }

        /// <summary>
        /// Unique values by the specified field
        /// </summary>
        /// <typeparam name="TItem">Datatype</typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source">Source collection</param>
        /// <param name="selector">Predicate</param>
        /// <returns></returns>
        public static IEnumerable<TItem> DistinctBy<TItem, TValue>(this IEnumerable<TItem> source, Func<TItem, TValue> selector)
        {
            var uniques = new HashSet<TValue>();

            foreach (var item in source)
            {
                if (uniques.Add(selector(item))) yield return item;
            }
        }

        /// <summary>
        /// Convert to SQL-typed table
        /// </summary>
        /// <typeparam name="T">Datatype</typeparam>
        /// <param name="source">Collection</param>
        public static DataTable ToSqlTable<T>(this IEnumerable<T> source)
        {
            var table = new DataTable();
            table.Columns.Add(new DataColumn("Value", typeof(T)));

            foreach (T item in source)
            {
                var row = table.NewRow();
                row.ItemArray = new object[] { item };
                table.Rows.Add(row);
            }

            return table;
        }

        /// <summary>
        /// Clone collection
        /// </summary>
        /// <typeparam name="T">Datatype</typeparam>
        /// <param name="source">Collection</param>
        /// <returns></returns>
        public static IEnumerable<T> Clone<T>(this IEnumerable<T> source) where T : ICloneable
        {
            var list = source.Select(item => (T)item.Clone()).ToList();
            var result = new List<T>();

            foreach (var item in list)
                result.Add(item);

            return result;
        }
    }
}
