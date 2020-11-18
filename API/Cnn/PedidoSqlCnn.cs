using API.Properties;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace API.Connection
{

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
  public class PedidoSqlCnn : IDbCnn
  {
    private readonly ILogger _log;
    private readonly IConfiguration _config;

    public string Schema => "dbo";
    public string DataBase => "Pedido";

    public PedidoSqlCnn(ILogger<PedidoSqlCnn> logger, IConfiguration config)
    {
      _log = logger;
      _config = config;

      if (_log != null && _config.GetConnectionString(DataBase) is null)
        _log.LogError("Missing Connection String");

      if (!HasSchema()) CreateSchema();
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
      if (_config == null)
        return Task.FromResult(HealthCheckResult.Unhealthy("Missing Configuration"));
      if (_config.GetConnectionString(DataBase) == null)
        return Task.FromResult(HealthCheckResult.Unhealthy("Missing Connection String"));

      try { using var test = GetConnection(); }
      catch (SqlException)
      { return Task.FromResult(HealthCheckResult.Unhealthy(Resources.SqlCnnException)); }
      catch (InvalidOperationException)
      { return Task.FromResult(HealthCheckResult.Unhealthy(Resources.SqlCnnAlreadyOpen)); }
      catch (Exception)
      { return Task.FromResult(HealthCheckResult.Unhealthy(Resources.SqlCnnGeneral)); }

      var sql = "SELECT top 1 1 FROM sys.databases";
      int result = 0;
      using var cnn = GetConnection();
      try { result = cnn.ExecuteScalar<int>(sql, commandTimeout: cnn.ConnectionTimeout); }
      catch (Exception e) { return Task.FromResult(HealthCheckResult.Unhealthy(e.Message)); }

      if (result != 1) { return Task.FromResult(HealthCheckResult.Unhealthy("No Results")); }

      return Task.FromResult(HealthCheckResult.Healthy(nameof(PedidoSqlCnn)));
    }

    public IDbConnection GetConnection()
    {
      try
      {
        return new SqlConnection(_config.GetConnectionString(DataBase));
      }
      catch (SqlException e)
      {
        if (_log != null) _log.LogError(e, Resources.SqlCnnException);
        throw;
      }
      catch (InvalidOperationException e)
      {
        if (_log != null) _log.LogError(e, Resources.SqlCnnAlreadyOpen);
        throw;
      }
      catch (Exception e)
      {
        if (_log != null) _log.LogError(e, Resources.SqlCnnGeneral);
        throw;
      }
    }

    private bool HasSchema()
    {
      using var cnn = GetConnection();
      string schema = cnn.ExecuteScalar<string>("select schema_name from information_schema.schemata where schema_name=@Schema", new { Schema }, commandTimeout: cnn.ConnectionTimeout);
      return !string.IsNullOrEmpty(schema);
    }
    private void CreateSchema()
    {
      if (string.IsNullOrWhiteSpace(Schema)) return;

      using var cnn = GetConnection();
      cnn.Execute($"create schema {Schema}");
    }

    public bool HasTable(string tableName)
    {
      using var cnn = GetConnection();
      string table = cnn.ExecuteScalar<string>("select table_name from information_schema.tables where table_name=@tableName and table_schema=@Schema", new { tableName, Schema }, commandTimeout: cnn.ConnectionTimeout);
      return !string.IsNullOrEmpty(table);
    }
  }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}