using System.Collections.Generic;
using tBlog.Domain.Entities;

namespace tBlog.Web.Models
{
    public class BaseViewModel
    {
        
        public string PageTitle { get; set; }
        public IEnumerable<NavLinkViewModel> MenuModel { get; set; }
        public string BlogTitle { get; set; }
        public bool IsAuthenticated { get; set; }

        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
    }
}