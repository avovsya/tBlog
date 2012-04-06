using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using tBlog.Domain.Entities;
using tBlog.Web.Models;

namespace tBlog.Web.Application.Services
{
    public static class ModelConverters
    {

        public static EntryViewModel ToViewModel(this Entry source)
        {
            return new EntryViewModel
                       {
                           Id = source.Id,
                           Body = source.Body,
                           Category = source.Category != null ? source.Category.Name : null,
                           Tags = source.Tags != null ? source.Tags.Select(t => t.Name) : new List<string>(),
                           Title = source.Title,
                           Slug = source.Slug,
                           Username = source.User != null ? source.User.UserName : null,
                           CreatedAt = source.CreatedAt.ToString("dd MMM yyyy"),
                           MetaKeywords = source.EntryMetaKeywords,
                           MetaDescription = source.EntryMetaDescription
                       };
        }

        public static IEnumerable<SelectListItem> ToSelectList(this IQueryable<Category> source)
        {
            var result = new List<SelectListItem>();
            var categories = source.ToList();
            foreach (var category in categories)
            {
                result.Add(new SelectListItem
                               {
                                   Selected = false,
                                   Text = category.Name,
                                   Value = category.Id.ToString(CultureInfo.InvariantCulture)
                               });
            }

            return result;
        }
    }
}
