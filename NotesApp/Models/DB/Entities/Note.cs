using System.ComponentModel.DataAnnotations.Schema;

namespace NotesApp.Models.DB.Entities
{
    public class Note
    {
        [Column(TypeName = "int")]
        public int Id { get; set; }
        [Column(TypeName = "varchar(40)")]
        public string Title { get; set; }
        [Column(TypeName="text")]
        public string Text { get; set; }
        [Column(TypeName = "date")]
        public DateTime CreationDate { get; set; }
    }
}
