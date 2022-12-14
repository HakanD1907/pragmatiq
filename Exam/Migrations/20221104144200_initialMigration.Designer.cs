// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Exam.Migrations
{
    [DbContext(typeof(HotelContext))]
    [Migration("20221104144200_initialMigration")]
    partial class initialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("BookingDate")
                        .HasColumnType("date");

                    b.Property<int?>("EmployeeID")
                        .HasColumnType("integer");

                    b.Property<int>("GuestID")
                        .HasColumnType("integer");

                    b.Property<int>("Nights")
                        .HasColumnType("integer");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("GuestID");

                    b.HasIndex("RoomNumber");

                    b.ToTable("bookings");
                });

            modelBuilder.Entity("Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int?>("BossID")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("BossID");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("Guest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("guests");
                });

            modelBuilder.Entity("Room", b =>
                {
                    b.Property<int>("Number")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Number"));

                    b.Property<int>("Floor")
                        .HasColumnType("integer");

                    b.Property<int>("RoomTypeId")
                        .HasColumnType("integer");

                    b.HasKey("Number");

                    b.HasIndex("RoomTypeId");

                    b.ToTable("rooms");
                });

            modelBuilder.Entity("RoomType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Occupancy")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("roomType");
                });

            modelBuilder.Entity("Booking", b =>
                {
                    b.HasOne("Employee", "Employee")
                        .WithMany("Bookings")
                        .HasForeignKey("EmployeeID");

                    b.HasOne("Guest", "guest")
                        .WithMany("Bookings")
                        .HasForeignKey("GuestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Room", "room")
                        .WithMany("Bookings")
                        .HasForeignKey("RoomNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("guest");

                    b.Navigation("room");
                });

            modelBuilder.Entity("Employee", b =>
                {
                    b.HasOne("Employee", "Boss")
                        .WithMany("Subordinate")
                        .HasForeignKey("BossID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Boss");
                });

            modelBuilder.Entity("Room", b =>
                {
                    b.HasOne("RoomType", "roomType")
                        .WithMany("Rooms")
                        .HasForeignKey("RoomTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("roomType");
                });

            modelBuilder.Entity("Employee", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Subordinate");
                });

            modelBuilder.Entity("Guest", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("Room", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("RoomType", b =>
                {
                    b.Navigation("Rooms");
                });
#pragma warning restore 612, 618
        }
    }
}
