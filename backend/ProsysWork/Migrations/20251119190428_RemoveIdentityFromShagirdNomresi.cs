using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProsysWork.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIdentityFromShagirdNomresi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the existing table and recreate without IDENTITY
            migrationBuilder.Sql(@"
                -- Create temporary table without IDENTITY
                CREATE TABLE [Shagirdler_temp] (
                    [Nomresi] int NOT NULL,
                    [Adi] varchar(30) NOT NULL,
                    [Soyadi] varchar(30) NOT NULL,
                    [Sinifi] smallint NOT NULL,
                    CONSTRAINT [PK_Shagirdler_temp] PRIMARY KEY ([Nomresi])
                );

                -- Copy data if exists
                IF EXISTS (SELECT * FROM [Shagirdler])
                BEGIN
                    SET IDENTITY_INSERT [Shagirdler_temp] ON;
                    INSERT INTO [Shagirdler_temp] ([Nomresi], [Adi], [Soyadi], [Sinifi])
                    SELECT [Nomresi], [Adi], [Soyadi], [Sinifi] FROM [Shagirdler];
                    SET IDENTITY_INSERT [Shagirdler_temp] OFF;
                END

                -- Drop foreign key constraints
                ALTER TABLE [Imtahanlar] DROP CONSTRAINT [FK_Imtahanlar_Shagirdler_ShagirdNomresi];

                -- Drop old table
                DROP TABLE [Shagirdler];

                -- Rename temp table
                EXEC sp_rename '[Shagirdler_temp]', 'Shagirdler';

                -- Recreate foreign key
                ALTER TABLE [Imtahanlar]
                ADD CONSTRAINT [FK_Imtahanlar_Shagirdler_ShagirdNomresi] 
                FOREIGN KEY ([ShagirdNomresi]) REFERENCES [Shagirdler] ([Nomresi]) ON DELETE NO ACTION;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Nomresi",
                table: "Shagirdler",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
