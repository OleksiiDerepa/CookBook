using System.Collections.Generic;

namespace CookBook.Infrastructure.Responses
{
    public class CollectionApiResponse<T> : ContentApiResponse<IReadOnlyCollection<T>>
    {
        public int CurrentPage { get;  set; }
        public int PageSize { get;  set; }
        public int OverallCount { get;  set; }
        public int PageCount { get; set; }
    }
}