using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Frame.Result
{
    public class ServerFaultException : Exception
    {
        public ServerFaultException(string msg) : base(msg) { }
    }
}
