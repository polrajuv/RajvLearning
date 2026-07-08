using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RajvLearning.API.Migrations
{
    /// <inheritdoc />
    public partial class RenameTopicTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Topics",
                table: "Topics");

            migrationBuilder.RenameTable(
                name: "Topics",
                newName: "LearningTopic");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LearningTopic",
                table: "LearningTopic",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LearningTopic",
                table: "LearningTopic");

            migrationBuilder.RenameTable(
                name: "LearningTopic",
                newName: "Topics");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topics",
                table: "Topics",
                column: "Id");
        }
    }
}
