using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TechDaily.Entities;

namespace TechDaily.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Question> Questions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<QuestionsSubCategory> QuestionsSubCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).HasMaxLength(250);
        });
        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).HasMaxLength(250);
            entity.Property(e => e.Priority).HasMaxLength(250);
            entity.Property(e => e.Answer).HasMaxLength(500);
            entity.Property(e => e.CategoryId).HasMaxLength(500);
            entity.Navigation(e => e.Category).AutoInclude();
            entity.Navigation(e => e.SubCategories).AutoInclude();
        });
        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).HasMaxLength(250);
            entity.Property(e => e.CategoryId).HasMaxLength(250);
            entity.Navigation(e => e.Category).AutoInclude();
        });
        modelBuilder.Entity<QuestionsSubCategory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.QuestionId).HasMaxLength(250);
            entity.Property(e => e.SubCategoryId).HasMaxLength(250);
            entity.Navigation(e => e.SubCategory).AutoInclude();
        });
        
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }
}