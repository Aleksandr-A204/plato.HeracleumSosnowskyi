using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeracleumSosnowskyiService.Data.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class InitialFileEarthsensing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EarthRemoteSensingDates",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "bytea", nullable: false),
                    LandsatProductId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EarthRemoteSensingDates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileInfo",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "bytea", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    MimeType = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<long>(type: "bigint", nullable: false),
                    FileStreamId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileMetadates",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "bytea", nullable: false),
                    ErsDataId = table.Column<byte[]>(type: "bytea", nullable: false),
                    FileInfoId = table.Column<byte[]>(type: "bytea", nullable: false),
                    FileStreamId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileMetadates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileMetadates_EarthRemoteSensingDates_ErsDataId",
                        column: x => x.ErsDataId,
                        principalTable: "EarthRemoteSensingDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileMetadates_FileInfo_FileInfoId",
                        column: x => x.FileInfoId,
                        principalTable: "FileInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileMetadates_ErsDataId",
                table: "FileMetadates",
                column: "ErsDataId");

            migrationBuilder.CreateIndex(
                name: "IX_FileMetadates_FileInfoId",
                table: "FileMetadates",
                column: "FileInfoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileMetadates");

            migrationBuilder.DropTable(
                name: "EarthRemoteSensingDates");

            migrationBuilder.DropTable(
                name: "FileInfo");
        }
    }
}
