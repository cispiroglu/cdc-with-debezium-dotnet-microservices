namespace Shared.Common.DbParams;

public class DbParams : IDbParams
{
    public string ConnectionString { get; }
    public int DbType { get; }
}