using NotesApp.Models.DB.Entities;

namespace NotesApp.Models.Repository
{
    public interface INoteRepository : IRepository<Note>
    {
        Task<IEnumerable<Note>> GetAll();
        Task<Note?> GetByTitle(string name);
    }
}
