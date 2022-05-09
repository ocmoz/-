using System;
using System.Collections.Generic;
using System.Text;
using WebUtility.Components;
using System.Xml;
using System.IO;

namespace WebUtility
{
    public class PermissionConfigFile
    {
        /// <summary>
        /// 加载xml文件
        /// </summary>
        /// <param name="filePath">需要加载的xml文件的绝对路径</param>
        /// <returns></returns>
        public static Permission LoadXmlConfigFile(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            Permission permission = new Permission();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            XmlNode root = xmlDoc.DocumentElement.SelectSingleNode("Permission") ;
            XmlNode AppNode = root.SelectSingleNode("ModuleID");
            permission.ModuleID = AppNode.InnerText;
            permission.ModuleName = AppNode.Attributes["name"].Value;

            XmlNodeList ItemNodes = root.SelectNodes("Item");
            foreach (XmlNode Node in ItemNodes)
            {
                PermissionItem Item = new PermissionItem();
                Item.Item_Name = Node.Attributes["name"].Value;
                Item.Item_Value = Convert.ToInt32(Node.Attributes["value"].Value);
                Item.Item_FileList = Node.InnerText.ToLower();
                permission.ItemList.Add(Item);

            }
            return permission;
        }
        /// <summary>
        /// 保存Xml文件
        /// </summary>
        /// <param name="filePath">需要保存的文件的绝对路径</param>
        /// <param name="permission"></param>
        public static void SaveXmlConfigFile(string filePath, Permission permission)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);   // 先删除，再重建
            }

            StringBuilder xmlString = new StringBuilder();
            xmlString.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>").Append("\n");
            xmlString.Append("<configuration>").Append("\n").Append("<Permission>").Append("\n");
            xmlString.AppendFormat("<ModuleID name=\"{0}\">{1}</ModuleID>\n", permission.ModuleName, permission.ModuleID);

            foreach (PermissionItem item in permission.ItemList)
            {
                xmlString.AppendFormat("<Item value=\"{0}\" name=\"{1}\">{2}</Item>", item.Item_Value, item.Item_Name, item.Item_FileList).Append("\n");
            }
            xmlString.Append("</Permission>").Append("\n").Append("</configuration>").Append("\n");

            using (FileStream configFile = File.Create(filePath))
            {

                using (StreamWriter streamWriter = new StreamWriter(configFile))
                {
                    streamWriter.Write(xmlString.ToString());
                    streamWriter.Flush();
                    streamWriter.Close();
                    configFile.Close();
                }
            }
        }
    }
}
