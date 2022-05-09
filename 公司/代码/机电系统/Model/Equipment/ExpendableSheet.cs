using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    public class ExpendableSheet
    {
        #region Model
        private int _id;
        private string _name;
        private DateTime? _time;
        private string _xinzhengyewu;
        private string _zongheshiwu;
        private string _jihuacaiwu;
        private string _fenguanlingdao;
        private string _zongjinli;

        private string _xinzhengyewustr;
        private string _zongheshiwustr;
        private string _jihuacaiwustr;
        private string _fenguanlingdaostr;
        private string _zongjinlistr;



        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? time
        {
            set { _time = value; }
            get { return _time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string xinzhengyewu
        {
            set { _xinzhengyewu = value;
            switch (value)
            {
                case "0":
                    this._xinzhengyewustr = "暂无";
                    break;
                case "1":
                    this._xinzhengyewustr = "同意";
                    break;
                case "2":
                    this._xinzhengyewustr = "不同意";
                    break;
                case "3":
                    this._xinzhengyewustr = "其它";
                    break;
            }
            }
            get { return _xinzhengyewu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zongheshiwu
        {
            set { _zongheshiwu = value;
            switch (value)
            {
                case "0":
                    this._zongheshiwustr = "暂无";
                    break;
                case "1":
                    this._zongheshiwustr = "同意";
                    break;
                case "2":
                    this._zongheshiwustr = "不同意";
                    break;
                case "3":
                    this._zongheshiwustr = "其它";
                    break;
            }
            }
            get { return _zongheshiwu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string jihuacaiwu
        {
            set { _jihuacaiwu = value;
            switch (value)
            {
                case "0":
                    this._jihuacaiwustr = "暂无";
                    break;
                case "1":
                    this._jihuacaiwustr = "同意";
                    break;
                case "2":
                    this._jihuacaiwustr = "不同意";
                    break;
                case "3":
                    this._jihuacaiwustr = "其它";
                    break;
            }
            }
            get { return _jihuacaiwu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string fenguanlingdao
        {
            set { _fenguanlingdao = value;
            switch (value)
            {
                case "0":
                    this._fenguanlingdaostr = "暂无";
                    break;
                case "1":
                    this._fenguanlingdaostr = "同意";
                    break;
                case "2":
                    this._fenguanlingdaostr = "不同意";
                    break;
                case "3":
                    this._fenguanlingdaostr = "其它";
                    break;
            }
            }
            get { return _fenguanlingdao; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zongjinli
        {
            set { _zongjinli = value;
            switch (value)
            {
                case "0":
                    this._zongjinlistr = "暂无";
                    break;
                case "1":
                    this._zongjinlistr = "同意";
                    break;
                case "2":
                    this._zongjinlistr = "不同意";
                    break;
                case "3":
                    this._zongjinlistr = "其它";
                    break;
            }
            }
            get { return _zongjinli; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string xinzhengyewustr
        {
            get {
           
                return this._xinzhengyewustr;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zongheshiwustr
        {
            get {
                
                return _zongheshiwustr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string jihuacaiwustr
        {
            get {
                
                return _jihuacaiwustr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string fenguanlingdaostr
        {
            get {
                
                return _fenguanlingdaostr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zongjinlistr
        {
            get {
           
                return _zongjinlistr; }
        }
        #endregion Model
    }
}
