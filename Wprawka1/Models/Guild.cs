using System.ComponentModel.DataAnnotations;

public class Guild
{
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; } = null!;

    [MaxLength(200)]
    public string? Description { get; set; }

    public List<Character> Members { get; set; } = new();
}