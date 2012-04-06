using System.Linq;
using tBlog.Domain.Entities;

namespace tBlog.Web.Models
{
    public class ArchiveModel : BaseViewModel
    {
        public IQueryable<YearArchiveEntry> Items { get; set; }
    }
}