namespace NotesApp.Models.Repository
{
    public interface IRepository<T>
    {
        bool Create(T entity);
        T GetById(int id);
        void Update(T entity);
        void Delete(int id);
    }
}
