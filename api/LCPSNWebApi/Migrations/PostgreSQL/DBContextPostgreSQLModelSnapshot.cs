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

                    b.Property<int?>("CommentId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DateAttachmentDeleted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateAttachmentUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateAttachmentUploaded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("boolean");

                    b.Property<int?>("PostId")
                        .HasColumnType("integer");

                    b.Property<int?>("ReactionId")
                        .HasColumnType("integer");

                    b.Property<int?>("ReplyId")
                        .HasColumnType("integer");

                    b.Property<int?>("ShareId")
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

            modelBuilder.Entity("LCPSNWebApi.Classes.ChatMessage", b =>
                {
                    b.Property<int?>("ChatMessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("ChatMessageId"));

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("integer");

                    b.Property<int?>("CommentId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DateChatMessageCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateChatMessageDeleted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateChatMessageReaded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateChatMessageUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool?>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<int?>("ReactionId")
                        .HasColumnType("integer");

                    b.Property<int?>("ReplyId")
                        .HasColumnType("integer");

                    b.Property<int?>("ShareId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<int?>("TargetUserId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

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
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("CommentId"));

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DateCommentCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateCommentDeleted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateCommentUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("text");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("boolean");

                    b.Property<int?>("PostId")
                        .HasColumnType("integer");

                    b.Property<int?>("ReactionId")
                        .HasColumnType("integer");

                    b.Property<int?>("ReplyId")
                        .HasColumnType("integer");

                    b.Property<int?>("ShareId")
                        .HasColumnType("integer");

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

            modelBuilder.Entity("LCPSNWebApi.Classes.FriendRequest", b =>
                {
                    b.Property<int?>("FriendRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("FriendRequestId"));

                    b.Property<DateTime?>("DateFriendRequestAccepted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateFriendRequestCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateFriendRequestDeleted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("FriendRequestType")
                        .HasColumnType("integer");

                    b.Property<bool?>("IsAccepted")
                        .HasColumnType("boolean");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

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
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("NotificationId"));

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("integer");

                    b.Property<int?>("CommentId")
                        .HasColumnType("integer");

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

                    b.Property<int?>("PostId")
                        .HasColumnType("integer");

                    b.Property<int?>("ReactionId")
                        .HasColumnType("integer");

                    b.Property<int?>("ReplyId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

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
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("PostId"));

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("integer");

                    b.Property<int?>("CommentId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DatePostCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DatePostDeleted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DatePostUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("text");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("boolean");

                    b.Property<int?>("ReactionId")
                        .HasColumnType("integer");

                    b.Property<int?>("ReplyId")
                        .HasColumnType("integer");

                    b.Property<int?>("ShareId")
                        .HasColumnType("integer");

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

            modelBuilder.Entity("LCPSNWebApi.Classes.Reaction", b =>
                {
                    b.Property<int?>("ReactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("ReactionId"));

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("integer");

                    b.Property<int?>("CommentId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DateReacted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("PostId")
                        .HasColumnType("integer");

                    b.Property<int?>("ReactionCounter")
                        .HasColumnType("integer");

                    b.Property<int?>("ReactionType")
                        .HasColumnType("integer");

                    b.Property<int?>("ReplyId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

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
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("ReplyId"));

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("integer");

                    b.Property<int?>("CommentId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DateReplyCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateReplyDeleted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateReplyUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("text");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("boolean");

                    b.Property<int?>("PostId")
                        .HasColumnType("integer");

                    b.Property<int?>("ReactionId")
                        .HasColumnType("integer");

                    b.Property<int?>("ShareId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

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
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("ShareId"));

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("integer");

                    b.Property<int?>("CommentId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DateShared")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("PostId")
                        .HasColumnType("integer");

                    b.Property<int?>("ReplyId")
                        .HasColumnType("integer");

                    b.Property<int?>("ShareCounter")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

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

                    b.Property<DateTime>("DateBirthday")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("PhoneNumber")
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
                            DateAccountCreated = new DateTime(2024, 4, 8, 8, 37, 59, 254, DateTimeKind.Utc).AddTicks(7650),
                            DateBirthday = new DateTime(1996, 6, 3, 23, 0, 0, 0, DateTimeKind.Utc),
                            Email = "luiscarvalho239@gmail.com",
                            FirstName = "Luis",
                            LastName = "Carvalho",
                            Password = "$2a$12$Es8r2Ei.fN5ZstQxI9w2B.PK0cUpTKBvv6hl9MTlvl3DG.YEPhp0e",
                            PhoneNumber = "123456789",
                            RefreshTokenExpiryTime = new DateTime(2024, 4, 8, 8, 37, 59, 254, DateTimeKind.Utc).AddTicks(7657),
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
                            DateAccountCreated = new DateTime(2024, 4, 8, 8, 37, 59, 568, DateTimeKind.Utc).AddTicks(1077),
                            DateBirthday = new DateTime(1996, 6, 3, 23, 0, 0, 0, DateTimeKind.Utc),
                            Email = "guest@localhost.loc",
                            FirstName = "Guest",
                            LastName = "Convidado",
                            Password = "$2a$12$FoBm1o6x6v1M/RArxsiBdOR.VirKgXcf6afZouvRHsChmmltoh0Ze",
                            PhoneNumber = "123456789",
                            RefreshTokenExpiryTime = new DateTime(2024, 4, 8, 8, 37, 59, 568, DateTimeKind.Utc).AddTicks(1087),
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
