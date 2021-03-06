﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserProfileManager.Data.DbContexts;

namespace UserProfileManager.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UserProfileManager.Entity.Entities.UserProfileEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOnUtc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<Guid>("RoleId");

                    b.Property<string>("Signature")
                        .HasMaxLength(200);

                    b.Property<string>("SkypeLogin")
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdatedOnUtc")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("UserProfileManager.Entity.Entities.UserProfileRoleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("UserProfileRoles");
                });

            modelBuilder.Entity("UserProfileManager.Entity.Entities.UserProfileSettingsEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Enabled");

                    b.Property<Guid>("UserProfileId");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileId")
                        .IsUnique();

                    b.ToTable("UserProfileSettings");
                });

            modelBuilder.Entity("UserProfileManager.Entity.Entities.UserProfileEntity", b =>
                {
                    b.HasOne("UserProfileManager.Entity.Entities.UserProfileRoleEntity", "Role")
                        .WithMany("UserProfiles")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("ForeignKey_UserProfile_UserProfileRole")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UserProfileManager.Entity.Entities.UserProfileSettingsEntity", b =>
                {
                    b.HasOne("UserProfileManager.Entity.Entities.UserProfileEntity", "UserProfile")
                        .WithOne("Settings")
                        .HasForeignKey("UserProfileManager.Entity.Entities.UserProfileSettingsEntity", "UserProfileId")
                        .HasConstraintName("ForeignKey_UserProfile_UserProfileSettings")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
