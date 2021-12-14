
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class LMSException : Exception
    {
        public LMSException() : base() { }

        public LMSException(string message) : base(message) { }

        public LMSException(string format, params object[] args) : base(string.Format(format, args)) { }

        public LMSException(ErrorCode message, Exception innerException) : base(Convert.ToString(message), innerException) { }

        public LMSException(string message, Exception innerException) : base(message, innerException) { }

        public LMSException(string format, Exception innerException, params object[] args) : base(string.Format(format, args), innerException) { }
    }
}
