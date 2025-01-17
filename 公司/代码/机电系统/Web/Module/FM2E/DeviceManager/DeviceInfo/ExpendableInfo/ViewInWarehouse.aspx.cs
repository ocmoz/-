﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using FM2E.WorkflowLayer;
using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;
using FM2E.BLL.Basic;
using FM2E.BLL.System;
using FM2E.BLL.Utils;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using System.Collections.Generic;

using System.IO;
using FM2E.Model.Exceptions;
using FM2E.Model.System;

public partial class Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_ViewInWarehouse : System.Web.UI.Page
{
    long id=(long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = SystemPermission.CheckButtonPermission(PopedomType.Edit);
            HeadMenuWebControls1.ButtonList[0].ButtonUrl = "InWarehouse.aspx?cmd=edit&id=" + id;
            HeadMenuWebControls1.ButtonList[0].ButtonUrlType = UrlType.Href;

            FillData();
        }
    }  

    private void FillData()
    {
        try
        {
            InWarehouse inbll = new InWarehouse();
            InWarehouseInfo info = inbll.GetInWarehouse(id);
            LB_Department.Text = info.CompanyName;
            LB_sheetName.Text = info.SheetName;

            if (info.CurrentStateName == InWarehouseInfoStatus.ReturnModify.ToString())//如果表单状态是返回修改
            {
                HeadMenuWebControls1.ButtonList[0].ButtonVisible = true;
            }
            else
            {
                HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
            }

            #region 附件绑定
            string separatorStr = "@First@";
            string[] split = { separatorStr };
            if (info.Attachment != null)
            {
                if (!info.Attachment.Contains(separatorStr))
                {
                    info.Attachment += " " + separatorStr + " ";  //附件名称+附件地址
                }
            }
            else
            {
                info.Attachment += " " + separatorStr + " ";  //附件名称+附件地址
            }
            string[] editreason1 = info.Attachment.Split(split, StringSplitOptions.None);
            if (info.Attachment.Length > 0)
            {
                HyperLink_File.NavigateUrl = editreason1[1];
                HyperLink_File.Text = editreason1[0];
                HyperLink_File.Visible = true;
            }
            #endregion

            #region 审批意见
            if (info.Editreason != null)
            {
                string[] aa = { "→" };
                string[] approvalrecordSplit = info.Editreason.Split(aa, StringSplitOptions.RemoveEmptyEntries);
                List<FM2E.Model.Maintain.ApprovalRecord> arList = new List<FM2E.Model.Maintain.ApprovalRecord>();
                for (int i = 0; i < approvalrecordSplit.Length; i++)
                {
                    string[] bb = { "#" };
                    string[] arsplitsplit = approvalrecordSplit[i].Split(bb, StringSplitOptions.None);
                    if (arsplitsplit.Length == 6)
                    {
                        FM2E.Model.Maintain.ApprovalRecord ar = new FM2E.Model.Maintain.ApprovalRecord(arsplitsplit[0], arsplitsplit[2], arsplitsplit[1], arsplitsplit[3], arsplitsplit[4], arsplitsplit[5]);
                        arList.Add(ar);
                    }
                }
                Repeater1.DataSource = arList;
                Repeater1.DataBind();
            }
            #endregion
            Lb_SubmitTime.Text = info.SubmitTime.ToString("yyyy-MM-dd"); 
            LB_IWRemark.Text=info.Remark;
            LB_IWName.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.InWarehouseList[0])).Name;
            LB_IWModel.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.InWarehouseList[0])).Model;
            LB_IWCount.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.InWarehouseList[0])).Count.ToString();
            LB_IWUnit.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.InWarehouseList[0])).Unit;           
            LB_IWCategory.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.InWarehouseList[0])).ExpendableType;
            LB_IWPrice.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.InWarehouseList[0])).ExpendablePrice.ToString();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }  
}



