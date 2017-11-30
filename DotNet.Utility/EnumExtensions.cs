using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Utility
{
    /// <summary>
    /// 枚举扩展类
    /// </summary>
    public static class EnumExtensions
    {
        public static int GetEnumValue(this object value)
        {
            return (int)value;
        }

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        public static string GetDescription(this Enum value)
        {
            if (value == null)
                return string.Empty;
            return GetDescriptionForEnum(value);
        }

        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescriptionForEnum(this object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()) || value.ToString() == "0") return string.Empty;
            var type = value.GetType();
            var field = type.GetField(Enum.GetName(type, value));
            if (field != null)
            {
                var des = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (des != null)
                {
                    return des.Description;
                }
            }
            return value.ToString();
        }

        /// <summary>
        /// Enum装datatable
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(Type enumType, string key, string val)
        {
            string[] Names = Enum.GetNames(enumType);

            Array Values = Enum.GetValues(enumType);

            DataTable table = new DataTable();
            var colType = typeof(string);
            table.Columns.Add(key, colType);
            table.Columns.Add(val, colType);
            table.Columns[key].Unique = true;
            for (int i = 0; i < Values.Length; i++)
            {
                var enumInfo = enumType.GetField(Names[i]);
                var enumAttributes = (DescriptionAttribute[])enumInfo.
                    GetCustomAttributes(typeof(DescriptionAttribute), false);

                DataRow DR = table.NewRow();
                DR[key] = enumAttributes.Length > 0 ? enumAttributes[0].Description : Names[i];
                try
                {
                    DR[val] = (int)Values.GetValue(i);
                }
                catch (Exception)
                {
                    DR[val] = (byte)Values.GetValue(i);
                }

                table.Rows.Add(DR);
            }
            return table;
        }
        /// <summary>
        /// 通过Flags枚举的值values获取对应的单独枚举集合
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static IEnumerable<int> GetValueList(this Enum values)
        {
            var sourceValues = (int)(object)values;

            return Enum.GetValues(values.GetType())
                .Cast<int>()
                .Where(value => value > 0)
                .Where(value => (value & sourceValues) == value);
        }
        public static IList<string> GetValueListString(this Enum values)
        {
            return values.GetValueList().Select(x => x.ToString()).ToList();
        }

        //public static IList<SelectOption> GetValueListOption(this Enum values)
        //{
        //    var sourceValues = (int)(object)values;

        //    var valueQuery = from value in Enum.GetValues(values.GetType()).Cast<object>()
        //                     let intValue = (int)value
        //                     where intValue > 0 && ((intValue & sourceValues) == intValue)
        //                     select new SelectOption()
        //                     {
        //                         Value = intValue.ToString(),
        //                         Text = value.GetDescriptionForEnum()
        //                     };

        //    return valueQuery.ToList();
        //}

        //public static int OptionListToFlagEnum(this IList<SelectOption> valueList)
        //{
        //    if (valueList == null || valueList.Count <= 0)
        //        return 0;

        //    return valueList.Select(x => x.Value).Select(int.Parse).Sum();
        //}

        public static int ValueListToFlagEnum(this IList<string> valueList)
        {
            if (valueList == null || valueList.Count <= 0)
                return 0;

            return valueList.Select(int.Parse).Sum();
        }

        /// <summary>
        /// 根据Description获取枚举
        /// 说明：
        /// 单元测试-->通过
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="description">枚举描述</param>
        /// <returns>枚举</returns>
        public static T GetEnumName<T>(string description)
        {
            Type _type = typeof(T);
            foreach (FieldInfo field in _type.GetFields())
            {

                if (field == null)
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }

                var _curDesc = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (_curDesc != null && _curDesc.Length > 0)
                {
                    if (_curDesc[0].Description == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }
            throw new ArgumentException(string.Format("{0} 未能找到对应的枚举.", description), "Description");
        }

        /// <summary>
        /// 将枚举转换为ArrayList
        /// 说明：
        /// 若不是枚举类型，则返回NULL
        /// 单元测试-->通过
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <returns>ArrayList</returns>
        public static IList<KeyValuePair<Enum, string>> ToArrayList(this Type type)
        {
            if (type.IsEnum)
            {
                var _array = new List<KeyValuePair<Enum, string>>();
                Array _enumValues = Enum.GetValues(type);
                foreach (Enum value in _enumValues)
                {
                    _array.Add(new KeyValuePair<Enum, string>(value, GetDescription(value)));
                }
                return _array;
            }
            return null;
        }

    }
}
