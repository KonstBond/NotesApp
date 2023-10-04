using Microsoft.AspNetCore.Mvc;
using NotesApp.Models.DTO;
using NotesApp.Models.Manager;

namespace NotesApp.Controllers
{
    public class NoteController : Controller
    {
        private readonly INoteManager _noteManager;

        public NoteController(INoteManager noteManager)
        {
            _noteManager = noteManager;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<NoteDto> noteDtos = await _noteManager.GetAllNotes();
            return View(noteDtos);
        }
    }
}
