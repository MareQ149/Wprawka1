using Microsoft.EntityFrameworkCore;
using Wprawka1.Models; 

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Character> Characters { get; set; }
}