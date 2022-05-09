/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				TabOptionWebControls.cs                  	                		*
 *      Description:																*
 *				 选项卡Web控件     		   							        	    *
 *      Author:																		*
 *				Lzppcc														        *
 *				Lzppcc@hotmail.com													*
 *				http://www.supesoft.com												*
 *      Finish DateTime:															*
 *				2007年8月6日														*
 *      History:																	*
 ***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.Drawing.Design;
using WebUtility.Components;
using System.Collections.Specialized;

namespace WebUtility.WebControls
{
    /// <summary>
    /// 选项卡Web控件
    /// </summary>
    [Designer(typeof(MyContainerControlDesigner))]
    [ControlBuilder(typeof(MultiViewControlBuilder))]
    [
    Description("选项卡Web控件"),
    ToolboxData("<{0}:TabOptionWebControls runat=\"server\"></{0}:TabOptionWebControls>"),
    ParseChildren(typeof(TabOptionItem)),
    AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal), 
    AspNetHostingPermission(SecurityAction.InheritanceDemand, 
        Level = AspNetHostingPermissionLevel.Minimal)]
    public class TabOptionWebControls : Control
    {
        #region "Private Variables"
        private string _TabHtml =
        @"<!-- 选项卡 Start -->
            <table cellpadding={0}0{0} cellspacing={0}0{0} border={0}0{0} style={0} text-align:center; width:100%;height:inherit; margin-top:0px;{0}>
            <tr style={0}height:19px;{0}>
            <td style={0}height: 19px; vertical-align:bottom;{0}>
            <table cellpadding={0}0{0} cellspacing={0}0{0} border={0}0{0} style={0}text-align:center; width:100%; height:100%;margin-left:-1px;{0}>
            <tr>
            <!--选项按钮 Start-->
            {2}
            <!--一个按钮 Start-->
            <td style={0}height: 18px{0}>&nbsp;</td>
            </tr>
            </table>
            </td>
            </tr>
            <tr>
            <td valign={0}top{0}>
            <input type={0}hidden{0} id={0}TabSelectIndex{0} name={0}TabSelectIndex{0} value={0}0{0}/>
            <table cellpadding={0}0{0} cellspacing={0}0{0} border={0}0{0} style={0}text-align:center; width:100%;border-top:solid 1px #a9bace;{0}>
            <tr>
            <td style={0}background-image:url({1}tab_border_l.gif); background-repeat:repeat-y; width:6px; background-position:left;{0}></td>
            <td>
        ";
        private string _TabHtmlEnd =
        @"</td>
            <td style={0}background-image:url({1}tab_border_r.gif); background-repeat:repeat-y; width:6px; background-position:right;{0}></td>
            </tr>
            <tr>
            <td style={0}background-image:url({1}tab_corner_lb.gif); background-repeat:no-repeat; height:7px; background-position:top right;{0}></td>
            <td style={0}background-image:url({1}tab_border_b.gif); background-repeat:repeat-x; height:7px; background-position:bottom;{0}></td>
            <td style={0}background-image:url({1}tab_corner_rb.gif); background-repeat:no-repeat; height:7px; width: 6px;  background-position:top left{0}></td>
            </tr>
            </table>
            </td></tr>
            <tr><td>&nbsp;</td></tr>
            </table>
        ";
        private string _TabButton =
        @" <td id={0}tabImgLeft_{2}{0} style={0} display:{5}; background-image:url({1}tab_unactive_left.gif); background-repeat:no-repeat; background-position:bottom right; width:3px; height: 18px;{0}>&nbsp;</td>
           <td id={0}tabLabel_{2}{0} onclick={0}javascript:tabClick({2},{3}){0} style={0}cursor:pointer;  display:{5}; background-image:url({1}tab_unactive_bg.gif); background-repeat:repeat-x; background-position:bottom; width:89px; height: 18px; vertical-align:baseline;{0}>{4}</td>
           <td id={0}tabImgRight_{2}{0} onclick={0}javascript:tabClick({2},{3}){0} style={0} display:{5}; background-image:url({1}tab_unactive_right.gif); background-repeat:no-repeat; background-position:bottom; width:23px; height: 18px;{0}>&nbsp;</td>";

        private string _TabButton1 =
        @" <td id={0}tabImgLeft_{2}{0} style={0} display:{5}; background-image:url({1}tab_unactive_left.gif); background-repeat:no-repeat; background-position:bottom right; width:3px; height: 18px;{0}>&nbsp;</td>
           <td id={0}tabLabel_{2}{0} onclick={0}javascript:tabClick({2},{3}){0} style={0}cursor:pointer; display:{5}; background-image:url({1}tab_unactive_bg.gif); background-repeat:repeat-x; background-position:bottom; width:89px; height: 18px; vertical-align:baseline;{0}>{4}</td>";

        private string _TabButtonx =
         @" <td id={0}tabImgLeft_{2}{0} style={0} display:{5}; background-image:url({1}tab_unactive_left.gif); background-repeat:no-repeat; background-position:bottom right; width:3px; height: 18px;{0}>&nbsp;</td>
           <td id={0}tabLabel_{2}{0} onmouseover={0}javascript:tabClick({2},{3}){0} style={0}cursor:pointer;  display:{5}; background-image:url({1}tab_unactive_bg.gif); background-repeat:repeat-x; background-position:bottom; width:89px; height: 18px; vertical-align:baseline;{0}>{4}</td>
           <td id={0}tabImgRight_{2}{0} onmouseover={0}javascript:tabClick({2},{3}){0} style={0} display:{5}; background-image:url({1}tab_unactive_right.gif); background-repeat:no-repeat; background-position:bottom; width:23px; height: 18px;{0}>&nbsp;</td>";
        private string _TabButton1x =
        @" <td id={0}tabImgLeft_{2}{0} style={0} display:{5}; background-image:url({1}tab_unactive_left.gif); background-repeat:no-repeat; background-position:bottom right; width:3px; height: 18px;{0}>&nbsp;</td>
           <td id={0}tabLabel_{2}{0} onmouseover={0}javascript:tabClick({2},{3}){0} style={0}cursor:pointer; display:{5}; background-image:url({1}tab_unactive_bg.gif); background-repeat:repeat-x; background-position:bottom; width:89px; height: 18px; vertical-align:baseline;{0}>{4}</td>";

        private string _TabImagesPath = "~/images/Menu/";
        private string TabButton;
        /// <summary>
        /// 生成选项卡
        /// </summary>
        private void CreateTabHtml()
        {
            StringBuilder sbTabButton = new StringBuilder();

            if (TaboptionItems.Count > 0)
            {
                string IsDisp = "block";
                for (int i = 0; i < TaboptionItems.Count; i++)
                {
                    IsDisp = "";
                    if (TaboptionItems[i].Visible == false)
                        IsDisp = "none";

                    if (i == TaboptionItems.Count - 1)  //最后一个
                    {
                        if (TabClick)
                            sbTabButton.AppendFormat(_TabButton, "\"", TabImagesPath, i , TaboptionItems.Count, TaboptionItems[i].Tab_Name, IsDisp);
                        else sbTabButton.AppendFormat(_TabButtonx, "\"", TabImagesPath, i, TaboptionItems.Count, TaboptionItems[i].Tab_Name, IsDisp);
                    }
                    else
                    {
                        if (TabClick)
                        sbTabButton.AppendFormat(_TabButton1, "\"", TabImagesPath, i , TaboptionItems.Count, TaboptionItems[i].Tab_Name, IsDisp);
                    else sbTabButton.AppendFormat(_TabButton1x, "\"", TabImagesPath, i, TaboptionItems.Count, TaboptionItems[i].Tab_Name, IsDisp);
                    }
                }
            }
            TabButton = sbTabButton.ToString();
        }

        private string _TabJs =
        @"<script type={0}text/javascript{0} language={0}javascript{0}>
        function tabClick(idx,count) {1}
          for (i_tr = 0; i_tr < count; i_tr++) {1}
            if (i_tr == idx) {1}
              var tabImgLeft = document.getElementById({0}tabImgLeft_{0} + idx);
              var tabImgRight={0} {0};
              var last=0;
              if(idx==count-1)
              {1}
                last=1;
                tabImgRight = document.getElementById({0}tabImgRight_{0} + idx);
              {2}
              var tabLabel = document.getElementById({0}tabLabel_{0} + idx);
              var tabContent = document.getElementById({0}tabContent_{0} + idx);

              tabImgLeft.style.backgroundImage={0}url({3}tab_active_left.gif){0};
              tabImgLeft.style.Width={0}3px{0};
              if(last==1)
              {1}
                tabImgRight.style.backgroundImage={0}url({3}tab_active_right.gif){0};
              {2}
              tabLabel.style.backgroundImage={0}url({3}tab_active_bg.gif){0};
              tabContent.style.display = {0}block{0};
              continue;
            {2}
            var tabImgLeft = document.getElementById({0}tabImgLeft_{0} + i_tr);
            var last=0;
              if(i_tr==count-1)
              {1}
                last=1;
                tabImgRight = document.getElementById({0}tabImgRight_{0} + i_tr);
              {2}
            var tabLabel = document.getElementById({0}tabLabel_{0} + i_tr);
            var tabContent = document.getElementById({0}tabContent_{0} + i_tr);

            tabImgLeft.style.backgroundImage={0}url({3}tab_unactive_left.gif){0};
            tabImgLeft.style.Width={0}3px{0};
              
             if(last==1)
               tabImgRight.style.backgroundImage={0}url({3}tab_unactive_right.gif){0};
            tabLabel.style.backgroundImage={0}url({3}tab_unactive_bg.gif){0};
            tabContent.style.display = {0}none{0};
          {2}
          
          document.getElementById({0}{4}{0}).value=idx;
        {2}
        tabClick({5},{6});
       </script>";

        private string _HiddenInputName = "TabSelectIndex";
      //  private string _HiddenSelectIndex = "<input type='hidden' ID='{0}' name='{0}' value='{1}'>";

        #endregion

        #region "Public Variables"
        /// <summary>
        /// 选择/设置选项卡
        /// </summary>
        public int SelectIndex
        {
            get {

                object m = ViewState["SelectIndex"];
                return m == null ? 0 : Convert.ToInt32(m);
            }
            set {
                if (value < 0 || value >= TaboptionItems.Count)
                {
                    value = 0;
                }
                ViewState["SelectIndex"] = value;
            }
        }
        /// <summary>
        /// 是否需要点击进行选项卡切换
        /// </summary>
        [Description("读取/设置选项卡切换方法"), Category("控制"), DefaultValue(true)]
        public bool TabClick
        {
            get { object m = ViewState["TabClick"]; return m == null ? true : Convert.ToBoolean(m) ; }
            set { ViewState["TabClick"] = value; }
        }
        /// <summary>
        /// 读取/设置选项卡图片路径
        /// </summary>
        [Description("读取/设置选项卡图片路径"), Category("外观"), DefaultValue("~/images/Menu/")]
        public string TabImagesPath
        {
            get
            {
                object m = ViewState["TabImagesPath"];
                return m == null ? ResolveClientUrl(_TabImagesPath) : ResolveClientUrl(m.ToString());
            }
            set
            {
                ViewState["TabImagesPath"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 重写RenderContents方法
        /// </summary>
        /// <param name="writer"></param>
        protected override void  Render(HtmlTextWriter writer)
        {
            CreateTabHtml();
            writer.Write(_TabHtml,"\"", TabImagesPath, TabButton);
            for (int i = 0; i < TaboptionItems.Count; i++)
            {
                writer.Write("<!--内容框Start-->");
                writer.Write("<div id={0}tabContent_{1}{0} style={0}display:none; text-align:center;{0}>","\"", i);
                TaboptionItems[i].RenderControl(writer);
                writer.Write("</div>");
                writer.Write("<!--内容框End-->");
            }
            writer.Write(_TabHtmlEnd,"\"", TabImagesPath);
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(string), "TabJs", string.Format(_TabJs,"\"",  "{", "}",TabImagesPath,_HiddenInputName, SelectIndex, TaboptionItems.Count));
        }

        /// <summary>
        /// 重写增加控件方法
        /// </summary>
        /// <param name="obj"></param>
        protected override void AddParsedSubObject(object obj)
        {
            if (obj is TabOptionItem)
            {
                this.Controls.Add((TabOptionItem)obj);
            }
            else if (!(obj is LiteralControl))
            {
                throw new HttpException(string.Format("MultiView_cannot_have_children_of_type", new object[] { obj.GetType().Name }));
            }
        }

        /// <summary>
        /// 重写加载事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            if (Page.Request.Form[_HiddenInputName] != null)
                this.SelectIndex = Convert.ToInt32(Page.Request.Form[_HiddenInputName]);
            base.OnInit(e);
        }

        /// <summary>
        /// 重写创建子控件集合
        /// </summary>
        /// <returns></returns>
        protected override ControlCollection CreateControlCollection()
        {
            return new TaboptionItemCollection(this);
        }

        

        /// <summary>
        /// 获取子控件集合
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerDefaultProperty), Browsable(false)]
        public virtual TaboptionItemCollection TaboptionItems
        {
            get
            {
                return (TaboptionItemCollection)this.Controls;
            }
        }

    }
}
