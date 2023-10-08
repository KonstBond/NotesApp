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

        public async Task Create(Note note)
        {
            await _dbContext.AddAsync(note);
        }


        public async Task Delete(Note note)
        {
            await Task.Run(() => _dbContext.Notes.Remove(note));
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

        public async Task Update(Note note)
        {
            await Task.Run(() => _dbContext.Update(note));
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
