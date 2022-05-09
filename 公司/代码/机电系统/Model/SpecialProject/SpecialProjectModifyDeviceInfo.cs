using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.SpecialProject
{
    /// <summary>
    /// 专项工程变更明细项实体类
    /// </summary>
    public class SpecialProjectModifyDeviceInfo
    {
        public SpecialProjectModifyDeviceInfo()
		{}
		#region Model
		private long _projectid;
		private decimal _amount;
        private string _remark = "";
        private long _modifyapplyid;
        private long _itemid;
		private bool _isadd;
        private string _devicename = "";
        private string _model = "";
		private decimal _count;
		private decimal _unitprice;
        private string _unit = "";
		/// <summary>
		/// 专项工程ID
		/// </summary>
        public long ProjectID
		{
			set{ _projectid=value;}
			get{return _projectid;}
		}
		/// <summary>
		/// 增加或者减少金额
		/// </summary>
		public decimal Amount
		{
			set{ _amount=value;}
			get{return _amount;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 变更申请单ID
		/// </summary>
		public long ModifyApplyID
		{
			set{ _modifyapplyid=value;}
			get{return _modifyapplyid;}
		}
		/// <summary>
		/// 明细项序号
		/// </summary>
        public long ItemID
		{
			set{ _itemid=value;}
			get{return _itemid;}
		}
		/// <summary>
		/// 增加或者减少（true增，false减）
		/// </summary>
		public bool IsAdd
		{
			set{ _isadd=value;}
			get{return _isadd;}
		}

        /// <summary>
        /// 增加或减少字符串
        /// </summary>
        public string IsAddString
        {
            get
            {
                if (_isadd)
                    return "＋";
                else
                    return "－";
            }
        }
		/// <summary>
		/// 设备名称
		/// </summary>
		public string DeviceName
		{
			set{ _devicename=value;}
			get{return _devicename;}
		}
		/// <summary>
		/// 型号
		/// </summary>
		public string Model
		{
			set{ _model=value;}
			get{return _model;}
		}
		/// <summary>
		/// 变化数量
		/// </summary>
		public decimal Count
		{
			set{ _count=value;}
			get{return _count;}
		}
		/// <summary>
		/// 单价
		/// </summary>
		public decimal UnitPrice
		{
			set{ _unitprice=value;}
			get{return _unitprice;}
		}
		/// <summary>
		/// 单位
		/// </summary>
		public string Unit
		{
			set{ _unit=value;}
			get{return _unit;}
		}

        


		#endregion Model
    }
}
