using System;
using System.Globalization;
using System.Linq;
using tBlog.Data.Infrastructure;
using tBlog.Domain.Entities;

namespace tBlog.Data.Repositories
{
    public interface ISettingRepository
    {
        int ListPageSize { get; set; }
        int CommentListPageSize { get; set; }
        string AboutPageText { get; set; }
        string BlogName { get; set; }
        string MetaKeywords { get; set; }
        string MetaDescription { get; set; }
        string Get(string name);
        void Set(string name, string value);
    }

    public class SettingRepository : BaseRepository<Setting>, ISettingRepository
    {
        public int ListPageSize
        {
            get
            {
                int result;
                var setting = Get("ListPageSize");
                int.TryParse(setting, out result);
                if (result == 0 && !String.Equals(setting, "0"))
                {
                    result = 10;
                }
                return result;
            }
            set
            {
                Set("ListPageSize", value.ToString());
            }
        }

        public int CommentListPageSize
        {
            get
            {
                int result;
                var setting = Get("CommentListPageSize");
                int.TryParse(setting, out result);
                if (result == 0 && !String.Equals(setting, "0"))
                {
                    result = 5;
                }
                return result;
            }
            set
            {
                Set("CommentListPageSize", value.ToString());
            }
        }

        public string AboutPageText
        {
            get
            {
                return Get("AboutPageText");
            }
            set
            {
                Set("AboutPageText", value);
            }
        }

        public string BlogName
        {
            get
            {
                return Get("BlogName");
            }
            set
            {
                Set("BlogName", value);
            }
        }

        public string MetaKeywords
        {
            get
            {
                return Get("MetaKeywords");
            }
            set
            {
                Set("MetaKeywords", value);
            }
        }


        public string MetaDescription
        {
            get
            {
                return Get("MetaDescription");
            }
            set
            {
                Set("MetaDescription", value);
            }
        }

        public SettingRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public string Get(string name)
        {
            var setting = base.GetAll().FirstOrDefault(s => s.Name == name);
            if (setting == null)
                return null;
            return setting.Value;
        }

        public void Set(string name, string value)
        {
            Setting setting = base.GetAll().FirstOrDefault(s => s.Name == name);

            if (setting == null)
            {
                setting = new Setting
                              {
                                  Name = name
                              };
            }

            setting.Value = value??"";

            base.Save(setting);
        }
    }
}
