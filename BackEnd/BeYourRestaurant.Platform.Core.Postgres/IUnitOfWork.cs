using System;

namespace BeYourRestaurant.Platform.Core.Postgres
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
