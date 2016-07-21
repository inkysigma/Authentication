using System;

namespace Authentication.Frame.Result
{
    public class ServerFaultException : Exception
    {
        public ServerFaultException(string msg) : base(msg) { }
    }
}
