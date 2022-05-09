using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Utils;

namespace FM2E.Model.Basic
{
    public enum AddressType
    {
        [EnumDescription("未知类型")]
        Unknown=0,
        [EnumDescription("路段")]
        Section = 1,
        [EnumDescription("收费站")]
        TollGate=2,
        [EnumDescription("隧道所")]
        ChannalCenter = 3,
        [EnumDescription("外场")]
        OutField = 4,
        [EnumDescription("广场")]
        Square = 5,
        [EnumDescription("站区")]
        StationArea=6,
        [EnumDescription("所区")]
        CenterArea = 7,
        [EnumDescription("隧道")]
        Channal=8,
        [EnumDescription("服务区")]
        ServiceArea = 9,
        [EnumDescription("路面设施")]
        RoadFacilities = 10,
        [EnumDescription("仓库")]
        Warehouse=11
    }
    [Serializable]
    public class AddressInfo
    {
        private long _id;
        private string _addresscode;
        private string _addressfullname;
        private AddressType _addresstype;
        private long _parentaddress;
        private int _childcount;
        private string _description;
        private string _modifier;
        private int _nextaddresscode;

        private long _mainteamid;
        private string _mainteamname;

        public long MainTeamID
        {
            set { _mainteamid = value; }
            get { return _mainteamid; }
        }

        public string MainTeamName
        {
            set { _mainteamname = value; }
            get { return _mainteamname; }
        }
        /// <summary>
        /// 地址主键ID
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 地址字符编码
        /// </summary>
        public string AddressCode
        {
            set { _addresscode = value; }
            get { return _addresscode; }
        }
        /// <summary>
        /// 地址名称
        /// </summary>
        public string AddressName
        {
            get
            {
                string addressName = "";
                int index = _addressfullname.LastIndexOf(" ");
                if (index > 0)
                    addressName = _addressfullname.Substring(index + 1, _addressfullname.Length - index-1);
                else addressName = _addressfullname;
                return addressName;
            }
        }
        /// <summary>
        /// 地址全称
        /// </summary>
        public string AddressFullName
        {
            set { _addressfullname = value; }
            get { return _addressfullname; }
        }
        /// <summary>
        /// 地址类型
        /// </summary>
        public AddressType AddressType
        {
            set { _addresstype = value; }
            get { return _addresstype; }
        }
        /// <summary>
        /// 上一级地址
        /// </summary>
        public long ParentAddress
        {
            set { _parentaddress = value; }
            get { return _parentaddress; }
        }
        /// <summary>
        /// 子结点数
        /// </summary>
        public int ChildCount
        {
            set { _childcount = value; }
            get { return _childcount; }
        }
        /// <summary>
        /// 地址描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 修改人
        /// </summary>
        public string Modifier
        {
            set { _modifier = value; }
            get { return _modifier; }
        }
        /// <summary>
        /// 下一地址编码
        /// </summary>
        public int NextAddressCode
        {
            get { return _nextaddresscode; }
            set { _nextaddresscode = value; }
        }
    }
}
