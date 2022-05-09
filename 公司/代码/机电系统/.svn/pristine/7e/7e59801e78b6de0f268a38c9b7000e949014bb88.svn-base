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
using FM2E.BLL.Equipment;
using FM2E.Model.Utils;
using System.IO;
using WebUtility;
using FM2E.Model.Equipment;
using System.Collections.Generic;
using FM2E.Model.System;

public partial class Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_ExportFile : System.Web.UI.Page
{
    private string searchquery = "Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_Expendable"; 
    protected void Page_Load(object sender, EventArgs e)
    {
        FillData();


    }

    

    private void FillData()
    {

        Expendable bll = new Expendable();
        int listCount = 0;

        QueryParam searchTerm = Session[searchquery] as QueryParam;
        IList filelist = new List<RoleInfo>();

        IList list = bll.GetList(searchTerm, out listCount);

        string tempfiledir = HttpContext.Current.Server.MapPath("~/tempfile") + "/" + UserData.CurrentUserData.UserName;
        
        DirectoryInfo CreateFile = new DirectoryInfo(tempfiledir);
        if (!CreateFile.Exists)
        {
            CreateFile.Create();
        }
        else
        {
            Directory.Delete(tempfiledir, true);
            CreateFile.Create();
        }

        System.IO.StreamWriter file;
        int count = list.Count, max = 65530;//65530
        int startindex = 1, endindex = (count < max) ? count : max;
        string filepath = tempfiledir+"/" + startindex + "~" + endindex + ".csv";
        RoleInfo info = new RoleInfo();
        info.Description = UserData.CurrentUserData.UserName + "/" + startindex + "~" + endindex + ".csv";
        info.RoleName = startindex + "~" + endindex + ".csv";
        filelist.Add(info);
        FileStream f = File.Create(filepath);
        f.Close();
        f.Dispose();

        file = new System.IO.StreamWriter(filepath,true,System.Text.Encoding.Unicode);
        writehead(file);

        for (int i = 0; i < count; i++)
        {
            ExpendableInfo item = list[i] as ExpendableInfo;
            writecontent(file, item);
            if (((i + 1) % max == 0) || (i + 1 == count))
            {
                file.Close();
                file.Dispose();
                int min = ((count - i - 1) < max) ? (count - i - 1) : max;
                if (min == 0)
                    break;
                endindex += min;
                startindex = i+2;
                filepath =tempfiledir+ "/" + startindex + "~" + endindex + ".csv";
                info = new RoleInfo();
                info.Description = UserData.CurrentUserData.UserName + "/" + startindex + "~" + endindex + ".csv";
                info.RoleName = startindex + "~" + endindex + ".csv";
                filelist.Add(info);
                f = File.Create(filepath);
                f.Close();
                f.Dispose();
                file = new System.IO.StreamWriter(filepath,true,System.Text.Encoding.Unicode);
                writehead(file);

            }
        }

        GridView1.DataSource = filelist;
        GridView1.DataBind();
    }

    private void writecontent(StreamWriter file,ExpendableInfo item)
    {
        file.Write(item.ExpendableID);
        file.Write("\t");
        file.Write(item.Name);
        file.Write("\t");
        file.Write(item.Model);
        file.Write("\t");
        file.Write(item.Unit);
        file.Write("\t");
        file.Write(item.CategoryName);
        file.Write("\t");
        file.Write(item.Price);
        file.Write("\t");
        file.Write(item.Count);
        file.Write("\t");
        file.Write(item.CompanyName+" "+item.WarehouseName);
        file.Write("\t");
        file.Write(item.UpdateTime);
        file.Write("\t");
        file.Write(item.CompanyName);
        file.Write("\t");
        file.Write(item.Remark);
        file.Write("\r\n");
    }

    private void writehead(StreamWriter file)
    {
        file.Write("序号");
        file.Write("\t");
        file.Write("设备名称");
        file.Write("\t");
        file.Write("型号");
        file.Write("\t");
        file.Write("单位");
        file.Write("\t");
        file.Write("产品类型");
        file.Write("\t");
        file.Write("价格");
        file.Write("\t");
        file.Write("数量");
        file.Write("\t");
        file.Write("地址");
        file.Write("\t");
        file.Write("采购日期");
        file.Write("\t");
        file.Write("所属公司");
        file.Write("\t");
        file.Write("备注");
        file.Write("\r\n");
    }
}
