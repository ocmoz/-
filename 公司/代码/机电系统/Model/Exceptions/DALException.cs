using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace FM2E.Model.Exceptions
{

    public class DALException : Exception
    {
        public DALException(string msg):base()
        {
        }

        public DALException()
            : base("数据库操作异常")
        {
        }

        public DALException(string msg,Exception e):base(msg,e)
        {
        }
    }
}
