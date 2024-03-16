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

                    b.Property<int?>("PostId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("AttachmentId");

                    b.HasIndex("UserId");

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

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("integer");

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

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("CommentId");

                    b.HasIndex("UserId");

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

            modelBuilder.Entity("LCPSNWebApi.Classes.Post", b =>
                {
                    b.Property<int?>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("PostId"));

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DatePostCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("text");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsPinned")
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
                            Biography = "Hello, I'm Luis Carvalho.",
                            CoverUrl = "images/users/covers/luis_cover.jpg",
                            DateAccountCreated = new DateTime(2024, 3, 16, 14, 22, 31, 293, DateTimeKind.Utc).AddTicks(4702),
                            Email = "luiscarvalho239@gmail.com",
                            FirstName = "Luis",
                            LastName = "Carvalho",
                            Password = "$2a$12$Q56F8JQj8iSepLh8WCgK7esb3gny0if0wJomLgc9gqmSipnSvMqKG",
                            RefreshTokenExpiryTime = new DateTime(2024, 3, 16, 14, 22, 31, 293, DateTimeKind.Utc).AddTicks(4708),
                            Role = "Administrator",
                            Status = "public",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.UserFriendRequest", b =>
                {
                    b.Property<int?>("UserFriendRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("UserFriendRequestId"));

                    b.Property<DateTime?>("DateUserFriendRequestAccepted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateUserFriendRequestCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateUserFriendRequestDeleted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool?>("IsAccepted")
                        .HasColumnType("boolean");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("UserFriendRequestId");

                    b.HasIndex("UserId");

                    b.ToTable("UserFriendRequests", t =>
                        {
                            t.HasTrigger("UserFriendRequests_Trigger");
                        });
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.UserMessage", b =>
                {
                    b.Property<int?>("UserMessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("UserMessageId"));

                    b.Property<DateTime?>("DateUserMessageCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateUserMessageDeleted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateUserMessageReaded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateUserMessageUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool?>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("UserMessageId");

                    b.HasIndex("UserId");

                    b.ToTable("UserMessages", t =>
                        {
                            t.HasTrigger("UserMessages_Trigger");
                        });
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.UserNotification", b =>
                {
                    b.Property<int?>("UserNotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("UserNotificationId"));

                    b.Property<DateTime?>("DateUserNotificationCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateUserNotificationDeleted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateUserNotificationMarked")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateUserNotificationUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool?>("IsMarkRead")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsPinned")
                        .HasColumnType("boolean");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("UserNotificationId");

                    b.HasIndex("UserId");

                    b.ToTable("UserNotifications", t =>
                        {
                            t.HasTrigger("UserNotifications_Trigger");
                        });
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.Attachment", b =>
                {
                    b.HasOne("LCPSNWebApi.Classes.User", null)
                        .WithMany("Attachments")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.Comment", b =>
                {
                    b.HasOne("LCPSNWebApi.Classes.User", null)
                        .WithMany("Comments")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.UserFriendRequest", b =>
                {
                    b.HasOne("LCPSNWebApi.Classes.User", null)
                        .WithMany("UserFriendRequests")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.UserMessage", b =>
                {
                    b.HasOne("LCPSNWebApi.Classes.User", null)
                        .WithMany("UserMessages")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.UserNotification", b =>
                {
                    b.HasOne("LCPSNWebApi.Classes.User", null)
                        .WithMany("UserNotifications")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.User", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("Comments");

                    b.Navigation("UserFriendRequests");

                    b.Navigation("UserMessages");

                    b.Navigation("UserNotifications");
                });
#pragma warning restore 612, 618
        }
    }
}
