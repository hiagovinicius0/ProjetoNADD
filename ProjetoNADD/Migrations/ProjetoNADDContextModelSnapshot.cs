﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjetoNADD.Data;

namespace ProjetoNADD.Migrations
{
    [DbContext(typeof(ProjetoNADDContext))]
    partial class ProjetoNADDContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ProjetoNADD.Models.Area", b =>
                {
                    b.Property<int>("Id_Area")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome_Area");

                    b.HasKey("Id_Area");

                    b.ToTable("Area");
                });

            modelBuilder.Entity("ProjetoNADD.Models.Avaliacao", b =>
                {
                    b.Property<int>("Id_Avaliacao")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Clareza_Avaliacao");

                    b.Property<int?>("ComplexidadeID");

                    b.Property<bool>("Contextualidade_Avaliacao");

                    b.Property<int>("DisciplinaId");

                    b.Property<bool>("Diversificacao_Avaliacao");

                    b.Property<bool>("EquilibrioValorQuestoes_Avaliacao");

                    b.Property<string>("Nome_Avaliacao");

                    b.Property<int>("NumeroQuestoes_Avaliacao");

                    b.Property<string>("Observacoes_Avaliacao");

                    b.Property<string>("QuestoesMEeD_Avaliacao");

                    b.Property<bool>("Referencias_Avaliacao");

                    b.Property<bool>("SomatorioQuestoes_Avaliacao");

                    b.Property<bool>("ValorExplicitoProva_Avaliacao");

                    b.Property<bool>("ValorExplicitoQuestoes_Avaliacao");

                    b.Property<double>("ValorProva_Avaliacao");

                    b.HasKey("Id_Avaliacao");

                    b.HasIndex("ComplexidadeID");

                    b.HasIndex("DisciplinaId");

                    b.ToTable("Avaliacao");
                });

            modelBuilder.Entity("ProjetoNADD.Models.Complexidade", b =>
                {
                    b.Property<int>("Id_Complexidade")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome_Complexidade");

                    b.HasKey("Id_Complexidade");

                    b.ToTable("Complexidade");
                });

            modelBuilder.Entity("ProjetoNADD.Models.Curso", b =>
                {
                    b.Property<int>("Id_Curso")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AreaId");

                    b.Property<string>("Nome_Curso");

                    b.HasKey("Id_Curso");

                    b.HasIndex("AreaId");

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("ProjetoNADD.Models.Disciplina", b =>
                {
                    b.Property<int>("Id_Disciplina")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Ano_Disciplina");

                    b.Property<int>("CursoId");

                    b.Property<string>("Nome_Disciplina");

                    b.Property<int>("Periodo_Disciplina");

                    b.HasKey("Id_Disciplina");

                    b.HasIndex("CursoId");

                    b.ToTable("Disciplina");
                });

            modelBuilder.Entity("ProjetoNADD.Models.DisciplinaProfessor", b =>
                {
                    b.Property<int>("Disciplina_id");

                    b.Property<int>("Professor_id");

                    b.Property<int?>("DisciplinaId_Disciplina");

                    b.Property<int?>("ProfessorId_Professor");

                    b.HasKey("Disciplina_id", "Professor_id");

                    b.HasIndex("DisciplinaId_Disciplina");

                    b.HasIndex("ProfessorId_Professor");

                    b.ToTable("DisciplinaProfessor");
                });

            modelBuilder.Entity("ProjetoNADD.Models.Professor", b =>
                {
                    b.Property<int>("Id_Professor")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome_Professor");

                    b.HasKey("Id_Professor");

                    b.ToTable("Professor");
                });

            modelBuilder.Entity("ProjetoNADD.Models.Questao", b =>
                {
                    b.Property<int>("Id_Questao")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Clareza_Questao");

                    b.Property<int?>("ComplexidadeID");

                    b.Property<bool>("Contextualizacao_Questao");

                    b.Property<int>("Id_Avaliacao");

                    b.Property<int>("Id_Numero");

                    b.Property<string>("Observacoes_Questao");

                    b.Property<int?>("TipoID");

                    b.Property<int?>("TipoQuestaoId_TipoQuestao");

                    b.HasKey("Id_Questao");

                    b.HasIndex("ComplexidadeID");

                    b.HasIndex("Id_Avaliacao");

                    b.HasIndex("TipoQuestaoId_TipoQuestao");

                    b.ToTable("Questao");
                });

            modelBuilder.Entity("ProjetoNADD.Models.TipoQuestao", b =>
                {
                    b.Property<int>("Id_TipoQuestao")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome_TipoQuestao");

                    b.HasKey("Id_TipoQuestao");

                    b.ToTable("TipoQuestao");
                });

            modelBuilder.Entity("ProjetoNADD.Models.Usuario", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Nome_User")
                        .IsRequired();

                    b.ToTable("Usuario");

                    b.HasDiscriminator().HasValue("Usuario");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjetoNADD.Models.Avaliacao", b =>
                {
                    b.HasOne("ProjetoNADD.Models.Complexidade", "Complexidade")
                        .WithMany("Avaliacao")
                        .HasForeignKey("ComplexidadeID");

                    b.HasOne("ProjetoNADD.Models.Disciplina", "Disciplina")
                        .WithMany("Avaliacao")
                        .HasForeignKey("DisciplinaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjetoNADD.Models.Curso", b =>
                {
                    b.HasOne("ProjetoNADD.Models.Area", "Area")
                        .WithMany("Cursos")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjetoNADD.Models.Disciplina", b =>
                {
                    b.HasOne("ProjetoNADD.Models.Curso", "Curso")
                        .WithMany("Disciplinas")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjetoNADD.Models.DisciplinaProfessor", b =>
                {
                    b.HasOne("ProjetoNADD.Models.Disciplina")
                        .WithMany("DisciplinaProfessor")
                        .HasForeignKey("DisciplinaId_Disciplina");

                    b.HasOne("ProjetoNADD.Models.Professor")
                        .WithMany("DisciplinaProfessor")
                        .HasForeignKey("ProfessorId_Professor");
                });

            modelBuilder.Entity("ProjetoNADD.Models.Questao", b =>
                {
                    b.HasOne("ProjetoNADD.Models.Complexidade", "Complexidade")
                        .WithMany("Questao")
                        .HasForeignKey("ComplexidadeID");

                    b.HasOne("ProjetoNADD.Models.Avaliacao", "Avaliacao")
                        .WithMany("Questoes")
                        .HasForeignKey("Id_Avaliacao")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjetoNADD.Models.TipoQuestao", "TipoQuestao")
                        .WithMany("Questao")
                        .HasForeignKey("TipoQuestaoId_TipoQuestao");
                });
#pragma warning restore 612, 618
        }
    }
}
