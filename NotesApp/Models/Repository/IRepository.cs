namespace NotesApp.Models.Repository
{
    public interface IRepository<T>
    {
        Task Create(T entity);
        Task<T?> GetById(int id);
        Task Update(T entity);
        Task Delete(T entity);
        Task Save();
    }
}
