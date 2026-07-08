using Microsoft.EntityFrameworkCore;

namespace Diary.Models
{
    public class MoodDiaryDBContext : DbContext
    {
        public MoodDiaryDBContext(DbContextOptions<MoodDiaryDBContext> options) : base(options) { }

        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Emoji> Emoji { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable(nameof(Post));

                entity.HasKey(e => e.IdPost);

                entity.Property(e => e.PostText)
                .IsRequired(false);

                entity.Property(e => e.EmojiId)
                .IsRequired(true);

                entity.Property(e => e.UserId)
                .IsRequired(false);

                entity.Property(e => e.CreatedAt);

                entity.HasOne(e => e.Emoji)
                .WithMany(t => t.Posts)
                .HasForeignKey(e => e.EmojiId)
                .OnDelete(DeleteBehavior.Cascade);
            }
            );

            modelBuilder.Entity<Emoji>(entity =>
            {
                entity.ToTable(nameof(Emoji));

                entity.HasKey(e => e.IdEmoji);

                entity.Property(e => e.NameEmoji)
                .IsRequired(true)
                .HasMaxLength(100);

                entity.Property(e => e.CodeEmoji)
                .IsRequired(true);

                entity.Property(e => e.IsPositive);

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(nameof(User));

                entity.HasKey(e => e.IdUser);

                entity.Property(e => e.UserName)
                .IsRequired(true)
                .HasMaxLength(100);

                entity.Property(e => e.CreatedAt)
                .IsRequired(true);

                entity.Property(e => e.BirthdayDate)
                .IsRequired(true);

                entity.Property(e => e.Email)
                .IsRequired(true);

                entity.Property(e => e.Password)
                .IsRequired(true);

                

            });
        }
    }
}
