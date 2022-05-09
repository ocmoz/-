using System;
using System. Collections. Generic;
using System. Linq;
using System. Web;
using System. Web. UI;
using System. Web. UI. WebControls;

using System. Drawing;
using System. Workflow. Activities. Rules;

using FM2E.WorkflowLayer;

public partial class Module_FM2E_WorkflowManager_WorkflowEditor_WorkflowEditor : System.Web.UI.Page
{
    protected void Page_Load( object sender , EventArgs e )
    {
        this. PreRender += new EventHandler( Module_FM2E_WorkflowManager_WorkflowEditor_PreRender );
        FillData( );
        PermissionControl( );
    }

    void Module_FM2E_WorkflowManager_WorkflowEditor_PreRender( object sender , EventArgs e )
    {
        //设置页面默认按钮
        Form.DefaultFocus = btn_SaveAll. ClientID;
        Form. DefaultButton = btn_SaveAll. UniqueID;

        //当状态描述被修改时，用js同步状态描述标题（该控件未包含在UpdatePanel中）
        for ( int i = 1 ; i < table_StateList. Controls. Count ; ++i )
        {
            TextBox stateBox = ( table_StateList. Rows[ i ]. Cells[ 1 ]. Controls[ 0 ] as TextBox );
            stateBox. Attributes[ "StateName" ] = stateBox.ID;
            stateBox. Attributes[ "onchange" ] = String. Format( "changeStateDescriptionTitle('{0}', '{1}');" , panel_WorkflowContent. ClientID , stateBox. ClientID );
        }

        //保证各Decimal属性和Enum属性的描述不能为空
        for ( int i = 1 ; i < table_DecimalList. Controls. Count ; ++i )
        {
            TextBox decimalBox = ( table_DecimalList. Rows[ i ]. Cells[ 1 ]. Controls[ 0 ] as TextBox );
            decimalBox. Attributes[ "OldValue" ] = decimalBox. Text;
            decimalBox. Attributes[ "onchange" ] = String. Format( "checkTextEmpty('{0}');" , decimalBox. ClientID );
        }
        for ( int i = 1 ; i < table_EnumList. Controls. Count ; ++i )
        {
            TextBox enumBox = ( table_EnumList. Rows[ i ]. Cells[ 0 ]. Controls[ 0 ] as TextBox );
            enumBox. Attributes[ "OldValue" ] = enumBox. Text;
            enumBox. Attributes[ "onchange" ] = String. Format( "checkTextEmpty('{0}');" , enumBox. ClientID );
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
        //获得一个临时的工作流Parser，并保存到Session
        IWorkflowParser parser = null;
        String parserGuid = ViewState[ "ParserGuid" ] == null ? null : ViewState[ "ParserGuid" ] as String;
        if ( IsPostBack )
            parser = Session[ parserGuid ] as IWorkflowParser;
        else
        {
            //为保存和还原按钮设置提示
            btn_SaveAll. Attributes[ "onclick" ] = "if(!confirm('确定保存对工作流的修改？')) return false;";
            btn_Withdraw. Attributes[ "onclick" ] = "if(!confirm('确定放弃对工作流所做的修改？')) return false;";

            //保存ScriptManager使后面的某些局部刷新可写入js代码
            Session[ "ScriptManager" ] = ScriptManager1;

            //获得临时工作流副本，并保存在Session中
            parser = WorkflowHelper. GetTempWorkflowParser( Request. QueryString[ "name" ] );
            parserGuid = Guid. NewGuid( ). ToString( );
            ViewState[ "ParserGuid" ] = parserGuid;
            Session[ parserGuid ] = parser;

            //写入工作流标题和描述
            lb_WorkflowName. Text = parser. WorkflowInfo. Name + " 基本信息";
            tb_WorkflowDescription. Text = parser. WorkflowInfo. Description;
            tb_WorkflowDescription. Attributes[ "onkeydown" ] = "return limitInput();";
        }

        TableCell cell1 , cell2;
        TableRow row;

        #region 呈现工作流的基本信息

        //各状态信息
        foreach ( WorkflowStateInfo wsi in parser. StateInfoList )
        {
            cell1 = new TableCell( );
            cell1. CssClass = "workflowTableBody";
            cell1. Text = wsi. Name;
            TextBox tbStateDescription = new TextBox( );
            tbStateDescription. ID = wsi. Name;
            tbStateDescription. Text = wsi. Description;
            tbStateDescription. CssClass = "workflowTableTextbox";
            tbStateDescription. AutoPostBack = true;
            tbStateDescription. Attributes[ "onkeydown" ] = "return limitInput();";
            tbStateDescription. TextChanged += new EventHandler( tbStateDescription_TextChanged );
            cell2 = new TableCell( );
            cell2. CssClass = "workflowTableNone";
            cell2. Controls. Add( tbStateDescription );
            row = new TableRow( );
            row. Cells. Add( cell1 );
            row. Cells. Add( cell2 );
            table_StateList. Rows. Add( row );

            //各事件信息
            foreach ( WorkflowEventInfo wei in parser. GetEventInfoList( wsi. Name , true ) )
            {
                cell1 = new TableCell( );
                cell1. CssClass = "workflowTableBody";
                cell1. Text = wei. Name;
                TextBox tbEventDescription = new TextBox( );
                tbEventDescription. ID = wei. Name;
                tbEventDescription. Text = wei. Description;
                tbEventDescription. CssClass = "workflowTableTextbox";
                tbEventDescription. AutoPostBack = true;
                tbEventDescription. Attributes[ "onkeydown" ] = "return limitInput();";
                tbEventDescription. TextChanged += new EventHandler( tbEventDescription_TextChanged );
                cell2 = new TableCell( );
                cell2. CssClass = "workflowTableNone";
                cell2. Controls. Add( tbEventDescription );
                row = new TableRow( );
                row. Cells. Add( cell1 );
                row. Cells. Add( cell2 );
                table_EventList. Rows. Add( row );
            }
        }
        //各Decimal属性信息
        if ( parser. DecimalInfoList. Count > 0 )
        {
            foreach ( RuleDataInfo rdi in parser. DecimalInfoList )
            {
                cell1 = new TableCell( );
                cell1. CssClass = "workflowTableBody";
                cell1. Text = rdi. Name;
                TextBox tbRuleDataDescription = new TextBox( );
                tbRuleDataDescription. ID = rdi. Name;
                tbRuleDataDescription. Text = rdi. Description;
                tbRuleDataDescription. CssClass = "workflowTableTextbox";
                tbRuleDataDescription. AutoPostBack = true;
                tbRuleDataDescription. Attributes[ "onkeydown" ] = "return limitInput();";
                tbRuleDataDescription. TextChanged += new EventHandler( tbRuleDataDescription_TextChanged );
                cell2 = new TableCell( );
                cell2. CssClass = "workflowTableNone";
                cell2. Controls. Add( tbRuleDataDescription );
                row = new TableRow( );
                row. Cells. Add( cell1 );
                row. Cells. Add( cell2 );
                table_DecimalList. Rows. Add( row );
            }
        }

        //各枚举属性信息
        if ( parser. EnumInfoList. Count > 0 )
        {
            foreach ( RuleDataInfo rdi in parser. EnumInfoList )
            {
                cell1 = new TableCell( );
                cell1. CssClass = "workflowTableBody";
                TextBox tbRuleDataDescription = new TextBox( );
                tbRuleDataDescription. ID = rdi. Name;
                tbRuleDataDescription. Text = rdi. Description;
                tbRuleDataDescription. CssClass = "workflowTableTextbox";
                tbRuleDataDescription. Width = 60;
                tbRuleDataDescription. AutoPostBack = true;
                tbRuleDataDescription. Attributes[ "onkeydown" ] = "return limitInput();";
                tbRuleDataDescription. TextChanged += new EventHandler( tbRuleDataDescription_TextChanged );
                ImageButton btnAddItem = new ImageButton( );
                btnAddItem. ImageUrl = "~\\images\\WorkflowButton\\Button_AddItem.png";
                btnAddItem. ToolTip = "增加一个选项";
                btnAddItem. Attributes[ "EnumName" ] = rdi. Name;
                btnAddItem. Click += new ImageClickEventHandler( btnAddItem_Click );
                cell1. Controls. Add( tbRuleDataDescription );
                cell1. Controls. Add( new LiteralControl( "&nbsp" ) );
                cell1. Controls. Add( btnAddItem );
                cell2 = new TableCell( );
                cell2. CssClass = "workflowTableNone";

                row = new TableRow( );
                row. Cells. Add( cell1 );
                row. Cells. Add( cell2 );
                table_EnumList. Rows. Add( row );

                //呈现枚举项列表
                EnumItemReorderList list = Page. LoadControl( "~/Module/FM2E/WorkflowManager/WorkflowEditor/EnumItemReorderList.ascx" ) as EnumItemReorderList;
                //使用绑定之前，一定要将模板控件加入控件树中！
                cell2. Controls. Add( list );
                list. EnumItemListBind( rdi. EnumItemList , rdi. Name , parserGuid , CheckEnumItemUsed , UpdateExpAfterEnumItemChanged );
            }
        }
        #endregion

        #region 呈现工作流各状态的WorkflowStateViewer
        int i = 0;
        Point pos = new Point( 300 , 100 );
        foreach ( KeyValuePair<String , StateActivityInfo> s in parser. StateActivityCollection )
        {
            if ( s. Value. EventDrivenActivityCollection. Count > 0 )
            {
                WorkflowStateViewer newViewer = Page. LoadControl( "~/Module/FM2E/WorkflowManager/WorkflowEditor/WorkflowStateViewer.ascx" ) as WorkflowStateViewer;
                newViewer. Position = pos;
                newViewer. ZIndex = i;
                newViewer. ContainerClientID = panel_WorkflowContent. ClientID;
                newViewer. StateBind( parser. WorkflowInfo. HasRule , Request. QueryString[ "workflow" ] , s. Value , parserGuid );
                panel_WorkflowContent. Controls. Add( newViewer );
                ++i;
                pos. Y += 30;
                pos. X += 20;
            }
        }
        #endregion
    }

    #region 对工作流的修改操作
    /// <summary>
    /// 修改工作流的描述
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tb_WorkflowDescription_TextChanged( object sender , EventArgs e )
    {
        TextBox tb = sender as TextBox;
        IWorkflowParser parser = Session[ ( ViewState[ "ParserGuid" ] as String ) ] as IWorkflowParser;
        parser. ChangeWorkflowDescription( tb. Text );
    }
    /// <summary>
    /// 修改状态的描述
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void tbStateDescription_TextChanged( object sender , EventArgs e )
    {
        TextBox tb = sender as TextBox;
        IWorkflowParser parser = Session[ ( ViewState[ "ParserGuid" ] as String ) ] as IWorkflowParser;
        try
        {
            parser. ChangeStateDescription( tb. ID , tb. Text );
        }
        catch ( WorkflowException ex )
        {
            ScriptManager. RegisterStartupScript( ( System. Web. UI. Page )HttpContext. Current. CurrentHandler , this. GetType( ) , "" , String. Format( "var m = '{0}'; window.setTimeout(\"alert(m)\",500);" , ex. Message ) , true );
            tb. Text = ex. WorkflowData;
            return;
        }

        //同步各StateViewer
        foreach ( WorkflowStateViewer wsv in panel_WorkflowContent. Controls )
        {
            if ( wsv != null )
                wsv. changeStateDescription( tb. ID , tb. Text );
        }
    }
    /// <summary>
    /// 修改事件的描述
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void tbEventDescription_TextChanged( object sender , EventArgs e )
    {
        TextBox tb = sender as TextBox;
        IWorkflowParser parser = Session[ ( ViewState[ "ParserGuid" ] as String ) ] as IWorkflowParser;
        try
        {
            parser. ChangeEventDescription( tb. ID , tb. Text );
        }
        catch ( WorkflowException ex )
        {
            ScriptManager. RegisterStartupScript( ( System. Web. UI. Page )HttpContext. Current. CurrentHandler , this. GetType( ) , "" , String. Format( "var m = '{0}'; window.setTimeout(\"alert(m)\",500);" , ex. Message ) , true );
            tb. Text = ex. WorkflowData;
            return;
        }

        //同步各StateViewer
        foreach ( WorkflowStateViewer wsv in panel_WorkflowContent. Controls )
        {
            if ( wsv != null )
                wsv. changeEventDescription( tb. ID , tb. Text );
        }
    }

    /// <summary>
    /// 修改规则属性的描述
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void tbRuleDataDescription_TextChanged( object sender , EventArgs e )
    {
        TextBox tb = sender as TextBox;
        IWorkflowParser parser = Session[ ( ViewState[ "ParserGuid" ] as String ) ] as IWorkflowParser;
        String old = null;
        try
        {
            old = parser. ChangeRuleDataDescription( tb. ID , tb. Text );
        }
        catch ( WorkflowException ex )
        {
            ScriptManager. RegisterStartupScript( ( System. Web. UI. Page )HttpContext. Current. CurrentHandler , this. GetType( ) , "" , String. Format( "var m = '{0}'; window.setTimeout(\"alert(m)\",500);" , ex. Message ) , true );
            tb. Text = ex. WorkflowData;
            return;
        }

        int type = 1;
        if ( parser. RuleDataInfoCollection[ tb. ID ]. IsEnum )
            type = 2;

        //同步各StateViewer
        foreach ( WorkflowStateViewer wsv in panel_WorkflowContent. Controls )
        {
            if ( wsv != null )
                wsv. changeRuleDataDescription( old , tb. Text, type );
        }
    }

    /// <summary>
    /// 枚举项的描述变化后，同步所有的规则表达式
    /// </summary>
    /// <param name="oldDp"></param>
    /// <param name="newDp"></param>
    void UpdateExpAfterEnumItemChanged( String oldDp , String newDp )
    {
        foreach ( WorkflowStateViewer wsv in panel_WorkflowContent. Controls )
        {
            if ( wsv != null )
                wsv. changeRuleDataDescription( oldDp , newDp , 3 );
        }
    }

    /// <summary>
    /// 检查Enum项是否已在规则中被使用
    /// </summary>
    /// <param name="itemDp"></param>
    /// <returns>
    /// true：已被使用
    /// false：未被使用
    /// </returns>
    bool CheckEnumItemUsed(String itemDp)
    {
        foreach ( WorkflowStateViewer wsv in panel_WorkflowContent. Controls )
        {
            if ( wsv != null && wsv. CheckEnumItemUsed( itemDp ) )
                return true;
        }
        return false;
    }

    /// <summary>
    /// 增加一个枚举项
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void btnAddItem_Click( object sender , ImageClickEventArgs e )
    {
        ImageButton ibtn = sender as ImageButton;
        IWorkflowParser parser = Session[ ( ViewState[ "ParserGuid" ] as String ) ] as IWorkflowParser;
        RuleDataInfo rdi = parser. RuleDataInfoCollection[ ibtn. Attributes[ "EnumName" ] ];
        int index = rdi. MaxEnumItemIndex + 1;
        String newItemDp = "新建选项" + index. ToString( );
        if(parser.AddEnumItem(ibtn.Attributes ["EnumName"], newItemDp))
        {
            ((ibtn.Parent .Parent as TableRow).Cells[1].Controls [0] as EnumItemReorderList).Rebind (rdi.EnumItemList);
        }
    }
    #endregion

    /// <summary>
    /// 保存对工作流的修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_SaveAll_Click( object sender , EventArgs e )
    {
        String workflowName = this. Request. QueryString[ "name" ];
        WorkflowHelper w = new WorkflowHelper( );
        IWorkflowParser parser = Session[ ( ViewState[ "ParserGuid" ] as String ) ] as IWorkflowParser;
        w. SaveWorkflowAsNewName( parser );
        ScriptManager. RegisterStartupScript( ( System. Web. UI. Page )HttpContext. Current. CurrentHandler , typeof( WorkflowStateViewer ) , "" , "window.alert(\"工作流定义保存成功!\");" , true );
    }

    /// <summary>
    /// 还原工作流
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Withdraw_Click( object sender , EventArgs e )
    {
        Session. Remove( ( ViewState[ "ParserGuid" ] as String ) );
        Response. Redirect( "WorkflowEditor.aspx?name=" + Request. QueryString[ "name" ] , false );
    }
}