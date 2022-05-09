
using System;
using System.Collections.Generic;
using System.Text;

namespace WebUtility.Components
{
    /// <summary>
    /// 权限类
    /// </summary>
    public class Permission
    {
        #region "Private Variables"

        private string moduleID;
        private string moduleName;
        private List<PermissionItem> _ItemList = new List<PermissionItem>();
        #endregion

        #region "Public Variables"
        /// <summary>
        /// 模块编号
        /// </summary>
        public string ModuleID
        {
            get {
                return moduleID;
            }
            set {
                moduleID = value;
            }
        }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName
        {
            get {
                return moduleName;
            }
            set {
                moduleName = value;
            }
        }

        /// <summary>
        /// 权限文件列表
        /// </summary>
        public List<PermissionItem> ItemList
        {
            get {
                return _ItemList;
            }
            set {
                _ItemList = value;
            }
        }
        #endregion
    }

    /// <summary>
    /// 权限详细类
    /// </summary>
    public class PermissionItem
    {
        #region "Private Variables"
        private string _Item_Name;
        private int _Item_Value;
        private string _Item_FileList;
        #endregion

        #region "Public Variables"
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Item_Name
        {
            get {
                return _Item_Name;
            }
            set {
                _Item_Name = value;
            }
        }
        /// <summary>
        /// 权限值
        /// </summary>
        public int Item_Value
        {
            get {
                return _Item_Value;
            }
            set {
                _Item_Value = value;
            }
        }

        /// <summary>
        /// 权限所属文件列表
        /// </summary>
        public string Item_FileList
        {
            get {
                return _Item_FileList;
            }
            set {
                _Item_FileList = value;
            }
        }
        #endregion
    }

    #region "权限类型"
    /// <summary>
    /// 权限类型
    /// </summary>
    public enum PopedomType
    {
        /// <summary>
        /// 不控制权限
        /// </summary>
        NotControl=0,
        /// <summary>
        /// 列表/查看
        /// </summary>
        List = 2,
        /// <summary>
        /// 新增
        /// </summary> 
        New = 4,
        /// <summary>
        /// 修改
        /// </summary>
        Edit = 8,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 16,
        /// <summary>
        /// 打印
        /// </summary>
        Print = 32,
        /// <summary>
        /// 审批
        /// </summary>
        Approval = 64,
        /// <summary>
        /// 权限A
        /// </summary>
        PermissionA = 128,
        /// <summary>
        /// 权限B
        /// </summary>
        PermissionB = 256
    }
    #endregion
}
