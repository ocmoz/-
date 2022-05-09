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
using FM2E.Model.Equipment;
using FM2E.Model.Utils;
using WebUtility;
using WebUtility.Components;
using FM2E.Model.Exceptions;

public partial class Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_VieweApproval : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private string id = (string)Common.sink("id", MethodType.Get, 50, 0, DataType.Str);


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = recordlist.Rows[Convert.ToInt32(e.CommandArgument)];
        if (e.CommandName == "view")
        {
            string recordid = gvRow.Attributes["recordid"];
            findmodifyhistory(Convert.ToInt32(recordid));
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            ExpendableInOutRecordInfo dv = e.Row.DataItem as ExpendableInOutRecordInfo;
            e.Row.Attributes["recordid"] = dv.ID.ToString();

        }

    }


    protected void savebutton_Click(object sender, EventArgs e)
    {
        findmodifyhistory(0);
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

        //QueryParam sheetqp = new QueryParam();
        ExpendableSheet sheetmodel = new ExpendableSheet();
        sheetmodel.id = Convert.ToInt32(id.Trim());
        QueryParam sheetqp = dal.GenerateSearchSheetTerm(sheetmodel);

        IList sheetlist = dal.GetSheetList(sheetqp, out recordCount);

        if (sheetlist.Count == 0)
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "该出入库单号已被删除", new WebException("该出入库单号已被删除"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");

        ExpendableSheet sheetitem = sheetlist[0] as ExpendableSheet;

        sheetname.Text = sheetitem.name;

        sheettime.Value = sheetitem.time.ToString();

        xinzhenyewu.Text = sheetitem.xinzhengyewustr;

        zongheshiwu.Text = sheetitem.zongheshiwustr;

        jihuacaiwu.Text = sheetitem.jihuacaiwustr;

        fenguanlingdao.Text = sheetitem.fenguanlingdaostr;

        zongjingli.Text = sheetitem.zongheshiwustr;

        findmodifyhistory(0);

    }

    private void findmodifyhistory(int recordid)
    {
        int recordCount = 0;
        //
        FM2E.SQLServerDAL.Equipment.ExpendableInOut dal = new FM2E.SQLServerDAL.Equipment.ExpendableInOut();
        ExpendableModify modifymodel = new ExpendableModify();
        modifymodel.SheetID = Convert.ToInt32(id);

        if (recordid != 0)
            modifymodel.RecordID = recordid;

        QueryParam modifyqp = dal.GenerateSearchModifyTerm(modifymodel);
        modifyqp.PageIndex = 1;
        modifyqp.PageSize = Int32.MaxValue;

        IList modifylist = dal.GetModifyList(modifyqp, out recordCount);

        modifygriview.DataSource = modifylist;
        modifygriview.DataBind();
    }
}
