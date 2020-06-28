using System;
using System.Net;
using CookBook.Infrastructure.Responses;

namespace CookBook.Infrastructure.Exceptions
{
    public class BadRequestException : Exception, IAppException
    {
        public BadRequestException(string message)
            : base(message)
        {
        }

        public string ErrorCode => ErrorCodes.InvalidParameter;
        public HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
    }
}
