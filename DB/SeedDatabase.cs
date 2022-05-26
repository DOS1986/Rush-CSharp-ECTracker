using DataLayer;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    internal class SeedDatabase
    {
        private readonly SqliteConnection _dbConnection;

        public SeedDatabase(SqliteConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void Execute()
        {
            new CreateUser(_dbConnection).SeedUser(_dbConnection);
            new CreateCategories(_dbConnection).SeedCategories(_dbConnection);
            //CreateFootPrintCategories();
            //CreateFootPrints();
            //CreateDistributors();
            //CreateManufacturers();
            //CreateAttachments();
            //CreateSiPrefixes();
            //CreateUnits();
            //CreateUnitSiPrefixes();
            //CreateMeasurementUnits();
            //CreateStockHistory();
            //CreateStorageLocationCategories();
            //CreateStorageLocations();
            //CreateParameters();
            //CreateParts();
            //CreatePartParameters();
            //CreateProjects();
            //CreateProjectParts();
            //CreateProjectAttachments();
            //CreatePartDistributors();
            //CreatePartManufacturers();
        }

        // Table Creation - Done
        // Insert Data - InProgress
        public class CreateUser
        {
            public CreateUser(SqliteConnection dbConnection)
            {
                // Create a User table
                dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [User] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [FirstName] NVARCHAR(64) NOT NULL,
                    [LastName] NVARCHAR(64) NOT NULL,
                    [Password] NVARCHAR(64) NOT NULL,
                    [EmailAddress] NVARCHAR(64) NOT NULL,
                    [UserName] NVARCHAR(64) NOT NULL,
                    [DateCreated] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [DateModified] TIMESTAMP
                )");
            }

            public void SeedUser(SqliteConnection dbConnection)
            {
                dbConnection.ExecuteNonQuery(@"
                INSERT INTO User (
                    FirstName,
                    LastName,
                    Password,
                    EmailAddress,
                    UserName)
                VALUES (
                    'David',
                    'Southwood',
                    'Test123456',
                    'dos1986@gmail.com',
                    'Techie1986');");
            }
        }

        // Table Creation - Done
        // Insert Data - InProgress
        public class CreateCategories
        {
            public CreateCategories(SqliteConnection dbConnection)
            {
                // Create a Category self-referencing table
                dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [Category] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [ParentId] INTEGER,
                    [Name] NVARCHAR(64) NOT NULL,
                    [Description] NVARCHAR(64) NOT NULL,
                    [DateCreated] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [DateModified] TIMESTAMP,
                    FOREIGN KEY(ParentId) REFERENCES Category(Id)
                )");
            }

            public void SeedCategories(SqliteConnection dbConnection)
            {
                dbConnection.ExecuteNonQuery(@"
                    INSERT INTO Category (
                        Name,
                        Description
                    ) VALUES (
                        'Diode',
                        'Two-terminal electronic component that conducts current primarily in one direction (asymmetric conductance); it has low (ideally zero) resistance in one direction, and high (ideally infinite) resistance in the other.'
                    );");
            }
        }

        // Table Creation - Done
        // Insert Data - InProgress
        public void CreateFootPrintCategories()
        {
            // Create a FootPrintCategory self-referencing table
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [FootPrintCategory] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [ParentId] INTEGER
                    [Name] NVARCHAR(64) NOT NULL,
                    [Description] NVARCHAR(64) NOT NULL,
                    [DateCreated] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [DateModified] TIMESTAMP,
                    FOREIGN KEY(ParentId) REFERENCES FootPrintCategory(Id)
                )");
        }

        // Table Creation - Done
        // Insert Data - InProgress
        public void CreateFootPrints()
        {
            // Create FootPrints table and add foreign key for categories
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [FootPrint] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [Name] NVARCHAR(64) NOT NULL,
                    [Description] NVARCHAR(64) NOT NULL,
                    [ImageLocation] NVARCHAR(64) NOT NULL,
                    [FootPrintCategoryId] Integer,
                    [DateCreated] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [DateModified] TIMESTAMP,
                    FOREIGN KEY(FootPrintCategoryId) REFERENCES FootPrintCategory(Id)
                )");
        }

        // Table Creation - Done
        // Insert Data - InProgress
        //
        public void CreateDistributors()
        {
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [Distributor] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [Name] NVARCHAR(64) NOT NULL,
                    [Address] NVARCHAR(64) NOT NULL,
                    [Website] NVARCHAR(64) NOT NULL,
                    [IsPricing] BOOLEAN NOT NULL CHECK (IsPricing IN (0,1)),
                    [Email] NVARCHAR(64) NOT NULL,
                    [Phone] NVARCHAR(64) NOT NULL,
                    [Fax] NVARCHAR(64) NOT NULL,
                    [Comment] NVARCHAR(64) NOT NULL,
                    [DateCreated] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [DateModified] TIMESTAMP
                )");
        }

        // Table Creation - Done
        // Insert Data - InProgress
        //
        public void CreateManufacturers()
        {
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [Manufacturer] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [Name] NVARCHAR(64) NOT NULL,
                    [Address] NVARCHAR(64) NOT NULL,
                    [Website] NVARCHAR(64) NOT NULL,
                    [Email] NVARCHAR(64) NOT NULL,
                    [Phone] NVARCHAR(64) NOT NULL,
                    [Fax] NVARCHAR(64) NOT NULL,
                    [Comment] NVARCHAR(64) NOT NULL,
                    [LogoLocation] NVARCHAR(64) NOT NULL,
                    [DateCreated] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [DateModified] TIMESTAMP
                )");
        }

        // Table Creation - Done
        // Insert Data - InProgress
        //
        public void CreateAttachments()
        {
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [Attachment] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [Filename] NVARCHAR(64) NOT NULL,
                    [Size] NVARCHAR(64) NOT NULL,
                    [UploadLocation] NVARCHAR(64) NOT NULL,
                    [DateCreated] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [DateModified] TIMESTAMP
                )");
        }

        // Table Creation - Done
        // Insert Data - InProgress
        //
        public void CreateSiPrefixes()
        {
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [SIPrefix] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [Prefix] NVARCHAR(64) NOT NULL,
                    [Symbol] NVARCHAR(64) NOT NULL,
                    [Power] NVARCHAR(64) NOT NULL,
                    [DateCreated] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [DateModified] TIMESTAMP
                )");
        }

        // Table Creation - Done
        // Insert Data - InProgress
        //
        public void CreateUnits()
        {
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [Unit] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [UnitName] NVARCHAR(64) NOT NULL,
                    [Symbol] NVARCHAR(64) NOT NULL,
                    [DateCreated] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [DateModified] TIMESTAMP
                )");
        }

        // Table Creation - Done
        // Insert Data - InProgress
        //
        public void CreateUnitSiPrefixes()
        {
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [PartSIPrefix] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [SIPrefixId] NVARCHAR(64) NOT NULL,
                    [UnitId] NVARCHAR(64) NOT NULL,
                    [DateCreated] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [DateModified] TIMESTAMP,
                    FOREIGN KEY(SIPrefixId) REFERENCES SIPrefix(Id),
                    FOREIGN KEY(UnitId) REFERENCES Unit(Id)
                )");
        }

        // Table Creation - Done
        // Insert Data - InProgress
        public void CreateMeasurementUnits()
        {
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [MeasurementUnit] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [Name] NVARCHAR(64) NOT NULL,
                    [ShortName] NVARCHAR(64) NOT NULL,
                    [DateCreated] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [DateModified] TIMESTAMP
                )");
        }

        // Table Creation - Done
        // Insert Data - InProgress
        //
        public void CreateStorageLocationCategories()
        {
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [StorageLocationCategory] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [ParentId] INTEGER,
                    [Name] NVARCHAR(64) NOT NULL,
                    [Description] NVARCHAR(64) NOT NULL,
                    [DateCreated] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [DateModified] TIMESTAMP,
                    FOREIGN KEY(ParentId) REFERENCES StorageLocationCategory(Id)
                )");
        }

        // Table Creation - Done
        // Insert Data - InProgress
        //
        public void CreateStorageLocations()
        {
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [StorageLocation] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [CategoryId] INTEGER,
                    [Name] NVARCHAR(64) NOT NULL,
                    [ImageLocation] NVARCHAR(64) NULL,
                    [DateCreated] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [DateModified] TIMESTAMP,
                    FOREIGN KEY(CategoryId) REFERENCES StorageLocationCategory(Id)
                )");
        }

        // Table Creation - Done
        // Insert Data - InProgress
        //
        public void CreateParameters()
        {
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [Parameter] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [Name] NVARCHAR(64) NOT NULL,
                    [Description] NVARCHAR(64) NOT NULL,
                    [UnitId] INTEGER,
                    [ValueType] NVARCHAR(64) NOT NULL,
                    [Value] NVARCHAR(64) NOT NULL,
                    [MinValue] NVARCHAR(64) NOT NULL,
                    [MinValueUnit] NVARCHAR(64) NOT NULL,
                    [NominalValue] NVARCHAR(64) NOT NULL,
                    [NominalValueUnit] NVARCHAR(64) NOT NULL,
                    [MaxValue] NVARCHAR(64) NOT NULL,
                    [MaxValueUnit] NVARCHAR(64) NOT NULL,
                    [DateCreated] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [DateModified] TIMESTAMP,
                    FOREIGN KEY(UnitId) REFERENCES Unit(Id)
                )");
        }

        // Table Creation - Done
        // Insert Data - InProgress
        //
        public void CreateParts()
        {
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [Part] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [Name] NVARCHAR(64) NOT NULL,
                    [Description] NVARCHAR(64) NOT NULL,
                    [CategoryId] INTEGER,
                    [MinimumStock] INTEGER NOT NULL,
                    [MeasurementUnitId] INTEGER NOT NULL,
                    [FootprintId] INTEGER NOT NULL,
                    [StorageLocationId] INTEGER NOT NULL,
                    [Comment] TEXT NOT NULL,
                    [ProductionRemarks] NVARCHAR(64) NOT NULL,
                    [Status] NVARCHAR(64) NOT NULL,
                    [NeedsReview] BOOLEAN NOT NULL CHECK (NeedsReview IN (0,1)),
                    [Condition] NVARCHAR(64) NOT NULL,
                    [InternalPartNumber] INTEGER NOT NULL,
                    [DateCreated] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [DateModified] TIMESTAMP,
                    FOREIGN KEY(CategoryId) REFERENCES Category(Id),
                    FOREIGN KEY(MeasurementUnitId) REFERENCES MeasurementUnit(Id),
                    FOREIGN KEY(FootprintId) REFERENCES FootPrint(Id),
                    FOREIGN KEY(StorageLocationId) REFERENCES StorageLocation(Id)
                )");
        }

        // Table Creation - Done
        // Insert Data - InProgress
        //
        public void CreateStockHistory()
        {
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [StockHistory] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [PartId] INTEGER,
                    [Date] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [Amount] INTEGER NULL,
                    [Price] INTEGER NULL,
                    [Comment] NVARCHAR(64) NULL,
                    FOREIGN KEY(PartId) REFERENCES Part(Id)
                )");
        }

        // Table Creation - Done
        // Insert Data - InProgress
        //
        public void CreatePartParameters()
        {
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [PartParameter] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [ParameterId] INTEGER,
                    [PartId] INTEGER,
                    [DateCreated] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [DateModified] TIMESTAMP,
                    FOREIGN KEY(ParameterId) REFERENCES Parameter(Id),
                    FOREIGN KEY(PartId) REFERENCES Part(Id)
                )");
        }

        // Table Creation - Done
        // Insert Data - InProgress
        //
        public void CreateProjects()
        {
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [Project] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [Name] NVARCHAR(64) NOT NULL,
                    [Description] NVARCHAR(64) NOT NULL,
                    [DateCreated] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [DateModified] TIMESTAMP
                )");
        }

        // Table Creation - Done
        // Insert Data - InProgress
        //
        public void CreateProjectParts()
        {
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [ProjectPart] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [ProjectId] INTEGER,
                    [Quantity] INTEGER,
                    [OverageType] NVARCHAR(64) NOT NULL,
                    [Overage] NVARCHAR(64) NOT NULL,
                    [PartId] INTEGER,
                    [Remarks] NVARCHAR(64) NOT NULL,
                    [LotNumber] NVARCHAR(64) NOT NULL,
                    [DateCreated] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [DateModified] TIMESTAMP,
                    FOREIGN KEY(ProjectId) REFERENCES Project(Id),
                    FOREIGN KEY(PartId) REFERENCES Part(Id)
                )");
        }

        // Table Creation - Done
        // Insert Data - InProgress
        //
        public void CreateProjectAttachments()
        {
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [ProjectAttachment] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [ProjectId] INTEGER,
                    [AttachmentId] INTEGER,
                    [Description] NVARCHAR(64) NOT NULL,
                    [DateCreated] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [DateModified] TIMESTAMP,
                    FOREIGN KEY(ProjectId) REFERENCES Project(Id),
                    FOREIGN KEY(AttachmentId) REFERENCES Attachment(Id)
                )");
        }

        // Table Creation - Done
        // Insert Data - InProgress
        //
        public void CreatePartDistributors()
        {
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [PartDistributor] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [PartId] INTEGER,
                    [DistributorId] INTEGER,
                    [OrderNumber] INTEGER,
                    [Packaging] INTEGER,
                    [PricePerUnit] INTEGER,
                    [Currency] INTEGER,
                    [PackagePrice] INTEGER,
                    [SKU] INTEGER,
                    [Pricing] INTEGER,
                    [IsIgnore] BOOLEAN NOT NULL CHECK (IsIgnore IN (0,1)),
                    [DateCreated] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [DateModified] TIMESTAMP,
                    FOREIGN KEY(PartId) REFERENCES Part(Id),
                    FOREIGN KEY(DistributorId) REFERENCES Distributor(Id)
                )");
        }

        // Table Creation - Done
        // Insert Data - InProgress
        //
        public void CreatePartManufacturers()
        {
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [PartManufacturers] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [PartId] INTEGER,
                    [ManufacturerId] INTEGER,
                    [Website] NVARCHAR(64) NOT NULL,
                    [Email] NVARCHAR(64) NOT NULL,
                    [Phone] NVARCHAR(64) NOT NULL,
                    [Fax] NVARCHAR(64) NOT NULL,
                    [Comment] NVARCHAR(64) NOT NULL,
                    [LogoLocation] NVARCHAR(64) NOT NULL,
                    [DateCreated] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [DateModified] TIMESTAMP,
                    FOREIGN KEY(PartId) REFERENCES Part(Id),
                    FOREIGN KEY(ManufacturerId) REFERENCES Manufacturer(Id)
                )");
        }
    }
}
