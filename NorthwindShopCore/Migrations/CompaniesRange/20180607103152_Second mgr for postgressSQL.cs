using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NorthwindShopCore.Migrations.CompaniesRange
{
    public partial class SecondmgrforpostgressSQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "company_sequence");

            migrationBuilder.CreateSequence(
                name: "companytype_sequence",
                startValue: 2L);

            migrationBuilder.CreateSequence(
                name: "general_sequence");

            migrationBuilder.CreateSequence(
                name: "investor_sequence");

            migrationBuilder.CreateTable(
                name: "companytype",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Info = table.Column<string>(nullable: true),
                    companytypeid = table.Column<int>(nullable: false, defaultValueSql: "nextval('companytype_sequence'::regclass)"),
                    nametype = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companytype", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "investor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Info = table.Column<string>(nullable: true),
                    investorid = table.Column<int>(nullable: false, defaultValueSql: "nextval('investor_sequence'::regclass)"),
                    name = table.Column<string>(nullable: false),
                    companyid = table.Column<int>(nullable: true),
                    capital = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_investor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "owner",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Info = table.Column<string>(nullable: true),
                    ownerid = table.Column<int>(nullable: false, defaultValueSql: "nextval('general_sequence'::regclass)"),
                    name = table.Column<string>(nullable: false),
                    salary = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_owner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Info = table.Column<string>(nullable: true),
                    companyid = table.Column<int>(nullable: false, defaultValueSql: "nextval('company_sequence'::regclass)"),
                    name = table.Column<string>(nullable: false),
                    ownerid = table.Column<int>(nullable: false),
                    typeid = table.Column<int>(nullable: false),
                    profitperyear = table.Column<int>(nullable: true),
                    investorid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.Id);
                    table.ForeignKey(
                        name: "investor_foreign",
                        column: x => x.investorid,
                        principalTable: "investor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "owner_foreign",
                        column: x => x.ownerid,
                        principalTable: "owner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "category_foreign",
                        column: x => x.typeid,
                        principalTable: "companytype",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_company_investorid",
                table: "company",
                column: "investorid");

            migrationBuilder.CreateIndex(
                name: "IX_company_ownerid",
                table: "company",
                column: "ownerid");

            migrationBuilder.CreateIndex(
                name: "IX_company_typeid",
                table: "company",
                column: "typeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "investor");

            migrationBuilder.DropTable(
                name: "owner");

            migrationBuilder.DropTable(
                name: "companytype");

            migrationBuilder.DropSequence(
                name: "company_sequence");

            migrationBuilder.DropSequence(
                name: "companytype_sequence");

            migrationBuilder.DropSequence(
                name: "general_sequence");

            migrationBuilder.DropSequence(
                name: "investor_sequence");
        }
    }
}
