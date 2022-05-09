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
using FM2E.BLL.Basic;
using WebUtility;
using WebUtility.Components;
using FM2E.Model.Basic;
using System.Collections.Generic;
using System.IO;
using FM2E.Model.Utils;

public partial class Module_FM2E_BasicData_CompanyManage_Company : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
            Process();
            PermissionControl();
        }
    }

    private void PermissionControl()
    {
        //if (SystemPermission.CheckPermission(PopedomType.Delete))
        //    GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
        //else GridView1.Columns[GridView1.Columns.Count - 1].Visible = false ;
    }

    private void FillData()
    {
        QueryParam qp = (QueryParam)ViewState["SearchTerm"];
        if (qp == null)
        {
            qp = new QueryParam();
        }
        qp.PageIndex = AspNetPager1.CurrentPageIndex;
        qp.PageSize = AspNetPager1.PageSize;

        //查询
        Company bll = new Company();
        int recordCount = 0;
        IList list = bll.GetList(qp, out recordCount);
        GridView1.DataSource = list;
        GridView1.DataBind();

        AspNetPager1.RecordCount = recordCount;


    }



    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        if (e.CommandName == "view")
        {
            string id = gvRow.Attributes["CompanyID"];
            Response.Redirect("ViewCompany.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "del")
        {
            try
            {
                int row = Convert.ToInt32(e.CommandArgument);
                string id = gvRow.Attributes["CompanyID"];
                Company cbll = new Company();
                cbll.DelCompany(id);

                FileUpLoadCommon.DeleteFile(gvRow.Attributes["PictureUrl"]);

                //GridView1.Rows[row].Visible = false;
                FillData();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败",ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
            }
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

            CompanyInfo dv = (CompanyInfo)e.Row.DataItem;
            string id = dv.CompanyID;
            e.Row.Attributes["CompanyID"] = id.ToString();
            string PictureUrl = dv.PictureUrl;
            e.Row.Attributes["PictureUrl"] = PictureUrl;
        }

    }


    private void Process()
    {
        //if (cmd == "export")
        //{
        //    //导出
        //    string file = Server.MapPath("~/public/2.xls");
        //    FileStream stream = File.Open(file, FileMode.Open);

        //    byte[] Buffer = null;
        //    long size;
        //    size = stream.Length;
        //    Buffer = new byte[size];
        //    stream.Read(Buffer, 0, int.Parse(stream.Length.ToString()));
        //    stream.Close();
        //    stream = null;

        //    HttpContext.Current.Response.ContentType = "application/xls";
        //    string header = "attachment; filename=" + file;
        //    HttpContext.Current.Response.AddHeader("content-disposition", header);
        //    HttpContext.Current.Response.BinaryWrite(Buffer);
        //    HttpContext.Current.Response.End();
        //    HttpContext.Current.Response.Flush();

        //}
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }
}
