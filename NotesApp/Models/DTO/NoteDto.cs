using System.ComponentModel.DataAnnotations;

namespace NotesApp.Models.DTO
{
    public class NoteDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
