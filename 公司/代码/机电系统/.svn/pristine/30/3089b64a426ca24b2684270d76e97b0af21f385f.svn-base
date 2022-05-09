using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FM2E.Model.Utils;
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;
using FM2E.BLL.System;
using FM2E.Model.System;

using WebUtility;
using WebUtility.Components;



public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaserManager_Select : System.Web.UI.Page
{
    /// <summary>
    /// 用户业务逻辑处理类
    /// </summary>
    User userBll = new User();

    /// <summary>
    /// 采购管理业务逻辑处理类对象
    /// </summary>
    Purchase purchaseBll = new Purchase();

    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //控件的事件
            
            Button_Query.Attributes.Add("onclick", "javascript:causeValidate = false;");
            Button_Query.Attributes.Add("onmouseover", "javascript:causeValidate = false;");
            Button_Query.Attributes.Add("onmouseout", "javascript:causeValidate = true;");

            //默认查询出所有结果
            FillData();
        }
    }

    /// <summary>
    /// 根据输入条件查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Query_Click(object sender, EventArgs e)
    {
        FillData();
        TabContainer1.ActiveTabIndex= 1;//查询结果的Tab
    }

    /// <summary>
    /// 往数据表中填充
    /// </summary>
    private void FillData()
    {
        string id = TextBox_UserID.Text.Trim();
        string name = TextBox_UserName.Text.Trim();
        string companyid = UserData.CurrentUserData.CompanyID;

        ////查询指导价格信息
        //UserSearchInfo queryInfo = new UserSearchInfo();

        //queryInfo.PersonName = name;
        //queryInfo.UserName = id;

        //QueryParam qp = userBll.GenerateSearchTerm(queryInfo);

        //qp.PageIndex = AspNetPager1.CurrentPageIndex;
        //qp.PageSize = AspNetPager1.PageSize;

        //int recordCount = 0;
        //获取一个公司所有用户，此处BLL没有直接提供关于名字以及用户名的查询和分页，需要把结果筛选后再分页
        IList Result = userBll.GetUsersByCompanyID(companyid);

        //把已经存在在本公司采购员的去掉
        IList list = purchaseBll.GetPurchaserList(UserData.CurrentUserData.CompanyID);
        //构造一个hash表，以userid为key
        Hashtable purchaserHash = new Hashtable();
        foreach (PurchaserInfo p in list)
        {
            purchaserHash.Add(p.UserID, p);
        }

        IList FilterResult = new ArrayList();
        foreach (UserInfo item in Result)
        {
            if (item.UserName.Contains(id) && item.PersonName.Contains(name))
            {
                if(!purchaserHash.Contains(item.UserName))
                    FilterResult.Add(item);
            }
        }

        //分页
        IList PageResult = new ArrayList();
        for (int i = (AspNetPager1.CurrentPageIndex-1) * AspNetPager1.PageSize;
            i < (AspNetPager1.CurrentPageIndex ) * AspNetPager1.PageSize && i < FilterResult.Count;
            i++)
        {
            PageResult.Add(FilterResult[i]);
        }

        GridView_Result.DataSource = PageResult;
        GridView_Result.DataBind();

        


        AspNetPager1.RecordCount = FilterResult.Count;
    }


    /// <summary>
    /// 换页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
        TabContainer1.ActiveTabIndex = 1;//查询结果的Tab
    }

    /// <summary>
    /// 清空输入框
    /// </summary>
    private void ClearInputBox()
    {
        TextBox_Remark.Text = "";
    }
    /// <summary>
    /// 把选中的结果添加到父页面中
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_AddItem_Click(object sender, EventArgs e)
    {

    }
}
