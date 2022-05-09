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
using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using System.Collections.Generic;
using FM2E.Model.Exceptions;
using System.IO;
using Microsoft.Office.Interop;
using System.Reflection;

public partial class Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_DImportExpendable : System.Web.UI.Page
{
    private object missing = Missing.Value;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Button_Import_Click(object sender, EventArgs e)
    {
        string UPLOADFOLDER = "ImportExpendable/";

        if (FileUpload_ImportDevice.HasFile)
        {

            //附件处理
            FileUpLoadCommon fuc = new FileUpLoadCommon(SystemConfig.Instance.UploadPath + UPLOADFOLDER, false);
            string file = "";
            if (FileUpload_ImportDevice.HasFile)
            {
                if (fuc.SaveFile(FileUpload_ImportDevice, false))
                {
                    file = SystemConfig.Instance.UploadPath + UPLOADFOLDER + "/" + fuc.NewFileName;
                }
                else
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "上传失败", new WebException(fuc.ErrorMsg), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                    return;
                }
            }

            file = Server.MapPath(file);

            try
            {
                importdata(file);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "导入失败" + ex.Message, Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
                return;
            }
        }

    }


    protected void importdata(string filename)
    {
        Microsoft.Office.Interop.Excel.Application app = null;
        Microsoft.Office.Interop.Excel.Workbook workBook = null;
        Microsoft.Office.Interop.Excel.Worksheet workSheet = null;
        Microsoft.Office.Interop.Excel.Range range = null;
        Microsoft.Office.Interop.Excel.TextBox textBox = null;
        try
        {
            app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            app.Visible = false;

            //打开一个WorkBook
            workBook = app.Workbooks.Open(filename,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            //得到WorkSheet对象
            workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Sheets.get_Item(1);

            int introwCount = workSheet.UsedRange.Rows.Count;
            int intcolCount = workSheet.UsedRange.Columns.Count;

            if (introwCount <= 0)
                throw new WebException("文件中没有数据记录");
            if (intcolCount < 10)
                throw new WebException("字段个数不对");

            for (int i = 0; i < introwCount; i++)
            {
                for (int j = 0; j < intcolCount; j++)
                {
                    range = (Microsoft.Office.Interop.Excel.Range)workSheet.Cells[i + 1, j + 1];
                    if (range.Value2 != null)
                    {
                        lbtest.Text += range.Value2.ToString().Trim() + "   ";
                    }
                }
                lbtest.Text += "</br>";
            }
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            workBook.Close(null, null, null);
            app.Workbooks.Close();
            app.Quit();

            if (range != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
                range = null;
            }
            if (textBox != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(textBox);
                textBox = null;
            }
            if (workSheet != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                workSheet = null;
            }
            if (workBook != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                workBook = null;
            }
            if (app != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                app = null;
            }
        }
    }

}
