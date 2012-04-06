namespace tBlog.Data.Infrastructure
{
    public interface IDatabaseFactory
    {
        BlogDbContext Get();
    }

    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private BlogDbContext _dbContext;

        public BlogDbContext Get()
        {
            return _dbContext ?? (_dbContext = new BlogDbContext());
        }

        protected override void DisposeCore()
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }
    }
}
