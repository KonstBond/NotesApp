using NotesApp.Models.DTO;

namespace NotesApp.Models.Manager
{
    public interface INoteManager
    {
        Task CreateNewNote(NoteDto noteDto);
        Task<NoteDto> FindNoteByTitle(string title);
        Task DeleteNote(string title);
        /// <summary>
        /// Edit old note by the title
        /// </summary>
        /// <param name="newNoteDto">new note model</param>
        /// <param name="oldDtoTitle">old note title</param>
        Task EditNote(NoteDto newNoteDto, string oldNoteTitle);
        Task<IEnumerable<NoteDto>> GetAllNotes();
    }
}
