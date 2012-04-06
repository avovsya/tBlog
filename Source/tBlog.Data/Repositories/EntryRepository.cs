using System;
using System.Data;
using System.Linq;
using tBlog.Data.Abstract;
using tBlog.Data.Infrastructure;
using tBlog.Domain.Entities;
using System.Data.Entity;

namespace tBlog.Data.Repositories
{
    public class EntryRepository : BaseRepository<Entry>, IEntryRepository
    {
        public EntryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public Entry GetBySlug(string slug)
        {
            return base.GetAll(e => e.Category, e => e.Tags, e => e.User).Single(e => String.Equals(e.Slug.ToLower(), slug.ToLower()));
        }

        public IQueryable<Entry> GetAll()
        {
            return base.GetAll(e => e.Category, e => e.Tags, e => e.User).OrderByDescending(e => e.CreatedAt);
        }

        public IQueryable<Entry> GetByTag(string tag)
        {
            return GetAll().Where(entry => entry.Tags.Any(t => t.Name == tag));
        }

        public IQueryable<Entry> GetByCategory(string category)
        {
            return GetAll().Where(entry => entry.Category.Name == category);
        }

        public IQueryable<Entry> Search(string term)
        {
            return GetAll().Where(entry => entry.Title.Contains(term) || entry.Body.Contains(term));
        }

        public bool Exists(string slug)
        {
            return base.GetAll().Count(e => String.Equals(e.Slug.ToLower(), slug.ToLower())) > 0;
        }

        public IQueryable<YearArchiveEntry> GetArchive()
        {
            return from p in this.GetAll()
                   group p by p.CreatedAt.Year into yg
                   orderby yg.Key descending
                   select
                       new YearArchiveEntry
                       {
                           Year = yg.Key,
                           MonthArchiveEntries =
                               from o in yg
                               group o by o.CreatedAt.Month into mg
                               orderby mg.Key descending
                               select new MonthArchiveEntry
                               {
                                   Month = mg.Key,
                                   Entries = mg
                               }
                       };

        }

        public override void Delete(Entry entity)
        {
            base.Delete(entity);

            var tagsToDelete = DbContext.Tags.Include(t => t.Entries).Where(t => t.Entries.Count <= 0).ToList();
            foreach (var tag in tagsToDelete)
            {
                DbContext.Entry(tag).State = EntityState.Deleted;
            }

            var categoriesToDelete = DbContext.Categories.Include(c => c.Entries).Where(c => c.Entries.Count <= 0);

            foreach (var category in categoriesToDelete)
            {
                DbContext.Entry(category).State = EntityState.Deleted;
            }

            DbContext.SaveChanges();
        }
    }

    public interface IEntryRepository : IRepository<Entry>
    {
        Entry GetBySlug(string slug);
        IQueryable<Entry> GetAll();
        IQueryable<Entry> GetByTag(string tag);
        IQueryable<Entry> GetByCategory(string category);
        bool Exists(string slug);
        IQueryable<YearArchiveEntry> GetArchive();
        IQueryable<Entry> Search(string term);
    }
}
