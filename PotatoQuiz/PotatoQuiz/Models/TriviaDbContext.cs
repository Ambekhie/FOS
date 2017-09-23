using System.Data.Entity;

namespace GeekQuiz.Models
{
    public class TriviaDbContext : DbContext
    {
        private static bool _created = false;

        public TriviaDbContext() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TriviaOption>()
                .HasKey(o => new { o.QuestionId, o.Id });

            modelBuilder.Entity<TriviaAnswer>()
                .HasRequired(a => a.TriviaOption)
                .WithMany()
                .HasForeignKey(a => new { a.QuestionId, a.OptionId });

            modelBuilder.Entity<TriviaQuestion>()
                .HasMany(q => q.Options)
                .WithRequired(o => o.TriviaQuestion);
        }
        
        public DbSet<TriviaQuestion> TriviaQuestions { get; set; }

        public DbSet<TriviaOption> TriviaOptions { get; set; }

        public DbSet<TriviaAnswer> TriviaAnswers { get; set; }
    }
}
