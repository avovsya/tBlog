using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using tBlog.Data.Abstract;
using tBlog.Domain.Entities;
using tBlog.Web.Models;

namespace tBlog.Web.Controllers
{
    public class WidgetController : Controller
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Tag> _tagRepository;

        public WidgetController(IRepository<Category> categoryRepository, IRepository<Tag> tagRepository)
        {
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;

        }

        public ActionResult TagWidget()
        {
            var tags = (from tag in _tagRepository.GetAll()
                        orderby tag.Name
                        select new TagViewModel
                        {
                            Name = tag.Name,
                            EntryCount = tag.Entries.Count
                        }).ToList();

            BuildTagsRank(tags, 20, 11);

            return PartialView(tags);
        }

        public ActionResult CategoryWidget()
        {
            var categories = (from tag in _categoryRepository.GetAll()
                              orderby tag.Name
                              select new CategoryViewModel
                              {
                                  Name = tag.Name,
                                  EntryCount = tag.Entries.Count
                              });


            return PartialView(categories);
        }

        private void BuildTagsRank(IEnumerable<TagViewModel> tags, int maxRank, int minRank)
        {
            if(!tags.Any()) return;
            var entryCounts = tags.Select(t => t.EntryCount);
            var maxNumber = entryCounts.Max();
            var minNumber = entryCounts.Min();

            double step = ((double)maxNumber - minNumber)/(maxRank - minRank);

            foreach (var tag in tags)
            {
                tag.Rank = minRank + (int)Math.Round((tag.EntryCount - minNumber) / step);
            }
        }
    }
}
