﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Module_FM2E_ConsumablesManager_ConsumablesTypeManager_ConsumablesTypeList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AddTree(0, (TreeNode)null);
            TreeView1.ShowLines = true;
        }
    }

    public void AddTree(long ParentID, TreeNode pNode)
    {

    }
}
