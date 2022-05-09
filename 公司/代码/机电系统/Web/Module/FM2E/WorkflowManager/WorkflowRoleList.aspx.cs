using System;
using System. Collections. Generic;
using System. Linq;
using System. Web;
using System. Web. UI;
using System. Web. UI. WebControls;

using FM2E. BLL. Workflow;
using FM2E. Model.Workflow ;
using WebUtility;
using WebUtility. Components;
using System. Collections;
using FM2E. Model. Utils;
using FM2E. Model. Exceptions;
using FM2E.WorkflowLayer;

public partial class Module_FM2E_WorkflowManager_WorkflowRoleList : System. Web. UI. Page
{
    protected void Page_Load( object sender , EventArgs e )
    {
        if ( !IsPostBack )
        {
            FillData( );
            PermissionControl( );
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

    private void FillData( )
    {
        //添加工作流名称信息
        HeadMenuWebControls1. HeadOPTxt = "当前工作流：" + Request. QueryString["name"];
        HeadMenuWebControls1. ButtonList[ 0 ]. ButtonUrl = "EditWorkflowRole.aspx?cmd=add&wfname=" + Request. QueryString["name"];

        //获取查询条件
        QueryParam qp = ( QueryParam )ViewState[ "SearchTerm" ];
        if ( qp == null )
        {
            qp = new QueryParam( );
            ViewState[ "SearchTerm" ] = qp;
        }
        qp. PageIndex = AspNetPager1. CurrentPageIndex;

        //查询
        WorkflowRole bll = new WorkflowRole( );
        int recordCount = 0;
        IList list = bll. GetWorkflowRoleList( Request. QueryString[ "name" ] , qp , out recordCount );
        GridView1. DataSource = list;
        GridView1. DataBind( );
        AspNetPager1. RecordCount = recordCount;
    }

    protected void GridView1_RowDataBound( object sender , GridViewRowEventArgs e )
    {
        if ( e. Row. RowType == DataControlRowType. DataRow )
        {
            //鼠标移动到每项时颜色交替效果    
            e. Row. Attributes. Add( "OnMouseOut" , "this.style.backgroundColor='White';" );
            e. Row. Attributes. Add( "OnMouseOver" , "this.style.backgroundColor='#f7f7f7';" );

            //设置悬浮鼠标指针形状为"小手"    
            e. Row. Attributes[ "style" ] = "Cursor:hand";

            WorkflowRoleInfo item = ( WorkflowRoleInfo )e. Row. DataItem;
            e. Row. Attributes[ "WorkflowRoleID" ] = item. WorkflowRoleID.ToString ();
            e. Row. Attributes[ "IsSingle" ] = item.IsSingle. ToString( );
            e. Row. Attributes[ "IsApprover" ] = item. IsApprover. ToString( );
        }
    }

    protected void GridView1_RowCommand( object sender , GridViewCommandEventArgs e )
    {
        GridViewRow gvRow = GridView1. Rows[ Convert. ToInt32( e. CommandArgument ) ];

        if ( e. CommandName == "viewRole" )
            Response. Redirect( "EditWorkflowRole.aspx?cmd=view&id=" + gvRow. Attributes[ "WorkflowRoleID" ] + "&wfname=" + Request. QueryString[ 0 ] );
        else if ( e. CommandName == "editRole" )
            Response. Redirect( "EditWorkflowRole.aspx?cmd=edit&id=" + gvRow. Attributes[ "WorkflowRoleID" ] + "&wfname=" + Request. QueryString[ 0 ] );
        else if(e.CommandName == "del")
        {
            bool bSuccess = false;
            WorkflowRole bll = new WorkflowRole( );
            try
            {
                bll. DeleteWorkflowRole( Convert. ToInt64( gvRow. Attributes[ "WorkflowRoleID" ] ) );
                bSuccess = true;
            }
            catch ( Exception ex )
            {
                EventMessage. MessageBox( Msg_Type. Error , "操作失败" , "删除工作流失败" , ex , Icon_Type. Error , true , "window.history.go(-1)" , UrlType. JavaScript , "" );
            }
            if ( bSuccess )
            {
                EventMessage. MessageBox( Msg_Type. Info , "操作成功" , "删除工作流角色成功！" , Icon_Type. OK , true , Common. GetHomeBaseUrl( "WorkflowRoleList.aspx?name=" +Request. QueryString["name"])  , UrlType. Href , "" );
            }
        }
    }

    protected void AspNetPager1_PageChanged( object sender , EventArgs e )
    {
        FillData( );
    }
}