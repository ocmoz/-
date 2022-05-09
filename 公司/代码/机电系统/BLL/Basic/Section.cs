using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Basic;
using FM2E.IDAL.Basic;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.DALFactory;
using System.Web.UI.WebControls;

namespace FM2E.BLL.Basic
{
    public class Section
    {
        public IList<SectionInfo> GetAllSection()
        {
            ISection dal = FM2E.DALFactory.BasicAccess.CreateSection();
            return dal.GetAllSection();
        }
        /// <summary>
        /// 获取下拉选择框的ListItemCollection，第一项为空项
        /// </summary>
        /// <returns></returns>
        public ListItem[] GenerateListItemCollectionWithBlank()
        {

            ISection dal = BasicAccess.CreateSection();
            IList<SectionInfo> list = dal.GetAllSection();
            ListItem[] collection = new ListItem[list.Count + 1];
            collection[0] = new ListItem("--请选择路段--", "");
            for (int i = 0; i < list.Count; i++)
            {
                SectionInfo s = list[i] as SectionInfo;
                collection[i + 1] = new ListItem(s.SectionName, s.SectionID);
            }
            return collection;
        }

        /// <summary>
        /// 获取下拉选择框的ListItemCollection，不含有空项
        /// </summary>
        /// <returns></returns>
        public ListItem[] GenerateListItemCollection()
        {

            ISection dal = BasicAccess.CreateSection();
            IList<SectionInfo> list = dal.GetAllSection();
            ListItem[] collection = new ListItem[list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                SectionInfo s = list[i] as SectionInfo;
                collection[i] = new ListItem(s.SectionName, s.SectionID);
            }
            return collection;
        }
        public IList<SectionInfo> GetAllSectionByCompany(string companyid)
        {
            ISection dal = FM2E.DALFactory.BasicAccess.CreateSection();
            return dal.GetAllSectionByCompany(companyid);
        }

        public SectionInfo GetSection(string Sectionid)
        {
            ISection dal = FM2E.DALFactory.BasicAccess.CreateSection();
            return dal.GetSection(Sectionid);
        }
        public void DelSection(string Sectionid)
        {
            ISection dal = FM2E.DALFactory.BasicAccess.CreateSection();
            dal.DelSection(Sectionid);
        }
        public IList<SectionInfo> SearchSection(SectionInfo item)
        {
            ISection dal = FM2E.DALFactory.BasicAccess.CreateSection();
            return dal.Search(item);
        }
        public void InsertSection(SectionInfo item)
        {
            ISection dal = FM2E.DALFactory.BasicAccess.CreateSection();
            dal.InsertSection(item);
        }
        public void UpdateSection(SectionInfo item)
        {
            ISection dal = FM2E.DALFactory.BasicAccess.CreateSection();
            dal.UpdateSection(item);
        }
        public QueryParam GenerateSearchTerm(SectionInfo item)
        {
            ISection dal = FM2E.DALFactory.BasicAccess.CreateSection();
            return dal.GenerateSearchTerm(item);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            ISection dal = FM2E.DALFactory.BasicAccess.CreateSection();
            return dal.GetList(searchTerm, out recordCount);
        }
    }
}
