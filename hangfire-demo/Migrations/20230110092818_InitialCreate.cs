﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hangfiredemo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CryptoCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangeRate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyPrices", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyPrices");
        }
    }
}
