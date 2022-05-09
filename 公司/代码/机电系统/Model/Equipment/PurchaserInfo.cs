using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 采购员实体类
    /// </summary>
    public class PurchaserInfo
    {
        public PurchaserInfo()
		{}
		#region Model
		private long _id;
		private string _companyid;
		private string _userid;
		private string _remark;
		/// <summary>
		/// 数据库自增ID
		/// </summary>
        public long ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 公司ID
		/// </summary>
		public string CompanyID
		{
			set{ _companyid=value;}
			get{return _companyid;}
		}
		/// <summary>
		/// 用户名
		/// </summary>
		public string UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 采购员描述，一般指采购的哪个方面
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

        private string _purchasername;
        /// <summary>
        /// 采购员名字
        /// </summary>
        public string PurchaserName
        {
            set { _purchasername = value; }
            get { return _purchasername; }
        }

        /// <summary>
        /// 三个字段串连在一起
        /// </summary>
        public string UserIDPurchaserNameRemark
        {
            get { return string.Format("{0}{1}[备注：{2}]",_userid,_purchasername,_remark); }
        }
    }
}
