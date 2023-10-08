using NotesApp.Models.DTO;

namespace NotesApp.Models.Manager
{
    public interface INoteManager
    {
        Task CreateNewNote(NoteDto noteDto);
        void DeleteNote();
        void EditNote(NoteDto noteDto);
        Task<IEnumerable<NoteDto>> GetAllNotes();
    }
}
