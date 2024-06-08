﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MultiLanguageExamManagementSystem.Data;

#nullable disable

namespace MultiLanguageExamManagementSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240608230357_newTablesssssssss")]
    partial class newTablesssssssss
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Exam", b =>
                {
                    b.Property<int>("ExamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExamId"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExamId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.ExamQuestion", b =>
                {
                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("ExamId", "QuestionId");

                    b.HasIndex("QuestionId");

                    b.ToTable("ExamQuestions");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSelected")
                        .HasColumnType("bit");

                    b.Property<string>("LanguageCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId", "LanguageCode")
                        .IsUnique();

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.LocalizationResource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BeautifiedNamespace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<string>("Namespace")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId", "Namespace", "Key")
                        .IsUnique();

                    b.ToTable("LocalizationResources");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuestionId"));

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuestionId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.TakenExam", b =>
                {
                    b.Property<int>("TakenExamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TakenExamId"));

                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("TakenDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TakenExamId");

                    b.HasIndex("ExamId");

                    b.HasIndex("UserId");

                    b.ToTable("TakenExams");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<bool>("IsProfessor")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Exam", b =>
                {
                    b.HasOne("User", "Creator")
                        .WithMany("CreatedExams")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.ExamQuestion", b =>
                {
                    b.HasOne("Exam", "Exam")
                        .WithMany("ExamQuestions")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MultiLanguageExamManagementSystem.Models.Entities.Question", "Question")
                        .WithMany("ExamQuestions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.Language", b =>
                {
                    b.HasOne("MultiLanguageExamManagementSystem.Models.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.LocalizationResource", b =>
                {
                    b.HasOne("MultiLanguageExamManagementSystem.Models.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.TakenExam", b =>
                {
                    b.HasOne("Exam", "Exam")
                        .WithMany("TakenExams")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("User", "User")
                        .WithMany("TakenExams")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Exam", b =>
                {
                    b.Navigation("ExamQuestions");

                    b.Navigation("TakenExams");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.Question", b =>
                {
                    b.Navigation("ExamQuestions");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Navigation("CreatedExams");

                    b.Navigation("TakenExams");
                });
#pragma warning restore 612, 618
        }
    }
}
