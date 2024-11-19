using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HD.FireTracker.Common.Classes.CustomExceptions
{
    public class CoreException : Exception
    {
        public CoreException(string businessMessage)
            : base(businessMessage)
        {
        }

        public CoreException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
