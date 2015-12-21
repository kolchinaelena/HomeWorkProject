using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayerLibrary.DAL.Repositories;
using BusinessLayerLibrary.Domain.Model;

namespace BusinessLayerLibrary.DAL.EntityFramework.Repositories
{
    internal class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(ContextModel context, DbContextTransaction transaction)
            : base(context, transaction)
        {
        }

        public User GetByID(Int32 id)
        {
            var user = (this.mContext.Users
                .Where(u => u.IdUser == id)
                .Select(u => u)).First();
            return user;

        }

        public User GetByLogin(String login)
        {
            var user = (this.mContext.Users
                .Where(u => u.Login == login)
                .Select(u => u)).FirstOrDefault();
            return user;
        }

        public void Delete(User user)
        {
            int numEntr = -1;
            this.mContext.Users.Remove(user);
            numEntr = this.mContext.SaveChanges();
        }
        public User Save(User user)
        {
            int numEntr = -1;
            if (user.IdUser != 0)
            {
                User tempUser = this.mContext.Users.Where(c=>c.IdUser == user.IdUser).FirstOrDefault<User>();
                tempUser.Name = user.Name;
                tempUser.Login = user.Login;
                tempUser.PasswordHash = user.PasswordHash;  
            }
            else
            {
                this.mContext.Users.Add(user);
            }
            numEntr = this.mContext.SaveChanges();
            if (numEntr > 0) return user;
            return null;
        }

    }

}
