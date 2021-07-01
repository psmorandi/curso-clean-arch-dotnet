namespace CleanArch.School.Infrastructure.Database.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

    public partial class AddedAutoIncrementIdToEventsForTracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "id",
                table: "invoice_events",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

            migrationBuilder.AddPrimaryKey(
                name: "pk_invoice_events",
                table: "invoice_events",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_invoice_events",
                table: "invoice_events");

            migrationBuilder.DropColumn(
                name: "id",
                table: "invoice_events");
        }
    }
}
