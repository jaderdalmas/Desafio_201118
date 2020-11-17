using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data;

namespace API.Connection
{
  public interface IDbCnn : IHealthCheck
  {
    string Schema { get; }

    /// <summary>
    /// Get DataBase Connection
    /// </summary>
    /// <returns>DataBase Connection</returns>
    IDbConnection GetConnection();

    /// <summary>
    /// Has Table?
    /// </summary>
    /// <param name="tableName">table name</param>
    /// <returns>true if exists</returns>
    bool HasTable(string tableName);
  }
}