using NotesApp.Models.Entities;

namespace NotesApp.Models.Repository
{
    public interface INoteRepository : IRepository<Note>
    {
        IEnumerable<Note> GetAll();
    }
}
