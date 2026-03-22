using System.ComponentModel.DataAnnotations;

namespace Wprawka1.Models
{
    public class Character
    {
        public int Id { get; set; }

        
        public string Name { get; set; } = null!;

        
        public string Description { get; set; }

        
        public string Class { get; set; }

        public int Level { get; set; }
    }
}
