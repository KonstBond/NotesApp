using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NotesApp.Models.DTO;
using NotesApp.Models.Manager;
using System.ComponentModel.DataAnnotations;

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

        [HttpGet]
        public IActionResult NewNote() => View();

        [HttpPost]
        public async Task<IActionResult> NewNote(NoteDto noteDto)
        {
            if (ModelState.IsValid)
            {
                await _noteManager.CreateNewNote(noteDto);
                return RedirectToAction("Index");
            }

            string message = "";
            foreach (var item in ModelState)
            {
                if (item.Value.ValidationState == ModelValidationState.Invalid)
                {
                    message += $"\nNot valid: {item.Key}\n";
                    foreach (var error in item.Value.Errors)
                    {
                        message += $"{error.ErrorMessage}\n";
                    }
                }
            }
            return BadRequest(message);
        }
    }
}
