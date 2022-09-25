using EA.BlogProject.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA.BlogProject.Data.Concrete.EntityFramework.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Picture).IsRequired();
            builder.Property(u => u.Picture).HasMaxLength(250);
            // Primary key
            builder.HasKey(u => u.Id);
             
            builder.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");
             
            builder.ToTable("AspNetUsers");
             
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
             
            builder.Property(u => u.UserName).HasMaxLength(50);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(50);
            builder.Property(u => u.Email).HasMaxLength(100);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(100);
             
            builder.HasMany<UserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
             
            builder.HasMany<UserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
             
            builder.HasMany<UserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
             
            builder.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

            var adminUser = new User
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@hotmail.com",
                NormalizedEmail = "ADMIN@HOTMAIL.COM",
                PhoneNumber = "+905555555555",
                Picture = "defaultUser.jpg",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            adminUser.PasswordHash = CreatePasswordHash(adminUser, "12345");

            var editorUser = new User
            {
                Id = 2,
                UserName = "editor",
                NormalizedUserName = "EDITOR",
                Email = "editor@hotmail.com",
                NormalizedEmail = "EDITOR@HOTMAIL.COM",
                PhoneNumber = "+905555555555",
                Picture = "defaultUser.jpg",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            editorUser.PasswordHash = CreatePasswordHash(editorUser, "12345");

            builder.HasData(adminUser, editorUser);

        }

        private string CreatePasswordHash(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            return passwordHasher.HashPassword(user, password);
        }

        #region User Tablosu Identity den gelmeseydi yani kendimiz oluştursaydık aşağıdaki gibi tanımlayacaktık
        //public void Configure(EntityTypeBuilder<User> builder)
        //{
        //    builder.HasKey(u => u.Id);
        //    builder.Property(u => u.Id).ValueGeneratedOnAdd();

        //    builder.Property(u => u.FirstName).HasMaxLength(50);
        //    builder.Property(u => u.FirstName).IsRequired();

        //    builder.Property(u => u.LastName).HasMaxLength(50);
        //    builder.Property(u => u.LastName).IsRequired();

        //    builder.Property(u => u.Username).HasMaxLength(50);
        //    builder.Property(u => u.Username).IsRequired();
        //    builder.HasIndex(u => u.Username).IsUnique(); // username alanın uniqe olmasını sağlamak için

        //    builder.Property(u => u.PasswordHash).HasColumnName("VARBINARY(500)");  // Binary olarak tutmak için
        //    builder.Property(u => u.PasswordHash).IsRequired();

        //    builder.Property(u => u.Email).HasMaxLength(50);
        //    builder.Property(u => u.Email).IsRequired();
        //    builder.HasIndex(u => u.Email).IsUnique(); // email adresinin uniqe olmasını sağlamak için, aynı email ile sadece 1 defa kayıt olunabilir

        //    builder.HasOne<Role>(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);

        //    builder.Property(u => u.Picture).HasMaxLength(250);
        //    builder.Property(u => u.Picture).IsRequired();

        //    builder.Property(u => u.Description).HasMaxLength(500);

        //    builder.Property(u => u.CreatedByName).IsRequired();
        //    builder.Property(u => u.CreatedByName).HasMaxLength(50);

        //    builder.Property(u => u.ModifiedByName).IsRequired();
        //    builder.Property(u => u.ModifiedByName).HasMaxLength(50);

        //    builder.Property(u => u.CreatedDate).IsRequired();

        //    builder.Property(u => u.ModifiedDate).IsRequired();

        //    builder.Property(u => u.IsActive).IsRequired();

        //    builder.Property(u => u.IsDeleted).IsRequired();

        //    builder.Property(u => u.Note).HasMaxLength(500);

        //    builder.ToTable("Users");

        //    builder.HasData(new User
        //    {
        //        Id = 1,
        //        RoleId = 1,
        //        FirstName = "Emre",
        //        LastName = "Aktürk",
        //        Username = "emreakturk",
        //        Email = "e@hotmail.com",
        //        Description = "İlk Admin kullanıcısı",
        //        IsActive = true,
        //        IsDeleted = false,
        //        CreatedByName = "InitialCreate",
        //        CreatedDate = DateTime.Now,
        //        ModifiedByName = "InitialCreate",
        //        ModifiedDate = DateTime.Now,
        //        Note = "Admin kullanıcısı",
        //        PasswordHash = Encoding.ASCII.GetBytes("c4ca4238a0b923820dcc509a6f75849b"),
        //        Picture = "https://upload.wikimedia.org/wikipedia/commons/5/50/User_icon-cp.svg"
        //    });
        //} 
        #endregion
    }
}
