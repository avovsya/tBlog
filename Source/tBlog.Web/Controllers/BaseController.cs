using System.Collections.Generic;
using System.Web.Mvc;
using tBlog.Data.Repositories;
using tBlog.Web.Models;

namespace tBlog.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly string _blogName;
        private readonly ISettingRepository _settingRepository;

        public BaseController(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;

            _blogName = _settingRepository.BlogName;
        }

        private List<NavLinkViewModel> BuildMenu()
        {
            var menuItems = new List<NavLinkViewModel>();

            menuItems.Add(new NavLinkViewModel
            {
                Title = "Entries",
                Url = Url.Action("Entries", "Blog", new { page = 1 }),
                IsActive = false
            });

            menuItems.Add(new NavLinkViewModel
            {
                Title = "Tags",
                Url = Url.Action("Tags", "Blog", new { page = 1 }),
                IsActive = false
            });

            menuItems.Add(new NavLinkViewModel
            {
                Title = "Categories",
                Url = Url.Action("Categories", "Blog", new { page = 1 }),
                IsActive = false
            });

            menuItems.Add(new NavLinkViewModel
            {
                Title = "Archive",
                Url = Url.Action("Archive", "Blog"),
                IsActive = false
            });

            menuItems.Add(new NavLinkViewModel
            {
                Title = "About",
                Url = Url.Action("About", "Blog"),
                IsActive = false
            });


            if (Request.IsAuthenticated)
            {
                menuItems.Add(new NavLinkViewModel
                {
                    Title = "Administration",
                    Url = Url.Action("Administration", "Admin"),
                    IsActive = false,
                    PullRight = true
                });
            }


            return menuItems;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var result = filterContext.Result as ViewResult;
            if (result != null)
            {
                if (filterContext.Controller.ViewData.Model as BaseViewModel == null)
                    filterContext.Controller.ViewData.Model = new BaseViewModel();

                (filterContext.Controller.ViewData.Model as BaseViewModel).IsAuthenticated = Request.IsAuthenticated;

                //Filling blog name
                (filterContext.Controller.ViewData.Model as BaseViewModel).BlogTitle = _blogName;

                //Filling page title
                (filterContext.Controller.ViewData.Model as BaseViewModel).PageTitle += " - " + _blogName;

                //Filling meta tags
                (filterContext.Controller.ViewData.Model as BaseViewModel).MetaKeywords += " " +
                    _settingRepository.MetaKeywords;
                (filterContext.Controller.ViewData.Model as BaseViewModel).MetaDescription += " " +
                                    _settingRepository.MetaDescription;

                //Filling menu model
                var actionName = filterContext.ActionDescriptor.ActionName;

                var menuItems = BuildMenu();

                menuItems.ForEach(item =>
                                       {
                                           if (item.Title.ToLower() == actionName.ToLower())
                                           {
                                               item.IsActive = true;
                                           }
                                       });

                (filterContext.Controller.ViewData.Model as BaseViewModel).MenuModel = menuItems;

            }
            base.OnActionExecuted(filterContext);
        }
    }
}
