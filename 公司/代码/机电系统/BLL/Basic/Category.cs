using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Basic
{
    public class Category
    {
        public IList<CategoryInfo> GetAllCategory()
        {
            ICategory dal = FM2E.DALFactory.BasicAccess.CreateEquimentCaterogy();
            return dal.GetAllCategory();
        }
        public CategoryInfo GetCategory(long id)
        {
            ICategory dal = FM2E.DALFactory.BasicAccess.CreateEquimentCaterogy();
            return dal.GetCategory(id);
        }


        public void InsertCategory(CategoryInfo item)
        {
            ICategory dal = FM2E.DALFactory.BasicAccess.CreateEquimentCaterogy();
            dal.InsertCategory(item);
        }

        public QueryParam GenerateSearchTerm(CategorysearchInfo item)
        {
            ICategory dal = FM2E.DALFactory.BasicAccess.CreateEquimentCaterogy();
            return dal.GenerateSearchTerm(item);
        }

        public void UpdateCategory(CategoryInfo item)
        {
            ICategory dal = FM2E.DALFactory.BasicAccess.CreateEquimentCaterogy();
            dal.UpdateCategory(item);
        }

        //同时删除子部门
        public void DelCategory(long id)
        {
            ICategory dal = FM2E.DALFactory.BasicAccess.CreateEquimentCaterogy();

            dal.DelCategory(id);
        }

        public IList<CategoryInfo> Search(CategorysearchInfo item)
        {
            ICategory dal = FM2E.DALFactory.BasicAccess.CreateEquimentCaterogy();
            return dal.Search(item);
        }

        public IList GetList(QueryParam term, out int recordCount)
        {
            ICategory dal = FM2E.DALFactory.BasicAccess.CreateEquimentCaterogy();
            return dal.GetList(term, out recordCount);
        }
        /// <summary>
        /// 获取当前结点的下一个子结点的编码
        /// </summary>
        public string GetNextCategoryCode(string CategoryCode)
        {
            ICategory dal = FM2E.DALFactory.BasicAccess.CreateEquimentCaterogy();
            int nextCode = dal.GetNextCategoryCode(CategoryCode);

            return CategoryCode + ConvertTo36String(nextCode);
        }
        private string ConvertTo36String(int value)
        {
            string result = "";
            int remainder = value % 36;
            int commerce = value / 36;
            result += IntToLetter(remainder);
            while (commerce != 0)
            {
                remainder = commerce % 36;
                result = IntToLetter(remainder) + result;
                commerce /= 36;
            }
            if (result.Length % 3 != 0)
            {
                string zerostr = "";
                for (int i = 0; i < 3 - result.Length % 3; i++)
                {
                    zerostr = "0" + zerostr;
                }
                result = zerostr + result;
            }
            return result;
        }
        /// <summary>
        /// 数字转换成字母
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static string IntToLetter(int num)
        {
            string result = "";
            if (num < 10)
                result = num.ToString();
            else
            {
                string startLetter = "A";
                int tmp = Encoding.ASCII.GetBytes(startLetter)[0];
                tmp = tmp + (num - 10);
                result += (char)tmp;
            }

            return result;
        }
    }
}
