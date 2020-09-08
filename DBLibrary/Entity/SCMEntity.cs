namespace DBLibrary.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SCMEntity : DbContext
    {
        public SCMEntity()
            : base("name=SCMEntity")
        {
        }

        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<PromoItem> PromoItems { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .HasMany(e => e.PromoItems)
                .WithRequired(e => e.Item)
                .WillCascadeOnDelete(false);
        }
    }
}
