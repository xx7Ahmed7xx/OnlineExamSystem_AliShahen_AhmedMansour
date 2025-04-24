﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineExamSystem_AliShahen_AhmedMansour.Migrations
{
    /// <inheritdoc />
    public partial class addedExamColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxAttempts",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxAttempts",
                table: "Exams");
        }
    }
}
