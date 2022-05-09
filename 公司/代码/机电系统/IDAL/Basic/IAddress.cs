using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FM2E.Model.Basic;

namespace FM2E.IDAL.Basic
{
    public interface IAddress
    {
        /// <summary>
        /// 获取所有地址信息
        /// </summary>
        /// <returns></returns>
        IList GetAllAddress();
        /// <summary>
        /// 通过位置id来获取具体的地址信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AddressInfo GetAddress(long id);
        /// <summary>
        /// 获取某个地址下的所有子地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList GetChildAddress(long id);
        /// <summary>
        /// 添加地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns>插入后的ID</returns>
        long AddAddress(AddressInfo model);
        /// <summary>
        /// 修改地址
        /// </summary>
        /// <param name="model"></param>
        void UpdateAddress(AddressInfo model);
        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="id"></param>
        void DelAddress(long id);
        /// <summary>
        /// 获取当前结点的下一个子结点的编码
        /// </summary>
        int GetNextAddressCode(string addressCode);
        /// <summary>
        /// 通过addressCode来获取具体的地址信息
        /// </summary>
        AddressInfo GetAddressByAddressCode(string addressCode);

        IList GetAddressByMaintainDept(long maintaindept);
        
    }
}
