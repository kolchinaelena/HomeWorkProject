using BusinessLayerLibrary.DAL.Repositories;

namespace BusinessLayerLibrary.DAL
{
    public interface IRepositoryFactory
    {
        T GetRepository<T>() where T : class, IRepository;
       
    }
}