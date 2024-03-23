﻿// <auto-generated />
using System;
using LCPSNWebApi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LCPSNWebApi.Migrations.MySQL
{
    [DbContext(typeof(DBContextMySQL))]
    [Migration("20240323115330_InitialCreatePostgreSQL")]
    partial class InitialCreatePostgreSQL
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LCPSNWebApi.Classes.Attachment", b =>
                {
                    b.Property<int?>("AttachmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AttachmentType")
                        .HasColumnType("longtext");

                    b.Property<string>("AttachmentUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("CommentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateAttachmentDeleted")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateAttachmentUpdated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateAttachmentUploaded")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<int?>("ReactionId")
                        .HasColumnType("int");

                    b.Property<int?>("ReplyId")
                        .HasColumnType("int");

                    b.Property<int?>("ShareId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AttachmentId");

                    b.HasIndex("UserId");

                    b.ToTable("Attachments", t =>
                        {
                            t.HasTrigger("Attachments_Trigger");
                        });
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.ChatMessage", b =>
                {
                    b.Property<int?>("ChatMessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("int");

                    b.Property<int?>("CommentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateChatMessageCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateChatMessageDeleted")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateChatMessageReaded")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateChatMessageUpdated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool?>("IsRead")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("ReactionId")
                        .HasColumnType("int");

                    b.Property<int?>("ReplyId")
                        .HasColumnType("int");

                    b.Property<int?>("ShareId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ChatMessageId");

                    b.HasIndex("UserId");

                    b.ToTable("ChatMessages", t =>
                        {
                            t.HasTrigger("ChatMessages_Trigger");
                        });
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.Comment", b =>
                {
                    b.Property<int?>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCommentCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateCommentDeleted")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateCommentUpdated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("longtext");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<int?>("ReactionId")
                        .HasColumnType("int");

                    b.Property<int?>("ReplyId")
                        .HasColumnType("int");

                    b.Property<int?>("ShareId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

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
                        .HasColumnType("int");

                    b.Property<string>("FileFullPath")
                        .HasColumnType("longtext");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("GId")
                        .HasColumnType("char(36)");

                    b.Property<byte[]>("ImageBytes")
                        .HasColumnType("longblob");

                    b.HasKey("Id");

                    b.ToTable("FilesData", t =>
                        {
                            t.HasTrigger("FilesData_Trigger");
                        });
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.FriendRequest", b =>
                {
                    b.Property<int?>("FriendRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateFriendRequestAccepted")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateFriendRequestCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateFriendRequestDeleted")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("FriendRequestType")
                        .HasColumnType("int");

                    b.Property<bool?>("IsAccepted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FriendRequestId");

                    b.HasIndex("UserId");

                    b.ToTable("FriendRequests", t =>
                        {
                            t.HasTrigger("FriendRequests_Trigger");
                        });
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.Notification", b =>
                {
                    b.Property<int?>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("int");

                    b.Property<int?>("CommentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateUserNotificationCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateUserNotificationDeleted")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateUserNotificationMarked")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateUserNotificationUpdated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool?>("IsMarkRead")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool?>("IsPinned")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<int?>("ReactionId")
                        .HasColumnType("int");

                    b.Property<int?>("ReplyId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("NotificationId");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications", t =>
                        {
                            t.HasTrigger("Notifications_Trigger");
                        });
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.Post", b =>
                {
                    b.Property<int?>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("int");

                    b.Property<int?>("CommentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DatePostCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DatePostDeleted")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DatePostUpdated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("longtext");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("ReactionId")
                        .HasColumnType("int");

                    b.Property<int?>("ReplyId")
                        .HasColumnType("int");

                    b.Property<int?>("ShareId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TypeTxtPost")
                        .HasColumnType("longtext");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PostId");

                    b.ToTable("Posts", t =>
                        {
                            t.HasTrigger("Posts_Trigger");
                        });
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.Reaction", b =>
                {
                    b.Property<int?>("ReactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("int");

                    b.Property<int?>("CommentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateReacted")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<int?>("ReactionCounter")
                        .HasColumnType("int");

                    b.Property<int?>("ReactionType")
                        .HasColumnType("int");

                    b.Property<int?>("ReplyId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReactionId");

                    b.ToTable("Reactions", t =>
                        {
                            t.HasTrigger("Reactions_Trigger");
                        });
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.Reply", b =>
                {
                    b.Property<int?>("ReplyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("int");

                    b.Property<int?>("CommentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateReplyCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateReplyDeleted")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateReplyUpdated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("longtext");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<int?>("ReactionId")
                        .HasColumnType("int");

                    b.Property<int?>("ShareId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReplyId");

                    b.ToTable("Replies", t =>
                        {
                            t.HasTrigger("Replies_Trigger");
                        });
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.Share", b =>
                {
                    b.Property<int?>("ShareId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("int");

                    b.Property<int?>("CommentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateShared")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<int?>("ReplyId")
                        .HasColumnType("int");

                    b.Property<int?>("ShareCounter")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ShareId");

                    b.ToTable("Shares", t =>
                        {
                            t.HasTrigger("Shares_Trigger");
                        });
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.User", b =>
                {
                    b.Property<int?>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("Biography")
                        .HasColumnType("longtext");

                    b.Property<string>("CoverUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("CurrentToken")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("DateAccountCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateBirthday")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Role")
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

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
                            DateAccountCreated = new DateTime(2024, 3, 23, 11, 53, 29, 326, DateTimeKind.Utc).AddTicks(4185),
                            DateBirthday = new DateTime(1996, 6, 3, 23, 0, 0, 0, DateTimeKind.Utc),
                            Email = "luiscarvalho239@gmail.com",
                            FirstName = "Luis",
                            LastName = "Carvalho",
                            Password = "$2a$12$bY3HMpK9T0HzkK8BXCVM9.79usGulEmFBbVzXylHZWZ96rugC1eWW",
                            PhoneNumber = "123456789",
                            RefreshTokenExpiryTime = new DateTime(2024, 3, 23, 11, 53, 29, 326, DateTimeKind.Utc).AddTicks(4198),
                            Role = "Administrator",
                            Status = "public",
                            Username = "admin"
                        },
                        new
                        {
                            UserId = 2,
                            AvatarUrl = "images/users/avatars/guest.png",
                            Biography = "Hello, I'm Guest.",
                            CoverUrl = "images/users/covers/guest_cover.jpeg",
                            DateAccountCreated = new DateTime(2024, 3, 23, 11, 53, 29, 653, DateTimeKind.Utc).AddTicks(1923),
                            DateBirthday = new DateTime(1996, 6, 3, 23, 0, 0, 0, DateTimeKind.Utc),
                            Email = "guest@localhost.loc",
                            FirstName = "Guest",
                            LastName = "Convidado",
                            Password = "$2a$12$W65AaUM1G.ZAiL3/yI2G2.JamrHnNTXoX3RpEC9tL0L4V7PHu42ee",
                            PhoneNumber = "123456789",
                            RefreshTokenExpiryTime = new DateTime(2024, 3, 23, 11, 53, 29, 653, DateTimeKind.Utc).AddTicks(1934),
                            Role = "Guest",
                            Status = "public",
                            Username = "guest"
                        });
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.Attachment", b =>
                {
                    b.HasOne("LCPSNWebApi.Classes.User", null)
                        .WithMany("Attachments")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.ChatMessage", b =>
                {
                    b.HasOne("LCPSNWebApi.Classes.User", null)
                        .WithMany("ChatMessages")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.Comment", b =>
                {
                    b.HasOne("LCPSNWebApi.Classes.User", null)
                        .WithMany("Comments")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.FriendRequest", b =>
                {
                    b.HasOne("LCPSNWebApi.Classes.User", null)
                        .WithMany("FriendRequests")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.Notification", b =>
                {
                    b.HasOne("LCPSNWebApi.Classes.User", null)
                        .WithMany("Notifications")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.User", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("ChatMessages");

                    b.Navigation("Comments");

                    b.Navigation("FriendRequests");

                    b.Navigation("Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}