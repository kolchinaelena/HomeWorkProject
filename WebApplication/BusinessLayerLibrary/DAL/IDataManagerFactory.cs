namespace BusinessLayerLibrary.DAL
{
    public interface IDataManagerFactory
    {
        /// <summary> Возвращает менеджер данных / UnitOfWork </summary>
        IDataManager GetDataManager();

        /// <summary> Возвращает менеджер данных / UnitOfWork с гарантированным типом конкурентного доступа </summary>
        IDataManager GetDataManager(ConcurrencyLock lockType);
    }
}