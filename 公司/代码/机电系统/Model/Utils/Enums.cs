using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Utils
{
    public enum TimeUnits
    {
        [EnumDescription("未知时间单位")]
        Unknown=0,
        [EnumDescription("分钟")]
        Minute=1,
        [EnumDescription("小时")]
        Hour=2,
        [EnumDescription("天")]
        Day=3,
        [EnumDescription("月")]
        Month=4,
        [EnumDescription("年")]
        Year=5
    }
}
