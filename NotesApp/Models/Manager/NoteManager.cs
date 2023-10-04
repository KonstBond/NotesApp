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
        public NoteManager(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

        public async void CreateNewNote(NoteDto noteDto)
        {
            Note? note = await _noteRepository.GetByTitle(noteDto.Title);
            if (note is null)
            {
                note = DataEntry(noteDto);
                try
                {
                    if (await _noteRepository.Create(note))
                        await _noteRepository.Save();
                }
                catch (ArgumentException ex)
                {
                    _logger.LogError(ex, $"Note has not created", noteDto);
                }  
            }
            else
            {
                //Такая запись уже есть в базе
            }
        }

        public Note DataEntry(NoteDto noteDto)
        {
            Note note = _mapper.Map<Note>(noteDto);
            note.CreationDate = DateTime.Now;
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
