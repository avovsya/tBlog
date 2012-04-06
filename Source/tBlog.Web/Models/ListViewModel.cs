using System.Web.Routing;
using PagedList;

namespace tBlog.Web.Models
{
    public class ListViewModel : BaseViewModel
    {
        public string SectionId { get; set; }
        public IPagedList<object> Items { get; set; }
        public string TemplateName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public RouteValueDictionary RouteValues { get; set; }
        public string EmptyPlaceHolder { get; set; }
        public string Caption { get; set; }
    }
}