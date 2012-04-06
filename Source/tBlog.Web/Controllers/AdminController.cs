using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using tBlog.Data.Abstract;
using tBlog.Data.Repositories;
using tBlog.Domain.Entities;
using tBlog.Web.Application.Services;
using tBlog.Web.Models;
using tBlog.Web.Models.Admin;

namespace tBlog.Web.Controllers
{
    public class AdminController : BaseController
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IEntryRepository _entryRepository;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;

        public AdminController(ISettingRepository settingRepository, 
            IEntryRepository entryRepository, 
            IRepository<Tag> tagRepository, 
            IRepository<Category> categoryRepository, 
            ICommentRepository commentRepository, 
            IUserRepository userRepository)
            : base(settingRepository)
        {
            _settingRepository = settingRepository;
            _entryRepository = entryRepository;
            _tagRepository = tagRepository;
            _categoryRepository = categoryRepository;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Administration()
        {
            var model = GetSettingsModel();
            model.PageTitle = "Administration";

            return View("Administration", model);
        }

        [HttpPost]
        public ActionResult Administration(SettingsEditModel model)
        {
            try
            {
                SetSettings(model);
                model.OperationResult = "Settings successfully saved";
            }
            catch (Exception)
            {
                model.OperationResult = "Settings was not saved";
            }

            model.PageTitle = "Administration";

            return View("Administration", model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateEntry()
        {
            var model = new EntryEditModel
            {
                PageTitle = "Create entry"
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEntry(EntryEditModel model)
        {
            var slug = model.Title.ToSlug();

            if (_entryRepository.Exists(slug))
                ModelState.AddModelError("Title", "An entry already exists with this title, please change it.");

            if (!ModelState.IsValid)
            {
                model.PageTitle = "Create entry";
                return View(model);
            }


            var stringTags = !String.IsNullOrEmpty(model.TagString) ? model.TagString.Split(',') : new string[] { };

            var newEntry = new Entry
            {
                Body = model.Body,
                Category = GetCategorie(model.CategoryName),
                Title = model.Title,
                Slug = slug,
                UserId = _userRepository.GetByName(User.Identity.Name).Id,
                Tags = GetTags(stringTags),
                EntryMetaKeywords = model.EntryMetaKeywords,
                EntryMetaDescription = model.EntryMetaDescription
            };

            _entryRepository.Save(newEntry);

            return RedirectToAction("Administration");
        }

        [Authorize]
        [HttpPost]
        public ActionResult RemoveEntry(int id)
        {
            try
            {
                var entry = _entryRepository.GetById(id, e => e.Category, e => e.Tags);
                _entryRepository.Delete(entry);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult RemoveComment(int id)
        {
            try
            {
                var comment = _commentRepository.GetById(id);
                _commentRepository.Delete(comment);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        private List<Tag> GetTags(IEnumerable<string> tagStrings)
        {
            var result = new List<Tag>();
            foreach (var tagString in tagStrings)
            {
                var ts = tagString.Trim().ToLower();
                if (String.IsNullOrEmpty(ts)) break;
                var tag = _tagRepository.GetAll().SingleOrDefault(t => t.Name.ToLower() == ts) ?? new Tag { Name = tagString.Trim() };
                result.Add(tag);
            }

            return result;
        }

        private Category GetCategorie(string categoryName)
        {
            if (String.IsNullOrEmpty(categoryName)) return null;
            var cn = categoryName.Trim().ToLower();
            var category = _categoryRepository.GetAll().SingleOrDefault(t => t.Name.ToLower() == cn) ?? new Category() { Name = categoryName.Trim() };

            return category;
        }

        private SettingsEditModel GetSettingsModel()
        {
            var model = new SettingsEditModel
                            {
                                BlogName = _settingRepository.BlogName,
                                AboutPageText = _settingRepository.AboutPageText,
                                CommentListPageSize = _settingRepository.CommentListPageSize,
                                ListPageSize = _settingRepository.ListPageSize,
                                MetaKeywords = _settingRepository.MetaKeywords,
                                MetaDescription = _settingRepository.MetaDescription
                            };
            return model;
        }

        private void SetSettings(SettingsEditModel model)
        {
            _settingRepository.BlogName = model.BlogName;
            _settingRepository.AboutPageText = model.AboutPageText;
            _settingRepository.CommentListPageSize = model.CommentListPageSize;
            _settingRepository.ListPageSize = model.ListPageSize;
            _settingRepository.MetaKeywords = model.MetaKeywords;
            _settingRepository.MetaDescription = model.MetaDescription;
        }

    }
}
