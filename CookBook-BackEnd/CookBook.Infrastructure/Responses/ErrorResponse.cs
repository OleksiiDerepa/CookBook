namespace CookBook.Infrastructure.Responses
{
    public class ErrorResponse<T> 
    {
        public string Code { get; set; }

        public T Error { get; set; }

        public ErrorResponse(string code, T error)
        {
            Code = code;
            Error = error;
        }
    }
}
