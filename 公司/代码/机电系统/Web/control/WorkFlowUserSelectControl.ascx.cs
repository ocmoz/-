using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FM2E.BLL.Basic;
using FM2E.BLL.Workflow;
using FM2E.Model.Basic;
using FM2E.Model.System;
using FM2E.BLL.Utils;
using System.Collections;
using FM2E.WorkflowLayer;
using FM2E.Model.Workflow;

public partial class control_WorkFlowUserSelectControl : System.Web.UI.UserControl
{
    Company companyBll = new Company();
    Department departmentBll = new Department();
    WorkflowRole workflowroleBll = new WorkflowRole();
    protected void Page_Load(object sender, EventArgs e)
    {
        //初始化
        if (!IsPostBack)
        {
            if (_eventdatasource != null)
            {
                RadioButtonList_Events.DataTextField = EventNameField;
                RadioButtonList_Events.DataValueField = EventIDField;
                RadioButtonList_Events.DataSource = _eventdatasource;
                RadioButtonList_Events.DataBind();
            }
            IList<CompanyInfo> companylist = companyBll.GetAllCompany();
            DropDownList_Company.Items.Clear();
            foreach (CompanyInfo c in companylist)
            {
                DropDownList_Company.Items.Add(new ListItem(c.CompanyName, c.CompanyID));
            }
            try//是否有默认的
            {
                DropDownList_Company.SelectedValue = _selectedcompanyid;
            }
            catch { }
            span_company.Visible = ShowCompanySelect;
            //读取默认公司的部门
            UpdateDepartmentDropDownList();
            UpdateUserDropDownList();

            DropDownList_Company.Attributes["onchange"] = "javascript:CheckButtonList();";
            DropDownList_Department.Attributes["onchange"] = "javascript:CheckButtonList();";
        }

    }

    private string _workflowstate = "";
    /// <summary>
    /// 当前的工作流状态
    /// </summary>
    public string WorkFlowState
    {
        get { return (string)ViewState["WorkFlowState"]; }
        set { _workflowstate = value;
        ViewState["WorkFlowState"] = value;
        }
    }

    private string _eventnamefield;
    /// <summary>
    /// 事件名称列
    /// </summary>
    public string EventNameField
    {
        get { return (string)ViewState["EventNameField"]; }
        set
        {
            _eventnamefield = value;
            ViewState["EventNameField"] = value;
        }
    }
    private string _eventidfield;
    /// <summary>
    /// 事件ID列
    /// </summary>
    public string EventIDField
    {
        get { return (string)ViewState["EventIDField"]; }
        set
        {
            _eventidfield = value;
            ViewState["EventIDField"] = value;
        }
    }

    private object _eventdatasource;
    /// <summary>
    /// 事件数据源
    /// </summary>
    public object EventListDataSource
    {
        get { return _eventdatasource; }
        set { _eventdatasource = value; }
    }

    /// <summary>
    /// 绑定事件数据源
    /// </summary>
    public void EventListDataBind()
    {
        RadioButtonList_Events.DataSource = _eventdatasource;
        RadioButtonList_Events.DataBind();
    }

    private List<string> _showselectvdivalue;
    /// <summary>
    /// 当RadioButtonList的选择值与此值相等的时候才显示下一用户的选择
    /// </summary>
    public List<string> ShowSelectDivValue
    {
        get
        {
            List<string> list = (List<string>)ViewState["ShowSelectDivValue"];

            if (list == null)
                list = new List<string>();
            return list;
        }
        set
        {
            _showselectvdivalue = value;
            ViewState["ShowSelectDivValue"] = value;
        }

    }
    /// <summary>
    /// 添加显示用户选择的事件
    /// </summary>
    /// <param name="v"></param>
    public void AddShowSelectDivValue(string v)
    {
        List<string> list = ShowSelectDivValue;
        list.Add(v);
        ShowSelectDivValue = list;
        
    }

    private bool _showcompanyselect;
    /// <summary>
    /// 是否显示公司选择
    /// </summary>
    public bool ShowCompanySelect
    {
        get
        {
            if (ViewState["ShowCompanySelect"] == null) return false;
            else
                return
            (bool)ViewState["ShowCompanySelect"];
        }
        set
        {
            _showcompanyselect = value;
            ViewState["ShowCompanySelect"] = value;
        }
    }
    private string _selectedcompanyid;
    /// <summary>
    /// 选中的公司ID
    /// </summary>
    public string SelectedCompanyID
    {
        get { return DropDownList_Company.SelectedValue; }
        set
        {
            _selectedcompanyid = value;
            DropDownList_Company.SelectedValue = value;
        }
    }
    private long _selecteddepartmentid;
    /// <summary>
    /// 选中的部门ID
    /// </summary>
    public long SelectedDepartmentID
    {
        get { return long.Parse(DropDownList_Department.SelectedValue); }
        set
        {
            _selecteddepartmentid = value;
            DropDownList_Department.SelectedValue = value.ToString();
        }
    }

    private string _selecteduserid;
    /// <summary>
    /// 选中的用户ID
    /// </summary>
    public string SelectedUserID
    {
        get
        {
            if (DropDownList_User.Items.Count == 0)
            {
                return "";
            }
            return DropDownList_User.SelectedValue;

        }
        set
        {
            _selecteduserid = value;
            DropDownList_User.SelectedValue = value;
        }
    }

    private string _selectedevent;
    /// <summary>
    /// 选中的事件
    /// </summary>
    public string SelectedEvent
    {
        get { return RadioButtonList_Events.SelectedValue; }
        set
        {
            _selectedevent = value;
            RadioButtonList_Events.SelectedValue = value;
        }
    }

    /// <summary>
    /// 事件变换的时候，还需要更新部门下面用户的列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RadioButtonList_Events_SelectedChanged(object sender, EventArgs e)
    {
        div_selectuser.Visible = false;
        for (int i = 0; i < ShowSelectDivValue.Count; i++)
        {
            if (RadioButtonList_Events.SelectedValue == ShowSelectDivValue[i])
            {
                div_selectuser.Visible = true;
                break;
            }
        }
        UpdateUserDropDownList();
    }

    /// <summary>
    /// 公司变化的时候，需要更新部门
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DropDownList_Company_SelectedChanged(object sender, EventArgs e)
    {
        UpdateDepartmentDropDownList();
        UpdateUserDropDownList();
    }
    /// <summary>
    /// 更新部门下拉菜单
    /// </summary>
    private void UpdateDepartmentDropDownList()
    {
        DropDownList_Department.Items.Clear();
        if (DropDownList_Company.Items.Count == 0)
            return;
        string companyid = DropDownList_Company.SelectedValue;
        
        DepartmentInfo dinfo = new DepartmentInfo();
        dinfo.CompanyID = companyid;
        dinfo.Level = 1;//先获取第一重节点
        IList<DepartmentInfo> list = departmentBll.Search(dinfo);
        Queue<DepartmentInfo> q = new Queue<DepartmentInfo>();
        foreach (DepartmentInfo item in list)
        {
            q.Enqueue(item);

            while (q.Count > 0)
            {
                DepartmentInfo department = q.Dequeue();
                string text = department.Name;
                if (department.Level > 1)
                {
                    string str = "";
                    for (int i = 1; i < department.Level; i++)
                    {
                        str += "  ";
                    }
                    text = str + text;
                }
                DropDownList_Department.Items.Add(new ListItem(text, department.DepartmentID.ToString()));

                DepartmentInfo childrendinfo = new DepartmentInfo();
                childrendinfo.ParentID = department.DepartmentID;
                IList<DepartmentInfo> childlist = departmentBll.Search(childrendinfo);
                foreach (DepartmentInfo child in childlist)
                {
                    q.Enqueue(child);
                }
            }

        }
        DropDownList_Department.Enabled = true;
        if (DropDownList_Department.Items.Count == 0)
        {
            DropDownList_Department.Items.Add(new ListItem("--找不到部门--", "0"));
            DropDownList_User.Items.Clear();
            DropDownList_User.Items.Add(new ListItem("--找不到用户--", ""));
            DropDownList_Department.Enabled = false;
            DropDownList_User.Enabled = false;
        }
    }

    /// <summary>
    /// 部门变化，更新用户列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DropDownList_Department_SelectedChanged(object sender, EventArgs e)
    {
        UpdateUserDropDownList();
    }

    private string _workflowname = "";
    /// <summary>
    /// 工作流名称
    /// </summary>
    public string WorkFlowName
    {
        get { return (string)ViewState["WorkFlowName"]; }
        set { _workflowname = value;
        ViewState["WorkFlowName"] = value;
        }
    }
    /// <summary>
    /// 选定事件的名称
    /// </summary>
   public string SelectedEventName
    {
        get
        {
            if (RadioButtonList_Events.SelectedItem != null)
            {
                return RadioButtonList_Events.SelectedItem.Text;

            }
            else
            {
                return "";
            }
        }
    }

    /// <summary>
    /// 更新用户下拉菜单
    /// </summary>
    private void UpdateUserDropDownList()
    {
        DropDownList_User.Items.Clear();
        if (DropDownList_Department.Items.Count == 0)
        {
            return;
        }
        if (RadioButtonList_Events.SelectedValue == null || RadioButtonList_Events.SelectedValue == "")
        {
            return;
        }
        long departmentid = long.Parse(DropDownList_Department.SelectedValue);
        string state = WorkFlowState;
        string workflowname =  WorkFlowName;
        string wfevent = RadioButtonList_Events.SelectedValue;
        WorkflowStateInfo nextstate = WorkflowHelper.GetNextStateInfo(workflowname,state,wfevent);
        IList userlist = new FM2E.BLL.System.User().GetWorkflowStateUser(workflowname, nextstate.Name, departmentid);
        //根据部门ID、工作流状态、工作流事件获取下一可选用户列表
        foreach (UserInfo u in userlist)
        {
            //对于每个用户判断是否被代理了
            string text = u.PersonName;
            string value = u.UserName;
            UserDelegateInfo ud = workflowroleBll.GetUserDelegate(u.UserName, DateTime.Now, workflowname, nextstate.Name);
            if (ud != null)
            {
                text += "," + ud.DelegateUserPersonName;
                value += "," + ud.DelegateUserName;
            }
            DropDownList_User.Items.Add(new ListItem(text, value));
        }
        DropDownList_User.Enabled = true;
        if (DropDownList_User.Items.Count == 0)
        {
            DropDownList_User.Items.Add(new ListItem("--找不到用户--", ""));
            DropDownList_User.Enabled = false;
        }
    }

    /// <summary>
    /// 返回是否有合法的选择结果
    /// </summary>
    public bool ProperlySelected
    {
        get
        {
            if (RadioButtonList_Events.SelectedItem == null ||
                string.IsNullOrEmpty(RadioButtonList_Events.SelectedValue))
                return false;
            if (div_selectuser.Visible)
            {
                if (string.IsNullOrEmpty(DropDownList_User.SelectedValue))
                {
                    return false;
                }
            }
            return true;
        }
    }

    /// <summary>
    /// 选中的下一用户
    /// </summary>
    public string NextUserName
    {
        get
        {
            if (div_selectuser.Visible == false)
            {
                return null;
            }
            string nextuser = null;
            string[] v = DropDownList_User.SelectedValue.Split(',');
            if (v.Length > 0)
                nextuser = v[0];
            return nextuser;
        }
    }
    /// <summary>
    /// 选中的代理用户
    /// </summary>
    public string DelegateUserName
    {
        get
        {
            if (div_selectuser.Visible == false)
            {
                return null;
            }
            string delegateuser = null;
            string[] v = DropDownList_User.SelectedValue.Split(',');
            if (v.Length > 1)
                delegateuser = v[1];
            return delegateuser;
        }
    }
}
