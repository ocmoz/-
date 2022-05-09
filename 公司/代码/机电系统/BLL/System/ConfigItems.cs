using System;
using System.Text;
using System.Configuration;

namespace FM2E.BLL.System
{
    public class ConfigItems
    {
        /// <summary>
        /// 条形码打印机打印方式选择（"NetWork"为网络打印，"BarCodeControl"为控件方式打印）
        /// </summary>
        public static string PrintSwitch
        {
            get
            {
                string rt = ConfigurationManager.AppSettings["PrintSwitch"];
                if (rt != null)
                    return rt;
                else
                    return "NetWork";
            }
        }


        public static bool UseApplyForChangeEquipment
        {
            get
            {
                string rt = ConfigurationManager.AppSettings["UseApplyForChangeEquipment"];
                if (rt == "true")
                    return true;
                else
                    return false;
            }
        }


        /// <summary>
        /// 自维中二级修的故障记录部门（即二级修后的验收部门）--隧道所所长验收
        /// </summary>
        public static string SelfMaintianRecordDeptID
        {
            get
            {
                string rt = ConfigurationManager.AppSettings["SelfMaintianRecordDeptID"];
                if (rt != null)
                    return rt;
                else
                    return "0";
            }
        }

        /// <summary>
        /// 自维中二级修的故障登记部门（即二级修的信息登记部门）--隧道所
        /// </summary>
        public static string SelfMaintianMaintainDeptID
        {
            get
            {
                string rt = ConfigurationManager.AppSettings["SelfMaintianMaintainDeptID"];
                if (rt != null)
                    return rt;
                else
                    return "0";
            }
        }


        /// <summary>
        /// 自维中二级修的故障登记用户名（系统工程师）
        /// </summary>
        public static string SelfMaintianReceiverName
        {
            get
            {
                string rt = ConfigurationManager.AppSettings["SelfMaintianReceiverName"];
                if (rt != null)
                {
                    return rt;
                }
                else
                {
                    return "SDadmin";
                }
            }
        }



        /// <summary>
        /// 可修改故障设备费用审核金额时的状态名（空值时默认为Wait4ElectDeptEngineerConfirm）
        /// </summary>
        public static string StateName4EqCostModify
        {
            get
            {
                string rt = ConfigurationManager.AppSettings["StateName4EqCostModify"];
                if (rt != null)
                {
                    return rt;
                }
                else
                {
                    return "Wait4ElectDeptManagerConfirm";
                }
            }
        }


        /// <summary>
        /// 设备类型（新件，二手件，报废件...）
        /// </summary>
        public static string[] EqType
        {
            get
            {
                string rt = ConfigurationManager.AppSettings["EqType"];
                if (rt != null)
                {
                    string[] rr = rt.Split('@');
                    return rr;
                }
                else
                {
                    string[] rr={ "新件","二手件","报废件"};
                    return rr;
                }
            }
        }
    }
}
