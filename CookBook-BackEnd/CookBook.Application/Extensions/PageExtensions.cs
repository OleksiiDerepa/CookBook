using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CookBook.Application.Queries;
using CookBook.Infrastructure.Responses;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Application.Extensions
{
    public static class PageExtensions
    {
        public static async Task<CollectionApiResponse<T>> GetPageAsync<T>(this IQueryable<T> source, ApiQueryRequest request,
            CancellationToken cancellationToken)
        {
            return await GetPageAsync(source, request.Page, request.Size, cancellationToken);
        } 
        
        public static async Task<CollectionApiResponse<T>> GetPageAsync<T>(this IQueryable<T> source, int targetPage, int size,
            CancellationToken cancellationToken)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (targetPage < 1)
                throw new ArgumentOutOfRangeException(nameof(targetPage), "Target page can't be less than 1");
            if (size <= 0)
                throw new ArgumentOutOfRangeException(nameof(size), "Size of page can't be less than 0");

            var page = new CollectionApiResponse<T>();

            page.CurrentPage = targetPage;
            page.OverallCount = await source.CountAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            page.PageSize = size;
            page.Content = await source.Skip((targetPage - 1) * size).Take(size)
                .ToListAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            page.PageCount = (int)Math.Ceiling((decimal)page.OverallCount / size);

            return page;
        }

    }
}