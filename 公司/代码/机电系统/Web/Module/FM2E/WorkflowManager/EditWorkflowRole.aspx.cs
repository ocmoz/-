using System;
using System. Text;
using System. Collections. Generic;
using System. Linq;
using System. Web;
using System. Web. UI;

using FM2E. BLL. Workflow;
using FM2E. Model. Workflow;
using WebUtility;
using WebUtility. Components;
using System. Collections;
using FM2E. Model. Utils;
using FM2E. Model. Exceptions;
using FM2E. WorkflowLayer;

public partial class Module_FM2E_WorkflowManager_EditWorkflowRole : System. Web. UI. Page
{
    protected void Page_Load( object sender , EventArgs e )
    {
        if ( !IsPostBack )
        {
            FillData( );
            PermissionControl( );
        }
    }

    void FillData( )
    {
        //添加工作流名称信息
        String workflowName = Request. QueryString[ "wfname" ];
        HeadMenuWebControls1. HeadOPTxt = "当前工作流：" + workflowName;
        HeadMenuWebControls1. ButtonList[ 0 ]. ButtonUrl = "WorkflowRoleList.aspx?name=" + Request. QueryString[ "wfname" ];

        String cmd = Request. QueryString[ "cmd" ];
        if (  cmd == "view" )
        {
            TextBox_RoleName. Enabled = false;
            RadioButtonList_IsSingle. Enabled = false;
            MultiListBox_BindingStateList. Enabled = false;
        }

        WorkflowRole bll = new WorkflowRole( );
        IList allList = WorkflowHelper.GetAllStateInfo( workflowName );
        if ( cmd == "edit" || cmd == "view")
        {
            WorkflowRoleInfo info = bll. GetWorkflowRoleInfo( Convert. ToInt64( Request. QueryString[ "id" ] ) );
            ViewState[ "WorkflowRoleID" ] = info. WorkflowRoleID;

            //初始化两个单选框
            TextBox_RoleName. Text = info. RoleName;
            if ( info. IsSingle )
                RadioButtonList_IsSingle. Items[ 0 ]. Selected = true;
            else
                RadioButtonList_IsSingle. Items[ 1 ]. Selected = true;

            if ( info. IsApprover )
                RadioButtonList_IsApprover. Items[ 0 ]. Selected = true;
            else
                RadioButtonList_IsApprover. Items[ 1 ]. Selected = true;

            //显示角色对应的工作流状态列表
            IList bindingList = WorkflowHelper. GetStateInfosByRole( workflowName , info );
            IList unbindList = new List<WorkflowStateInfo>( allList. Count - bindingList. Count );
            foreach ( WorkflowStateInfo ws in allList )
            {
                bool canAdd = true;
                foreach ( WorkflowStateInfo ws2 in bindingList )
                    if ( ws2. Name == ws. Name )
                    {
                        canAdd = false;
                        break;
                    }
                if ( canAdd )
                    unbindList. Add( ws );
                else
                    continue;
            }
            MultiListBox_BindingStateList. SecondListBox. DataSource = bindingList;
            MultiListBox_BindingStateList.FirstListBox . DataSource = unbindList;
        }
        else if(cmd == "add")
        {
            RadioButtonList_IsSingle. Items[ 1 ]. Selected = true;
            RadioButtonList_IsApprover. Items[ 1 ]. Selected = true;
            MultiListBox_BindingStateList.FirstListBox.DataSource = allList;
        }
      MultiListBox_BindingStateList. DataBind( );
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

    protected void Button_OK_Click( object sender , EventArgs e )
    {
        WorkflowRole bll = new WorkflowRole( );
        WorkflowRoleInfo info = new WorkflowRoleInfo( );
        info. WorkflowName = Request. QueryString[ "wfname" ];
        info. RoleName = TextBox_RoleName. Text;
        info. IsSingle = RadioButtonList_IsSingle. Items[ 0 ]. Selected;
        info. IsApprover = RadioButtonList_IsApprover. Items[ 0 ]. Selected;

        info. BindingStates = new List<string>( );
        foreach (System.Web.UI.WebControls.ListItem li in MultiListBox_BindingStateList.SecondListBox.Items)
            info. BindingStates. Add( li.Value );

        bool bSuccess = false;
        String cmd = Request. QueryString[ "cmd" ];
        String backUrl = "WorkflowRoleList.aspx?name=" + Request. QueryString[ "wfname" ];
        if ( cmd == "edit" )
        {
            try
            {
                info. WorkflowRoleID = Convert. ToInt64( ViewState[ "WorkflowRoleID" ] );
                bll. UpdateWorkflowRoleInfo( info );
                bSuccess = true;
            }
            catch ( Exception ex )
            {
                EventMessage. MessageBox( Msg_Type. Error , "操作失败" , "修改工作流角色失败" , ex , Icon_Type. Error , true , "window.history.go(-1)" , UrlType. JavaScript , "" );
            }
            if ( bSuccess )
            {
                EventMessage. MessageBox( Msg_Type. Info , "操作成功" , "修改工作流角色成功！" , Icon_Type. OK , true , Common. GetHomeBaseUrl( backUrl ) , UrlType. Href , "" );
            }
        }
        else if ( cmd == "add" )
        {
            try
            {
                bll. InsertWorkflowRole( info );
                bSuccess = true;
            }
            catch ( Exception ex )
            {
                EventMessage. MessageBox( Msg_Type. Error , "操作失败" , "添加工作流角色失败" , ex , Icon_Type. Error , true , "window.history.go(-1)" , UrlType. JavaScript , "" );
            }
            if ( bSuccess )
            {
                EventMessage. MessageBox( Msg_Type. Info , "操作成功" , "添加工作流角色成功！" , Icon_Type. OK , true , Common. GetHomeBaseUrl( backUrl ) , UrlType. Href , "" );
            }
        }
        else
            Response. Redirect( backUrl );
    }
}
