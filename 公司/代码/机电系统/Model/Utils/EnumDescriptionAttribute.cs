using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace FM2E.Model.Utils
{
    /// <summary>
    /// Enum自定义描述属性
    /// </summary>
    public class EnumDescriptionAttribute : DescriptionAttribute
    {
        private bool replaced;

        public EnumDescriptionAttribute(string description)
            : base(description)
        {
        }
        public override string Description
        {
            get
            {
                if (!this.replaced)
                {
                    this.replaced = true;
                    base.DescriptionValue = base.Description;
                }
                return base.Description;
            }
        }
    }

}
