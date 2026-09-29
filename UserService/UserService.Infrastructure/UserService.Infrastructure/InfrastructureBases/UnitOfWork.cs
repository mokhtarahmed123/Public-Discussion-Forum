using Microsoft.EntityFrameworkCore.Storage;
using UserService.Infrastructure.Context;

namespace UserService.Infrastructure.InfrastructureBases
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext appDbContext;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            _transaction = await appDbContext.Database.BeginTransactionAsync();

            return _transaction;
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
                await _transaction.CommitAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
                await _transaction.RollbackAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await appDbContext.SaveChangesAsync();
        }
    }
}
