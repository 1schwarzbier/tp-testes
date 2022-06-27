using System.Data;

namespace RealEstate.Database;

public interface ISqlConnectionFactory
{
    IDbConnection GetConnection();
}