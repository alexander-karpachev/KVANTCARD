Update-Database -Verbose -ConnectionStringName Db
Uninstall-Package EntityFramework -Force
Update-Package -reinstall
Install-Package Microsoft.EntityFrameworkCore
Install-Package Ninject

Remove-Migration

Add-Migration -name Initial

                        .Annotation("Sqlite:Autoincrement", true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
