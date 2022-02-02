using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ParseTsgReplays.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Replays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Server = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    Island = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Mission = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsFinished = table.Column<bool>(type: "boolean", nullable: false),
                    WinnerSide = table.Column<int>(type: "integer", nullable: true),
                    CommanderWest = table.Column<string>(type: "text", nullable: true),
                    CommanderEast = table.Column<string>(type: "text", nullable: true),
                    CommanderGuer = table.Column<string>(type: "text", nullable: true),
                    Admin = table.Column<string>(type: "text", nullable: true),
                    PlayTime = table.Column<int>(type: "integer", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnterExit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UnitId = table.Column<int>(type: "integer", nullable: false),
                    IsEnter = table.Column<bool>(type: "boolean", nullable: false),
                    User = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ReplayId = table.Column<int>(type: "integer", nullable: false),
                    Time = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterExit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnterExit_Replays_ReplayId",
                        column: x => x.ReplayId,
                        principalTable: "Replays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UnitId = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<double>(type: "double precision", nullable: false),
                    Message = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsUnconscious = table.Column<bool>(type: "boolean", nullable: false),
                    IsPlayer = table.Column<bool>(type: "boolean", nullable: false),
                    ReplayId = table.Column<int>(type: "integer", nullable: false),
                    Time = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goals_Replays_ReplayId",
                        column: x => x.ReplayId,
                        principalTable: "Replays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShooterId = table.Column<int>(type: "integer", nullable: false),
                    TargetId = table.Column<int>(type: "integer", nullable: false),
                    Weapon = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Magazine = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Ammo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Distance = table.Column<double>(type: "double precision", nullable: false),
                    Damage = table.Column<double>(type: "double precision", nullable: false),
                    IsUnconscious = table.Column<bool>(type: "boolean", nullable: false),
                    ShooterVehicleId = table.Column<int>(type: "integer", nullable: true),
                    ReplayId = table.Column<int>(type: "integer", nullable: false),
                    Time = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hits_Replays_ReplayId",
                        column: x => x.ReplayId,
                        principalTable: "Replays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KillerId = table.Column<int>(type: "integer", nullable: false),
                    TargetId = table.Column<int>(type: "integer", nullable: false),
                    ReplayId = table.Column<int>(type: "integer", nullable: false),
                    Time = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kills_Replays_ReplayId",
                        column: x => x.ReplayId,
                        principalTable: "Replays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medicals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MedicId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    Action = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    IsUnconscious = table.Column<bool>(type: "boolean", nullable: false),
                    IsPlayer = table.Column<bool>(type: "boolean", nullable: false),
                    ReplayId = table.Column<int>(type: "integer", nullable: false),
                    Time = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicals_Replays_ReplayId",
                        column: x => x.ReplayId,
                        principalTable: "Replays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReplayId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Class = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Side = table.Column<int>(type: "integer", nullable: false),
                    Icon = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Squad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Damage = table.Column<double>(type: "double precision", nullable: false),
                    TimeOfDeath = table.Column<int>(type: "integer", nullable: true),
                    IsVehicle = table.Column<bool>(type: "boolean", nullable: false),
                    TimeInVehicle = table.Column<int>(type: "integer", nullable: false),
                    TimeOnFoot = table.Column<int>(type: "integer", nullable: false),
                    VehicleOrDriverId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_Replays_ReplayId",
                        column: x => x.ReplayId,
                        principalTable: "Replays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnterExit_ReplayId",
                table: "EnterExit",
                column: "ReplayId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_ReplayId",
                table: "Goals",
                column: "ReplayId");

            migrationBuilder.CreateIndex(
                name: "IX_Hits_ReplayId",
                table: "Hits",
                column: "ReplayId");

            migrationBuilder.CreateIndex(
                name: "IX_Kills_ReplayId",
                table: "Kills",
                column: "ReplayId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicals_ReplayId",
                table: "Medicals",
                column: "ReplayId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_ReplayId",
                table: "Units",
                column: "ReplayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnterExit");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "Hits");

            migrationBuilder.DropTable(
                name: "Kills");

            migrationBuilder.DropTable(
                name: "Medicals");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Replays");
        }
    }
}
