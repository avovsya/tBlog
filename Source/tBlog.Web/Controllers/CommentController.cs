using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using PagedList;
using tBlog.Data.Repositories;
using tBlog.Domain.Entities;
using tBlog.Web.Models;

namespace tBlog.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly int _pageSize;
        private readonly ICommentRepository _commentRepository;
        private readonly ISettingRepository _settingRepository;

        public CommentController(ICommentRepository commentRepository, ISettingRepository settingRepository)
        {
            _commentRepository = commentRepository;

            _settingRepository = settingRepository;

            _pageSize = _settingRepository.CommentListPageSize;
        }

        public ActionResult List(string slug, int page = 1)
        {
            var pagedList = _commentRepository.GetCommentsForEntry(slug).AsQueryable().ToPagedList(page, _pageSize);
            
            var routeValues = new RouteValueDictionary();
            routeValues["slug"] = slug;

            var model = new ListViewModel
            {
                Items = pagedList,
                TemplateName = "DisplayTemplates/CommentListItem",
                ActionName = "List",
                ControllerName = "Comment",
                RouteValues = routeValues,
                SectionId = "comments"
            };

            return PartialView("ListPartial", model);
        }

        [HttpPost]
        public PartialViewResult Create(Comment model)
        {
            _commentRepository.Save(model);

            return PartialView("DisplayTemplates/CommentListItem", model);
        }

    }
}
