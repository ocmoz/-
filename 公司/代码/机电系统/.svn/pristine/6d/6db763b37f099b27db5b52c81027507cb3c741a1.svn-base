using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Utils;
using FM2E.IDAL.Utils;

namespace FM2E.BLL.Utils
{
    public class SheetNOGenerator
    {
        public static string GetSheetNO(string companyID, SheetType sheetType)
        {
            ISheetNO dal = FM2E.DALFactory.UtilsAccess.CreateSheetNO();
            return dal.GetSheetNO(companyID, sheetType);
        }
    }
}