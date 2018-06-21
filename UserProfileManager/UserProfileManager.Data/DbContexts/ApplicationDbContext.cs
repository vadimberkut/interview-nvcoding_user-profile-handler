using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UserProfileManager.Entity.Entities;

namespace UserProfileManager.Data.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UserProfileEntity> UserProfiles { get; set; }
        public DbSet<UserProfileRoleEntity> UserProfileRoles { get; set; }
        public DbSet<UserProfileSettingsEntity> UserProfileSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserProfileEntity>()
                .HasKey(u => u.Id);
            builder.Entity<UserProfileEntity>()
                .HasOne(u => u.Role)
                .WithMany(r => r.UserProfiles)
                .HasForeignKey(u => u.RoleId)
                .HasConstraintName("ForeignKey_UserProfile_UserProfileRole");
            builder.Entity<UserProfileEntity>()
                .HasOne(u => u.Settings)
                .WithOne(r => r.UserProfile)
                .HasConstraintName("ForeignKey_UserProfile_UserProfileSettings");

            builder.Entity<UserProfileRoleEntity>()
                .HasKey(u => u.Id);

            builder.Entity<UserProfileSettingsEntity>()
                .HasKey(u => u.Id);
        }
    }
}
