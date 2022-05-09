using System;
using System. Linq;
using System. Drawing;
using System. Collections. Generic;
using System. ComponentModel;
using System. Text;
using System. Web;
using System. Web. UI;
using System. Web. UI. WebControls;
using System. Collections;
using System. Text. RegularExpressions;

using AjaxControlToolkit;
using FM2E.WorkflowLayer;

public partial class WorkflowStateViewer : System. Web. UI. UserControl
{
    const String IMAGE_DIR = "~\\images\\WorkflowButton\\";

    #region Properties
    Point _position;
    public Point Position
    {
        get
        {
            return _position;
        }
        set
        {
            _position = value;
        }
    }
    public int ZIndex
    {
        get;
        set;
    }
    public String ContainerClientID
    {
        get;
        set;
    }
    #endregion

    #region 初始化与显示的Functions
    protected void Page_Load( object sender , EventArgs e )
    {
        this. PreRender += new EventHandler( WorkflowStateViewer_PreRender );   
    }

    void WorkflowStateViewer_PreRender( object sender , EventArgs e )
    {
        //按照Viewer当前的状态来显示
        if ( field_DisplayMode. Value == "none" )
            panel_Content. Style. Add( "display" , "none" );
        else
            panel_Content. Style. Add( "display" , "block" );

        //改变Viewer的位置
        panel_StateViewer. Style. Value = String. Format( "position:absolute;top:{0}px;left:{1}px;z-Index:{2}" , _position. Y , _position. X , ZIndex );

        //获得现有规则表达式，并填入ruleEditorBox中，同时focus（要解决局部刷新的问题）
        foreach ( Panel panel in panel_Content. Controls )
        {
            ControlCollection cc = ( panel. Controls[ 0 ] as Table ). Rows[ 0 ]. Cells[ 2 ]. Controls;
            if ( cc[ 0 ] is TextBox )
            {
                for ( int i = 0 ; i < cc. Count ; i += 7 )
                {
                    TextBox ruleBox = cc[ i ] as TextBox;
                    if ( ruleBox != null )
                    {
                        TextBox ruleEditorBox = ruleBox. Parent. Controls[ i + 3 ]. Controls[ 0 ] as TextBox;
                        Button enterButton = ruleBox. Parent. Controls[ i + 3 ]. Controls[ 2 ] as Button;
                        //点击规则框自动对焦编辑框
                        String js = String. Format( "var tmp = $('{0}'); tmp.value=$('{1}').value; tmp.focus();" , ruleEditorBox. ClientID , ruleBox. ClientID);
                        ruleBox. Attributes[ "onclick" ] = js;
                        //按下Enter提交编辑好的规则
                        ruleEditorBox. Attributes[ "onkeydown" ] = String.Format ("if(window.event.keyCode==13){{ $('{0}').onclick(); return false;}}", enterButton.ClientID);
                    }
                }
            }
        }

        //增加双击窗口标题栏自动隐藏的功能
        panel_StateDescription. Attributes[ "ondblclick" ] = String. Format( "togglePanelVisible('{0}','{1}');recordDisplayMode('{2}','{3}');" ,ContainerClientID, panel_Content. ClientID, field_DisplayMode.ClientID, panel_Content.ClientID );

        //增加点击窗口自动到最前面的功能
        panel_StateViewer. Attributes[ "onclick" ] = String. Format( "clickOnWindow('{0}','{1}');" , ContainerClientID , panel_StateViewer. ClientID );
    }

    public void StateBind( bool hasRule , String workflowName , StateActivityInfo bindState , String parserGuid )
    {
        ViewState[ "ParserGuid" ] = parserGuid;

        //填充状态名
        lb_StateDescription. Text = bindState. StateDescription;
        lb_StateDescription. Attributes[ "StateName" ] = bindState. StateName;
        foreach ( KeyValuePair<String , EventDrivenActivityInfo> t in bindState. EventDrivenActivityCollection )
        {
            EventDrivenActivityInfo edai = t. Value;
            if ( edai. IsParallelBranch )
                continue;

            //填充事件描述
            TableCell firstCell = new TableCell( );
            firstCell. CssClass = "firstCell";
            Label textLabel = null;
            if ( edai. IsParallel )
            {
                foreach ( EventDrivenActivityInfo edai2 in edai. ParallelEventDrivenList )
                {
                    textLabel = new Label( );
                    textLabel. Text = edai2. EventDescription;
                    textLabel. Attributes[ "EventName" ] = edai2. EventName;
                    textLabel. CssClass = "eventBlock";
                    firstCell. Controls. Add( textLabel );
                }
            }
            else
            {
                textLabel = new Label( );
                textLabel. Text = edai. EventDescription;
                textLabel. Attributes[ "EventName" ] = edai. EventName;
                textLabel. CssClass = "eventBlock";
                firstCell. Controls. Add( textLabel );
            }

            //指示箭头按钮（增加规则）
            TableCell secondCell = new TableCell( );
            secondCell. CssClass = "secondCell";
            ImageButton newButton = new ImageButton( );
            newButton. ID = edai. EventName;
            newButton. ImageUrl = IMAGE_DIR + "Button_AddRule.gif";
            newButton. Attributes[ "EventName" ] = edai. EventName;
            if ( hasRule )
            {
                newButton. ToolTip = "添加规则";
                newButton. Click += new ImageClickEventHandler( AddRule_Click );
            }

            secondCell. Controls. Add( newButton );

            Panel eventDrivenPanel = new Panel( );
            eventDrivenPanel. CssClass = "eventDrivenPanel";
            eventDrivenPanel. Controls. Add( new Table( ) );
            TableRow row = new TableRow( );
            row. Cells. Add( firstCell );
            row. Cells. Add( secondCell );

            //填充跳转列表
            DisplaySetStateList( row , edai. EventName );

            eventDrivenPanel. Controls[ 0 ]. Controls. Add( row );
            panel_Content. Controls. Add( eventDrivenPanel );
        }
        ( panel_Content. Controls[ panel_Content. Controls. Count - 1 ] as Panel ). CssClass = null;
    }

    void DisplaySetStateList( TableRow row , String eventName )
    {
        TableCell thirdCell = new TableCell( );
        thirdCell. CssClass = "thirdCell";
        IWorkflowParser parser = Session[ ( ViewState[ "ParserGuid" ] as String ) ] as IWorkflowParser;
        StateActivityInfo bindState = parser. GetStateActivityInfoByName( lb_StateDescription. Attributes[ "StateName" ] );
        EventDrivenActivityInfo edai = bindState. EventDrivenActivityCollection[ eventName ];
        
        if ( edai. HasRule )
        {
            int i = 0;
            foreach ( KeyValuePair<String , IfElseBranchActivityInfo> b in edai. IfElseBranchCollection )
            {
                IfElseBranchActivityInfo branch = b. Value;

                //生成规则显示框
                TextBox ruleBox = new TextBox( );
                ruleBox. CssClass = "ruleBox";
                ruleBox. ID = branch. SetStateActivityBody. Name + "_RuleBox_" + i. ToString( );
                ruleBox. Text = branch. ExpString;
                ruleBox. ReadOnly = true;
                ruleBox. Attributes[ "EventName" ] = edai. EventName;
                ruleBox. Attributes[ "SetStateName" ] = branch. SetStateActivityBody. Name;

                //生成删除相应规则的按钮
                ImageButton btnDelRule = new ImageButton( );
                btnDelRule. ToolTip = "删除规则";
                btnDelRule. ID = branch. SetStateActivityBody. Name + "_DelRule_" + i. ToString( );
                btnDelRule. ImageUrl = IMAGE_DIR + "Button_Delete.gif";
                btnDelRule. Attributes[ "SetStateName" ] = branch. SetStateActivityBody. Name;
                btnDelRule. Attributes[ "EventName" ] = edai. EventName;
                btnDelRule. Attributes[ "onclick" ] = "if(!confirm('确认删除该规则？')) return false;";
                btnDelRule. Click += new ImageClickEventHandler( btnDelRule_Click );

                //生成规则的动态编辑框
                Panel ruleEditor = GetNewRuleEditorPanel( edai. EventName , i );
                ruleEditor. ID = branch. SetStateActivityBody. Name + "_RuleEditor_" + i. ToString( );
                TextBox ruleEditorBox = ( ruleEditor. Controls[ 0 ] as TextBox );
                ruleEditorBox. Attributes[ "EventName" ] = edai. EventName;
                ruleEditorBox. Attributes[ "SetStateName" ] = branch. SetStateActivityBody. Name;
                ruleEditorBox. Attributes[ "TriggerIndex" ] = thirdCell. Controls. Count. ToString( );

                PopupControlExtender newExtender = new PopupControlExtender( );
                newExtender. TargetControlID = ruleBox. ID;
                newExtender. PopupControlID = ruleEditor. ID;
                newExtender. Position = PopupControlPopupPosition. Bottom;
                newExtender. CommitProperty = "value";
                newExtender. CommitScript = "e.value";
                ruleEditor. Controls. Add( newExtender );

                DropDownList newList = new DropDownList( );
                newList. ID = branch. SetStateActivityBody. Name + "_list_" + i. ToString( );
                newList. CssClass = "setStateList";
                newList. DataSource = bindState. AllStateInfoList;
                newList. DataTextField = "Description";
                newList. DataValueField = "Name";
                newList. DataBind( );
                newList. Attributes[ "EventName" ] = edai. EventName;
                newList. Attributes[ "SetStateName" ] = branch. SetStateActivityBody. Name;
                newList. SelectedIndex = bindState. AllStateInfoList. FindIndex( p => p. Name == branch. SetStateActivityBody. TargetStateName );
                newList. SelectedIndexChanged += new EventHandler( SetStateList_SelectedIndexChanged );

                //将上述生成的控件依次添加进setStateCell
                thirdCell. Controls. Add( ruleBox );
                thirdCell. Controls. Add( new LiteralControl( "&nbsp&nbsp&nbsp" ) );
                thirdCell. Controls. Add( btnDelRule );
                thirdCell. Controls. Add( ruleEditor );
                thirdCell. Controls. Add( new LiteralControl( "<br/>" ) );
                thirdCell. Controls. Add( newList );
                thirdCell. Controls. Add( new LiteralControl( "<br/>" ) );

                ++i;
            }
        }
        else
        {
            DropDownList newList = new DropDownList( );
            newList. ID = edai. SingleSetStateActivity. Name + "_list";
            newList. CssClass = "setStateList";
            newList. DataSource = bindState. AllStateInfoList;
            newList. DataTextField = "Description";
            newList. DataValueField = "Name";
            newList. DataBind( );
            newList. Attributes[ "EventName" ] = edai. EventName;
            newList. Attributes[ "SetStateName" ] = edai. SingleSetStateActivity. Name;
            newList. SelectedIndexChanged += new EventHandler( SetStateList_SelectedIndexChanged );
            newList. SelectedIndex = bindState. AllStateInfoList. FindIndex( p => p. Name == edai. SingleSetStateActivity. TargetStateName );
            thirdCell. Controls. Add( newList );
        }
        row. Cells. Add( thirdCell );
    }

    void UpdateStateDisplay( TableRow row , String eventName )
    {
        row. Cells. RemoveAt( 2 );
        DisplaySetStateList( row , eventName );
    }

    Panel GetNewRuleEditorPanel( String eventName , int index )
    {
        Panel panel = new Panel( );
        panel. CssClass = "ruleEditorPanel";
        TextBox ruleEditorBox = new TextBox( );
        ruleEditorBox. CssClass = "ruleEditorBox";
        ruleEditorBox. ID = eventName + "_EditorRuleBox_" + index. ToString( );
        Button btnEnter = new Button( );
        btnEnter. ID = eventName + "_btnEnter_" + index. ToString( );
        btnEnter. CssClass = "button_bak";
        btnEnter. Click += new EventHandler( btnEnter_Click );
        btnEnter. Text = "确定";
        btnEnter. UseSubmitBehavior = false;
        Button btnCancel = new Button( );
        btnCancel. ID = eventName + "_btnCancel_" + index. ToString( );
        btnCancel. CssClass = "button_bak";
        btnCancel. Click += new EventHandler( btnCancel_Click );
        btnCancel. Text = "取消";
        btnCancel. UseSubmitBehavior = false;
        panel. Controls. Add( ruleEditorBox );
        panel. Controls. Add( new LiteralControl( "<br/>" ) );
        panel. Controls. Add( btnEnter );
        panel. Controls. Add( new LiteralControl( "&nbsp&nbsp" ) );
        panel. Controls. Add( btnCancel );
        return panel;
    }

    #endregion

    #region 处理各种修改工作流操作事件的Functions
    /// <summary>
    /// 确认对规则的编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void btnEnter_Click( object sender , EventArgs e )
    {
        Button lbtn = sender as Button;
        PopupControlExtender extender = ( lbtn. Parent. Controls[ 5 ] ) as PopupControlExtender;
        TextBox ruleEditorBox = ( lbtn. Parent. Controls[ 0 ] as TextBox );

        IWorkflowParser parser = Session[ ( ViewState[ "ParserGuid" ] as String ) ] as IWorkflowParser;
        try
        {
            parser. ModifyRule( lb_StateDescription. Attributes[ "StateName" ] , ruleEditorBox. Attributes[ "EventName" ] , ruleEditorBox. Attributes[ "SetStateName" ] , ruleEditorBox. Text , false );
            ( ruleEditorBox. Parent. Parent. Controls[ Int32. Parse( ruleEditorBox. Attributes[ "TriggerIndex" ] ) ] as TextBox ). Text = ruleEditorBox. Text;
            extender. Commit( ruleEditorBox. Text );
        }
        catch ( System. Exception ex )
        {
            ScriptManager. RegisterStartupScript( ( System. Web. UI. Page )HttpContext. Current. CurrentHandler , this. GetType( ) , "" , String. Format( "var m = '{0}'; window.setTimeout(\"alert(m)\",500);" , ex. Message ) , true );
        }
    }

    /// <summary>
    /// 取消对规则的编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void btnCancel_Click( object sender , EventArgs e )
    {
        Button lbtn = sender as Button;
        ( ( lbtn. Parent. Controls[ 5 ] ) as PopupControlExtender ). Cancel( );
    }

    /// <summary>
    /// 增加一条规则
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void AddRule_Click( object sender , ImageClickEventArgs e )
    {
        IWorkflowParser parser = Session[ ( ViewState[ "ParserGuid" ] as String ) ] as IWorkflowParser;
        ImageButton btn = sender as ImageButton;
        parser. AddNewRule( lb_StateDescription. Attributes[ "StateName" ] , btn. Attributes[ "EventName" ] , String. Empty
 );
        UpdateStateDisplay( btn. Parent. Parent as TableRow , btn. Attributes[ "EventName" ] );
    }

    /// <summary>
    /// 改变状态跳转
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void SetStateList_SelectedIndexChanged( object sender , EventArgs e )
    {
        IWorkflowParser parser = Session[ ( ViewState[ "ParserGuid" ] as String ) ] as IWorkflowParser;
        DropDownList list = sender as DropDownList;
        parser. ChangeTargetState( lb_StateDescription. Attributes[ "StateName" ] , list. Attributes[ "EventName" ] , list. Attributes[ "SetStateName" ] , list. SelectedValue );
    }

    /// <summary>
    /// 删除一条规则
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void btnDelRule_Click( object sender , ImageClickEventArgs e )
    {
        IWorkflowParser parser = Session[ ( ViewState[ "ParserGuid" ] as String ) ] as IWorkflowParser;
        ImageButton btnDelRule = sender as ImageButton;
        parser. DeleteOldRule( lb_StateDescription. Attributes[ "StateName" ] , btnDelRule. Attributes[ "EventName" ] , btnDelRule. Attributes[ "SetStateName" ] );
        UpdateStateDisplay( btnDelRule. Parent. Parent as TableRow , btnDelRule. Attributes[ "EventName" ] );
    }

    #endregion

    #region 处理各种工作流基础信息变化的Functions
    public void changeStateDescription( String stateName , String newDp )
    {
        //修改Title
        if ( lb_StateDescription. Attributes[ "StateName" ] == stateName )
            lb_StateDescription. Text = newDp;

        //修改下拉菜单
        foreach (Panel panel in panel_Content .Controls)
        {
            if(panel != null)
            {
                ControlCollection cc = ( panel. Controls[ 0 ] as Table ). Rows[ 0 ]. Cells[ 2 ]. Controls;
                DropDownList list = null;
                if ( ( list = cc[ 0 ] as DropDownList ) != null || ( list = cc[ 5 ] as DropDownList ) != null )
                    ( list. Items. FindByValue( stateName ) ). Text = newDp;
            }

        }
    }

    public void changeEventDescription( String eventName , String newDp )
    {
        foreach ( Panel p in panel_Content. Controls )
        {
            ControlCollection cc = ( p. Controls[ 0 ] as Table ). Rows[ 0 ]. Cells[ 0 ]. Controls;
            foreach ( Label lb in cc )
            {
                if ( lb != null && lb. Attributes[ "EventName" ] == eventName )
                {
                    lb. Text = newDp;
                    return;
                }
            }
        }
    }

    /// <summary>
    /// 改变规则数据的描述
    /// </summary>
    /// <param name="oldDp"></param>
    /// <param name="newDp"></param>
    /// <param name="type">
    /// 1: Decimal属性描述
    /// 2: Enum属性描述
    /// 3: Enum项描述
    /// </param>
    public void changeRuleDataDescription( String oldDp , String newDp , int type )
    {
        IWorkflowParser parser = null;
        String template = null;
        switch ( type )
        {
            case 1:
                template = @"(?<=\b)+{0}(?=\b)+";               //左右两侧单词边界
                break;
            case 2:
                template = @"(?<=([^=]\b)|^)+{0}(?=\b)+";    //右侧单词边界，左侧单词边界前不为=号（或为开头）
                break;
            case 3:
                template = @"(?<=\b)+{0}(?=(\b[^=])|$)+";    //左侧单词边界，右侧单词边界后不为=号（或为末尾）
                break;
        }
        Regex r = new Regex( String. Format( template , oldDp ) );
        foreach ( Panel p in panel_Content. Controls )
        {
            ControlCollection cc = ( p. Controls[ 0 ] as Table ). Rows[ 0 ]. Cells[ 2 ]. Controls;
            if ( cc[ 0 ] is TextBox )
            {
                for ( int i = 0 ; i < cc. Count ; i += 7 )
                {
                    TextBox ruleBox = cc[ i ] as TextBox;
                    if ( ruleBox. Text. Contains( oldDp ) )
                    {
                        ruleBox. Text = r. Replace( ruleBox. Text , newDp );
                        if ( parser == null )
                            parser = Session[ ( ViewState[ "ParserGuid" ] as String ) ] as IWorkflowParser;
                        parser. ModifyRule( lb_StateDescription. Attributes[ "StateName" ] , ruleBox. Attributes[ "EventName" ] , ruleBox. Attributes[ "SetStateName" ] , ruleBox. Text , true );
                    }
                }
            }
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
    public bool CheckEnumItemUsed( String itemDp )
    {
        Regex r = new Regex( String. Format( @"(?<=\b)+{0}(?=(\b[^=])|$)+" , itemDp ) );
        foreach ( Panel p in panel_Content. Controls )
        {
            ControlCollection cc = ( p. Controls[ 0 ] as Table ). Rows[ 0 ]. Cells[ 2 ]. Controls;
            if ( cc[ 0 ] is TextBox )
            {
                for ( int i = 0 ; i < cc. Count ; i += 7 )
                {
                    if ( r. IsMatch( ( cc[ i ] as TextBox ). Text ) )
                        return true;
                }
            }
        }
        return false;
    }
    #endregion
}