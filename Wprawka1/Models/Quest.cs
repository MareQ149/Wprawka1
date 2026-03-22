using System.ComponentModel.DataAnnotations;

public class Quest
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Title { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; }

    
    public List<CharacterQuest> CharacterQuests { get; set; } = new();
}