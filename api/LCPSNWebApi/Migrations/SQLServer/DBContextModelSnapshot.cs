﻿// <auto-generated />
using System;
using LCPSNWebApi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LCPSNWebApi.Migrations.SQLServer
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LCPSNWebApi.Classes.Attachment", b =>
                {
                    b.Property<int?>("AttachmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("AttachmentId"));

                    b.Property<string>("AttachmentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AttachmentUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CommentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateAttachmentDeleted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateAttachmentUpdated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateAttachmentUploaded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("bit");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<int?>("ReactionId")
                        .HasColumnType("int");

                    b.Property<int?>("ReplyId")
                        .HasColumnType("int");

                    b.Property<int?>("ShareId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AttachmentId");

                    b.HasIndex("UserId");

                    b.ToTable("Attachments", t =>
                        {
                            t.HasTrigger("Attachments_Trigger");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.ChatMessage", b =>
                {
                    b.Property<int?>("ChatMessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ChatMessageId"));

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("int");

                    b.Property<int?>("CommentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateChatMessageCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateChatMessageDeleted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateChatMessageReaded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateChatMessageUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsRead")
                        .HasColumnType("bit");

                    b.Property<int?>("ReactionId")
                        .HasColumnType("int");

                    b.Property<int?>("ReplyId")
                        .HasColumnType("int");

                    b.Property<int?>("ShareId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ChatMessageId");

                    b.HasIndex("UserId");

                    b.ToTable("ChatMessages", t =>
                        {
                            t.HasTrigger("ChatMessages_Trigger");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.Comment", b =>
                {
                    b.Property<int?>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("CommentId"));

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCommentCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateCommentDeleted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateCommentUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("bit");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<int?>("ReactionId")
                        .HasColumnType("int");

                    b.Property<int?>("ReplyId")
                        .HasColumnType("int");

                    b.Property<int?>("ShareId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments", t =>
                        {
                            t.HasTrigger("Comments_Trigger");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.Files.FileData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FileFullPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("ImageBytes")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("FilesData", t =>
                        {
                            t.HasTrigger("FilesData_Trigger");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.FriendRequest", b =>
                {
                    b.Property<int?>("FriendRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("FriendRequestId"));

                    b.Property<DateTime?>("DateFriendRequestAccepted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateFriendRequestCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateFriendRequestDeleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FriendRequestType")
                        .HasColumnType("int");

                    b.Property<bool?>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FriendRequestId");

                    b.HasIndex("UserId");

                    b.ToTable("FriendRequests", t =>
                        {
                            t.HasTrigger("FriendRequests_Trigger");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.Notification", b =>
                {
                    b.Property<int?>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("NotificationId"));

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("int");

                    b.Property<int?>("CommentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateUserNotificationCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUserNotificationDeleted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUserNotificationMarked")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUserNotificationUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsMarkRead")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsPinned")
                        .HasColumnType("bit");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<int?>("ReactionId")
                        .HasColumnType("int");

                    b.Property<int?>("ReplyId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("NotificationId");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications", t =>
                        {
                            t.HasTrigger("Notifications_Trigger");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.Post", b =>
                {
                    b.Property<int?>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("PostId"));

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("int");

                    b.Property<int?>("CommentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DatePostCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DatePostDeleted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DatePostUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("bit");

                    b.Property<int?>("ReactionId")
                        .HasColumnType("int");

                    b.Property<int?>("ReplyId")
                        .HasColumnType("int");

                    b.Property<int?>("ShareId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeTxtPost")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PostId");

                    b.ToTable("Posts", t =>
                        {
                            t.HasTrigger("Posts_Trigger");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.Reaction", b =>
                {
                    b.Property<int?>("ReactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ReactionId"));

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("int");

                    b.Property<int?>("CommentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateReacted")
                        .HasColumnType("datetime2");

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

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.Reply", b =>
                {
                    b.Property<int?>("ReplyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ReplyId"));

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("int");

                    b.Property<int?>("CommentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateReplyCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateReplyDeleted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateReplyUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("bit");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<int?>("ReactionId")
                        .HasColumnType("int");

                    b.Property<int?>("ShareId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReplyId");

                    b.ToTable("Replies", t =>
                        {
                            t.HasTrigger("Replies_Trigger");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.Share", b =>
                {
                    b.Property<int?>("ShareId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ShareId"));

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("int");

                    b.Property<int?>("CommentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateShared")
                        .HasColumnType("datetime2");

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

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("LCPSNWebApi.Classes.User", b =>
                {
                    b.Property<int?>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("UserId"));

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Biography")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoverUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrentToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateAccountCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateBirthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users", t =>
                        {
                            t.HasTrigger("Users_Trigger");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            AvatarUrl = "images/users/avatars/luis.jpg",
                            Biography = "Hello, I'm Luis Carvalho.",
                            CoverUrl = "images/users/covers/luis_cover.jpg",
                            DateAccountCreated = new DateTime(2024, 3, 23, 11, 54, 5, 329, DateTimeKind.Utc).AddTicks(100),
                            DateBirthday = new DateTime(1996, 6, 3, 23, 0, 0, 0, DateTimeKind.Utc),
                            Email = "luiscarvalho239@gmail.com",
                            FirstName = "Luis",
                            LastName = "Carvalho",
                            Password = "$2a$12$7avsDvacSWLIszINJAinHuxtG94Gf8iPPeo4f2LgkIYzonNnXs1hq",
                            PhoneNumber = "123456789",
                            RefreshTokenExpiryTime = new DateTime(2024, 3, 23, 11, 54, 5, 329, DateTimeKind.Utc).AddTicks(106),
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
                            DateAccountCreated = new DateTime(2024, 3, 23, 11, 54, 5, 691, DateTimeKind.Utc).AddTicks(7111),
                            DateBirthday = new DateTime(1996, 6, 3, 23, 0, 0, 0, DateTimeKind.Utc),
                            Email = "guest@localhost.loc",
                            FirstName = "Guest",
                            LastName = "Convidado",
                            Password = "$2a$12$7.75JJmR3dYbqMeIQot/4O4HHVSsffSRhsWlD1VwZeZH2jkYR3O2q",
                            PhoneNumber = "123456789",
                            RefreshTokenExpiryTime = new DateTime(2024, 3, 23, 11, 54, 5, 691, DateTimeKind.Utc).AddTicks(7123),
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
