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
using FM2E.BLL.Equipment;
using FM2E.Model.Basic;
using FM2E.BLL.Basic;
using System.Collections.Generic;
using FM2E.Model.Equipment;
using WebUtility;
using WebUtility.Components;
using System.Drawing;
using FM2E.Model.Utils;
using FM2E.Model.Exceptions;

public partial class Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_ExpendableApproval : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private string id = (string)Common.sink("id", MethodType.Get, 50, 0, DataType.Str);

    string viewstatename = "Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_ExpendableApproval";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        List<ExpendableModify> list = Session[viewstatename] as List<ExpendableModify>;
        if (list == null)
        {
            list = new List<ExpendableModify>();
            ExpendableModify item = new ExpendableModify();
            item.SheetID = Convert.ToInt32(id);
            item.RecordID = Convert.ToInt32(expendableid.Value.Trim());
            item.equipmentname = expendablename.Value.Trim();
            item.modifytime = DateTime.Now;
            item.oldnum = Convert.ToInt32(oldvalue.Value.Trim());
            item.newnum = Convert.ToInt32(newvalue.Value.Trim());
            item.userid = UserData.CurrentUserData.UserName;
            item.username = UserData.CurrentUserData.PersonName;
            item.type = expendabletype.Value.Trim();
            list.Add(item);
        }
        else
        {
            ExpendableModify item = new ExpendableModify();
            item.SheetID = Convert.ToInt32(id);
            item.RecordID = Convert.ToInt32(expendableid.Value.Trim());
            item.equipmentname = expendablename.Value.Trim();
            item.modifytime = DateTime.Now;
            item.oldnum = Convert.ToInt32(oldvalue.Value.Trim());
            item.newnum = Convert.ToInt32(newvalue.Value.Trim());
            item.userid = UserData.CurrentUserData.UserName;
            item.username = UserData.CurrentUserData.PersonName;
            item.type = expendabletype.Value.Trim();
            list.Add(item);
        }
        Session[viewstatename] = list;

    }

    protected void savebutton_Click(object sender, EventArgs e)
    {
        FM2E.SQLServerDAL.Equipment.ExpendableInOut dal = new FM2E.SQLServerDAL.Equipment.ExpendableInOut();

        List<ExpendableModify> list = Session[viewstatename] as List<ExpendableModify>;
        if (list != null && list.Count != 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                ExpendableModify modifyitem = list[i] as ExpendableModify;
                ExpendableInOutRecordInfo recordinfo = new ExpendableInOutRecordInfo();
                recordinfo.ID = Convert.ToInt64(modifyitem.RecordID);
                recordinfo.Amount = Convert.ToDecimal(modifyitem.newnum);

                dal.insertmodify(modifyitem);
                dal.updaterecordamount(recordinfo);
            }
        }

        ExpendableSheet item = new ExpendableSheet();
        item.id = Convert.ToInt32(id);
        item.name = sheetname.Text;
        item.time = Convert.ToDateTime(sheettime.Value);
        item.xinzhengyewu = xinzhenyewu.SelectedValue;
        item.zongheshiwu = zongheshiwu.SelectedValue;
        item.jihuacaiwu = jihuacaiwu.SelectedValue;
        item.fenguanlingdao = fenguanlingdao.SelectedValue;
        item.zongjinli = zongjingli.SelectedValue;

        dal.updatesheet(item);
        Session.Remove(viewstatename);
    }

    private void FillData()
    {
        FM2E.SQLServerDAL.Equipment.ExpendableInOut dal = new FM2E.SQLServerDAL.Equipment.ExpendableInOut();

        QueryParam qp = (QueryParam)ViewState["SearchTerm"];
        if (qp == null)
        {
            ExpendableInOutRecordInfo model = new ExpendableInOutRecordInfo();
            model.SheetID = Convert.ToInt64(id.Trim());
            qp = dal.GenerateSearchRecordTerm(model);
        }
        qp.PageIndex = 1;
        qp.PageSize = Int32.MaxValue;


        int recordCount = 0;
        IList list = dal.GetList(qp, out recordCount);
        recordlist.DataSource = list;
        recordlist.DataBind();


        //
        ExpendableSheet sheetmodel = new ExpendableSheet();
        sheetmodel.id = Convert.ToInt32(id.Trim());
        QueryParam sheetqp = dal.GenerateSearchSheetTerm(sheetmodel);
        sheetqp.PageIndex = 1;
        sheetqp.PageSize = Int32.MaxValue;

        IList sheetlist = dal.GetSheetList(sheetqp, out recordCount);

        if(sheetlist.Count == 0)
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "该出入库单号已被删除", new WebException("该出入库单号已被删除"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");

        ExpendableSheet sheetitem = sheetlist[0] as ExpendableSheet;

        sheetname.Text = sheetitem.name;

        sheettime.Value = sheetitem.time.ToString();

        xinzhenyewu.SelectedValue = sheetitem.xinzhengyewu;

        zongheshiwu.SelectedValue = sheetitem.zongheshiwu;

        jihuacaiwu.SelectedValue = sheetitem.jihuacaiwu;

        fenguanlingdao.SelectedValue = sheetitem.fenguanlingdao;

        zongjingli.SelectedValue = sheetitem.zongheshiwu;

        

    }

}
