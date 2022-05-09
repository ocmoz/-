using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using FM2E.Model.Utils;
using System.Collections;
using System.Globalization;

namespace FM2E.BLL.Utils
{
    /// <summary>
    /// 枚举类型工具类(用于获取枚举类型值的自定义描述)
    /// </summary>
    public class EnumHelper
    {
        // maps用于保存每种枚举及其对应的EnumMap对象
        private static Dictionary<Type, EnumMap> maps;

        /// <summary>
        /// 获取枚举类型值的自定义描述
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string GetDescription(Enum item)
        {
            if (maps == null)
            {
                maps = new Dictionary<Type, EnumMap>();
            }

            Type enumType = item.GetType();

            EnumMap mapper = null;
            if (maps.ContainsKey(enumType))
            {
                mapper = maps[enumType];
            }
            else
            {
                mapper = new EnumMap(enumType);
                maps.Add(enumType, mapper);
            }
            return mapper[item];
        }
        /// <summary>
        /// 获取枚举值列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IList ToList<T>(Type type, params T[] filters)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (!type.IsEnum)
            {
                throw new ArgumentException("传入的类型必须为Enum类型", "type");
            }

            ArrayList list = new ArrayList();
            Array enumValues = Enum.GetValues(type);

            foreach (Enum value in enumValues)
            {
                T tmp = (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
                if (!IsNeedToFilter<T>(tmp, filters))
                    list.Add(new KeyValuePair<T, string>(tmp, GetDescription(value)));
            }
            return list;
        }

        /// <summary>
        /// 检查是否需要排除此项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="filters"></param>
        private static bool IsNeedToFilter<T>(T key, T[] filters)
        {
            if (filters == null || filters.Length == 0)
                return false;

            for (int i = 0; i < filters.Length; i++)
            {
                if (key.Equals(filters[i]))
                    return true;
            }
            return false;
        }

        private class EnumMap
        {
            private Type internalEnumType;
            private Dictionary<Enum, string> map;

            public EnumMap(Type enumType)
            {
                if (!enumType.IsSubclassOf(typeof(Enum)))
                {
                    throw new InvalidCastException();
                }
                internalEnumType = enumType;
                FieldInfo[] staticFiles = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);

                map = new Dictionary<Enum, string>(staticFiles.Length);

                for (int i = 0; i < staticFiles.Length; i++)
                {
                    if (staticFiles[i].FieldType == enumType)
                    {
                        string description = "";
                        object[] attrs = staticFiles[i].GetCustomAttributes(typeof(EnumDescriptionAttribute), true);
                        description = attrs.Length > 0 ?
                            ((EnumDescriptionAttribute)attrs[0]).Description :
                            //若没找到EnumItemDescription标记，则使用该枚举值的名字
                            description = staticFiles[i].Name;

                        map.Add((Enum)staticFiles[i].GetValue(enumType), description);
                    }
                }
            }

            public string this[Enum item]
            {
                get
                {
                    if (item.GetType() != internalEnumType)
                    {
                        throw new ArgumentException();
                    }
                    return map[item];
                }
            }
        }

    }


}
