using System.Data.Entity.Infrastructure;

namespace BusinessLayerLibrary.Domain.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
   
    public partial class ContextModel : DbContext
    {
        static ContextModel()
        {            
        }
        public ContextModel(string connectionString)
            : base(connectionString)
        {

        }

        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Offer>()
                .Property(e => e.NameOffer)
                .IsUnicode(false);

            modelBuilder.Entity<Offer>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Offer>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Offers)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }


    }
}
