/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				HeadMenuWebControls.cs	                                   			*
 *      Description:																*
 *				 头部菜单web控件 			    								    *
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
using System.Web.UI.WebControls;
using System.Drawing.Design;
using WebUtility.Components;


namespace WebUtility.WebControls
{
    /// <summary>
    /// 头部菜单web控件
    /// </summary>
    [
    AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.InheritanceDemand,
        Level = AspNetHostingPermissionLevel.Minimal),
    DefaultProperty("ButtonList"),
    ParseChildren(true, "ButtonList"),
    ToolboxData("<{0}:HeadMenuWebControls runat=\"server\"> </{0}:HeadMenuWebControls>"),
    Description("头部菜单web控件")
    ]
    public class HeadMenuWebControls:WebControl
    {
        /// <summary>
        /// 构造函数,不能少如果用泛型需要初始化
        /// </summary>
        public HeadMenuWebControls()
        {
            _ButtonList = new List<HeadMenuButtonItem>();
        }

        #region "Private Variables"
        private  List<HeadMenuButtonItem> _ButtonList;
        private string HeadMenuTemplateTxt =
        @"
	          <table border={0}0{0} cellpadding={0}0{0} cellspacing={0}0{0} width={0}100%{0} style={0}text-align:center; height:inherit; margin-top:0px;{0}>
              <tr style={0}display:{7}{0}>
                <td class={0}menubar_title{0} style={0}height: 38px; width:auto; text-align:left; vertical-align:bottom; padding-left:5;{0}><img border={0}0{0} src={0}{1}{0}  align={0}absmiddle{0} hspace={0}3{0} vspace={0}3{0}/>&nbsp;{2}</td>
                <td class='menubar_readme_text' valign='bottom' style={0}height: 38px; vertical-align:bottom; padding-right:5;{0}>{3}&nbsp;</td>
              </tr>
              <tr style={0}display:{7}{0}>
                <td colspan={0}2{0} style={0}height:3px;background-image:url({4}); background-repeat:no-repeat; background-position:center center; height:1px;{0}>
              </td>
              </tr>
              <tr> 
                <td style={0}height:27px;{0} class={0}menubar_function_text{0}>{5}</td>
                <td class={0}menubar_menu_td{0} style={0}text-align:right;{0}>{6}</td>
              </tr>
              <tr><td style={0}height:5px;{0} colspan={0}2{0}></td></tr>
            </table>
        ";

        private string HeadMenuHelpTxt = @"<img src={0}{1}{0} border={0}0{0} align={0}absmiddle{0} hspace={0}3{0} vspace={0}3{0} />{2}";

        private string _HeadIconPath = "~/images/HeadMenu/";
        private string _HeadTitleIcon = "default.gif";
        private string _HeadTitleTxt = "标题";
        private string _HeadHelpIcon = "office.gif";
        private string _HeadHelpTxt = "帮助？";
        private string _HeadOPTxt = "";
        private string _HeadSeperator = "Seperator.gif";
        private bool _haveHelp = true;
        private bool _showHeader = true;

        private string CreateButtonHtml()
        {
            StringBuilder sb = new StringBuilder();
            if (_ButtonList != null && _ButtonList.Count > 0)
            {
                sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr>");
                string OnUrlJs = "";
                string ButtonIcon = "";
                string ButtonTxt = "";
                for (int i = 0; i < _ButtonList.Count; i++)
                {
                    if (_ButtonList[i].ButtonVisible &&(_ButtonList[i].ButtonPopedom==PopedomType.NotControl|| SystemPermission.CheckButtonPermission(_ButtonList[i].ButtonPopedom)))
                    {
                        OnUrlJs = "";
                        ButtonIcon = "";
                        ButtonTxt = "";
                        switch (_ButtonList[i].ButtonUrlType)
                        {
                            case UrlType.Href:
                                OnUrlJs = string.Format("JavaScript:window.location.href='{0}';", _ButtonList[i].ButtonUrl);
                                break;
                            case UrlType.JavaScript:
                                OnUrlJs = string.Format("JavaScript:{0}", _ButtonList[i].ButtonUrl);
                                break;
                            case UrlType.VBScript:
                                OnUrlJs = string.Format("VBScript:{0}", _ButtonList[i].ButtonUrl);
                                break;
                        }
                        if (_ButtonList[i].ButtonIcon != string.Empty)
                        {
                            ButtonIcon = HeadIconPath + _ButtonList[i].ButtonIcon;
                        }
                        else
                        {
                            ButtonIcon = HeadIconPath + _ButtonList[i].ButtonPopedom.ToString() + ".gif";
                            switch (_ButtonList[i].ButtonPopedom)
                            {
                                case PopedomType.PermissionA:
                                    ButtonTxt = "权限A";
                                    break;
                                case PopedomType.PermissionB:
                                    ButtonTxt = "权限B";
                                    break;
                                case PopedomType.Delete:
                                    ButtonTxt = "删除";
                                    break;
                                case PopedomType.Edit:
                                    ButtonTxt = "修改";
                                    break;
                                case PopedomType.List:
                                    ButtonTxt = "列表";
                                    break;
                                case PopedomType.Print:
                                    ButtonTxt = "打印";
                                    break;
                                case PopedomType.New:
                                    ButtonTxt = "新增";
                                    break;
                                case PopedomType.Approval:
                                    ButtonTxt = "审批";
                                    break;
                            }
                        }
                        sb.AppendFormat("<td class=\"menubar_button\" id=\"button_{1}\" OnClick=\"{0}\" OnMouseOut=\"javascript:MenuOnMouseOver(this);\" OnMouseOver=\"javascript:MenuOnMouseOut(this);\">", OnUrlJs, i);
                        sb.AppendFormat("<img border=\"0\" align=\"texttop\" src=\"{0}\">&nbsp;", ButtonIcon);
                        sb.AppendFormat("{0}{1}</td>", ButtonTxt, _ButtonList[i].ButtonName);
                    }
                }
                sb.Append("</tr></table>");
            }
            if (sb.ToString() == string.Empty)
                sb.Append("&nbsp");
            return sb.ToString();
        }


        #endregion

        #region "Public Variables"

        /// <summary>
        /// 读取/设置头部是否显示
        /// </summary>
        [Description("读取/设置头部是否显示"), Category("外观"), DefaultValue(true)]
        public bool ShowHeader
        {
            get
            {
                object m = ViewState["ShowHeader"];
                return m == null ? _showHeader : Convert.ToBoolean(m);
                
            }
            set
            {
                ViewState["ShowHeader"] = value;
            }
        }

        /// <summary>
        /// 读取/设置头部菜单路径
        /// </summary>
        [Description("读取/设置头部菜单路径"), Category("外观"), DefaultValue("~/images/HeadMenu/")]
        public string HeadIconPath
        {
            get
            {
                object m = ViewState["HeadIconPath"];
                return m == null ? ResolveClientUrl(_HeadIconPath) : ResolveClientUrl(m.ToString());
            }
            set
            {
                ViewState["HeadIconPath"] = value;
            }
        }
        /// <summary>
        /// 读取/设置标题Icon图片名
        /// </summary>
        [Description("读取/设置标题Icon图片名"), Category("外观"), DefaultValue("default.gif")]
        public string HeadTitleIcon
        {
            get
            {
                object m = ViewState["HeadTitleIcon"];
                return m == null ? string.Format("{0}{1}", HeadIconPath, _HeadTitleIcon) : string.Format("{0}{1}", HeadIconPath, m);
            }
            set
            {
                ViewState["HeadTitleIcon"] = value;
            }
        }
        /// <summary>
        /// 读取/设置标题名称
        /// </summary>
        [Description("读取/设置标题名称"), Category("外观"), DefaultValue("标题名称")]
        public string HeadTitleTxt
        {
            get
            {
                object m = ViewState["HeadTitleTxt"];
                return m == null ? _HeadTitleTxt : m.ToString();
            }
            set
            {
                ViewState["HeadTitleTxt"] = value;
            }
        }

        /// <summary>
        /// 读取/设置帮助Icon名称
        /// </summary>
        [Description("读取/设置帮助Icon图片名"), Category("外观"), DefaultValue("office.gif")]
        public string HeadHelpIcon
        {
            get
            {
                object m = ViewState["HeadHelpIcon"];
                return m == null ? string.Format("{0}{1}", HeadIconPath, _HeadHelpIcon) : string.Format("{0}{1}", HeadIconPath, m);
            }
            set
            {
                ViewState["HeadHelpIcon"] = value;
            }
        }
        /// <summary>
        /// 读取/设置帮助文字
        /// </summary>
        [Description("读取/设置帮助文字"), Category("外观"), DefaultValue("帮助？")]
        public string HeadHelpTxt
        {
            get
            {
                object m = ViewState["HeadHelpTxt"];
                return m == null ? _HeadHelpTxt : m.ToString();
            }
            set
            {
                ViewState["HeadHelpTxt"] = value;
            }
        }
        /// <summary>
        /// 读取/设置操作说明
        /// </summary>
        [Description("读取/设置操作说明"), Category("外观"), DefaultValue("")]
        public string HeadOPTxt
        {
            get
            {
                object m = ViewState["HeadOPTxt"];
                return m == null ? _HeadOPTxt : m.ToString();
            }
            set
            {
                ViewState["HeadOPTxt"] = value;
            }
        }
        /// <summary>
        /// 读取/设置分隔条图片名称
        /// </summary>
        [Description("读取/设置分隔条图片名称"), Category("外观"), DefaultValue("Seperator.gif")]
        public string HeadSeperator
        {
            get
            {
                object m = ViewState["Seperator"];
                return m == null ? string.Format("{0}{1}", HeadIconPath,_HeadSeperator) : string.Format("{0}{1}", HeadIconPath, m);
            }
            set
            {
                ViewState["Seperator"] = value;
            }
        }
        
        /// <summary>
        /// 读取/设置是否显示帮助
        /// </summary>
        [Description("读取/设置是否显示帮助 "), Category("控制"), DefaultValue("true")]
        public bool HaveHelpIcon
        {
            get
            {
                object m = ViewState["HaveHelp"];
                return m == null ? _haveHelp : Convert.ToBoolean(m);
            }
            set
            {
                ViewState["HaveHelp"] = value;
            }
        }
        
        /// <summary>
        /// 按钮集合
        /// </summary>
        [
        Category("Behavior"),
        Description("按钮集合"),
        Editor(typeof(CollectionEditor), typeof(UITypeEditor)),
        PersistenceMode(PersistenceMode.InnerDefaultProperty)
        ]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)] 
        public List<HeadMenuButtonItem> ButtonList
        {
            get
            {
                //object m = ViewState["ButtonList"];
                //return m == null ? _ButtonList : (List<HeadMenuButtonItem>)m;
                return _ButtonList;
            }
            //set
            //{
            //    ViewState["ButtonList"] = value;
            //}
        }

        /// <summary>
        /// 重写RenderContents方法
        /// </summary>
        /// <param name="writer"></param>
        protected override void  Render(HtmlTextWriter writer)
        {

            if (HaveHelpIcon)
            {
                StringBuilder helpTxt = new StringBuilder();
                helpTxt.AppendFormat(HeadMenuHelpTxt, "\"", HeadHelpIcon, HeadHelpTxt);
                writer.Write(HeadMenuTemplateTxt, "\"", HeadTitleIcon, HeadTitleTxt, helpTxt.ToString(), HeadSeperator, HeadOPTxt, CreateButtonHtml(),
                    ShowHeader?"block":"none");
            }
            else
            {
                writer.Write(HeadMenuTemplateTxt, "\"", HeadTitleIcon, HeadTitleTxt, "&nbsp;", HeadSeperator, HeadOPTxt, CreateButtonHtml(),
                    ShowHeader ? "block" : "none");
            }
        }
        #endregion
    }
}
