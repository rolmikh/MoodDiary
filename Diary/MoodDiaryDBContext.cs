using Microsoft.EntityFrameworkCore;

namespace Diary
{
    public class MoodDiaryDBContext : DbContext
    {
        public MoodDiaryDBContext(DbContextOptions<MoodDiaryDBContext> options) : base(options) { }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Emoji> Emojis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable(nameof(Posts));

                entity.HasKey(e => e.IdPost);

                entity.Property(e => e.PostText)
                .IsRequired(true)
                .HasMaxLength(100);

                entity.Property(e => e.EmojiId)
                .IsRequired(true);

                entity.HasOne(e => e.Emoji)
                .WithMany(t => t.Posts)
                .HasForeignKey(e => e.EmojiId)
                .OnDelete(DeleteBehavior.Cascade);
            }
            );

            modelBuilder.Entity<Emoji>(entity =>
            {
                entity.ToTable(nameof(Emojis));

                entity.HasKey(e => e.IdEmoji);

                entity.Property(e => e.NameEmoji)
                .IsRequired(true)
                .HasMaxLength(100);

            });
        }
    }
}
