using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FM2E.Model.Basic;
using FM2E.IDAL.Basic;

namespace FM2E.BLL.Basic
{
    public class Address
    {
        /// <summary>
        /// 获取所有地址信息
        /// </summary>
        /// <returns></returns>
        public IList GetAllAddress()
        {
            IAddress dal = FM2E.DALFactory.BasicAccess.CreateAddress();
            return dal.GetAllAddress();
        }
        /// <summary>
        /// 通过位置id来获取具体的地址信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AddressInfo GetAddress(long id)
        {
            IAddress dal = FM2E.DALFactory.BasicAccess.CreateAddress();
            return dal.GetAddress(id);
        }
        /// <summary>
        /// 通过addressCode来获取具体的地址信息
        /// </summary>
        public AddressInfo GetAddressByAddressCode(string addressCode)
        {
            IAddress dal = FM2E.DALFactory.BasicAccess.CreateAddress();
            return dal.GetAddressByAddressCode(addressCode);
        }
        /// <summary>
        /// 获取某个地址下的所有子地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList GetChildAddress(long id)
        {
            IAddress dal = FM2E.DALFactory.BasicAccess.CreateAddress();
            return dal.GetChildAddress(id);
        }
        /// <summary>
        /// 添加地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns>插入后的ID</returns>
        public long AddAddress(AddressInfo model)
        {
            IAddress dal = FM2E.DALFactory.BasicAccess.CreateAddress();
            return dal.AddAddress(model);
        }
        /// <summary>
        /// 修改地址
        /// </summary>
        /// <param name="model"></param>
        public void UpdateAddress(AddressInfo model)
        {
            IAddress dal = FM2E.DALFactory.BasicAccess.CreateAddress();
            dal.UpdateAddress(model);
        }
        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="id"></param>
        public void DelAddress(long id)
        {
            IAddress dal = FM2E.DALFactory.BasicAccess.CreateAddress();
            dal.DelAddress(id);
        }

        public IList GetAddressByMaintainDept(long maintaindept)
        {
            IAddress dal = FM2E.DALFactory.BasicAccess.CreateAddress();
            return dal.GetAddressByMaintainDept(maintaindept);
        }

        /// <summary>
        /// 获取当前结点的下一个子结点的编码
        /// </summary>
        public string GetNextAddressCode(string addressCode)
        {
            IAddress dal = FM2E.DALFactory.BasicAccess.CreateAddress();
            int nextCode = dal.GetNextAddressCode(addressCode);

            return addressCode + ConvertTo36String(nextCode);
        }
        /// <summary>
        /// 把数字转化为36进制的字符串表示
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ConvertTo36String(int value)
        {
            string result = "";
            int remainder = value % 36;
            int commerce=value/36;
            result += IntToLetter(remainder);
            while (commerce != 0)
            {
                remainder = commerce % 36;
                result = IntToLetter(remainder) + result;
                commerce /= 36;
            }
            if (result.Length % 2 != 0)
                result = "0" + result;
            return result;
        }
        /// <summary>
        /// 数字转换成字母
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static string IntToLetter(int num)
        {
            string result = "";
            if (num < 10)
                result = num.ToString();
            else
            {
                string startLetter = "A";
                int tmp = Encoding.ASCII.GetBytes(startLetter)[0];
                tmp = tmp + (num - 10);
                result += (char)tmp;
            }

            return result;
        }
    }
}
