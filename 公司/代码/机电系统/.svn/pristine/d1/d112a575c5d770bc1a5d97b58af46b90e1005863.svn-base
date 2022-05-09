using System;
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
using System.Xml.Linq;
using WebUtility;
using WebUtility.Components;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using FM2E.BLL.System;


public partial class Module_FM2E_DeviceManager_AssetManager_ScrapManager_ScrapApply_ViewScrapApply : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 20, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 20, 0, DataType.Long);
    private readonly Scrap scrapBll = new Scrap();
    private readonly Equipment EqBll = new Equipment();
    private ScrapStatus status = ScrapStatus.Unknown;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
            ButtonBind();
        }
    }

    private void FillData()
    {
        if (cmd == "view"||cmd=="viewArchives")
        {
            try
            {
                ScrapApplyInfo item = scrapBll.GetScrapApply(id);
                if (item == null)
                    return;
               
                lbSheetName.Text = item.SheetName;
                lbCompany.Text = item.CompanyName;
                lbDep.Text = item.DepName;
                lbApplicant.Text = item.ApplicantName;
                lbStatus.Text = item.StatusString;
                lbApplyDate.Text = item.ApplyDate.ToString("yyyy-MM-dd HH:mm:ss");
                lbRemark.Text = item.Remark;
                status = item.Status;

               
                #region 附件绑定
                string separatorStr = "@First@";
                string[] split = { separatorStr };
                if (item.Attachment != null)
                {
                    if (!item.Attachment.Contains(separatorStr))
                    {
                        item.Attachment += " " + separatorStr + " ";  //附件名称+附件地址
                    }
                }
                else
                {
                    item.Attachment += " " + separatorStr + " ";  //附件名称+附件地址
                }
                string[] editreason1 = item.Attachment.Split(split, StringSplitOptions.None);
                if (item.Attachment.Length > 0)
                {
                    HyperLink_File.NavigateUrl = editreason1[1];
                    HyperLink_File.Text = editreason1[0];
                    HyperLink_File.Visible = true;
                }
                #endregion
                
                if (item.Equipments != null && item.Equipments.Count != 0)
                {
                    ScrapApplyDetailInfo equipment = (ScrapApplyDetailInfo)item.Equipments[0];
                    //lbEquipment.Text = equipment.EquipmentName + "（" + equipment.EquipmentNo + "）";
                    lbEquipment.Text = equipment.EquipmentName;
                    EquipmentInfoFacade Eqitem = EqBll.GetEquipmentBYNO(equipment.EquipmentNo);

                   object object1= string.Format("javascript:showPopWin('查看设备信息','{0}Module/FM2E/DeviceManager/DeviceInfo/CurrentEuipementInfo/AllEquipmentInfo/ViewDeviceInfo.aspx?cmd=view&id={1}&companyid=SG&index=',800, 430, null,true,true);", Page.ResolveUrl("~/"), Eqitem.EquipmentID);

                    lbEquipmentNo.Text = string.Format("<a style=\"color: Blue\" href=\"{0}\">{1}</a>",string.Format("javascript:showPopWin('查看设备信息','{0}Module/FM2E/DeviceManager/DeviceInfo/CurrentEuipementInfo/AllEquipmentInfo/ViewDeviceInfo.aspx?cmd=view&id={1}&companyid=SG&index=',800, 430, null,true,true);",Page.ResolveUrl("~/"), Eqitem.EquipmentID),equipment.EquipmentNo);
                    lbReason.Text = equipment.ScrapReason;
                }

                if (item.ApprovalList != null)
                {
                    GridView2.DataSource = item.ApprovalList;
                    GridView2.DataBind();
                }
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取报废申请单信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        else if (cmd == "delete")
        {
            try
            {
                scrapBll.DeleteScrapApply(id);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取报废申请单信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除设备报废申请成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ScrapApply.aspx"), UrlType.Href, "");
        }
    }

    private void ButtonBind()
    {
        HeadMenuWebControls1.ButtonList[0].ButtonUrl = "EditScrapApply.aspx?cmd=edit&id=" + id;
        HeadMenuWebControls1.ButtonList[0].ButtonUrlType = UrlType.Href;

        HeadMenuWebControls1.ButtonList[1].ButtonUrl = string.Format("DelData('?cmd=delete&id={0}');", id);
        HeadMenuWebControls1.ButtonList[1].ButtonUrlType = UrlType.JavaScript;

        if (status != ScrapStatus.Draft)
        {
            //只有草稿才能编辑删除
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
        }
    }

}
