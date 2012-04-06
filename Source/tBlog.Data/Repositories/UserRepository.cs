using System.Linq;
using System.Web.Security;
using tBlog.Data.Abstract;
using tBlog.Data.Infrastructure;
using tBlog.Domain.Entities;

namespace tBlog.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        bool ValidateUser(string username, string password);
        void CreateUser(string userName, string password, out MembershipCreateStatus createStatus);
        User GetByName(string username);
    }

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public User GetByName(string username)
        {
            return base.GetAll().FirstOrDefault(u => u.UserName == username);
        }
        
        public bool ValidateUser(string username, string password)
        {
            if (base.GetAll().FirstOrDefault(u => u.UserName == username && u.Password == password) != null)
                return true;
            return false;
        }

        public void CreateUser(string userName, string password, out MembershipCreateStatus createStatus)
        {
            if(base.GetAll().FirstOrDefault(u => u.UserName == userName) != null)
            {
                createStatus = MembershipCreateStatus.DuplicateUserName;
                return;
            }

            if (string.IsNullOrEmpty(userName))
            {
                createStatus = MembershipCreateStatus.InvalidUserName;
                return;
            }

            var user = new User
                           {
                               UserName = userName,
                               Password = password
                           };

            base.Save(user);

            createStatus = MembershipCreateStatus.Success;
        }
    }
}
