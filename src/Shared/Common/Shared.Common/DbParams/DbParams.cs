namespace Shared.Common.DbParams;

public class DbParams : IDbParams
{
    public string ConnectionString { get; set; } = string.Empty;
    public int DbType { get; set; } = (int)Shared.Common.DbType.NotSelected;
}