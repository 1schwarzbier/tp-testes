using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using RealEstate.Database;

namespace RealEstate.IntegrationTests.TestInfrastructure;

/// <summary>
/// <b>This aberration should not exist in any project</b>, don't use it as a reference for your future projects.
/// <br/> <br/>
/// The only reason this code still exist is that <b>I don't want to host this database anywhere.</b>
/// <br/> <br/>
/// Said, we can now proceed with this madness.
/// </summary>
public class TestInfrastructure
{
    private const string SetupSchema = @"
        IF NOT EXISTS ( SELECT * FROM  sys.schemas WHERE name = 'Platform' )
        EXEC('CREATE SCHEMA [Platform]')";
    
    private const string SetupPropertiesTable = @"
        IF NOT EXISTS (SELECT * from sysobjects WHERE name='Properties' and xtype='U')
        BEGIN
            CREATE TABLE [Platform].[Properties](
                [Id] [UNIQUEIDENTIFIER] NOT NULL default NEWID(),
		        [Name] [nvarchar](max) NULL,
                [Description] [nvarchar](max) NULL,
                [ImageURL] [nvarchar](max) NULL,
                [Type] [nvarchar](128) NOT NULL,
                [SaleMode] [nvarchar](128) NOT NULL,
		        [Address] [nvarchar](max) NOT NULL,
		        [Size] [decimal](9,2) NOT NULL,
                [ContactInfo] [nvarchar](max) NOT NULL,
                [Price] [decimal](9,2) NOT NULL,
                [BathroomCount] [int] NOT NULL,
                [RoomCount] [int] NOT NULL,
                [ParkingCount] [int] NOT NULL,
                [IsActive] [bit] NOT NULL,
                [IsCommercial] [bit] NOT NULL,
            CONSTRAINT [PK_Properties] PRIMARY KEY CLUSTERED ([Id] ASC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
                ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

            INSERT INTO Platform.Properties (Name, Description, ImageURL, Type, SaleMode, Address, Size, ContactInfo, Price, BathroomCount, RoomCount, ParkingCount, IsActive, IsCommercial) VALUES (N'Apartamento 1', N'Nope', null, N'Apartment', N'Rent', N'Test', 100.00, N'aaaaaa', 3000.00, 3, 3, 2, 1, 0);
            INSERT INTO Platform.Properties (Name, Description, ImageURL, Type, SaleMode, Address, Size, ContactInfo, Price, BathroomCount, RoomCount, ParkingCount, IsActive, IsCommercial) VALUES (N'Apartamento 2', N'Nope', null, N'Apartment', N'Rent', N'Test', 100.00, N'aaaaaa', 2500.00, 5, 4, 1, 1, 0);
            INSERT INTO Platform.Properties (Name, Description, ImageURL, Type, SaleMode, Address, Size, ContactInfo, Price, BathroomCount, RoomCount, ParkingCount, IsActive, IsCommercial) VALUES (N'Apartamento 3', N'Nope', null, N'Apartment', N'Rent', N'Test', 100.00, N'aaaaaa', 2000.00, 2, 1, 1, 1, 0);
            INSERT INTO Platform.Properties (Name, Description, ImageURL, Type, SaleMode, Address, Size, ContactInfo, Price, BathroomCount, RoomCount, ParkingCount, IsActive, IsCommercial) VALUES (N'Apartamento 4', N'Nope', null, N'Apartment', N'Rent', N'Test', 100.00, N'aaaaaa', 1500.00, 4, 5, 1, 1, 0);
            INSERT INTO Platform.Properties (Name, Description, ImageURL, Type, SaleMode, Address, Size, ContactInfo, Price, BathroomCount, RoomCount, ParkingCount, IsActive, IsCommercial) VALUES (N'Apartamento 5', N'Nope', null, N'Apartment', N'Rent', N'Test', 100.00, N'aaaaaa', 1000.00, 1, 1, 0, 0, 0);
        END";

    [Ignore("This method should only execute the first time running this project locally")]
    [Test, Order(0)]
    public async Task CreateSchema()
    {
        var config = Configuration.GetConfiguration();
        using var connection = new SqlConnectionFactory(config.GetConnectionString("database")).GetConnection();

        if (connection.State != ConnectionState.Open)
            connection.Open();
        
        var result = await connection.ExecuteAsync(SetupSchema);
        
        Assert.That(result, Is.GreaterThanOrEqualTo(0));
    }
    
    [Ignore("This method should only execute the first time running this project locally")]
    [Test, Order(1)]
    public async Task CreatePropertiesTable()
    {
        var config = Configuration.GetConfiguration();
        using var connection = new SqlConnectionFactory(config.GetConnectionString("database")).GetConnection();

        if (connection.State != ConnectionState.Open)
            connection.Open();
        
        var result = await connection.ExecuteAsync(SetupPropertiesTable);
        
        Assert.That(result, Is.GreaterThanOrEqualTo(0));
    }
}