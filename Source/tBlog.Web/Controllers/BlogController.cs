using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PagedList;
using tBlog.Data.Abstract;
using tBlog.Data.Repositories;
using tBlog.Data.Service;
using tBlog.Domain.Entities;
using tBlog.Web.Application.Services;
using tBlog.Web.Models;

namespace tBlog.Web.Controllers
{
    public class BlogController : BaseController
    {
        private readonly int _pageSize;
        private readonly IEntryRepository _entryRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Tag> _tagRepository;
        private readonly ISettingRepository _settingRepository;

        public BlogController(IEntryRepository entryRepository, IRepository<Category> categoryRepository, IRepository<Tag> tagRepository, ISettingRepository settingRepository)
            : base(settingRepository)
        {
            _entryRepository = entryRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;

            _settingRepository = settingRepository;

            _pageSize = _settingRepository.ListPageSize;
        }

        [HttpGet]
        public ActionResult Entries(int page = 1)
        {
            var pagedList = _entryRepository.GetAll().ToViewModelPagedList(e => e.ToViewModel(), page, _pageSize);

            var model = new ListViewModel
                            {
                                SectionId = "entries",
                                Items = pagedList,
                                PageTitle = "Blog",
                                TemplateName = "DisplayTemplates/EntryListItem",
                                ActionName = "Entries",
                                ControllerName = "Blog"
                            };

            return View("List", model);
        }

        [HttpGet]
        public ActionResult Show(string slug)
        {
            Entry entry;

            try
            {
                entry = _entryRepository.GetBySlug(slug);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Can not find entry";
                return View("Error");
            }

            var model = entry.ToViewModel();
            model.PageTitle = model.Title;
            return View(model);
        }

        [HttpGet]
        public ActionResult Tags(int page = 1)
        {
            var pagedList = (from tag in _tagRepository.GetAll()
                     orderby tag.Name
                     select new TagViewModel
                                {
                                    Name = tag.Name,
                                    EntryCount = tag.Entries.Count
                                }).ToPagedList(page, _pageSize);

            var model = new ListViewModel
            {
                SectionId = "TagList",
                Items = pagedList,
                PageTitle = "Tags",
                TemplateName = "DisplayTemplates/TagListItem",
                ActionName = "Tags",
                ControllerName = "Blog"
            };

            return View("List", model);           
        }

        [HttpGet]
        public ActionResult Categories(int page = 1)
        {
            var pagedList = (from category in _categoryRepository.GetAll()
                             orderby category.Name
                             select new CategoryViewModel
                             {
                                 Name = category.Name,
                                 EntryCount = category.Entries.Count
                             }).ToPagedList(page, _pageSize);

            var model = new ListViewModel
            {
                SectionId = "CategorieList",
                Items = pagedList,
                PageTitle = "Categories",
                TemplateName = "DisplayTemplates/CategoryListItem",
                ActionName = "Categories",
                ControllerName = "Blog"
            };

            return View("List", model);
        }

        [HttpGet]
        public ActionResult Tag(string tag, int page = 1)
        {
            var pagedList = _entryRepository.GetByTag(tag).ToViewModelPagedList(e => e.ToViewModel(), page, _pageSize);

            var routeValues = new RouteValueDictionary();
            routeValues["tag"] = tag;

            var model = new ListViewModel
            {
                Caption = String.Format("Showing entries with tag <strong>{0}</strong>. <a href='{1}'> Show all entries </a>", tag, Url.Action("Entries", "Blog", new { page = 1 })),
                SectionId = "entries",
                Items = pagedList,
                PageTitle = String.Format("Tag - {0}", tag),
                TemplateName = "DisplayTemplates/EntryListItem",
                ActionName = "Tag",
                ControllerName = "Blog",
                RouteValues = routeValues
            };

            return View("List", model);
        }

        [HttpGet]
        public ActionResult Category(string category, int page = 1)
        {
            var pagedList = _entryRepository.GetByCategory(category).ToViewModelPagedList(e => e.ToViewModel(), page, _pageSize);

            var routeValues = new RouteValueDictionary();
            routeValues["category"] = category;

            var model = new ListViewModel
            {
                Caption = String.Format("Showing entries on category <strong>{0}</strong>. <a href='{1}'> Show all entries </a>", category, Url.Action("Entries", "Blog", new { page = 1 })),
                SectionId = "entries",
                Items = pagedList,
                PageTitle = String.Format("Category - {0}", category),
                TemplateName = "DisplayTemplates/EntryListItem",
                ActionName = "Category",
                ControllerName = "Blog",
                RouteValues = routeValues
            };

            return View("List", model);
        }

        [HttpGet]
        public ActionResult Search(string term, int page = 1)
        {
            //TODO: implement full text search with Lucene.NET
            var pagedList = _entryRepository.Search(term).ToViewModelPagedList(e => e.ToViewModel(), page, _pageSize);

            var routeValues = new RouteValueDictionary();
            routeValues["term"] = term;

            var model = new ListViewModel
            {
                Caption = String.Format("Showing entries on search result for: <strong>{0}</strong>. <a href='{1}'> Show all entries </a>", term, Url.Action("Entries", "Blog", new { page = 1 })),
                SectionId = "entries",
                Items = pagedList,
                PageTitle = String.Format("Searching: \"{0}\"", term),
                TemplateName = "DisplayTemplates/EntryListItem",
                ActionName = "Search",
                ControllerName = "Blog",
                RouteValues = routeValues
            };

            return View("List", model);
        }

        [HttpGet]
        public ActionResult Archive(int page = 1)
        {
            var model = new ArchiveModel
                            {
                                Items = _entryRepository.GetArchive(),
                                PageTitle = "Archive",
                            };

            return View("Archive", model);
        }

        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Html = _settingRepository.AboutPageText;
            return View();
        }
    }
}
