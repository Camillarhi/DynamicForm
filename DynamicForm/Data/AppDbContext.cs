using DynamicForm.Models;
using DynamicForm.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace DynamicForm.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ProgramConfiguration> ProgramConfigurations { get; set; }
        public DbSet<ApplicationFormConfiguration> ApplicationFormConfigurations { get; set; }
        public DbSet<QuestionConfiguration> QuestionConfigurations { get; set; }
        public DbSet<CandidateApplication> CandidateApplications { get; set; }
        public DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasManualThroughput(400);

            builder.Entity<ProgramConfiguration>()
             .ToContainer(nameof(ProgramConfiguration))
             .HasPartitionKey(c => c.Id)
             .HasNoDiscriminator();

            builder.Entity<ApplicationFormConfiguration>()
            .ToContainer(nameof(ApplicationFormConfiguration))
            .HasPartitionKey(c => c.Id)
            .HasNoDiscriminator();

            builder.Entity<QuestionConfiguration>()
            .ToContainer(nameof(QuestionConfiguration))
            .HasPartitionKey(c => c.Id)
            .HasNoDiscriminator();

            builder.Entity<CandidateApplication>()
            .ToContainer(nameof(CandidateApplication))
            .HasPartitionKey(c => c.Id)
            .HasNoDiscriminator();

            builder.Entity<Question>()
            .ToContainer(nameof(Question))
            .HasPartitionKey(c => c.Id)
            .HasNoDiscriminator();

            builder.Entity<ProgramConfiguration>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<ApplicationFormConfiguration>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<QuestionConfiguration>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<CandidateApplication>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Question>().HasQueryFilter(e => !e.IsDeleted);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
