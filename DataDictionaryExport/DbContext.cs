using SqlSugar;
using System.Data;

namespace DataDictionaryExport;

public class Db
{
    private static SqlSugarClient Client = new Lazy<SqlSugarClient>(() => new SqlSugarClient(new ConnectionConfig()
    {
        ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.3.103.201)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User Id=c##longking;Password=longking123;", // 这里替换为你的数据库连接字符串
        DbType = SqlSugar.DbType.Oracle, // 这里替换为你的数据库类型
        IsAutoCloseConnection = true, // 自动关闭数据库连接
        InitKeyType = InitKeyType.Attribute // 从实体特性中读取主键自增列信息
    })).Value;

    public static DataTable GetDataTable(string sql, params SugarParameter[] parameters) => Client.Ado.GetDataTable(sql, parameters);
    public static string GetString(string sql, params SugarParameter[] parameters) => Client.Ado.GetString(sql, parameters);
}
