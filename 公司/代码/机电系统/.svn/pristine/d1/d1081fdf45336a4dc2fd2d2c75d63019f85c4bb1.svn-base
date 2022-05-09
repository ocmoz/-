using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.Basic;
using FM2E.Model.Basic;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.BLL.Basic
{
    public class Department
    {
        /// <summary>
        /// 获取所有部门列表
        /// </summary>
        /// <returns></returns>
        public IList<DepartmentInfo> GetAllDepartment()
        {
            IDepartment dal = FM2E.DALFactory.BasicAccess.CreateDepartment();
            return dal.GetAllDepartment();
        }

        public DepartmentInfo GetDepartment(long id)
        {
            IDepartment dal = FM2E.DALFactory.BasicAccess.CreateDepartment();
            return dal.GetDepartment(id);
        }


        public long InsertDepartment(DepartmentInfo item)
        {
            IDepartment dal = FM2E.DALFactory.BasicAccess.CreateDepartment();
            return dal.InsertDepartment(item);
        }

        public QueryParam GenerateSearchTerm(DepartmentInfo item)
        {
            IDepartment dal = FM2E.DALFactory.BasicAccess.CreateDepartment();
            return dal.GenerateSearchTerm(item);
        }

        public void UpdateDepartment(DepartmentInfo item)
        {
            IDepartment dal = FM2E.DALFactory.BasicAccess.CreateDepartment();
            dal.UpdateDepartment(item);
        }

        //同时删除子部门
        public void DelDepartment(long id)
        {
            IDepartment dal = FM2E.DALFactory.BasicAccess.CreateDepartment();

            dal.DelDepartment(id);
        }

        public IList<DepartmentInfo> Search(DepartmentInfo item)
        {
            IDepartment dal = FM2E.DALFactory.BasicAccess.CreateDepartment();
            return dal.Search(item);
        }

        public IList GetList(QueryParam term, out int recordCount,string companyid)
        {
            IDepartment dal = FM2E.DALFactory.BasicAccess.CreateDepartment();
            return dal.GetList(term, out recordCount,companyid);
        }


        public IList GetList(QueryParam term, out int recordCount, int level)
        {
            IDepartment dal = FM2E.DALFactory.BasicAccess.CreateDepartment();
            return dal.GetList(term, out recordCount, level);
        }

        //public long GenerateID()
        //{
        //    IDepartment dal = FM2E.DALFactory.BasicAccess.CreateDepartment();
        //    return dal.GenerateID();
        //}
    }
}
