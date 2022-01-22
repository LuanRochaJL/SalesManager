namespace SalesManager.Repository.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T Ent);
        void Update(T Ent);
        void Delete(T Ent);
        Task<T> GetByIdAsync(int Id);
        Task<T[]> ListAsync();
        Task<bool> SaveChangesAsync();
    }
}