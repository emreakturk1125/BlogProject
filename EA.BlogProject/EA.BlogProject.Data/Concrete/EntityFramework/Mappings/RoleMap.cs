using EA.BlogProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA.BlogProject.Data.Concrete.EntityFramework.Mappings
{
   
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Primary key
            builder.HasKey(r => r.Id);

            // Index for "normalized" role name to allow efficient lookups
            builder.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();

            // Maps to the AspNetRoles table
            builder.ToTable("AspNetRoles");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.Name).HasMaxLength(100);
            builder.Property(u => u.NormalizedName).HasMaxLength(100);

            // The relationships between Role and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each Role can have many entries in the UserRole join table
            builder.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

            // Each Role can have many associated RoleClaims
            builder.HasMany<RoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();

            builder.HasData(

                new Role
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp =  Guid.NewGuid().ToString() 
                },
                new Role
                {
                    Id = 2,
                    Name = "Editör",
                    NormalizedName = "EDITOR",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }

             );

        }


        #region  Role Tablosu Identity den gelmeseydi yani kendimiz oluştursaydık aşağıdaki gibi tanımlayacaktık

        // Veritabanı tablolarını oluştururken, data ile birlikte oluşturulması için HasData'lı kısım
        // henüz db olmadğı için id ye de değer veriyoruz

        //public void Configure(EntityTypeBuilder<Role> builder)
        //{
        //    builder.HasKey(r => r.Id);
        //    builder.Property(r => r.Id).ValueGeneratedOnAdd();

        //    builder.Property(r => r.Name).HasMaxLength(50);
        //    builder.Property(r => r.Name).IsRequired();

        //    builder.Property(r => r.Description).HasMaxLength(500);
        //    builder.Property(r => r.Description).IsRequired();

        //    builder.Property(c => c.CreatedByName).IsRequired();
        //    builder.Property(c => c.CreatedByName).HasMaxLength(50);

        //    builder.Property(c => c.ModifiedByName).IsRequired();
        //    builder.Property(c => c.ModifiedByName).HasMaxLength(50);

        //    builder.Property(c => c.CreatedDate).IsRequired();

        //    builder.Property(c => c.ModifiedDate).IsRequired();

        //    builder.Property(c => c.IsActive).IsRequired();

        //    builder.Property(c => c.IsDeleted).IsRequired();

        //    builder.Property(c => c.Note).HasMaxLength(500);

        //    builder.ToTable("Roles");

        //    builder.HasData(new Role
        //    {
        //        Id = 1,
        //        Name = "Admin",
        //        Description = "Admin, tüm haklara sahiptir.",
        //        IsActive = true,
        //        IsDeleted = false,
        //        CreatedByName = "InitialCreate",
        //        CreatedDate = DateTime.Now,
        //        ModifiedByName = "InitialCreate",
        //        ModifiedDate = DateTime.Now,
        //        Note = "Admin Rolüdür."
        //    });
        //} 
        #endregion
    }
}
