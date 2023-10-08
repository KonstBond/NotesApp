using AutoMapper;
using NotesApp.Models.DB.Entities;
using NotesApp.Models.DTO;
using NotesApp.Models.Repository;

namespace NotesApp.Models.Manager
{
    public class NoteManager : INoteManager
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<NoteManager> _logger;
        public NoteManager(
            INoteRepository noteRepository,
            IMapper mapper,
            ILogger<NoteManager> logger)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateNewNote(NoteDto noteDto)
        {
            if (_noteRepository.GetByTitle(noteDto.Title) is null)
            {
                await WriteNoteToDB(noteDto);
                _logger.LogInformation("Note has been created", noteDto);
            }     
            else 
                throw new ArgumentException($"Note with title {noteDto.Title} already exists", noteDto.Title);
        }

        private async Task WriteNoteToDB(NoteDto noteDto)
        {
            Note note = DataEntry(noteDto);
            try
            {
                await _noteRepository.Create(note);
                await _noteRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Note has not created", ex);
            }
        }

        private Note DataEntry(NoteDto noteDto)
        {
            Note note = _mapper.Map<Note>(noteDto);
            note.CreationDate = DateTime.Now.Date;
            return note;
        }

        public void DeleteNote()
        {
            throw new NotImplementedException();
        }

        public void EditNote(NoteDto noteDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<NoteDto>> GetAllNotes()
        {
            return _mapper.Map<IEnumerable<NoteDto>>(await _noteRepository.GetAll());
        }
    }
}
