using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Persistence.EntityFramework
{
    public class ArtistDbContext : DbContext
    {
        public ArtistDbContext()
        {

        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Alias> Aliases { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Artist>().ToTable("Artists");
            modelBuilder.Entity<Artist>().HasKey(i => i.Artist_Id);
            modelBuilder.Entity<Artist>()
              .Property(i => i.Artist_Id)
              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
              .HasColumnName("Artist_Id");


            modelBuilder.Entity<Alias>().ToTable("Aliases");
            modelBuilder.Entity<Alias>().HasKey(i => i.Alias_Id);
            modelBuilder.Entity<Alias>()
              .Property(i => i.Alias_Id)
              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
              .HasColumnName("Alias_Id");

            modelBuilder.Entity<Alias>()
                    .HasRequired<Artist>(a => a.Artist) 
                    .WithMany(a => a.Aliases);
        }
    }
}
