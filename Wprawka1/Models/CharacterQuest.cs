public class CharacterQuest
{
    public int CharacterId { get; set; }
    public Character Character { get; set; } = null!;

    public int QuestId { get; set; }
    public Quest Quest { get; set; } = null!;
}