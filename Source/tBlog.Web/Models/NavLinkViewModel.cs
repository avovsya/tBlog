using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tBlog.Web.Models
{
    public class NavLinkViewModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public bool PullRight;
    }
}