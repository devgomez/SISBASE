using Microsoft.Data.Sqlite;
using System.Diagnostics;
using System.Text;

namespace SISBase.Infrastructure.Persistence.Sqlite
{
    public static class DbDiagnostics
    {
        private static readonly object Sync = new();

        public static void LogStartupInfo(string? connectionString)
        {
            Log("================ DB STARTUP ================");
            Log($"CurrentDirectory: {Environment.CurrentDirectory}");
            Log($"AppContext.BaseDirectory: {AppContext.BaseDirectory}");
            Log($"RawConnectionString: {connectionString ?? "<null>"}");

            var resolved = ResolveDbPath(connectionString);
            Log($"ResolvedDataSource: {resolved ?? "<cannot-resolve>"}");

            if (!string.IsNullOrWhiteSpace(resolved))
            {
                Log($"ResolvedDataSourceExists: {File.Exists(resolved)}");
            }
        }

        public static string? ResolveDbPath(string? connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                return null;
            }

            try
            {
                var builder = new SqliteConnectionStringBuilder(connectionString);
                var dataSource = builder.DataSource;

                if (string.IsNullOrWhiteSpace(dataSource) || dataSource == ":memory:")
                {
                    return dataSource;
                }

                return Path.IsPathRooted(dataSource)
                    ? Path.GetFullPath(dataSource)
                    : Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, dataSource));
            }
            catch (Exception ex)
            {
                Log($"ResolveDbPathError: {ex.Message}");
                return null;
            }
        }

        public static void LogEf(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            if (message.Contains("Opening connection", StringComparison.OrdinalIgnoreCase)
                || message.Contains("Opened connection", StringComparison.OrdinalIgnoreCase)
                || message.Contains("Closing connection", StringComparison.OrdinalIgnoreCase)
                || message.Contains("Executed DbCommand", StringComparison.OrdinalIgnoreCase)
                || message.Contains("CREATE TABLE", StringComparison.OrdinalIgnoreCase)
                || message.Contains("SELECT", StringComparison.OrdinalIgnoreCase)
                || message.Contains("INSERT", StringComparison.OrdinalIgnoreCase)
                || message.Contains("UPDATE", StringComparison.OrdinalIgnoreCase)
                || message.Contains("DELETE", StringComparison.OrdinalIgnoreCase))
            {
                Log($"EF: {message.Trim()}");
            }
        }

        public static void Log(string message)
        {
            var line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] {message}";

            lock (Sync)
            {
                var logDir = Path.Combine(AppContext.BaseDirectory, "logs");
                Directory.CreateDirectory(logDir);
                var logPath = Path.Combine(logDir, "db-diagnostics.log");
                File.AppendAllText(logPath, line + Environment.NewLine, Encoding.UTF8);
            }

            Debug.WriteLine(line);
        }
    }
}
