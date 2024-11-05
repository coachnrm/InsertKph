using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsertKph.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IpdProgressNote",
                columns: table => new
                {
                    progress_note_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    an = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    progress_note_date = table.Column<DateOnly>(type: "date", nullable: false),
                    progress_note_time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    progress_note_owner_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    progress_note_doctor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    progress_note_enter_datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    create_user = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    create_datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    update_user = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    update_datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pre_order_progress_note_id = table.Column<int>(type: "int", nullable: true),
                    pre_order_progress_note_date = table.Column<DateOnly>(type: "date", nullable: false),
                    pre_order_progress_note_time = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpdProgressNote", x => x.progress_note_id);
                });

            migrationBuilder.CreateTable(
                name: "IpdProgressNoteItems",
                columns: table => new
                {
                    progress_note_id = table.Column<int>(type: "int", nullable: false),
                    progress_note_item_id = table.Column<int>(type: "int", nullable: false),
                    an = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    progress_note_item_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    progress_note_item_detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    progress_note_item_detail2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    create_user = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    create_datetime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    update_user = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    update_datetime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpdProgressNoteItems", x => x.progress_note_id);
                    table.ForeignKey(
                        name: "FK_IpdProgressNoteItems_IpdProgressNote_progress_note_id",
                        column: x => x.progress_note_id,
                        principalTable: "IpdProgressNote",
                        principalColumn: "progress_note_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IpdProgressNote_progress_note_id",
                table: "IpdProgressNote",
                column: "progress_note_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IpdProgressNoteItems");

            migrationBuilder.DropTable(
                name: "IpdProgressNote");
        }
    }
}
