using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaciVerseApi.Classes
{
    public class Environment2D
    {
        [Key]
        public int EnvironmentId { get; set; }

        [Required, ForeignKey("User")]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int MaxWidth { get; set; }

        [Required]
        public int MaxHeight { get; set; }

        public ICollection<Object2D> Objects { get; set; } = new List<Object2D>();

    }
}
