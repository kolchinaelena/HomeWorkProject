namespace BusinessLayerLibrary.DAL.Repositories
{
    /// <summary>
    /// Репозиторий предоставляет удобную возможность работы с базой данной на языке классов этого проекта
    /// Реализации репозитория в этом проекте не ответственны за создание подключения и управление транзакциями - это обязанность IDataManager
    /// </summary>
    public interface IRepository
    {}

    public interface IRepository<T> : IRepository
    {
        T Save(T item);

        void Delete(T item);
       
    }

   
}