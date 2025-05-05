﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitFriend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityUserIdToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "FitnessUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "FitnessUsers");
        }
    }
}
