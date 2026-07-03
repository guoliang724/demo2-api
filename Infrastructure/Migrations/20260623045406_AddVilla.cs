using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class AddVilla : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.UpdateData(
          table: "villas",
          keyColumn: "Id",
          keyValue: 1,
          column: "CreatedDate",
          value: new DateTime(2026, 6, 22, 22, 54, 5, 909, DateTimeKind.Local).AddTicks(1718));

      migrationBuilder.UpdateData(
          table: "villas",
          keyColumn: "Id",
          keyValue: 2,
          column: "CreatedDate",
          value: new DateTime(2026, 6, 22, 22, 54, 5, 911, DateTimeKind.Local).AddTicks(2158));

      migrationBuilder.UpdateData(
          table: "villas",
          keyColumn: "Id",
          keyValue: 3,
          column: "CreatedDate",
          value: new DateTime(2026, 6, 22, 22, 54, 5, 911, DateTimeKind.Local).AddTicks(2181));

      migrationBuilder.UpdateData(
          table: "villas",
          keyColumn: "Id",
          keyValue: 4,
          column: "CreatedDate",
          value: new DateTime(2026, 6, 22, 22, 54, 5, 911, DateTimeKind.Local).AddTicks(2185));

      migrationBuilder.UpdateData(
          table: "villas",
          keyColumn: "Id",
          keyValue: 5,
          column: "CreatedDate",
          value: new DateTime(2026, 6, 22, 22, 54, 5, 911, DateTimeKind.Local).AddTicks(2188));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.UpdateData(
          table: "villas",
          keyColumn: "Id",
          keyValue: 1,
          column: "CreatedDate",
          value: new DateTime(2026, 6, 22, 22, 52, 5, 771, DateTimeKind.Local).AddTicks(9646));

      migrationBuilder.UpdateData(
          table: "villas",
          keyColumn: "Id",
          keyValue: 2,
          column: "CreatedDate",
          value: new DateTime(2026, 6, 22, 22, 52, 5, 773, DateTimeKind.Local).AddTicks(6701));

      migrationBuilder.UpdateData(
          table: "villas",
          keyColumn: "Id",
          keyValue: 3,
          column: "CreatedDate",
          value: new DateTime(2026, 6, 22, 22, 52, 5, 773, DateTimeKind.Local).AddTicks(6721));

      migrationBuilder.UpdateData(
          table: "villas",
          keyColumn: "Id",
          keyValue: 4,
          column: "CreatedDate",
          value: new DateTime(2026, 6, 22, 22, 52, 5, 773, DateTimeKind.Local).AddTicks(6725));

      migrationBuilder.UpdateData(
          table: "villas",
          keyColumn: "Id",
          keyValue: 5,
          column: "CreatedDate",
          value: new DateTime(2026, 6, 22, 22, 52, 5, 773, DateTimeKind.Local).AddTicks(6728));
    }
  }
}
