using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NorthwindShopCore.Migrations.CompaniesRangeJSON
{
    public partial class InitialpostgressSQLmgr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "companytype",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    info = table.Column<string>(type: "json", nullable: false),
                    Companytypeid = table.Column<int>(nullable: false),
                    Nametype = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companytype", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "investor",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    info = table.Column<string>(type: "json", nullable: false),
                    Investorid = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Companyid = table.Column<int>(nullable: true),
                    Capital = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_investor", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "owner",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    info = table.Column<string>(type: "json", nullable: false),
                    Ownerid = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Salary = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_owner", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    info = table.Column<string>(type: "json", nullable: false),
                    Companyid = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Ownerid = table.Column<int>(nullable: false),
                    Typeid = table.Column<int>(nullable: false),
                    Profitperyear = table.Column<int>(nullable: true),
                    Investorid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.id);
                    table.ForeignKey(
                        name: "FK_company_investor_Investorid",
                        column: x => x.Investorid,
                        principalTable: "investor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_company_owner_Ownerid",
                        column: x => x.Ownerid,
                        principalTable: "owner",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_company_companytype_Typeid",
                        column: x => x.Typeid,
                        principalTable: "companytype",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_company_Investorid",
                table: "company",
                column: "Investorid");

            migrationBuilder.CreateIndex(
                name: "IX_company_Ownerid",
                table: "company",
                column: "Ownerid");

            migrationBuilder.CreateIndex(
                name: "IX_company_Typeid",
                table: "company",
                column: "Typeid");
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
        }
    }
}
