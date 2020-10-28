using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeavePlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeavePlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResourceLeaves",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ResourceId = table.Column<Guid>(nullable: false),
                    LeavePlanId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceLeaves", x => x.Id);
                    table.UniqueConstraint("AK_ResourceLeaves_ResourceId", x => x.ResourceId);
                    table.ForeignKey(
                        name: "FK_ResourceLeaves_LeavePlans_LeavePlanId",
                        column: x => x.LeavePlanId,
                        principalTable: "LeavePlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeavePlanAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    LeavePlanId = table.Column<Guid>(nullable: false),
                    LeaveTypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeavePlanAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeavePlanAssignments_LeavePlans_LeavePlanId",
                        column: x => x.LeavePlanId,
                        principalTable: "LeavePlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeavePlanAssignments_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leaves",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Emergency = table.Column<bool>(nullable: false),
                    ResourceId = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    StatusReason = table.Column<string>(nullable: true),
                    LeaveTypeId = table.Column<Guid>(nullable: false),
                    ResourceLeaveId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leaves_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Leaves_ResourceLeaves_ResourceLeaveId",
                        column: x => x.ResourceLeaveId,
                        principalTable: "ResourceLeaves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResourceLeaveAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    ResourceLeaveId = table.Column<Guid>(nullable: false),
                    LeaveTypeId = table.Column<Guid>(nullable: false),
                    LeavePlanAssignmentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceLeaveAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceLeaveAssignments_LeavePlanAssignments_LeavePlanAssi~",
                        column: x => x.LeavePlanAssignmentId,
                        principalTable: "LeavePlanAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceLeaveAssignments_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceLeaveAssignments_ResourceLeaves_ResourceLeaveId",
                        column: x => x.ResourceLeaveId,
                        principalTable: "ResourceLeaves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("ea948a1d-1579-4651-8fe3-4632b1c41dcc"), "Annual" },
                    { new Guid("94911d50-f55e-4641-ad16-e61311059b7f"), "Sick" },
                    { new Guid("1e6236d5-04a9-4651-9ec9-a9f9cb9a6a75"), "Paternity" },
                    { new Guid("eea4be83-e41a-4c59-a78e-d94be7768909"), "Maternity" },
                    { new Guid("693ffd51-6030-41eb-90bd-a38c736f9b98"), "ChildCare" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeavePlanAssignments_LeavePlanId",
                table: "LeavePlanAssignments",
                column: "LeavePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_LeavePlanAssignments_LeaveTypeId",
                table: "LeavePlanAssignments",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_LeaveTypeId",
                table: "Leaves",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_ResourceLeaveId",
                table: "Leaves",
                column: "ResourceLeaveId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceLeaveAssignments_LeavePlanAssignmentId",
                table: "ResourceLeaveAssignments",
                column: "LeavePlanAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceLeaveAssignments_LeaveTypeId",
                table: "ResourceLeaveAssignments",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceLeaveAssignments_ResourceLeaveId",
                table: "ResourceLeaveAssignments",
                column: "ResourceLeaveId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceLeaves_LeavePlanId",
                table: "ResourceLeaves",
                column: "LeavePlanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leaves");

            migrationBuilder.DropTable(
                name: "ResourceLeaveAssignments");

            migrationBuilder.DropTable(
                name: "LeavePlanAssignments");

            migrationBuilder.DropTable(
                name: "ResourceLeaves");

            migrationBuilder.DropTable(
                name: "LeaveTypes");

            migrationBuilder.DropTable(
                name: "LeavePlans");
        }
    }
}
