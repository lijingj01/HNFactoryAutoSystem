using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace HNFactoryAutoSystem.Data
{

    /// <summary>
    /// 数据实体转换类
    /// </summary>
    public static class EntityHelper
    {

        /// <summary>
        /// 判断DataSet默认表是否为空:true:不为空 false:为空。
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static bool CheckDataSet(DataSet ds)
        {
            bool isNull = CheckDataSet(ds, 0);
            return isNull;
        }

        /// <summary>
        /// 判断DataSet指定索引表是否为空:true:不为空 false:为空。
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <param name="tableIndex">表的索引值</param>
        /// <returns></returns>
        public static bool CheckDataSet(DataSet ds, int tableIndex)
        {
            bool isNull = false;
            if (ds != null && ds.Tables != null && ds.Tables.Count > tableIndex && ds.Tables[tableIndex] != null && ds.Tables[tableIndex].Rows != null && ds.Tables[tableIndex].Rows.Count > 0)
            {
                isNull = true;
            }
            return isNull;
        }

        /// <summary>
        /// 根据数据表生成相应的实体对象列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="srcDT">数据</param>
        /// <param name="relation">数据库表列名与对象属性名对应关系；如果列名与实体对象属性名相同，该参数可为空</param>
        /// <returns>对象列表</returns>
        public static List<T> GetEntityListByDT<T>(DataTable srcDT, Hashtable relation)
        {
            List<T> list = null;
            T destObj = default(T);
            if (srcDT != null && srcDT.Rows.Count > 0)
            {
                list = new List<T>();
                foreach (DataRow row in srcDT.Rows)
                {
                    destObj = GetEntityListByDT<T>(row, relation);
                    list.Add(destObj);
                }
            }
            return list;
        }

        /// <summary>
        ///  将SqlDataReader转换成数据实体 add by trenhui
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="relation"></param>
        /// <returns></returns>
        public static T GetEntityListByDT<T>(SqlDataReader dr)
        {
            Type type = typeof(T);
            T destObj = Activator.CreateInstance<T>();
            foreach (PropertyInfo prop in type.GetProperties())
            {
                try
                {
                    if (dr[prop.Name] != null && dr[prop.Name] != DBNull.Value)
                    {
                        SetPropertyValue(prop, destObj, dr[prop.Name]);
                    }
                }
                catch { }
            }
            return destObj;
        }

        /// <summary>
        ///  将SqlDataReader转换成数据实体 add by trenhui
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="relation"></param>
        /// <returns></returns>
        public static T GetEntityListByDT<T>(MySqlDataReader dr)
        {
            Type type = typeof(T);
            T destObj = Activator.CreateInstance<T>();
            foreach (PropertyInfo prop in type.GetProperties())
            {
                try
                {
                    if (dr[prop.Name] != null && dr[prop.Name] != DBNull.Value)
                    {
                        SetPropertyValue(prop, destObj, dr[prop.Name]);
                    }
                }
                catch { }
            }
            return destObj;
        }


        /// <summary>
        ///  将数据行转换成数据实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static T GetEntityListByDT<T>(DataSet ds)
        {
            Type type = typeof(T);
            T destObj = Activator.CreateInstance<T>();
            try
            {
                DataRow row = ds.Tables[0].Rows[0];
                foreach (PropertyInfo prop in type.GetProperties())
                {
                    if (row.Table.Columns.Contains(prop.Name) &&
                        row[prop.Name] != DBNull.Value)
                    {
                        SetPropertyValue(prop, destObj, row[prop.Name]);
                    }
                }
            }
            catch (Exception ex)
            {
             }
            return destObj;
        }



        /// <summary>
        ///  将数据行转换成数据实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row">单行Datarow</param>
        /// <param name="relation">需要实体转换的Hashtable 可为null</param>
        /// <returns>返回一个泛型</returns>
        public static T GetEntityListByDT<T>(DataRow row, Hashtable relation)
        {
            Type type = typeof(T);
            T destObj = Activator.CreateInstance<T>();
            PropertyInfo temp = null;
            foreach (PropertyInfo prop in type.GetProperties())
            {
                if (row.Table.Columns.Contains(prop.Name) &&
                    row[prop.Name] != DBNull.Value)
                {
                    SetPropertyValue(prop, destObj, row[prop.Name]);
                }
            }
            if (relation != null)
            {
                foreach (string name in relation.Keys)
                {
                    temp = type.GetProperty(relation[name].ToString());
                    if (temp != null &&
                        row[name] != DBNull.Value)
                    {
                        SetPropertyValue(temp, destObj, row[name]);
                    }
                }
            }
            return destObj;
        }



        /// <summary>
        ///  将多数据行转换成数据实体列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="row">行数组</param>
        /// <param name="relation">需要实体转换的Hashtable 可为null</param>
        /// <returns>返回List泛型数组</returns>
        public static List<T> GetEntityListByDT<T>(DataRow[] rows, Hashtable relation)
        {
            List<T> list = null;
            T destObj = default(T);
            if (rows != null && rows.Length > 0)
            {
                list = new List<T>();
                foreach (DataRow row in rows)
                {
                    destObj = GetEntityListByDT<T>(row, relation);
                    list.Add(destObj);
                }
            }
            return list;
        }



        /// <summary>
        /// 为对象的属性赋值
        /// </summary>
        /// <param name="prop">属性</param>
        /// <param name="destObj">目标对象</param>
        /// <param name="value">源值</param>
        private static void SetPropertyValue(PropertyInfo prop, object destObj, object value)
        {
            object temp = ChangeType(prop.PropertyType, value);
            prop.SetValue(destObj, temp, null);
        }



        /// <summary>
        /// 用于类型数据的赋值
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <param name="value">原值</param>
        /// <returns></returns>
        private static object ChangeType(Type type, object value)
        {
            int temp = 0;
            if ((value == null) && type.IsGenericType)
            {
                return Activator.CreateInstance(type);
            }
            if (value == null)
            {
                return null;
            }
            if (type == value.GetType())
            {
                return value;
            }
            if (type.IsEnum)
            {
                if (value is string)
                {
                    return Enum.Parse(type, value as string);
                }
                return Enum.ToObject(type, value);
            }
            if (type == typeof(bool) && typeof(int).IsInstanceOfType(value))
            {
                temp = int.Parse(value.ToString());
                return temp != 0;
            }
            if (!type.IsInterface && type.IsGenericType)
            {
                Type type1 = type.GetGenericArguments()[0];
                object obj1 = ChangeType(type1, value);
                return Activator.CreateInstance(type, new object[] { obj1 });
            }
            if ((value is string) && (type == typeof(Guid)))
            {
                return new Guid(value as string);
            }
            if ((value is string) && (type == typeof(Version)))
            {
                return new Version(value as string);
            }
            if (!(value is IConvertible))
            {
                return value;
            }
            return Convert.ChangeType(value, type);
        }

    }
}
