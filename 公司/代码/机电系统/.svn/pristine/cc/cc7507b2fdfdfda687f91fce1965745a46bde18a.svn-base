using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using System.Web.UI.WebControls;
using System.Collections;
using FM2E.Model.Archives;
using FM2E.BLL.Archives;

/// <summary>
///ListItemHelper 的摘要说明
/// </summary>
public class ListItemHelper
{
    private static Department departmentBll = new Department();
    private static EquipmentSystem systemBll = new EquipmentSystem();
    private static Company companyBll = new Company();
    private static Category categoryBll = new Category();

    private ListItemHelper()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 获取部门下拉列表选项
    /// </summary>
    /// <param name="companyID"></param>
    /// <returns></returns>
    public static ListItem[] GetDepartmentListItems(string companyID)
    {
        DepartmentInfo dinfo = new DepartmentInfo();
        //dinfo.CompanyID = companyID;
        dinfo.Level = 1;//先获取第一重节点
        IList<DepartmentInfo> list = departmentBll.Search(dinfo);
        ArrayList itemArray = new ArrayList();
        Stack<DepartmentInfo> q = new Stack<DepartmentInfo>();
        string pcompanyid = Constants.GetParentCompanyID();

        foreach (DepartmentInfo item in list)
        {
            if (companyID == pcompanyid || pcompanyid == item.CompanyID || companyID == item.CompanyID)
                q.Push(item);

            while (q.Count > 0)
            {
                DepartmentInfo department = q.Pop();
                string text = department.Name;
                //if (department.Level > 1)
                //{
                    string str = "";
                    for (int i = 1; i < department.Level; i++)
                    {
                        str += "　";
                    }
                    text = str + text;
                //}
                itemArray.Add(new ListItem(text, department.DepartmentID.ToString()));

                DepartmentInfo childrendinfo = new DepartmentInfo();
                childrendinfo.ParentID = department.DepartmentID;
                IList<DepartmentInfo> childlist = departmentBll.Search(childrendinfo);
                foreach (DepartmentInfo child in childlist)
                {
                    if (companyID == pcompanyid || pcompanyid == child.CompanyID || companyID == child.CompanyID)
                        q.Push(child);

                }
            }

        }
        ListItem[] collection = new ListItem[itemArray.Count];
        int index = 0;
        foreach (ListItem it in itemArray)
        {
            collection[index++] = it;
        }
        return collection;
    }
    /// <summary>
    /// 获取部门下拉列表选项
    /// </summary>
    /// <param name="companyID"></param>
    /// <returns></returns>
    public static ListItem[] GetDepartmentListItemsByCompany(string companyID)
    {
        DepartmentInfo dinfo = new DepartmentInfo();
        dinfo.CompanyID = companyID;
        dinfo.Level = 1;//先获取第一重节点
        IList<DepartmentInfo> list = departmentBll.Search(dinfo);
        ArrayList itemArray = new ArrayList();
        Stack<DepartmentInfo> q = new Stack<DepartmentInfo>();
        string pcompanyid = Constants.GetParentCompanyID();

        foreach (DepartmentInfo item in list)
        {
            if (companyID == pcompanyid || pcompanyid == item.CompanyID || companyID == item.CompanyID)
                q.Push(item);

            while (q.Count > 0)
            {
                DepartmentInfo department = q.Pop();
                string text = department.Name;
                //if (department.Level > 1)
                //{
                string str = "";
                for (int i = 1; i < department.Level; i++)
                {
                    str += "　";
                }
                text = str + text;
                //}
                itemArray.Add(new ListItem(text, department.DepartmentID.ToString()));

                DepartmentInfo childrendinfo = new DepartmentInfo();
                childrendinfo.ParentID = department.DepartmentID;
                IList<DepartmentInfo> childlist = departmentBll.Search(childrendinfo);
                foreach (DepartmentInfo child in childlist)
                {
                    if (companyID == pcompanyid || pcompanyid == child.CompanyID || companyID == child.CompanyID)
                        q.Push(child);

                }
            }

        }
        ListItem[] collection = new ListItem[itemArray.Count];
        int index = 0;
        foreach (ListItem it in itemArray)
        {
            collection[index++] = it;
        }
        return collection;
    }
    /// <summary>
    /// 返回ListItem[]，第一个元素为--请选择--,""
    /// </summary>
    /// <param name="companyID"></param>
    /// <returns></returns>
    public static ListItem[] GetDepartmentListItemsWithBlank(string companyID)
    {
        ListItem[] sourcelist = GetDepartmentListItems(companyID);
        ListItem[] targetlist = new ListItem[sourcelist.Length+1];
        targetlist[0] = new ListItem("--请选择--", "0");
        for (int i = 0; i < sourcelist.Length; i++)
        {
            targetlist[i + 1] = sourcelist[i];
        }
        return targetlist;
    }

    public static ListItem[] GetDepartmentListItems()
    {
        DepartmentInfo dinfo = new DepartmentInfo();
        //dinfo.CompanyID = companyID;
        dinfo.Level = 1;//先获取第一重节点
        IList<DepartmentInfo> list = departmentBll.Search(dinfo);
        ArrayList itemArray = new ArrayList();
        Stack<DepartmentInfo> q = new Stack<DepartmentInfo>();
        string pcompanyid = Constants.GetParentCompanyID();

        foreach (DepartmentInfo item in list)
        {
           
                q.Push(item);

            while (q.Count > 0)
            {
                DepartmentInfo department = q.Pop();
                string text = department.Name;
                //if (department.Level > 1)
                //{
                string str = "";
                for (int i = 1; i < department.Level; i++)
                {
                    str += "　";
                }
                text = str + text;
                //}
                itemArray.Add(new ListItem(text, department.DepartmentID.ToString()));

                DepartmentInfo childrendinfo = new DepartmentInfo();
                childrendinfo.ParentID = department.DepartmentID;
                IList<DepartmentInfo> childlist = departmentBll.Search(childrendinfo);
                foreach (DepartmentInfo child in childlist)
                {
                   
                        q.Push(child);

                }
            }

        }
        ListItem[] collection = new ListItem[itemArray.Count];
        int index = 0;
        foreach (ListItem it in itemArray)
        {
            collection[index++] = it;
        }
        return collection;
    }

    /// <summary>
    /// 获取档案类型列表
    /// </summary>
    /// <returns></returns>
    public static ListItem[] GetArchivesTypeListItems()
    {
        ArchivesType arbll = new ArchivesType();
        ArchivesTypeInfo arinfo = new ArchivesTypeInfo();
        arinfo.Level = 1;//先获取第一重节点
        IList<ArchivesTypeInfo> list = arbll.Search(arinfo);
        ArrayList itemArray = new ArrayList();
        Stack<ArchivesTypeInfo> q = new Stack<ArchivesTypeInfo>();

        foreach (ArchivesTypeInfo item in list)
        {

            q.Push(item);

            while (q.Count > 0)
            {
                ArchivesTypeInfo archivestype = q.Pop();
                string text = archivestype.ArchivesTypeName;
                //if (department.Level > 1)
                //{
                string str = "";
                for (int i = 1; i < archivestype.Level; i++)
                {
                    str += "　";
                }
                text = str + text;
                //}
                itemArray.Add(new ListItem(text, archivestype.ArchivesTypeID.ToString()));

                ArchivesTypeInfo childrenarinfo = new ArchivesTypeInfo();
                childrenarinfo.ParentID = archivestype.ArchivesTypeID;
                IList<ArchivesTypeInfo> childlist = arbll.Search(childrenarinfo);
                foreach (ArchivesTypeInfo child in childlist)
                {
                    q.Push(child);
                }
            }

        }
        ListItem[] collection = new ListItem[itemArray.Count];
        int index = 0;
        foreach (ListItem it in itemArray)
        {
            collection[index++] = it;
        }
        return collection;
    }

    /// <summary>
    /// 返回ListItem[]，第一个元素为--请选择--,""
    /// </summary>
    /// <param name="companyID"></param>
    /// <returns></returns>
    public static ListItem[] GetDepartmentListItemsWithBlank()
    {
        ListItem[] sourcelist = GetDepartmentListItems();
        ListItem[] targetlist = new ListItem[sourcelist.Length + 1];
        targetlist[0] = new ListItem("--请选择--", "0");
        for (int i = 0; i < sourcelist.Length; i++)
        {
            targetlist[i + 1] = sourcelist[i];
        }
        return targetlist;
    }

    public static ListItem[] GetAllMaintainTeams(string companyID)
    {
        DepartmentInfo dinfo1 = new DepartmentInfo();
        //By L 5-2 *******************************************************
        //dinfo.CompanyID = companyID;
        DepartmentInfo dinfo2 = new DepartmentInfo();

        dinfo1.DepartmentType = DepartmentType.MaintainTeamOther;
        dinfo2.DepartmentType = DepartmentType.MaintainTeam;
        IList<DepartmentInfo> list1 = departmentBll.Search(dinfo1);
        IList<DepartmentInfo> list2 = departmentBll.Search(dinfo2);

        //*******************************************************************
        IList<DepartmentInfo> list = list2;
       

        ListItem[] collection=null;
        if (list == null || list.Count == 0)
        {
            collection = new ListItem[1];
            collection[0] = new ListItem("没有维修单位", "0");
        }
        else
        {
            collection = new ListItem[list.Count+list1.Count];
            int i=0;
            foreach (DepartmentInfo item in list)
            {
                collection[i++] = new ListItem(item.Name, item.DepartmentID.ToString());
            }
            //By L 5-1获取所有维修单位默认所有都能查询**************************************************************************************
            int j = i;
            foreach (DepartmentInfo item in list1)
            {
                collection[j++] = new ListItem(item.Name, item.DepartmentID.ToString());
            }
            //********************************************************************************************************************************
        }
        return collection;
    }

    //********** Modified by Xue    For ShenGaoSu    2011-11-28 ********************************************************************************
    public static ListItem[] GetAllMaintainTeamsByDeptType(string companyID,int deptType)
    {
        DepartmentInfo dinfo = new DepartmentInfo();
        //dinfo.CompanyID = companyID;
        dinfo.DepartmentType = (DepartmentType)deptType;
        IList<DepartmentInfo> list = departmentBll.Search(dinfo);
        ListItem[] collection = null;
        if (list == null || list.Count == 0)
        {
            collection = new ListItem[1];
            collection[0] = new ListItem("没有维修单位", "0");
        }
        else
        {
            collection = new ListItem[list.Count];
            int i = 0;
            foreach (DepartmentInfo item in list)
            {
                collection[i++] = new ListItem(item.Name, item.DepartmentID.ToString());
            }
        }
        return collection;
    }
    //********** Modification Finished 2011-11-28 **********************************************************************************************



    /// <summary>
    /// 获取下拉选择框的ListItemCollection，不含有空项
    /// </summary>
    /// <returns></returns>
    public static ListItem[] GetSystemListItems()
    {
        IList list = systemBll.GetAllSystem();
        ListItem[] collection = new ListItem[list.Count];

        for (int i = 0; i < list.Count; i++)
        {
            EquipmentSystemInfo s = list[i] as EquipmentSystemInfo;
            collection[i] = new ListItem(s.SystemName, s.SystemID);
        }
        return collection;
    }

    /// <summary>
    /// 返回ListItem[]，第一个元素为--请选择--,""
    /// </summary>
    /// <param name="companyID"></param>
    /// <returns></returns>
    public static ListItem[] GetSystemListItemsWithBlank()
    {
        ListItem[] sourcelist = GetSystemListItems();
        ListItem[] targetlist = new ListItem[sourcelist.Length + 1];
        targetlist[0] = new ListItem("--请选择--", "");
        for (int i = 0; i < sourcelist.Length; i++)
        {
            targetlist[i + 1] = sourcelist[i];
        }
        return targetlist;
    }

    /// <summary>
    /// 返回指定SystemID的子系统列表，带空选择
    /// </summary>
    /// <param name="systemid"></param>
    /// <returns></returns>
    public static ListItem[] GetSubSystemListItemsWithBlank(string systemid)
    {
        
        EquipmentSystem ebll = new EquipmentSystem();
        IList list = ebll.GetAllSubSystem(systemid);

        ListItem[] collection = new ListItem[list.Count+1];
        collection[0] = new ListItem("--请选择--", "0");
        for (int i = 0; i < list.Count; i++)
        {
            SubEquipmentSystemInfo s = list[i] as SubEquipmentSystemInfo;
            collection[i+1] = new ListItem(s.SubSystemName, s.SubSystemID.ToString());
        }
        return collection;
    }

    /// <summary>
    /// 返回公司ListItem[]，第一个元素为--请选择--,""
    /// </summary>
    /// <returns></returns>
    public static ListItem[] GetCompanyListItemsWithBlank()
    {
        IList<CompanyInfo> list = companyBll.GetAllCompany();
        ListItem[] collection = new ListItem[list.Count+1];

        collection[0] = new ListItem("--请选择--", "");
        for (int i = 0; i < list.Count; i++)
        {
            CompanyInfo c = list[i] as CompanyInfo;
            collection[i+1] = new ListItem(c.CompanyName,c.CompanyID);
        }
        return collection;
    }

    /// <summary>
    /// 返回设备种类ListItem[]
    /// </summary>
    /// <returns></returns>
    public static ListItem[] GetCategoryListItems()
    {
        CategorysearchInfo cinfo = new CategorysearchInfo();
        cinfo.Level = 1;//先获取第一重节点
        IList<CategoryInfo> list = categoryBll.Search(cinfo);
        ArrayList itemArray = new ArrayList();
        Stack<CategoryInfo> q = new Stack<CategoryInfo>();

        foreach (CategoryInfo item in list)
        {

            q.Push(item);

            while (q.Count > 0)
            {
                CategoryInfo category = q.Pop();
                string text = category.CategoryName;
                //if (category.Level > 1)
                //{
                string str = "";
                for (int i = 1; i < category.Level; i++)
                {
                    str += "　";
                }
                text = str + text;
                //}
                itemArray.Add(new ListItem(text, category.CategoryID.ToString()));

                CategorysearchInfo childrencinfo = new CategorysearchInfo();
                childrencinfo.ParentID = category.CategoryID;
                IList<CategoryInfo> childlist = categoryBll.Search(childrencinfo);
                foreach (CategoryInfo child in childlist)
                {

                    q.Push(child);

                }
            }

        }
        ListItem[] collection = new ListItem[itemArray.Count + 1];
        collection[0] = new ListItem("--请选择--","0");
        int index = 1;
        foreach (ListItem it in itemArray)
        {
            collection[index++] = it;
        }
        return collection;
    }

}
