using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KHHub.MasterDataService.Migrations
{
    /// <inheritdoc />
    public partial class Added_JobTagMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobTagMappings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<string>(type: "text", nullable: true),
                    JobTagId = table.Column<Guid>(type: "uuid", nullable: false),
                    JobId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTagMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobTagMappings_JobTags_JobTagId",
                        column: x => x.JobTagId,
                        principalTable: "JobTags",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobTagMappings_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobTagMappings_JobId",
                table: "JobTagMappings",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTagMappings_JobTagId",
                table: "JobTagMappings",
                column: "JobTagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobTagMappings");
        }
    }
}
