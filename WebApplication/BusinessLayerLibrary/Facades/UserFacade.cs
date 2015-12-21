using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BusinessLayerLibrary.DAL;
using BusinessLayerLibrary.DAL.Repositories;
using BusinessLayerLibrary.Domain.Model;


namespace BusinessLayerLibrary.Facades
{
    public class UserFacade
    {
        private readonly IDataManagerFactory factory;
        private readonly HashAlgorithm algorithm;

        public UserFacade(IDataManagerFactory factory, HashAlgorithm algorithm)
        {
            this.factory = factory;
            this.algorithm = algorithm;
        }

        public User Validate(String login, String password)
        {
            User user;

            using (var dataManager = factory.GetDataManager())
            {
                var userRepository = dataManager.GetRepository<IUserRepository>();
                user = userRepository.GetByLogin(login);

                if (user == null)
                    return null;
            }

            if (user.PasswordHash == null)
            {
                using (var dataManager = factory.GetDataManager(ConcurrencyLock.Pessimistic))
                {
                    var userRepository = dataManager.GetRepository<IUserRepository>();
                    user = userRepository.GetByLogin(login);

                    if (user == null)
                        throw new NullReferenceException(String.Format("User with login \"{0}\" not found", login));

                    if (user.PasswordHash == null)
                    {
                        user.PasswordHash = algorithm.ComputeHash(new MemoryStream(Encoding.UTF8.GetBytes(password)));
                        userRepository.Save(user);
                        dataManager.Commit();
                        return user;
                    }
                }
            }

            return user.PasswordHash.SequenceEqual(algorithm.ComputeHash(new MemoryStream(Encoding.UTF8.GetBytes(password)))) ? user : null;
        }

        public User CheckLogin(String login)
        {
            using (var dataManager = factory.GetDataManager())
            {
                var userRepository = dataManager.GetRepository<IUserRepository>();
                return userRepository.GetByLogin(login);
            }
        }

        public void SetPassword(String login, String password)
        {
            using (var dataManager = factory.GetDataManager(ConcurrencyLock.Pessimistic))
            {
                var userRepository = dataManager.GetRepository<IUserRepository>();
                var user = userRepository.GetByLogin(login);

                if (user == null)
                    throw new NullReferenceException(String.Format("User with login \"{0}\" not found", login));

                user.PasswordHash = algorithm.ComputeHash(new MemoryStream(Encoding.UTF8.GetBytes(password)));
                userRepository.Save(user);
                dataManager.Commit();
            }
        }

        public void ClearPassword(String login)
        {
            using (var dataManager = factory.GetDataManager(ConcurrencyLock.Pessimistic))
            {
                var userRepository = dataManager.GetRepository<IUserRepository>();
                var user = userRepository.GetByLogin(login);

                if (user == null)
                    throw new NullReferenceException(String.Format("User with login \"{0}\" not found", login));

                user.PasswordHash = null;
                dataManager.Commit();
            }
        }
    }
}
