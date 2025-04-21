using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreApp.Migrations
{
    /// <inheritdoc />
    public partial class NotificationAndNotificationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "notification_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    icon = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    created = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NotificationTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    title = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    body = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    created = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.id);
                    table.ForeignKey(
                        name: "FK_notifications_notification_types_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "notification_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceNotification",
                columns: table => new
                {
                    ReceivedNotificationsId = table.Column<int>(type: "INTEGER", nullable: false),
                    SentDevicesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceNotification", x => new { x.ReceivedNotificationsId, x.SentDevicesId });
                    table.ForeignKey(
                        name: "FK_DeviceNotification_devices_SentDevicesId",
                        column: x => x.SentDevicesId,
                        principalTable: "devices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceNotification_notifications_ReceivedNotificationsId",
                        column: x => x.ReceivedNotificationsId,
                        principalTable: "notifications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceNotification_SentDevicesId",
                table: "DeviceNotification",
                column: "SentDevicesId");

            migrationBuilder.CreateIndex(
                name: "IX_notification_types_title",
                table: "notification_types",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_notifications_NotificationTypeId",
                table: "notifications",
                column: "NotificationTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceNotification");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "notification_types");
        }
    }
}
