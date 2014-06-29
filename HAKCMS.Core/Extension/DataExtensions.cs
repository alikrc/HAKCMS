using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HAKCMS.Core.Extension
{
    public static class DataExtensions
    {
        public static IEnumerable<T> ExtractDeletedRecords<T>(this IEnumerable<T> list) where T : class
        {
            var deletedItems = new List<T>();
            foreach (var item in list)
            {
                var prop = item.GetType().GetProperty("IsDeleted");
                bool isDeleted = prop != null ? (bool)prop.GetValue(item) : false;

                if (isDeleted)
                {
                    deletedItems.Add(item);
                }

            }


            list = list.Except(deletedItems).ToList();

            return list;
        }

        //returns null if IsDeleted column is true
        public static bool IsDeletedRecord<T>(this IDbSet<T> entity) where T : class
        {
            var prop = entity.GetType().GetProperty("IsDeleted");
            bool isDeleted = prop != null ? (bool)prop.GetValue(entity) : false;

            if (isDeleted)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// TODO
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public static void Delete<T>(this IDbSet<T> entity) where T : class
        {
            var prop = entity.GetType().GetProperty("IsDeleted");

            prop.SetValue(entity, true);

        }
    }
}
