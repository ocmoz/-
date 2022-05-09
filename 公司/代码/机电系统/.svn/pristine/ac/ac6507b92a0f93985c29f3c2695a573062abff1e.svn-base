using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using FM2E.Model.Exceptions;
using System.Web.UI.WebControls;

/// <summary>
///EnumString 的摘要说明
/// </summary>
public class EnumHelper
{
    public EnumHelper()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 获取枚举类型的自定义描述
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static string GetDescription(Enum item)
    {
        return FM2E.BLL.Utils.EnumHelper.GetDescription(item);
    }
    /// <summary>
    /// 从枚举类型中获取列表
    /// </summary>
    /// <param name="item"></param>
    /// <param name="filters">需要排除值为filters的枚举项</param>
    /// <returns></returns>
    public static ListItem[] GetListItems(Type type,params int[] filters)
    {
        ArrayList tmpList = new ArrayList();
        try
        {
            IList list = FM2E.BLL.Utils.EnumHelper.ToList<int>(type, filters);
            if (list == null || list.Count == 0)
                return null;
            foreach (KeyValuePair<int, string> item in list)
            {
                tmpList.Add(new ListItem(item.Value, item.Key.ToString()));
            }
        }
        catch (Exception ex)
        {
            tmpList.Clear();
            throw new WebException("获取枚举值列表失败", ex);
        }
        
       
        if (tmpList.Count == 0)
            return null;
        ListItem[] items = new ListItem[tmpList.Count];
        int i=0;
        foreach (ListItem item in tmpList)
        {
            items[i++] = item;
        }
        return items;
    }
    /// <summary>
    /// 从枚举类型中获取列表，并默认选择某项
    /// </summary>
    /// <param name="item"></param>
    /// <param name="selectValue">默认选择项的枚举值</param>
    /// <param name="filters">需要排除值为filters的枚举项</param>
    /// <returns></returns>
    public static ListItem[] GetListItemsEx(Type type,int selectValue, params int[] filters)
    {
        ArrayList tmpList = new ArrayList();
        try
        {
            IList list = FM2E.BLL.Utils.EnumHelper.ToList<int>(type, filters);
            if (list == null || list.Count == 0)
                return null;
            foreach (KeyValuePair<int, string> item in list)
            {
                ListItem tmp = new ListItem(item.Value, item.Key.ToString());
                if (item.Key == selectValue)
                   tmp.Selected = true;
                tmpList.Add(tmp);
            }
        }
        catch (Exception ex)
        {
            tmpList.Clear();
            throw new WebException("获取枚举值列表失败", ex);
        }


        if (tmpList.Count == 0)
            return null;
        ListItem[] items = new ListItem[tmpList.Count];
        int i = 0;
        foreach (ListItem item in tmpList)
        {
            items[i++] = item;
        }
        return items;
    }
    /// <summary>
    /// 从枚举类型中获取树形列表，并默认选择某项
    /// </summary>
    /// <param name="type"></param>
    /// <param name="selectValue">默认选择项的枚举值</param>
    /// <param name="filters">需要排除值为filters的枚举项</param>
    /// <returns></returns>
    public static ListItem[] GetListItemTree(Type type, int selectValue, params int[] filters)
    {
        ArrayList tmpList = new ArrayList();
        Hashtable leafTable = new Hashtable();
        ArrayList parentList = new ArrayList();
        try
        {
            IList list = FM2E.BLL.Utils.EnumHelper.ToList<int>(type, filters);
            if (list == null || list.Count == 0)
                return null;
            
            foreach (KeyValuePair<int, string> item in list)
            {
                if (IsRootItem(item.Key, list))
                {
                    parentList.Add(item);
                }
                else leafTable.Add(item.Key, item.Value);
            }

            foreach (KeyValuePair<int, string> item in parentList)
            {
                ListItem tmp = new ListItem(item.Value, item.Key.ToString());
                if (item.Key == selectValue)
                    tmp.Selected = true;
                tmpList.Add(tmp);

                int value = item.Key;
                int childValue = 1;
                while (value != 0)
                {
                    if ((value & 0x01) != 0)
                    {
                        //有子节点
                        if (leafTable.ContainsKey(childValue))
                        {
                            tmp = new ListItem("　" + (string)leafTable[childValue], childValue.ToString());
                            if (childValue == selectValue)
                                tmp.Selected = true;
                            tmpList.Add(tmp);
                        }
                    }
                    value >>= 1;
                    childValue <<= 1;
                }
            }
        }
        catch (Exception ex)
        {
            tmpList.Clear();
            throw new WebException("获取枚举值列表失败", ex);
        }


        if (tmpList.Count == 0)
            return null;
        ListItem[] items = new ListItem[tmpList.Count];
        int i = 0;
        foreach (ListItem item in tmpList)
        {
            items[i++] = item;
        }
        return items;
    }
    /// <summary>
    /// 检测某个item是否包含子item,通过检测key的二进制表示是否包含多于一个1来实现
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    private static bool IsRootItem(int key,IList list)
    {
        int tmpKey = key;
        tmpKey &= (tmpKey - 1);
        if (tmpKey != 0)
            return true;

        foreach (KeyValuePair<int, string> item in list)
        {
            if ((key & item.Key) != 0 && key != item.Key)
            {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// 检查是否需要排除此项
    /// </summary>
    /// <param name="key"></param>
    /// <param name="filters"></param>
    private static bool IsNeedToFilter(int key, int[] filters)
    {
        if (filters == null || filters.Length == 0)
            return false;

        for (int i = 0; i < filters.Length; i++)
        {
            if (key == filters[i])
                return true;
        }
        return false;
    }
}
