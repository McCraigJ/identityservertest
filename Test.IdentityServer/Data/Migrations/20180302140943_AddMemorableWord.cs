using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Test.IdentityServer.Data.Migrations
{
  public partial class AddMemorableWord : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<string>(name: "MemorableWord", type: "varchar(15)", table: "AspNetUsers", nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(name: "MemorableWord", table: "AspNetUsers");
    }
  }
}
