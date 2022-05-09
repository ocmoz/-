using System;
using System. Collections. Generic;
using System. Linq;
using System. Web;
using System. Web. UI;
using System. Web. UI. WebControls;

using FM2E. BLL. Workflow;
using FM2E. Model. Workflow;
using WebUtility;
using WebUtility. Components;
using System. Collections;
using FM2E. Model. Utils;
using FM2E. Model. Exceptions;
using FM2E. WorkflowLayer;

public partial class Module_FM2E_SystemManager_UserManager_EditUserWorkflowRole : System. Web. UI. Page
{
    protected void Page_Load( object sender , EventArgs e )
    {
        if ( !IsPostBack )
        {
            FillData( );
            PermissionControl( );
        }
        this. LoadComplete += new EventHandler( Module_FM2E_SystemManager_UserManager_EditUserWorkflowRole_LoadComplete );
    }

    /// <summary>
    /// 需要使用UniqueID和ClientID的在这里处理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void Module_FM2E_SystemManager_UserManager_EditUserWorkflowRole_LoadComplete( object sender , EventArgs e )
    {
        Form. DefaultButton = btn_Save. UniqueID;
    }

    void FillData( )
    {
        String userName = Request .QueryString ["name"];
        HeadMenuWebControls1. HeadOPTxt = "当前用户：" + userName;
        HeadMenuWebControls1. ButtonList[ 1 ]. ButtonUrl = "ViewUser.aspx?cmd=view&name=" + userName;
        btn_Save. Attributes[ "onclick" ] = "if(!confirm('确定保存所作修改？')) return false;";

        //建立将绑定于Repeater的rows数据
        List<WorkflowClassInfo> workflowList = WorkflowHelper. GetAllWorkflowList( );
        List<WorkflowRoleRow> rows = new List<WorkflowRoleRow>( workflowList. Count );
        WorkflowRole bll =new WorkflowRole ();
        foreach (WorkflowClassInfo info in workflowList)
        {
            WorkflowRoleRow rr = new WorkflowRoleRow( );
            rr. WorkflowName = info. Name;
            rr. WorkflowDescription = info. Description;
            rr. AllRoleList = bll. GetWorkflowRoleList( info. Name ) as List<WorkflowRoleInfo>;
            rr. SelectedRoleList = bll. GetWorkflowRoleList( userName , info. Name ) as List<WorkflowRoleInfo>;
            rows. Add( rr );
        }

        //绑定，并把用户已有的角色Select
        Repeater1.DataSource = rows;
        Repeater1.DataBind();
        for(int i = 0; i<Repeater1.Items.Count; ++i)
        {
            CheckBoxList list = Repeater1.Items[i]. FindControl( "chkboxlist_RoleList" ) as CheckBoxList;
            foreach (WorkflowRoleInfo selectedInfo in rows[i].SelectedRoleList)
            {
                foreach (ListItem li in list.Items)
                {
                    if ( li.Text == selectedInfo. RoleName )
                    {
                        li. Selected = true;
                        break;
                    }
                }
            }
        }
    }

    private void PermissionControl( )
    {
        //         if ( SystemPermission. CheckPermission( PopedomType. Delete ) )
        //             GridView1. Columns[ 5 ]. Visible = true;
        //         else GridView1. Columns[ 5 ]. Visible = false;
        // 
        //         //只有超级用户才有权限删除用户
        //         if ( UserData. CurrentUserData. IsAdministrator )
        //             GridView1. Columns[ 5 ]. Visible = true;
        //         else GridView1. Columns[ 5 ]. Visible = false;
    }

    /// <summary>
    /// 保存工作流角色配置
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click( object sender , EventArgs e )
    {
        String userName = Request. QueryString[ "name" ];
        List<long> roleIdList = new List<long>( );
        WorkflowRole bll = new WorkflowRole( );
        foreach ( RepeaterItem item in Repeater1. Items )
        {
            CheckBoxList cbl = item. FindControl( "chkboxlist_RoleList" ) as CheckBoxList;
            String workflowName = ( item. FindControl( "lb_WorkflowName" ) as Label ). Text;
            if ( cbl. Attributes[ "changed" ] != null )
            {
                roleIdList. Clear( );
                foreach ( ListItem li in cbl. Items )
                {
                    if ( li. Selected )
                    {
                        roleIdList. Add( Int64. Parse( li. Value ) );
                    }
                }
                try
                {
                    bll. UpdateUserWorkflowRole( userName , workflowName , roleIdList );
                }
                catch ( Exception ex )
                {
                    EventMessage. MessageBox( Msg_Type. Error , "操作失败" , "配置用户工作流角色失败" , ex , Icon_Type. Error , true , "window.history.go(-1)" , UrlType. JavaScript , "" );
                }
            }
        }
        EventMessage. MessageBox( Msg_Type. Info , "操作成功" , "配置用户工作流角色成功！" , Icon_Type. OK , true , Common. GetHomeBaseUrl( "ViewUser.aspx?cmd=view&name=" + userName ) , UrlType. Href , "" );
    }

    /// <summary>
    /// 全选或全否
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckAll_CheckedChanged( object sender , EventArgs e )
    {
        CheckBox cb = sender as CheckBox;
        RepeaterItem ri = ( sender as CheckBox ). Parent as RepeaterItem;
        CheckBoxList cbl = ri. FindControl( "chkboxlist_RoleList" ) as CheckBoxList;
        cbl. Attributes[ "changed" ] = "t";
        foreach ( ListItem li in cbl. Items )
        {
            li. Selected = cb. Checked;
        }
    }

    /// <summary>
    /// 设置一个标记位用于指示该工作流角色配置是否被改变
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cbl_SelectedIndexChanged( object sender , EventArgs e )
    {
        ( sender as CheckBoxList ). Attributes[ "changed" ] = "t";
    }
}

/// <summary>
/// 为Repeater绑定设置
/// </summary>
public class WorkflowRoleRow
{
    public String WorkflowName
    {
        get;
        set;
    }
    public String WorkflowDescription
    {
        get;
        set;
    }
    public List<WorkflowRoleInfo> AllRoleList
    {
        get;
        set;
    }
    public List<WorkflowRoleInfo> SelectedRoleList
    {
        get;
        set;
    }
}