using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using Autofac;
using BusinessLayerLibrary.DAL.EntityFramework.Repositories;
using BusinessLayerLibrary.DAL.Repositories;
using BusinessLayerLibrary.Domain.Model;
using BusinessLayerLibrary.Infrastructure;

namespace BusinessLayerLibrary.DAL.EntityFramework
{
    class DataManager: IDataManager,IRepository
    {
        private readonly IDbConfig config;
        private readonly ContextModel contextDB;
        private readonly DbContextTransaction transaction;
        private readonly ConcurrencyLock? lockType;
        private readonly ContainerBuilder builder;

        public DataManager(IDbConfig config)
            : this(config, null)
        { }
        public DataManager(IDbConfig config, ConcurrencyLock? lockType)
        {
            this.config = config;
            this.lockType = lockType;

            contextDB = InitContext();
            transaction = InitTransaction();
            builder = new ContainerBuilder();
            builder.RegisterInstance(transaction).SingleInstance();
            builder.RegisterInstance(contextDB).SingleInstance();
            builder.RegisterType<OfferRepository>().As<IOfferRepository>().InstancePerDependency();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerDependency();
           // var container=builder.Build();

        }
     
        ContextModel InitContext()
        {
            ContextModel context = new ContextModel(config.SqlConnectionString);
            context.Database.Initialize(true);
            return context;
        }
        DbContextTransaction InitTransaction()
        {
            if (lockType == null)
                return contextDB.Database.BeginTransaction(IsolationLevel.ReadCommitted);

            switch (lockType.Value)
            {
                case ConcurrencyLock.Optimistic:
                    return contextDB.Database.BeginTransaction(IsolationLevel.RepeatableRead);

                case ConcurrencyLock.Pessimistic:
                    return contextDB.Database.BeginTransaction(IsolationLevel.Serializable);
            }

            throw new InvalidOperationException("Неизвестный тип блокировки");
        }

        ContainerBuilder InitLocator()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(transaction).InstancePerDependency();
            builder.RegisterInstance(contextDB).InstancePerDependency();
            builder.RegisterType<OfferRepository>().As<IOfferRepository>().InstancePerDependency();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerDependency();
            return builder;
        }

        public T GetRepository<T>() where T : class, IRepository
        {
            var container = builder.Build();
            T repository;

            using (var scope = container.BeginLifetimeScope())
            {
                repository = scope.Resolve<T>();
            }

            var baseRepository = repository as RepositoryBase;
            if (baseRepository != null)
                baseRepository.Lock = lockType;
            return repository;
        }

        public void Commit()
        {
            transaction.Commit();
        }
        public void Rollback()
        {
            transaction.Rollback();
        }

        public Boolean IsDataChanged
        { get {return false;} }

        public void Dispose()
        {
            if (transaction != null)
                transaction.Dispose();

            if (contextDB != null)
                contextDB.Dispose();
        }

    }
}
