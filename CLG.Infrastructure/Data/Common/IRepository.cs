namespace CLG.Infrastructure.Data.Common
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;

        Task AddAsync<T>(T entity) where T : class;

        IQueryable<T> All<T>() where T : class;

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
