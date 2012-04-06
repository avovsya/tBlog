using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace tBlog.Web.Models.Admin
{
    public class SettingsEditModel : BaseViewModel
    {
        [Required]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Must be a positive number bigger than 0")]
        [DisplayName("Page size(for entries, tags, etc.)")]
        public int ListPageSize { get; set; }

        [Required]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Must be a positive number bigger than 0")]
        [DisplayName("Page size(for comments)")]
        public int CommentListPageSize { get; set; }

        [Required]
        [DisplayName("Blog name")]
        public string BlogName { get; set; }

        [DisplayName("Meta keywords")]
        public string MetaKeywords { get; set; }

        [DisplayName("Meta description")]
        public string MetaDescription { get; set; }

        [AllowHtml]
        public string AboutPageText { get; set; }

        public string OperationResult { get; set; }
    }
}