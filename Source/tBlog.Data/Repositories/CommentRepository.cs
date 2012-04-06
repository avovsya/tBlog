using System.Linq;
using tBlog.Data.Abstract;
using tBlog.Data.Infrastructure;
using tBlog.Domain.Entities;

namespace tBlog.Data.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IQueryable<Comment> GetCommentsForEntry(string slug);
    }

    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        private readonly IEntryRepository _entryRepository;

        public CommentRepository(IDatabaseFactory databaseFactory, IEntryRepository entryRepository)
            : base(databaseFactory)
        {
            _entryRepository = entryRepository;
        }

        public IQueryable<Comment> GetCommentsForEntry(string slug)
        {
            var entryId = _entryRepository.GetBySlug(slug).Id;

            return base.GetAll().Where(c => c.EntryId == entryId).OrderByDescending(c => c.CreatedAt);
        }
    }
}
