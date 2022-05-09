using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FM2E.Model.Equipment;
using FM2E.IDAL.EmployeesInfo;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.IDAL.Equipment;
using System.Configuration;


namespace FM2E.BLL.EmployeesInfo
{
    public class EmployeesInfo
    {
        public IList GetAllEquipment()
        {
            IEmployeesInfo dal = FM2E.DALFactory.EmployeesInfoAccess.CreateEquipment();
            return dal.getxxx();
        }

        //***********************************************************************2012-6-25*************************************
        public void InsertEmployeesInfo(EmployeesInfomodel item)
        {
            IEmployeesInfo dal = FM2E.DALFactory.EmployeesInfoAccess.CreateEquipment();
             dal.InsertEmployeesInfo(item);
        }
        //***********************************************************************2012-6-25*************************************
    }
}

