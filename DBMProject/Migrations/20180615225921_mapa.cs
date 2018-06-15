using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DBMProject.Migrations
{
    public partial class mapa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoordenadasId",
                table: "Projetos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projetos_CoordenadasId",
                table: "Projetos",
                column: "CoordenadasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projetos_Coordenadas_CoordenadasId",
                table: "Projetos",
                column: "CoordenadasId",
                principalTable: "Coordenadas",
                principalColumn: "CoordenadasId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projetos_Coordenadas_CoordenadasId",
                table: "Projetos");

            migrationBuilder.DropIndex(
                name: "IX_Projetos_CoordenadasId",
                table: "Projetos");

            migrationBuilder.DropColumn(
                name: "CoordenadasId",
                table: "Projetos");
        }
    }
}
