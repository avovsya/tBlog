using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace tBlog.Domain.Entities
{
    public class User : EntityWithTimestamps
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}