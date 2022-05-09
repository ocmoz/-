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
using System.Data.OleDb;

public partial class Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_ImportExpendables : System.Web.UI.Page
{
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


            //EventMessage.MessageBox(Msg_Type.Error, "操作失败", "上传失败"+systemid, Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");

            IList list = null;
            try
            {
                DataSet ds = ImportXlsToData(file);
                list = AddDataToObject(ds);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "导入失败" + ex.Message, Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
                return;
            }
            bool success = false;
            int count = 0;
            try
            {
                Expendable bll = new Expendable();
                foreach (ExpendableInOutRecordInfo record in list)
                {
                    if (record.Type == ExpendableInOutRecordType.In)
                    {
                        bll.ExpendableInWarehouse(UserData.CurrentUserData.CompanyID, record);
                    }
                    else if (record.Type == ExpendableInOutRecordType.Out)
                    {
                        bll.ExpendableOutWarehouse(UserData.CurrentUserData.CompanyID, record);
                    }
                    count++;
                }
                success = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "装载数据失败" + ex.Message, Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
                return;
            }
            if (success == true)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作失败", "导入成功：" + count, Icon_Type.OK, false, Common.GetHomeBaseUrl("Expendable.aspx"), UrlType.Href, "");
            }
        }

    }

    /// <summary>
    /// 从Excel提取数据--》Dataset
    /// </summary>
    /// <param name="filename">Excel文件路径名</param>
    private DataSet ImportXlsToData(string fileName)
    {
        try
        {
            if (fileName == string.Empty)
            {
                throw new ArgumentNullException("上传文件失败！");
            }
            //
            string oleDBConnString = String.Empty;
            oleDBConnString = "Provider=Microsoft.Jet.OLEDB.4.0;";
            oleDBConnString += "Data Source=";
            oleDBConnString += fileName;
            oleDBConnString += ";Extended Properties=Excel 8.0;";
            //
            OleDbConnection oleDBConn = null;
            OleDbDataAdapter oleAdMaster = null;
            DataTable m_tableName = new DataTable();
            DataSet ds = new DataSet();

            oleDBConn = new OleDbConnection(oleDBConnString);
            oleDBConn.Open();
            m_tableName = oleDBConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);  //表架构

            if (m_tableName != null && m_tableName.Rows.Count > 0)
            {

                m_tableName.TableName = m_tableName.Rows[0]["TABLE_NAME"].ToString();

            }
            string sqlMaster;
            sqlMaster = " SELECT *  FROM [" + m_tableName.TableName + "]";
            oleAdMaster = new OleDbDataAdapter(sqlMaster, oleDBConn);
            oleAdMaster.Fill(ds, "m_tableName");  //Fill DataSet
            oleAdMaster.Dispose();
            oleDBConn.Close();
            oleDBConn.Dispose();

            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// 把DataSet->Object IList
    /// </summary>
    private IList AddDataToObject(DataSet ds)
    {
        try
        {
            ArrayList list = new ArrayList();
            int ic, ir;
            ic = ds.Tables[0].Columns.Count;
            if (ds.Tables[0].Columns.Count < 16)
            {
                throw new Exception("导入Excel格式错误！Excel只有" + ic.ToString() + "列");
            }
            ir = ds.Tables[0].Rows.Count;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count - 1; i++)
                {
                    ExpendableInOutRecordInfo record = new ExpendableInOutRecordInfo();
                    if (ds.Tables[0].Rows[i][1].ToString() != "")
                    {
                        if (Convert.ToInt32(ds.Tables[0].Rows[i][1].ToString()) == 1)  //Type
                        {
                            record.Type = ExpendableInOutRecordType.In;
                        }
                        else
                        {
                            record.Type = ExpendableInOutRecordType.Out;
                        }
                    }
                    else
                    {
                        break;  //退出条件为此行没有出入库标记
                    }
                    if (ds.Tables[0].Rows[i][0].ToString() != "")
                    {
                        record.ID = Convert.ToInt32(ds.Tables[0].Rows[i][1].ToString());  //ID
                    }
                    if (ds.Tables[0].Rows[i][2].ToString() != "")
                    {
                        record.WarehouseID = ds.Tables[0].Rows[i][2].ToString();  //WarehouseID
                    }
                    if (ds.Tables[0].Rows[i][3].ToString() != "")
                    {
                        record.WarehouseKeeper = ds.Tables[0].Rows[i][3].ToString();  //WarehouseKeeperID
                    }
                    if (ds.Tables[0].Rows[i][4].ToString() != "")
                    {
                        record.WarehouseKeeperName = ds.Tables[0].Rows[i][4].ToString();  //WarehouseKeeperName
                    }
                    if (ds.Tables[0].Rows[i][5].ToString() != "")
                    {
                        record.Receiver = ds.Tables[0].Rows[i][5].ToString();  //Receiver
                    }
                    if (ds.Tables[0].Rows[i][6].ToString() != "")
                    {
                        record.ReceiverName = ds.Tables[0].Rows[i][6].ToString();  //ReceiverName
                    }
                    if (ds.Tables[0].Rows[i][7].ToString() != "")
                    {
                        record.InOutTime = Convert.ToDateTime(ds.Tables[0].Rows[i][7].ToString());  //InOutTime
                    }
                    if (ds.Tables[0].Rows[i][8].ToString() != "")
                    {
                        record.Name = ds.Tables[0].Rows[i][8].ToString();  //Name
                    }
                    record.Model = ds.Tables[0].Rows[i][9].ToString();  //Model
                    if (ds.Tables[0].Rows[i][10].ToString() != "")
                    {
                        record.Amount = Convert.ToDecimal(ds.Tables[0].Rows[i][10].ToString());  //Amount
                    }
                    if (ds.Tables[0].Rows[i][11].ToString() != "")
                    {
                        record.Unit = ds.Tables[0].Rows[i][11].ToString();  //Unit
                    }
                    if (ds.Tables[0].Rows[i][12].ToString() != "")
                    {
                        record.Price = Convert.ToDecimal(ds.Tables[0].Rows[i][12].ToString());  //Price
                    }
                    if (ds.Tables[0].Rows[i][13].ToString() != "")
                    {
                        record.CategoryID = Convert.ToInt64(ds.Tables[0].Rows[i][13].ToString());  //CategoryID
                    }
                    record.Remark = ds.Tables[0].Rows[i][14].ToString();  //Remark
                    if (ds.Tables[0].Rows[i][15].ToString() != "")
                    {
                        record.CompanyID = ds.Tables[0].Rows[i][15].ToString();  //CompanyID
                    }
                    list.Add(record);  //Add object
                }
                return list;
            }
            else
            {
                list.Clear();
                throw new Exception("导入数据为空或数据格式不符合！");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
