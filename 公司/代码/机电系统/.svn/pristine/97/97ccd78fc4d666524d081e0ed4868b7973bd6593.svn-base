using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.IDAL.Basic
{
    public interface ICategory
    {
        IList<CategoryInfo> GetAllCategory();
        CategoryInfo GetCategory(long id);

        void InsertCategory(CategoryInfo item);
        void UpdateCategory(CategoryInfo item);
        void DelCategory(long id);
        IList<CategoryInfo> Search(CategorysearchInfo item);
        void DelCategory(IList<CategoryInfo> Categorys);
        QueryParam GenerateSearchTerm(CategorysearchInfo item);
        IList GetList(QueryParam term, out int recordCount);
        int GetNextCategoryCode(string CategoryCode);
    }
}
