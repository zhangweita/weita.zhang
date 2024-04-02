
using SqlSugar;

var Db = new SqlSugarClient(new ConnectionConfig()
{
    ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.3.103.201)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User Id=c##longking;Password=longking123;", // 这里替换为你的数据库连接字符串
    DbType = DbType.Oracle, // 这里替换为你的数据库类型
    IsAutoCloseConnection = true, // 自动关闭数据库连接
    InitKeyType = InitKeyType.Attribute // 从实体特性中读取主键自增列信息
});


ApiLog apiLog = new()
{
    //CreateTime = DateTime.Now,
    CreateTime1 = DateTime.Now,
    ActionName = "测试测试",
    EquipmentCode = "EQU_ZWT"
};

try
{
    Db.Insertable(apiLog).ExecuteCommand();
}
catch (Exception)
{
    throw;
}
