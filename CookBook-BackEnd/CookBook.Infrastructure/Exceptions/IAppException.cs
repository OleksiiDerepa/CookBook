using System.Net;

namespace CookBook.Infrastructure.Exceptions
{
    public interface IAppException
    {
        string ErrorCode { get; }
        HttpStatusCode HttpStatusCode { get; }
    }
}
