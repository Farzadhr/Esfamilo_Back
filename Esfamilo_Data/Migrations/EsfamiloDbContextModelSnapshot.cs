﻿// <auto-generated />
using Esfamilo_Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Esfamilo_Data.Migrations
{
    [DbContext(typeof(EsfamiloDbContext))]
    partial class EsfamiloDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Esfamilo_Domain.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CategroyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Esfamilo_Domain.Models.CategoryInLobby", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("LobbyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("LobbyId");

                    b.ToTable("categoryInLobbies");
                });

            modelBuilder.Entity("Esfamilo_Domain.Models.ChatMessageInLobby", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("LobbyId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SenderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LobbyId");

                    b.ToTable("chatMessageInLobbies");
                });

            modelBuilder.Entity("Esfamilo_Domain.Models.Friend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FriendId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("friends");
                });

            modelBuilder.Entity("Esfamilo_Domain.Models.Lobby", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CurrentRound")
                        .HasColumnType("int");

                    b.Property<bool>("InGameStatus")
                        .HasColumnType("bit");

                    b.Property<bool>("IsLimitLobby")
                        .HasColumnType("bit");

                    b.Property<int>("LimitUserCount")
                        .HasColumnType("int");

                    b.Property<string>("LobbyGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LobbyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoundCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Lobbies");
                });

            modelBuilder.Entity("Esfamilo_Domain.Models.UserInLobby", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsUserOwner")
                        .HasColumnType("bit");

                    b.Property<int>("LobbyId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserScore")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LobbyId");

                    b.ToTable("userInLobbies");
                });

            modelBuilder.Entity("Esfamilo_Domain.Models.WordForCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<int>("LobbyId")
                        .HasColumnType("int");

                    b.Property<int>("LobbyRound")
                        .HasColumnType("int");

                    b.Property<string>("TargetLetter")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WordScore")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LobbyId");

                    b.ToTable("WordForCategories");
                });

            modelBuilder.Entity("Esfamilo_Domain.Models.CategoryInLobby", b =>
                {
                    b.HasOne("Esfamilo_Domain.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Esfamilo_Domain.Models.Lobby", "Lobby")
                        .WithMany("CategoryInLobbies")
                        .HasForeignKey("LobbyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Lobby");
                });

            modelBuilder.Entity("Esfamilo_Domain.Models.ChatMessageInLobby", b =>
                {
                    b.HasOne("Esfamilo_Domain.Models.Lobby", "Lobby")
                        .WithMany("ChatMessageInLobbies")
                        .HasForeignKey("LobbyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lobby");
                });

            modelBuilder.Entity("Esfamilo_Domain.Models.UserInLobby", b =>
                {
                    b.HasOne("Esfamilo_Domain.Models.Lobby", "lobby")
                        .WithMany("UserInLobbies")
                        .HasForeignKey("LobbyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("lobby");
                });

            modelBuilder.Entity("Esfamilo_Domain.Models.WordForCategory", b =>
                {
                    b.HasOne("Esfamilo_Domain.Models.Lobby", "lobby")
                        .WithMany("WordForCategories")
                        .HasForeignKey("LobbyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("lobby");
                });

            modelBuilder.Entity("Esfamilo_Domain.Models.Lobby", b =>
                {
                    b.Navigation("CategoryInLobbies");

                    b.Navigation("ChatMessageInLobbies");

                    b.Navigation("UserInLobbies");

                    b.Navigation("WordForCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
