

using DbUp;
using DbUp.Engine;

namespace Auth.Core.Common
{

    public class MigrationEngine
    {
        public static DatabaseUpgradeResult RunScript(string conn, string loc)
        {
            if(string.IsNullOrEmpty(conn))
            {
                return new DatabaseUpgradeResult(null,false,null);
            }
            var upgrader = DeployChanges.To.SqlDatabase(conn)
            .WithScriptsFromFileSystem($"{loc}")
            .WithTransactionPerScript()
            .JournalToSqlTable("dbo","MigrationsJournal")
            .LogToConsole()
            .LogScriptOutput()
            .Build();

            return upgrader.PerformUpgrade();
        }
    }
    
}