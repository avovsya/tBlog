using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tBlog.Web.Models
{
    public class EntryViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Body { get; set; }
        public string Username { get; set; }
        public string Category { get; set; }
        public string CreatedAt { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}