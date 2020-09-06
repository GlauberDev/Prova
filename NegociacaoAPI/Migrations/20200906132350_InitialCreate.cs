using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NegociacaoAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tomadores",
                columns: table => new
                {
                    TomadorId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CPF = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tomadores", x => x.TomadorId);
                });

            migrationBuilder.CreateTable(
                name: "dividas",
                columns: table => new
                {
                    DividaId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Valor = table.Column<decimal>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: false),
                    TomadorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dividas", x => x.DividaId);
                    table.ForeignKey(
                        name: "FK_dividas_tomadores_TomadorId",
                        column: x => x.TomadorId,
                        principalTable: "tomadores",
                        principalColumn: "TomadorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "simulacoes",
                columns: table => new
                {
                    SimulacaoId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DividaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_simulacoes", x => x.SimulacaoId);
                    table.ForeignKey(
                        name: "FK_simulacoes_dividas_DividaId",
                        column: x => x.DividaId,
                        principalTable: "dividas",
                        principalColumn: "DividaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "acordos",
                columns: table => new
                {
                    AcordoId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ativo = table.Column<bool>(nullable: false),
                    SimulacaoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_acordos", x => x.AcordoId);
                    table.ForeignKey(
                        name: "FK_acordos_simulacoes_SimulacaoId",
                        column: x => x.SimulacaoId,
                        principalTable: "simulacoes",
                        principalColumn: "SimulacaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "parcelamentos",
                columns: table => new
                {
                    ParcelaId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumeroParcela = table.Column<int>(nullable: false),
                    VencimentoParcela = table.Column<DateTime>(nullable: false),
                    ValorParcela = table.Column<decimal>(nullable: false),
                    SimulacaoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parcelamentos", x => x.ParcelaId);
                    table.ForeignKey(
                        name: "FK_parcelamentos_simulacoes_SimulacaoId",
                        column: x => x.SimulacaoId,
                        principalTable: "simulacoes",
                        principalColumn: "SimulacaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tomadores",
                columns: new[] { "TomadorId", "CPF" },
                values: new object[] { 1, "987.335.450-65" });

            migrationBuilder.InsertData(
                table: "tomadores",
                columns: new[] { "TomadorId", "CPF" },
                values: new object[] { 2, "245.546.680-96" });

            migrationBuilder.InsertData(
                table: "tomadores",
                columns: new[] { "TomadorId", "CPF" },
                values: new object[] { 3, "435.964.560-02" });

            migrationBuilder.InsertData(
                table: "tomadores",
                columns: new[] { "TomadorId", "CPF" },
                values: new object[] { 4, "940.872.480-11" });

            migrationBuilder.InsertData(
                table: "dividas",
                columns: new[] { "DividaId", "DataAtualizacao", "TomadorId", "Valor" },
                values: new object[] { 1, new DateTime(2020, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1000m });

            migrationBuilder.InsertData(
                table: "dividas",
                columns: new[] { "DividaId", "DataAtualizacao", "TomadorId", "Valor" },
                values: new object[] { 2, new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1500m });

            migrationBuilder.InsertData(
                table: "dividas",
                columns: new[] { "DividaId", "DataAtualizacao", "TomadorId", "Valor" },
                values: new object[] { 4, new DateTime(2020, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2500m });

            migrationBuilder.InsertData(
                table: "dividas",
                columns: new[] { "DividaId", "DataAtualizacao", "TomadorId", "Valor" },
                values: new object[] { 3, new DateTime(2020, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2500m });

            migrationBuilder.InsertData(
                table: "simulacoes",
                columns: new[] { "SimulacaoId", "DividaId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "simulacoes",
                columns: new[] { "SimulacaoId", "DividaId" },
                values: new object[] { 2, 2 });

            migrationBuilder.InsertData(
                table: "simulacoes",
                columns: new[] { "SimulacaoId", "DividaId" },
                values: new object[] { 4, 4 });

            migrationBuilder.InsertData(
                table: "simulacoes",
                columns: new[] { "SimulacaoId", "DividaId" },
                values: new object[] { 3, 3 });

            migrationBuilder.InsertData(
                table: "acordos",
                columns: new[] { "AcordoId", "Ativo", "SimulacaoId" },
                values: new object[] { 1, true, 1 });

            migrationBuilder.InsertData(
                table: "acordos",
                columns: new[] { "AcordoId", "Ativo", "SimulacaoId" },
                values: new object[] { 2, true, 2 });

            migrationBuilder.InsertData(
                table: "acordos",
                columns: new[] { "AcordoId", "Ativo", "SimulacaoId" },
                values: new object[] { 4, true, 4 });

            migrationBuilder.InsertData(
                table: "acordos",
                columns: new[] { "AcordoId", "Ativo", "SimulacaoId" },
                values: new object[] { 3, true, 3 });

            migrationBuilder.InsertData(
                table: "parcelamentos",
                columns: new[] { "ParcelaId", "NumeroParcela", "SimulacaoId", "ValorParcela", "VencimentoParcela" },
                values: new object[] { 1, 1, 1, 100m, new DateTime(2020, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "parcelamentos",
                columns: new[] { "ParcelaId", "NumeroParcela", "SimulacaoId", "ValorParcela", "VencimentoParcela" },
                values: new object[] { 2, 1, 2, 150m, new DateTime(2020, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "parcelamentos",
                columns: new[] { "ParcelaId", "NumeroParcela", "SimulacaoId", "ValorParcela", "VencimentoParcela" },
                values: new object[] { 4, 1, 4, 170m, new DateTime(2020, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "parcelamentos",
                columns: new[] { "ParcelaId", "NumeroParcela", "SimulacaoId", "ValorParcela", "VencimentoParcela" },
                values: new object[] { 3, 1, 3, 170m, new DateTime(2020, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_acordos_SimulacaoId",
                table: "acordos",
                column: "SimulacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_dividas_TomadorId",
                table: "dividas",
                column: "TomadorId");

            migrationBuilder.CreateIndex(
                name: "IX_parcelamentos_SimulacaoId",
                table: "parcelamentos",
                column: "SimulacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_simulacoes_DividaId",
                table: "simulacoes",
                column: "DividaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "acordos");

            migrationBuilder.DropTable(
                name: "parcelamentos");

            migrationBuilder.DropTable(
                name: "simulacoes");

            migrationBuilder.DropTable(
                name: "dividas");

            migrationBuilder.DropTable(
                name: "tomadores");
        }
    }
}
