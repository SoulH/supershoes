using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException() : base("400 - Bad request ") { }
    }

    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException() : base("401 – Not authorized") { }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException() : base("404 - Record not found") { }
    }

    public class ServerErrorException : Exception
    {
        public ServerErrorException() : base("500 - Server Error") { }
    }
}
