#region Using derectives

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace SQL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                                         "Waiters",
                                         table => new
                                                  {
                                                          Id = table.Column<int>("INTEGER", nullable:false)
                                                                  .Annotation("Sqlite:Autoincrement", true),
                                                          Name = table.Column<string>("TEXT", nullable:true),
                                                          Active = table.Column<bool>("INTEGER", nullable:false)
                                                  },
                                         constraints:table => { table.PrimaryKey("PK_Waiters", x => x.Id); });

            migrationBuilder.CreateTable(
                                         "PreOrders",
                                         table => new
                                                  {
                                                          Id = table.Column<int>("INTEGER", nullable:false)
                                                                  .Annotation("Sqlite:Autoincrement", true),
                                                          VisitorName = table.Column<string>("TEXT", nullable:true),
                                                          OrderName = table.Column<string>("TEXT", nullable:true),
                                                          WaiterId = table.Column<int>("INTEGER", nullable:false),
                                                          Cost = table.Column<int>("INTEGER", nullable:false),
                                                          Date = table.Column<DateTime>("TEXT", nullable:false),
                                                          TableNum = table.Column<int>("INTEGER", nullable:false)
                                                  },
                                         constraints:table =>
                                                     {
                                                         table.PrimaryKey("PK_PreOrders", x => x.Id);

                                                         table.ForeignKey(
                                                                          "FK_PreOrders_Waiters_WaiterId",
                                                                          x => x.WaiterId,
                                                                          "Waiters",
                                                                          "Id",
                                                                          onDelete:ReferentialAction.Cascade);
                                                     });

            migrationBuilder.CreateTable(
                                         "Visitors",
                                         table => new
                                                  {
                                                          Id = table.Column<int>("INTEGER", nullable:false)
                                                                  .Annotation("Sqlite:Autoincrement", true),
                                                          VisitorName = table.Column<string>("TEXT", nullable:true),
                                                          TableNum = table.Column<int>("INTEGER", nullable:false),
                                                          Cost = table.Column<int>("INTEGER", nullable:false),
                                                          Time = table.Column<int>("INTEGER", nullable:false),
                                                          WaiterId = table.Column<int>("INTEGER", nullable:false)
                                                  },
                                         constraints:table =>
                                                     {
                                                         table.PrimaryKey("PK_Visitors", x => x.Id);

                                                         table.ForeignKey(
                                                                          "FK_Visitors_Waiters_WaiterId",
                                                                          x => x.WaiterId,
                                                                          "Waiters",
                                                                          "Id",
                                                                          onDelete:ReferentialAction.Cascade);
                                                     });

            migrationBuilder.CreateIndex(
                                         "IX_PreOrders_WaiterId",
                                         "PreOrders",
                                         "WaiterId");

            migrationBuilder.CreateIndex(
                                         "IX_Visitors_WaiterId",
                                         "Visitors",
                                         "WaiterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                                       "PreOrders");

            migrationBuilder.DropTable(
                                       "Visitors");

            migrationBuilder.DropTable(
                                       "Waiters");
        }
    }
}