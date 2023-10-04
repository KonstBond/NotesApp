namespace NotesApp.Models.Repository
{
    public interface IRepository<T>
    {
        Task<bool> Create(T entity);
        Task<T?> GetById(int id);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task Save();
    }
}
