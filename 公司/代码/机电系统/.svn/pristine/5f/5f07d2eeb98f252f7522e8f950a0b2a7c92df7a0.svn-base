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
using FM2E.Model.Budget;
using FM2E.BLL.Budget;
using FM2E.Model.Utils;
using System.Collections.Generic;
using WebUtility;
using System.Web.UI.MobileControls;

public partial class Module_FM2E_BudgetManager_BudgetStaticsManager_EditDetail : System.Web.UI.Page
{
    long Detail = (long)Common.sink("index", MethodType.Get, 255, 0, DataType.Long);
    string companyid = UserData.CurrentUserData.CompanyID;
    int index = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Moveindex();
        if (!IsPostBack)
        {
            //AddTree2(0, (TreeNode)null);
            //TreeView2.ShowLines = true;
            InitPage();
        }
    }

    private void Moveindex()
    {
        foreach (BudgetDetailInfo item in (List<BudgetDetailInfo>)Session["BudgetDetaillist"])
        {
            if (item.DetailID == Detail)
            {
                break;
            }
            index++;
        }
    }

    private void InitPage()
    {
        BudgetDetailInfo budgetdetailinfo = ((List<BudgetDetailInfo>)Session["BudgetDetaillist"])[index];
        SubIDNametb.Text = budgetdetailinfo.SubName;
        SubIDtb.Text = budgetdetailinfo.SubID.ToString();
        ExpenditureNametb.Text = budgetdetailinfo.ExpenditureName;
        BudgetApprove.Text = budgetdetailinfo.BudgetApprove.ToString();
        if (budgetdetailinfo.RealExpenditure != 0)
            RealExpendituretb.Text = budgetdetailinfo.RealExpenditure.ToString();
        Supplier.Text = budgetdetailinfo.Supplier;
        LB_CompanyName.Text = budgetdetailinfo.CompanyName;
        if (DateTime.Compare(budgetdetailinfo.PayDate, DateTime.MinValue) != 0)
            PayDate.Text = budgetdetailinfo.PayDate.ToString("yyyy-MM-dd");
    }



    protected void AddDetail_Click(object sender, EventArgs e)
    {
        IList list = ((List<BudgetDetailInfo>)Session["BudgetDetaillist"]);
        //BudgetDetailInfo item = ((BudgetDetailInfo)list[index]);
        if (RealExpendituretb.Text != string.Empty)
            ((BudgetDetailInfo)list[index]).RealExpenditure = Convert.ToDecimal(RealExpendituretb.Text);
        else
            ((BudgetDetailInfo)list[index]).RealExpenditure = Convert.ToDecimal(0);
        if (PayDate.Text != string.Empty)
            ((BudgetDetailInfo)list[index]).PayDate = Convert.ToDateTime(PayDate.Text);
        ((BudgetDetailInfo)list[index]).Supplier = Supplier.Text;

        //list[index] = item;
        Session["BudgetDetaillist"] = list;

        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "BudgetDetailRefresh", "window.parent.hidePopWin(true);", true);

    }





}
