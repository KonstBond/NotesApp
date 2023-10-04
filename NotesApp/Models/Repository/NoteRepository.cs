using Microsoft.EntityFrameworkCore;
using NotesApp.Models.DB;
using NotesApp.Models.DB.Entities;

namespace NotesApp.Models.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly NoteDbContext _dbContext;

        public NoteRepository(NoteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Note note)
        {
            if (await GetByTitle(note.Title) is not null)
            {
                await _dbContext.AddAsync(note);
                return true;
            }
            else throw new ArgumentException($"Note with title {note.Title} already exists", note.Title);
        }

        public async Task<bool> Delete(Note note)
        {
            if (note is not null)
            {
                await Task.Run(() => _dbContext.Notes.Remove(note));
                return true;
            }
            else throw new ArgumentException($"Note {note} not exists", note?.ToString());
        }

        public async Task<IEnumerable<Note>> GetAll()
        {
            return await _dbContext.Notes.ToListAsync();
        }

        public async Task<Note?> GetById(int id)
        {
            return await _dbContext.Notes
                .FirstOrDefaultAsync(note => note.Id == id);
        }

        public async Task<Note?> GetByTitle(string title)
        {
            return await _dbContext.Notes
                .FirstOrDefaultAsync(note => note.Title == title);
        }

        public async Task<bool> Update(Note note)
        {
            if (note is not null)
            {
                await Task.Run(() => _dbContext.Update(note));
                return true;
            }
            else throw new ArgumentException($"Note with title {note?.Title} not exists", note?.Title);
        }

        public async Task Save() => await _dbContext.SaveChangesAsync();
    }
}
