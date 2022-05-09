using System;
using System. Collections. Generic;
using System. Linq;
using System. Web;
using System. Web. UI;
using System. Web. UI. WebControls;

using AjaxControlToolkit;
using FM2E.WorkflowLayer;

public partial class EnumItemReorderList : System. Web. UI. UserControl
{
    public delegate bool CheckStringDelegate( String dp );
    public delegate void UpdateStringDelegate( String oldDp , String newDp );
    private CheckStringDelegate _checkItemDpUsed;
    private UpdateStringDelegate _updataAllExp;

    protected void Page_Load( object sender , EventArgs e )
    {
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="dataSource"></param>
    /// <param name="enumName"></param>
    /// <param name="parserGuid"></param>
    /// <param name="checkItemDpUsed"></param>
    /// <param name="updateAllExp"></param>
    public void EnumItemListBind( List<EnumItemInfo> dataSource , String enumName , String parserGuid , 
        CheckStringDelegate checkItemDpUsed, UpdateStringDelegate updateAllExp )
    {
        _checkItemDpUsed = checkItemDpUsed;
        _updataAllExp = updateAllExp;
        field_ParserGuid. Value = parserGuid;
        field_EnumName. Value = enumName;
        Rebind( dataSource );
    }

    /// <summary>
    /// 重新绑定
    /// </summary>
    /// <param name="dataSource"></param>
    public void Rebind( List<EnumItemInfo> dataSource )
    {
        list_EnumItemList. DataSource = dataSource;
        list_EnumItemList. DataBind( );
    }

    #region 对枚举项的修改操作
    /// <summary>
    /// 顺序被改变
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void list_EnumItemList_ItemReorder( object sender , AjaxControlToolkit. ReorderListItemReorderEventArgs e )
    {
        IWorkflowParser parser = Session[ field_ParserGuid. Value ] as IWorkflowParser;
        parser. UpdateEnumItemOrder( field_EnumName. Value );
        Rebind( parser. RuleDataInfoCollection[ field_EnumName. Value ]. EnumItemList );
    }

    /// <summary>
    /// 改变描述
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ItemDescription_TextChanged( object sender , EventArgs e )
    {
        TextBox itemBox = sender as TextBox;
        if(itemBox.Text == String.Empty)
        {
            itemBox. Text = itemBox. Attributes[ "OldValue" ];
            return;
        }

        IWorkflowParser parser = Session[ field_ParserGuid. Value ] as IWorkflowParser;
        int itemValue = Int32.Parse (itemBox.Attributes["ItemValue"]);
        String old = null;
        try
        {
            old = parser. ChangeEnumItemDescription( field_EnumName. Value , itemValue , itemBox. Text );
            _updataAllExp( old , itemBox. Text );
        }
        catch (WorkflowException ex)
        {
            ScriptManager. RegisterStartupScript( ( System. Web. UI. Page )HttpContext. Current. CurrentHandler , this. GetType( ) , "" , String. Format( "var m = '{0}'; window.setTimeout(\"alert(m)\",500);" , ex. Message ) , true );
            itemBox.Text = old;
        }
    }

    /// <summary>
    /// 删除一项
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtn_DelItem_Click( object sender , ImageClickEventArgs e )
    {
        ImageButton ibtn = sender as ImageButton;
        IWorkflowParser parser = Session[ field_ParserGuid. Value ] as IWorkflowParser;
        int itemValue = Int32. Parse( ibtn. Attributes[ "ItemValue" ] );
        String itemDp = parser. RuleDataInfoCollection[ field_EnumName. Value ]. EnumItemList. Find( p => p. Value == itemValue ). Description;

        //检查该枚举项是否被规则表达式使用
        if(_checkItemDpUsed( itemDp ))
        {
            ScriptManager. RegisterStartupScript( ( System. Web. UI. Page )HttpContext. Current. CurrentHandler , this. GetType( ) , "" , String. Format( "var m = '{0}'; window.setTimeout(\"alert(m)\",500);" , "枚举项已在规则中使用，请先删除相关规则！" ) , true );
            return;
        }

        try
        {
            if(parser. RemoveEnumItem( field_EnumName. Value , itemValue ))
            {
                Rebind( parser. RuleDataInfoCollection[ field_EnumName. Value ]. EnumItemList );
            }
            else
            {
                ScriptManager. RegisterStartupScript( ( System. Web. UI. Page )HttpContext. Current. CurrentHandler , this. GetType( ) , "" , String. Format( "var m = '{0}'; window.setTimeout(\"alert(m)\",500);" , "删除该项会影响现有数据！" ) , true );
            }
        }
        catch ( WorkflowException ex )
        {
            ScriptManager. RegisterStartupScript( ( System. Web. UI. Page )HttpContext. Current. CurrentHandler , this. GetType( ) , "" , String. Format( "var m = '{0}'; window.setTimeout(\"alert(m)\",500);" , ex. Message ) , true );
        }
    }
    #endregion
}