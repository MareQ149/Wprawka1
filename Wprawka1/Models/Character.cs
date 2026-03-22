using System.ComponentModel.DataAnnotations;


public class Character
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [MaxLength(200)]
    public string Description { get; set; }

    [MaxLength(50)]
    public string Class { get; set; }

    public int Level { get; set; }
}