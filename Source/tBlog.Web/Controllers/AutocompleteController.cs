using System.Linq;
using System.Web.Mvc;
using tBlog.Data.Abstract;
using tBlog.Data.Repositories;
using tBlog.Domain.Entities;

namespace tBlog.Web.Controllers
{
    public class AutocompleteController : Controller
    {
        private readonly int _listSize;
        private readonly ISettingRepository _settingRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Tag> _tagRepository;

        public AutocompleteController(IRepository<Category> categoryRepository, IRepository<Tag> tagRepository, ISettingRepository settingRepository)
        {
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;

            _settingRepository = settingRepository;

            _listSize = _settingRepository.ListPageSize;
        }

        [HttpGet]
        public JsonResult GetCategories(string term)
        {
            var categories =
                _categoryRepository.GetAll().Where(c => c.Name.ToLower().Contains(term.ToLower())).Take(_listSize);

            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTags(string term)
        {
            var tags =
                _tagRepository.GetAll().Where(c => c.Name.ToLower().Contains(term.ToLower())).Take(_listSize).Select(
                    c => c.Name);

            return Json(tags, JsonRequestBehavior.AllowGet);
        }
        
    }
}
