using System;
using System.Linq;
using PagedList;

namespace tBlog.Data.Service
{
    public static class DomainExtensions
    {
        public static IPagedList<TViewModel> ToViewModelPagedList<TEntity, TViewModel>(this IQueryable<TEntity> source, Func<TEntity, TViewModel> transform, int page, int pageSize)
        {
            var raw = source.ToPagedList(page, pageSize);
            var viewModels = raw.Select(transform);
            return new StaticPagedList<TViewModel>(viewModels, raw.GetMetaData());
        }
    }
}
