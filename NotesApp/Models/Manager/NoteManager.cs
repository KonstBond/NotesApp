using AutoMapper;
using NotesApp.Models.DB.Entities;
using NotesApp.Models.DTO;
using NotesApp.Models.Repository;
using Npgsql;
using System.Data.SqlTypes;

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
            if (await _noteRepository.GetByTitle(noteDto.Title) is null)
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
            catch (NpgsqlException ex)
            {
                throw new NpgsqlException("Note has not created", ex);
            }
        }

        private Note DataEntry(NoteDto noteDto)
        {
            Note note = _mapper.Map<Note>(noteDto);
            note.CreationDate = DateTime.Now.Date;
            return note;
        }

        public async Task<NoteDto> FindNoteByTitle(string title)
        {
            Note? note = await _noteRepository.GetByTitle(title);
            if (note is not null)
                return _mapper.Map<NoteDto>(note);
            else
                throw new SqlNullValueException($"Note {title} not exists");
        }

        public async Task EditNote(NoteDto newNoteDto, string oldNoteTitle)
        {
            Note? note = await _noteRepository.GetByTitle(oldNoteTitle);
            if (note is not null)
            {
                await EditNote(note, newNoteDto);
                _logger.LogInformation($"Note {oldNoteTitle} has been edited", newNoteDto);
            }                
            else
                throw new SqlNullValueException($"Note {oldNoteTitle} not found");
        }

        private async Task EditNote(Note note, NoteDto newNoteDto)
        {
            if  (await _noteRepository.GetByTitle(newNoteDto.Title) is null)
            {
                note.Title = newNoteDto.Title;
                note.Text = newNoteDto.Text;
                note.CreationDate = DateTime.Now.Date;
                await _noteRepository.Update(note);
                await _noteRepository.Save();
            }   
            else
                throw new ArgumentException($"Note with title: {newNoteDto.Title} already exists");
        }

        public async Task DeleteNote(string noteTitle)
        {
            Note? note = await _noteRepository.GetByTitle(noteTitle);
            if (note is not null)
            {
                await _noteRepository.Delete(note);
                await _noteRepository.Save();
                _logger.LogInformation($"Note {noteTitle} has been deleted", note);
            }
            else
                throw new SqlNullValueException($"Note {noteTitle} not found");
        }

        public async Task<IEnumerable<NoteDto>> GetAllNotes()
        {
            return _mapper.Map<IEnumerable<NoteDto>>(await _noteRepository.GetAll());
        }
    }
}
