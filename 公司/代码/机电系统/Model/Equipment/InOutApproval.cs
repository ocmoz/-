using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    public class InOutApproval
    {
        public InOutApproval()
        { }
        #region Model
        private string _xingzhenyewu;
        private string _zongheshiwu;
        private string _jihuacaiwu;
        private string _fenguanlingdao;
        private string _zongjingli;
        private long _id;

        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string xingzhenyewu
        {
            set { _xingzhenyewu = value; }
            get { return _xingzhenyewu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zongheshiwu
        {
            set { _zongheshiwu = value; }
            get { return _zongheshiwu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string jihuacaiwu
        {
            set { _jihuacaiwu = value; }
            get { return _jihuacaiwu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string fenguanlingdao
        {
            set { _fenguanlingdao = value; }
            get { return _fenguanlingdao; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zongjingli
        {
            set { _zongjingli = value; }
            get { return _zongjingli; }
        }
        #endregion Model

    }
}
