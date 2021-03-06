#region Using derectives

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SQL.Models;

#endregion

namespace SQL.Migrations
{
    [DbContext(typeof(HotelContext))]
    internal class HotelContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            #pragma warning disable 612, 618
            modelBuilder
                    .HasAnnotation("ProductVersion", "6.0.0-preview.3.21201.2");

            modelBuilder.Entity("SQL.Models.PreOrders", b =>
                                                       {
                                                           b.Property<int>("Id")
                                                                   .ValueGeneratedOnAdd()
                                                                   .HasColumnType("INTEGER");

                                                           b.Property<int>("Cost")
                                                                   .HasColumnType("INTEGER");

                                                           b.Property<DateTime>("Date")
                                                                   .HasColumnType("TEXT");

                                                           b.Property<string>("OrderName")
                                                                   .HasColumnType("TEXT");

                                                           b.Property<int>("TableNum")
                                                                   .HasColumnType("INTEGER");

                                                           b.Property<string>("VisitorName")
                                                                   .HasColumnType("TEXT");

                                                           b.Property<int>("WaiterId")
                                                                   .HasColumnType("INTEGER");

                                                           b.HasKey("Id");
                                                           b.HasIndex("WaiterId");
                                                           b.ToTable("PreOrders");
                                                       });

            modelBuilder.Entity("SQL.Models.Visitor", b =>
                                                      {
                                                          b.Property<int>("Id")
                                                                  .ValueGeneratedOnAdd()
                                                                  .HasColumnType("INTEGER");

                                                          b.Property<int>("Cost")
                                                                  .HasColumnType("INTEGER");

                                                          b.Property<int>("TableNum")
                                                                  .HasColumnType("INTEGER");

                                                          b.Property<int>("Time")
                                                                  .HasColumnType("INTEGER");

                                                          b.Property<string>("VisitorName")
                                                                  .HasColumnType("TEXT");

                                                          b.Property<int>("WaiterId")
                                                                  .HasColumnType("INTEGER");

                                                          b.HasKey("Id");
                                                          b.HasIndex("WaiterId");
                                                          b.ToTable("Visitors");
                                                      });

            modelBuilder.Entity("SQL.Models.Waiter", b =>
                                                     {
                                                         b.Property<int>("Id")
                                                                 .ValueGeneratedOnAdd()
                                                                 .HasColumnType("INTEGER");

                                                         b.Property<bool>("Active")
                                                                 .HasColumnType("INTEGER");

                                                         b.Property<string>("Name")
                                                                 .HasColumnType("TEXT");

                                                         b.HasKey("Id");
                                                         b.ToTable("Waiters");
                                                     });

            modelBuilder.Entity("SQL.Models.PreOrders", b =>
                                                       {
                                                           b.HasOne("SQL.Models.Waiter", "Waiter")
                                                                   .WithMany()
                                                                   .HasForeignKey("WaiterId")
                                                                   .OnDelete(DeleteBehavior.Cascade)
                                                                   .IsRequired();

                                                           b.Navigation("Waiter");
                                                       });

            modelBuilder.Entity("SQL.Models.Visitor", b =>
                                                      {
                                                          b.HasOne("SQL.Models.Waiter", "Waiter")
                                                                  .WithMany()
                                                                  .HasForeignKey("WaiterId")
                                                                  .OnDelete(DeleteBehavior.Cascade)
                                                                  .IsRequired();

                                                          b.Navigation("Waiter");
                                                      });
            #pragma warning restore 612, 618
        }
    }
}