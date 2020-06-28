using System;
using System.Net;
using CookBook.Infrastructure.Responses;

namespace CookBook.Infrastructure.Exceptions
{
    public class NotFoundException : Exception, IAppException
    {
        public NotFoundException(string message)
            : base(message)
        {
        }

        public string ErrorCode => ErrorCodes.NotFound;
        public HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;
    }
}