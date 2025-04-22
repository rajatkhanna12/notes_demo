using System.ComponentModel.DataAnnotations;

namespace NotesProject.Models
{
    public class NotesModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string CardType { get; set; }
    }
}
