﻿// <auto-generated />
using System;
using LCPSNWebApi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LCPSNWebApi.Migrations.SQLite
{
    [DbContext(typeof(DBContextSQLite))]
    partial class DBContextSQLiteModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("LCPSNWebApi.Classes.Attachment", b =>
                {
                    b.Property<int?>("AttachmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AttachmentType")
                        .HasColumnType("TEXT");

                    b.Property<string>("AttachmentUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateAttachmentUploaded")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("AttachmentId");

                    b.ToTable("Attachments", t =>
                        {
                            t.HasTrigger("Attachments_Trigger");
                        });
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.Comment", b =>
                {
                    b.Property<int?>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DatePostCreated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CommentId");

                    b.ToTable("Comments", t =>
                        {
                            t.HasTrigger("Comments_Trigger");
                        });
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.Files.FileData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FileFullPath")
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("FileSize")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GId")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("ImageBytes")
                        .HasColumnType("BLOB");

                    b.HasKey("Id");

                    b.ToTable("FilesData", t =>
                        {
                            t.HasTrigger("FilesData_Trigger");
                        });
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.Friend", b =>
                {
                    b.Property<int?>("FriendId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Biography")
                        .HasColumnType("TEXT");

                    b.Property<string>("CoverUrl")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateAccountCreated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("FriendId");

                    b.ToTable("Friends", t =>
                        {
                            t.HasTrigger("Friends_Trigger");
                        });
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.Post", b =>
                {
                    b.Property<int?>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DatePostCreated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PostId");

                    b.ToTable("Posts", t =>
                        {
                            t.HasTrigger("Posts_Trigger");
                        });
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.User", b =>
                {
                    b.Property<int?>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Biography")
                        .HasColumnType("TEXT");

                    b.Property<string>("CoverUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("CurrentToken")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateAccountCreated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users", t =>
                        {
                            t.HasTrigger("Users_Trigger");
                        });

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            AvatarUrl = "images/users/avatars/luis.jpg",
                            CoverUrl = "images/users/covers/luis_cover.jpg",
                            DateAccountCreated = new DateTime(2024, 3, 12, 15, 45, 51, 357, DateTimeKind.Utc).AddTicks(9934),
                            FirstName = "Luis",
                            LastName = "Carvalho",
                            Password = "$2a$12$Tbi.ghkVDRiM7b361i36meIZG12STIf6zBh.3ZKMyrKQZC8Rp4Dv.",
                            RefreshTokenExpiryTime = new DateTime(2024, 3, 12, 15, 45, 51, 357, DateTimeKind.Utc).AddTicks(9941),
                            Role = "Administrator",
                            Username = "admin"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
