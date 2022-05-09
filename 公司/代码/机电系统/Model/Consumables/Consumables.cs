using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Consumables
{
    public class Consumables
    {
        #region Model
        private long _consumablesid;
        private string _consumablesname;
        private bool _isdeleted;
        /// <summary>
        /// 
        /// </summary>
        public long ConsumablesID
        {
            set { _consumablesid = value; }
            get { return _consumablesid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ConsumablesName
        {
            set { _consumablesname = value; }
            get { return _consumablesname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted
        {
            set { _isdeleted = value; }
            get { return _isdeleted; }
        }
        #endregion Model
    }
}
