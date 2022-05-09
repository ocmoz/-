using System;
using System. Collections. Generic;
using System. Linq;
using System. Web;
using System. Web. UI;
using System. Web. UI. WebControls;

using FM2E. BLL. System;
using FM2E. Model. System;
using WebUtility;
using WebUtility. Components;
using System. Collections;
using FM2E. Model. Utils;
using FM2E. Model. Exceptions;
using FM2E. WorkflowLayer;

public partial class Module_FM2E_WorkflowManager_WorkflowList : System. Web. UI. Page
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
        List<WorkflowClassInfo> list = WorkflowHelper. GetAllWorkflowList( );
        GridView1. DataSource = list;
        GridView1. DataBind( );
        ////AspNetPager1. RecordCount = list. Count;
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

            WorkflowClassInfo item = ( WorkflowClassInfo )e. Row. DataItem;
            e. Row. Attributes[ "Name" ] = item.Name;
            e. Row. Attributes[ "HasRule" ] = item.HasRule. ToString( );
        }
    }

    protected void GridView1_RowCommand( object sender , GridViewCommandEventArgs e )
    {
        GridViewRow gvRow = GridView1. Rows[ Convert. ToInt32( e. CommandArgument ) ];
        string workflowName = gvRow. Attributes[ "Name" ];

        if(e.CommandName == "editRole")
        {
             Response. Redirect( "WorkflowRoleList.aspx?name=" + workflowName, false );
        }
        else if ( e. CommandName == "editDef" )
        {
            Response. Redirect( "WorkflowEditor/WorkflowEditor.aspx?name=" + workflowName , false );
        }
    }

    protected void AspNetPager1_PageChanged( object sender , EventArgs e )
    {
        FillData( );
    }
}
