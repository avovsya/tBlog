using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tBlog.Web.Models
{
    public class EntryEditModel : BaseViewModel
    {
        [Required(ErrorMessage = "Please enter the title of this post.")]
        public string Title { get; set; }

        public string Slug { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Please enter some content.")]
        public string Body { get; set; }

        //public int CategoryId { get; set; }

        [DisplayName("Tags(comma separated)")]
        public string TagString { get; set; }

        [Required(ErrorMessage = "Please add category")]
        public string CategoryName { get; set; }

        [DisplayName("Meta keywords")]
        public string EntryMetaKeywords { get; set; }

        [DisplayName("Meta description")]
        public string EntryMetaDescription { get; set; }
    }
}