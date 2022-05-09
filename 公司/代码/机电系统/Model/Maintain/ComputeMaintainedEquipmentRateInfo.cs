using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Maintain
{
    public class ComputeMaintainedEquipmentRateInfo
    {
        private int _count;
        private string _name;
        private string _model;

        //数量
        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        //名称
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        //型号
        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }
    }
}
