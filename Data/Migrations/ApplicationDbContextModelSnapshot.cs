﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PWEB.Data;

#nullable disable

namespace PWEB.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PWEB.Models.Avaliacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClienteId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("CondicaoCarro")
                        .HasColumnType("float");

                    b.Property<int?>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<double>("FacilidadeEncontrar")
                        .HasColumnType("float");

                    b.Property<double>("LimpezaCarro")
                        .HasColumnType("float");

                    b.Property<double>("Prestabilidade")
                        .HasColumnType("float");

                    b.Property<double>("TempoLevantamento")
                        .HasColumnType("float");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.Property<double>("VelocidadeDevolucao")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Avaliacoes", (string)null);
                });

            modelBuilder.Entity("PWEB.Models.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Disponivel")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Eliminar")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Empresas", (string)null);
                });

            modelBuilder.Entity("PWEB.Models.Entrega", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Danos")
                        .HasColumnType("bit");

                    b.Property<string>("EmpregadoId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Kilometros")
                        .HasColumnType("int");

                    b.Property<string>("Observaçoes")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmpregadoId");

                    b.ToTable("Entregas", (string)null);
                });

            modelBuilder.Entity("PWEB.Models.Imagem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observaçoes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecolhaId")
                        .HasColumnType("int");

                    b.Property<byte[]>("imagem")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("RecolhaId");

                    b.ToTable("Imagens", (string)null);
                });

            modelBuilder.Entity("PWEB.Models.Recolha", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Danos")
                        .HasColumnType("bit");

                    b.Property<string>("EmpregadoId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Kilometros")
                        .HasColumnType("int");

                    b.Property<string>("Observaçoes")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmpregadoId");

                    b.ToTable("Recolhas", (string)null);
                });

            modelBuilder.Entity("PWEB.Models.Reserva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AvaliacaoId")
                        .HasColumnType("int");

                    b.Property<bool?>("Confirmado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataDeEntrega")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataDeLevantaneto")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Eliminar")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<int?>("EntregaId")
                        .HasColumnType("int");

                    b.Property<int?>("RecolhaId")
                        .HasColumnType("int");

                    b.Property<int>("VeiculoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AvaliacaoId");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("EntregaId");

                    b.HasIndex("RecolhaId");

                    b.HasIndex("VeiculoId");

                    b.ToTable("Reserva", (string)null);
                });

            modelBuilder.Entity("PWEB.Models.Subscricao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Activa")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataTermino")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<int>("TipoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("TipoId");

                    b.ToTable("Subscricoes", (string)null);
                });

            modelBuilder.Entity("PWEB.Models.TipoSubs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Duracao")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Preço")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("TiposSubs", (string)null);
                });

            modelBuilder.Entity("PWEB.Models.TipoVeiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TiposVeis", (string)null);
                });

            modelBuilder.Entity("PWEB.Models.Utilizador", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Apelido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CvdCartaoMultibanco")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Disponivel")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Eliminar")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Morada")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NIF")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("NumeroCartaoMultibanco")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("ValidadeCartaoMultibanco")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("PWEB.Models.Veiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Disponivel")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Eliminar")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<string>("Localização")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Lugares")
                        .HasColumnType("int");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("MudançasManuais")
                        .HasColumnType("bit");

                    b.Property<double>("PreçoPorHora")
                        .HasColumnType("float");

                    b.Property<int>("TipoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("TipoId");

                    b.ToTable("Veiculos", (string)null);
                });

            modelBuilder.Entity("ReservaUtilizador", b =>
                {
                    b.Property<string>("EmpregadoClienteId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ReservasId")
                        .HasColumnType("int");

                    b.HasKey("EmpregadoClienteId", "ReservasId");

                    b.HasIndex("ReservasId");

                    b.ToTable("ReservaUtilizador", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PWEB.Models.Utilizador", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PWEB.Models.Utilizador", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PWEB.Models.Utilizador", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("PWEB.Models.Utilizador", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PWEB.Models.Avaliacao", b =>
                {
                    b.HasOne("PWEB.Models.Utilizador", "Cliente")
                        .WithMany("Avaliacoes")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PWEB.Models.Empresa", null)
                        .WithMany("Avaliacoes")
                        .HasForeignKey("EmpresaId");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("PWEB.Models.Entrega", b =>
                {
                    b.HasOne("PWEB.Models.Utilizador", "Empregado")
                        .WithMany("Entregas")
                        .HasForeignKey("EmpregadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empregado");
                });

            modelBuilder.Entity("PWEB.Models.Imagem", b =>
                {
                    b.HasOne("PWEB.Models.Recolha", "Recolha")
                        .WithMany("Images")
                        .HasForeignKey("RecolhaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recolha");
                });

            modelBuilder.Entity("PWEB.Models.Recolha", b =>
                {
                    b.HasOne("PWEB.Models.Utilizador", "Empregado")
                        .WithMany("Recolhas")
                        .HasForeignKey("EmpregadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empregado");
                });

            modelBuilder.Entity("PWEB.Models.Reserva", b =>
                {
                    b.HasOne("PWEB.Models.Avaliacao", "Avaliacao")
                        .WithMany()
                        .HasForeignKey("AvaliacaoId");

                    b.HasOne("PWEB.Models.Empresa", "empresa")
                        .WithMany("Reservas")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PWEB.Models.Entrega", "Entrega")
                        .WithMany()
                        .HasForeignKey("EntregaId");

                    b.HasOne("PWEB.Models.Recolha", "Recolha")
                        .WithMany()
                        .HasForeignKey("RecolhaId");

                    b.HasOne("PWEB.Models.Veiculo", "Veiculo")
                        .WithMany("Reservas")
                        .HasForeignKey("VeiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Avaliacao");

                    b.Navigation("Entrega");

                    b.Navigation("Recolha");

                    b.Navigation("Veiculo");

                    b.Navigation("empresa");
                });

            modelBuilder.Entity("PWEB.Models.Subscricao", b =>
                {
                    b.HasOne("PWEB.Models.Empresa", "Empresa")
                        .WithMany("Subscricoes")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PWEB.Models.TipoSubs", "Tipo")
                        .WithMany("Subscricoes")
                        .HasForeignKey("TipoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");

                    b.Navigation("Tipo");
                });

            modelBuilder.Entity("PWEB.Models.Utilizador", b =>
                {
                    b.HasOne("PWEB.Models.Empresa", "empresa")
                        .WithMany("Empregados")
                        .HasForeignKey("EmpresaId");

                    b.Navigation("empresa");
                });

            modelBuilder.Entity("PWEB.Models.Veiculo", b =>
                {
                    b.HasOne("PWEB.Models.Empresa", "empresa")
                        .WithMany("Veiculos")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PWEB.Models.TipoVeiculo", "Tipo")
                        .WithMany("Veiculos")
                        .HasForeignKey("TipoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tipo");

                    b.Navigation("empresa");
                });

            modelBuilder.Entity("ReservaUtilizador", b =>
                {
                    b.HasOne("PWEB.Models.Utilizador", null)
                        .WithMany()
                        .HasForeignKey("EmpregadoClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PWEB.Models.Reserva", null)
                        .WithMany()
                        .HasForeignKey("ReservasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PWEB.Models.Empresa", b =>
                {
                    b.Navigation("Avaliacoes");

                    b.Navigation("Empregados");

                    b.Navigation("Reservas");

                    b.Navigation("Subscricoes");

                    b.Navigation("Veiculos");
                });

            modelBuilder.Entity("PWEB.Models.Recolha", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("PWEB.Models.TipoSubs", b =>
                {
                    b.Navigation("Subscricoes");
                });

            modelBuilder.Entity("PWEB.Models.TipoVeiculo", b =>
                {
                    b.Navigation("Veiculos");
                });

            modelBuilder.Entity("PWEB.Models.Utilizador", b =>
                {
                    b.Navigation("Avaliacoes");

                    b.Navigation("Entregas");

                    b.Navigation("Recolhas");
                });

            modelBuilder.Entity("PWEB.Models.Veiculo", b =>
                {
                    b.Navigation("Reservas");
                });
#pragma warning restore 612, 618
        }
    }
}
