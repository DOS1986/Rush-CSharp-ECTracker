using DB;
using Microsoft.Data.Sqlite;
using System;
using System.IO;

namespace DataLayer
{
    public class DbRepository : IDbRepository
    {
        protected const string ApplicationFolderName = "ECTracker";
        protected const string SqLiteDatabaseName = "ECTracker.db";
        private readonly string _dbFolderPath;
        private static SqliteConnection DbConnection { get; set; }
        public static SqliteConnection ConnectionString { get; set; }

        public DbRepository()
        {
            _dbFolderPath = FullFolderPath(ApplicationFolderName);
            ConnectionString = new SqliteConnection($"Data Source={Path.Combine(_dbFolderPath, Path.GetFileName(SqLiteDatabaseName) ?? throw new InvalidOperationException())};");
            InitializeConnections();
        }

        public void InitializeConnections()
        {
            CreateAndOpenDb();
        }

        public string FullFolderPath(string applicationFolder)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), applicationFolder);
        }

        public static SqliteConnection GetConnection()
        {
            using var connection = DbConnection = ConnectionString;
            return DbConnection;
        }

        private void CreateAndOpenDb()
        {
            if (!Directory.Exists(_dbFolderPath)) Directory.CreateDirectory(_dbFolderPath);

            if (!File.Exists(Path.Combine(_dbFolderPath, Path.GetFileName(SqLiteDatabaseName))))
            {
                using var connection = DbConnection = ConnectionString;
                connection.Open();
                new SeedDatabase(connection).Execute();
                connection.Close();
            }
                
        }
    }
}
