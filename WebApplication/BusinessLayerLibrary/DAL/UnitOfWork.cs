using System;

namespace BusinessLayerLibrary.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        Boolean IsDataChanged { get; }

        void Commit();
        void Rollback();
    }
}