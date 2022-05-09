using System;
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
using FM2E.WorkflowLayer;
using FM2E.BLL.Budget;
using FM2E.Model.Budget;

public partial class WorkFlowChart : System.Web.UI.UserControl
{
    public long id;
    public string workflowname;
    public string workflowstate;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        DataBind2();
    }

    protected void DataBind2()
    {
        if(id != 0 && workflowname != null && workflowname != string.Empty)
        {
            IWorkflowParser parser = WorkflowHelper.GetTempWorkflowParser(workflowname);
            int top = 0;
            int linecount = 0;
            foreach (WorkflowStateInfo wsi in parser.StateInfoList)
            {
                if (wsi.Name != "End")
                {
                    if (workflowstate != null && workflowstate != string.Empty && workflowstate == wsi.Name)
                        DrawRedRect(wsi.Description, top);
                    else
                        DrawRect(wsi.Description, top);
                }

                int leftwidth = 0;
                foreach (WorkflowEventInfo wei in parser.GetEventInfoList(wsi.Name, true))
                {
                    StateActivityInfo bindState = parser.GetStateActivityInfoByName(wsi.Name);
                    EventDrivenActivityInfo edai = bindState.EventDrivenActivityCollection[wei.Name];

                    int endpoint = 0;
                    foreach (WorkflowStateInfo wsi2 in parser.StateInfoList)
                    {
                        if (wsi2.Name == edai.SingleSetStateActivity.TargetStateName)
                        {
                            DrawLine(top,endpoint,wei.Description,leftwidth,linecount);
                            leftwidth++;
                            linecount++;
                        }

                        endpoint++;
                    }
                    
                }

                top++;
            }
        }

        DrawGroup();
    }

    private void DrawRect(string contenttext, int topposition)
    {
        contentdiv.InnerHtml += "<div><v:RoundRect style='position:relative;top:"+topposition*100+";left:0;width:200;height:50px'>"
    + "<v:shadow on='T' type='single' color='#b3b3b3' offset='5px,5px'/>"
    + "<v:TextBox inset='5pt,5pt,5pt,5pt' style='font-size:10.2pt;'>"+contenttext+"</v:TextBox>"
    + "</v:RoundRect> "
    + "</div>";
    }

    private void DrawGroup()
    {
        contentdiv.InnerHtml = "<v:group ID='group1' style='position:relative;WIDTH:200px;HEIGHT:200px;' coordsize = '200,200'>"
    + contentdiv.InnerHtml + "</v:group>";
    }

    private void DrawLine(int startpoint,int endpoint,string contentstr,int leftwidth,int lincount)
    {
        contentdiv.InnerHtml += "<v:PolyLine filled='false'  Points='200," + (startpoint * 100 + 10 * leftwidth+10) + " " + (200 + 25 * lincount+50) + "," + (startpoint * 100 + 10 * leftwidth+10) + " " + (200 + 25 * lincount+50) + "," + (endpoint * 100 - 10-2*startpoint) + " 100," + (endpoint * 100 - 10-2*startpoint) + " 100," + endpoint * 100 + "' style='position:relative'>"
    + "<v:stroke StartArrow='Oval' EndArrow='Classic' />"
    + "</v:PolyLine>"
    + "<v:shape strokecolor='none' filled='False' style='position:relative;left:" + (200 +10) + ";top:" + (startpoint * 100 + 10 * leftwidth + 5) + ";width:150;height:12;' inset='1px,1px,1px,1px'>"
    + "<div style='Text-align:left;font-size:9pt;color:blue'>"+contentstr+"</div>"
    + "</v:shape>";
    }

    private void DrawRedRect(string contenttext, int topposition)
    {
        contentdiv.InnerHtml += "<div><v:RoundRect style='position:relative;top:" + topposition * 100 + ";left:0;width:200;height:50px'>"
    + "<v:shadow on='T' type='single' color='red' offset='5px,5px'/>"
    + "<v:TextBox inset='5pt,5pt,5pt,5pt' style='font-size:10.2pt;'>" + contenttext + "</v:TextBox>"
    + "</v:RoundRect> "
    + "</div>";
    }
}
