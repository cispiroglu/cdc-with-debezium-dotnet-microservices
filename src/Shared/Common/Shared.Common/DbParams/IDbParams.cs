namespace Shared.Common.DbParams;

public interface IDbParams
{
    string ConnectionString { get; }
    int DbType { get; }
}