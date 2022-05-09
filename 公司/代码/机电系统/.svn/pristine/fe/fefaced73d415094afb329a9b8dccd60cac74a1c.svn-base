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
using FM2E.Model.Insurance;
using FM2E.Model.SpecialProject;

public partial class Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_InWarehouseApprove : System.Web.UI.Page
{
    long id = (long)Common.sink("id", MethodType.Get, 255, 0, DataType.Long);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
        }
    }

    private void FillData()
    {
        try
        {
            InWarehouseInfo project = Session[sessionName] as InWarehouseInfo;
            InWarehouseInfo info = inbll.GetInWarehouse(id);

            Session[sessionName] = info;

            WorkFlowUserSelectControl1.EventIDField = "Name";
            WorkFlowUserSelectControl1.EventNameField = "Description";
            WorkFlowUserSelectControl1.WorkFlowState = info.CurrentStateName;
            WorkFlowUserSelectControl1.WorkFlowName = SGS_InWarehouseWorkflow.WorkflowName;

            List<WorkflowEventInfo> eventlist = WorkflowHelper.GetEventInfoList(SGS_InWarehouseWorkflow.WorkflowName, info.CurrentStateName);
            List<WorkflowEventInfo> temlist = eventlist;
            WorkFlowUserSelectControl1.EventListDataSource = temlist;
            //  [4/11/2012 L]
            WorkFlowUserSelectControl1.EventListDataBind();

            WorkFlowUserSelectControl1.ShowCompanySelect = false;

            LB_Department.Text = info.CompanyName;
            LB_sheetName.Text = info.SheetName;

            if (info.CurrentStateName != InWarehouseInfoStatus.WaitEngineer.ToString())
            {
                FileUpload_div.Visible = false;
            }
            if (info.CurrentStateName == InWarehouseInfoStatus.WaitWarehouseKeeper.ToString())
                EAndA.Visible = false;

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
            List<CurrentStatusFile> arList = new List<CurrentStatusFile>();

            int editreason1id = 1;
            for (int i = 0; i < editreason1.Length; i++)
            {
                string[] bb = { "#" };
                string[] arsplitsplit = editreason1[i].Split(bb, StringSplitOptions.None);
                if (arsplitsplit.Length == 2)
                {
                    CurrentStatusFile ar = new CurrentStatusFile(editreason1id, arsplitsplit[0], arsplitsplit[1]);
                    arList.Add(ar);
                    editreason1id += 1;
                }
            }
            gridviewFile.DataSource = arList;
            gridviewFile.DataBind();

            #endregion


            #region 审批意见
            if (info.Editreason != null)
            {
                string[] aa = { "→" };
                string[] approvalrecordSplit = info.Editreason.Split(aa, StringSplitOptions.RemoveEmptyEntries);
                List<FM2E.Model.Maintain.ApprovalRecord> arList1 = new List<FM2E.Model.Maintain.ApprovalRecord>();
                for (int i = 0; i < approvalrecordSplit.Length; i++)
                {
                    string[] bb = { "#" };
                    string[] arsplitsplit = approvalrecordSplit[i].Split(bb, StringSplitOptions.None);
                    if (arsplitsplit.Length == 6)
                    {
                        FM2E.Model.Maintain.ApprovalRecord ar = new FM2E.Model.Maintain.ApprovalRecord(arsplitsplit[0], arsplitsplit[2], arsplitsplit[1], arsplitsplit[3], arsplitsplit[4], arsplitsplit[5]);
                        arList1.Add(ar);
                    }
                }
                Repeater1.DataSource = arList1;
                Repeater1.DataBind();
            }
            #endregion

            Lb_SubmitTime.Text = info.SubmitTime.ToString("yyyy-MM-dd");
            LB_IWRemark.Text = info.Remark;

            LB_IWName.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.InWarehouseList[0])).Name;
            LB_IWModel.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.InWarehouseList[0])).Model;
            LB_IWCount.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.InWarehouseList[0])).Count.ToString();
            LB_IWUnit.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.InWarehouseList[0])).Unit;
            CategorysearchInfo item = new CategorysearchInfo();
            item.CategoryID = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.InWarehouseList[0])).ExpendableTypeID;
            Category bll = new Category();
            QueryParam qp = bll.GenerateSearchTerm(item);
            qp.PageIndex = 1;
            qp.PageSize = 1;
            int recordCount = 0;
            IList list = bll.GetList(qp, out recordCount);
            LB_IWCategory.Text = ((FM2E.Model.Basic.CategoryInfo)(list[0])).CategoryName;
            LB_IWPrice.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.InWarehouseList[0])).ExpendablePrice.ToString();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    private string sessionName = "Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_InWarehouseApprove";
    InWarehouse inbll = new InWarehouse();
    protected void GridView_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName != null && e.CommandName != "")
        {

            int itemid = int.Parse(e.CommandArgument.ToString());
            try
            {
                InWarehouseInfo project = Session[sessionName] as InWarehouseInfo;
                string separatorStr = "@First@";
                string[] split = { separatorStr };
                string[] editreason1 = project.Attachment.Split(split, StringSplitOptions.None);
                string files = "";
                for (int i = 0; i < editreason1.Count(); i++)
                {
                    if (i != (itemid - 1))
                    {
                        if (files != "")
                        {
                            files += "@First@" + editreason1[i].ToString();
                        }
                        else
                        {
                            files += editreason1[i].ToString();
                        }
                    }
                    else
                    {
                        string[] bb = { "#" };
                        string[] arsplitsplit = editreason1[i].Split(bb, StringSplitOptions.None);
                        if (arsplitsplit.Length == 2)
                        {
                            string delFile = arsplitsplit[1];
                            File.Delete(Server.MapPath(UPLOADFOLDER + delFile));

                        }
                    }
                }
                project.Attachment = files;
                inbll.UpdateInWarehouse(project);

            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "删除失败", "删除失败，请重试", ex, Icon_Type.Error, true, "history.go(-1);", UrlType.JavaScript, "");

                return;
            }

            FillData();
        }
    }

    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";
        }
    }
    /// <summary>
    /// 上传文件路径，相对于~/public文件夹
    /// </summary>
    private const string UPLOADFOLDER = "~/public/ScrapManager/";

    protected void Button1_Click(object sender, EventArgs e)
    {
        InWarehouseInfo item = inbll.GetInWarehouse(id);
    
        string currentstatus_file = "";
        //附件处理

        HttpFileCollection uploadedFiles = Request.Files;
        for (int i = 0; i < uploadedFiles.Count; i++)
        {
            HttpPostedFile userPostedFile = uploadedFiles[i];
            if (userPostedFile.ContentLength > 0)
            {
                string fileName = "";//定义文件名
                fileName = Path.GetFileName(userPostedFile.FileName);
                int index = fileName.IndexOf(".");
                string FileType = fileName.Substring(index).ToLower();//截取文件后缀名
                //FileTypeImg = "../FileTypeimg/" + hz + ".gif";
                Guid fileGuid = Guid.NewGuid();//生成新的文件名称 以GUID命名防止文件名相同
                string NewFileName = fileGuid.ToString();//新的文件名
                NewFileName = NewFileName + FileType;//新的文件名+后缀名
                if (userPostedFile.ContentLength > 2097151 * 1024)//判断是否大于配置文件中的上传文件大小
                {
                    Page.RegisterStartupScript("提示", "<script language='javascript'>alert('对不起您的上传资源过大!');return;</script>");
                    return ;
                }
                else
                {
                    if (fileName != "")//如果文件名不为空
                    {
                        try
                        {
                            //文件虚拟路径
                            string strpath = Server.MapPath(UPLOADFOLDER + NewFileName);
                            if (!Directory.Exists(strpath))//判断目录是否存在
                            {
                                Directory.CreateDirectory(Server.MapPath(UPLOADFOLDER));
                            }
                            userPostedFile.SaveAs(strpath);
                            if (currentstatus_file == "")
                            {
                                currentstatus_file += fileName + "#" + NewFileName;
                            }
                            else
                                currentstatus_file += "@First@" + fileName + "#" + NewFileName;

                            Response.Write("上传成功!");

                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.ToString());
                        }
                    }
                    else
                    {
                        Response.Write("上传文件不能为空!");
                        return ;
                    }
                }
            }
        }

        FM2E.Model.System.UserInfo userinfor = (new FM2E.BLL.System.User().GetUser(WebUtility.Common.Get_UserName));
        // 用户真名，职位名，部门名，时间，事件名，意见
        if (item.CurrentStateName == "WaitWarehouseKeeper")
        {
            tbApprovalRemark.Text = WorkFlowUserSelectControl1.SelectedEventName;

            #region 入库
            //入库记录
            ExpendableInOutRecordInfo record = new ExpendableInOutRecordInfo();
            record.InOutTime = DateTime.Now;
            record.CompanyID = item.CompanyID;
            record.Amount = ((FM2E.Model.Equipment.InEquipmentsInfo)(item.InWarehouseList[0])).Count;
            record.CategoryID = ((FM2E.Model.Equipment.InEquipmentsInfo)(item.InWarehouseList[0])).ExpendableTypeID;
            record.Model = ((FM2E.Model.Equipment.InEquipmentsInfo)(item.InWarehouseList[0])).Model;
            record.Name = ((FM2E.Model.Equipment.InEquipmentsInfo)(item.InWarehouseList[0])).Name;
            record.Price = ((FM2E.Model.Equipment.InEquipmentsInfo)(item.InWarehouseList[0])).ExpendablePrice;
            record.Receiver = UserData.CurrentUserData.UserName;
            record.ReceiverName = UserData.CurrentUserData.PersonName;
            record.Remark = item.Remark;
            record.Type = ExpendableInOutRecordType.In;
            record.Unit = ((FM2E.Model.Equipment.InEquipmentsInfo)(item.InWarehouseList[0])).Unit;
            record.WarehouseID = item.WarehouseID;
            record.WarehouseKeeper = Common.Get_UserName;
            record.WarehouseKeeperName = UserData.CurrentUserData.PersonName;

            Expendable bll = new Expendable();
            bll.ExpendableInWarehouse(UserData.CurrentUserData.CompanyID, record);



            #endregion
        }

        item.Editreason += "→" + userinfor.PersonName + "#" + userinfor.PositionName + "#" + userinfor.DepartmentName + "#" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "#" + WorkFlowUserSelectControl1.SelectedEventName + "#" + tbApprovalRemark.Text.Replace('#', '。').Trim();

        try
        {
            inbll.UpdateInWarehouse(item);

            string URL = "../DeviceManager/DeviceInfo/ExpendableInfo/InWarehouseApply.aspx";
            string title = "你有新的易耗品入库申请" + item.SheetName + "待审批";

            //这里需要工作流跳转 更新下一个审批者 发送待办事务

            WorkflowApplication.SetStateMachineAndSendingPendingOrderAndNextUserMachine<SGS_InWarehouseEventService>(id, title, URL, SGS_InWarehouseWorkflow.WorkflowName, WorkFlowUserSelectControl1.SelectedEvent, SGS_InWarehouseWorkflow.TableName, Common.Get_UserName, UserData.CurrentUserData.PersonName, 0, null);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交失败", ex, Icon_Type.Error, true,
                "window.history.go(-1)", UrlType.JavaScript, "");
        }
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", string.Format("审批成功,入库单号为：{0}", item.SheetName), Icon_Type.OK, true, Common.GetHomeBaseUrl("InWarehouseApprovalList.aspx"), UrlType.Href, "");
    }
}



