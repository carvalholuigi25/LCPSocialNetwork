﻿// <auto-generated />
using System;
using LCPSNWebApi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LCPSNWebApi.Migrations.PostgreSQL
{
    [DbContext(typeof(DBContextPostgreSQL))]
    partial class DBContextPostgreSQLModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LCPSNWebApi.Classes.Attachment", b =>
                {
                    b.Property<int?>("AttachmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("AttachmentId"));

                    b.Property<string>("AttachmentType")
                        .HasColumnType("text");

                    b.Property<string>("AttachmentUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DateAttachmentUploaded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("boolean");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

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
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("CommentId"));

                    b.Property<DateTime?>("DatePostCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("text");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("boolean");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

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
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FileFullPath")
                        .HasColumnType("text");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("GId")
                        .HasColumnType("uuid");

                    b.Property<byte[]>("ImageBytes")
                        .HasColumnType("bytea");

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
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("FriendId"));

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("text");

                    b.Property<string>("Biography")
                        .HasColumnType("text");

                    b.Property<string>("CoverUrl")
                        .HasColumnType("text");

                    b.Property<DateTime?>("DateAccountCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

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
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("PostId"));

                    b.Property<DateTime?>("DatePostCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("text");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("boolean");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TypeTxtPost")
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

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
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("UserId"));

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("text");

                    b.Property<string>("Biography")
                        .HasColumnType("text");

                    b.Property<string>("CoverUrl")
                        .HasColumnType("text");

                    b.Property<string>("CurrentToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("DateAccountCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

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
                            DateAccountCreated = new DateTime(2024, 3, 13, 15, 2, 24, 696, DateTimeKind.Utc).AddTicks(1181),
                            FirstName = "Luis",
                            LastName = "Carvalho",
                            Password = "$2a$12$oibQyA8zKIVW.MfGeuS4/uQe8/IiKOJAFeqwzVNjKUee.6.tGk75G",
                            RefreshTokenExpiryTime = new DateTime(2024, 3, 13, 15, 2, 24, 696, DateTimeKind.Utc).AddTicks(1188),
                            Role = "Administrator",
                            Username = "admin"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
