using Microsoft.EntityFrameworkCore;
using Wprawka1.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Character> Characters { get; set; }
    public DbSet<Guild> Guilds { get; set; }
    public DbSet<Quest> Quests { get; set; }
    public DbSet<CharacterQuest> CharacterQuests { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<CharacterQuest>()
            .HasKey(cq => new { cq.CharacterId, cq.QuestId });

        modelBuilder.Entity<CharacterQuest>()
            .HasOne(cq => cq.Character)
            .WithMany(c => c.CharacterQuests)
            .HasForeignKey(cq => cq.CharacterId);

        modelBuilder.Entity<CharacterQuest>()
            .HasOne(cq => cq.Quest)
            .WithMany(q => q.CharacterQuests)
            .HasForeignKey(cq => cq.QuestId);
    }
}