using SqlSugar;

/// <summary>
/// 接口日志模型
/// </summary>
[SugarTable(TableName = "API_LOG")]
public class ApiLog
{
    /// <summary>
    /// 接口地址
    /// </summary>
    [SugarColumn(ColumnName = "URL")]
    public string Url { get; set; }
    /// <summary>
    /// 接口执行方法
    /// </summary>
    [SugarColumn(ColumnName = "ACTION_NAME")]
    public string ActionName { get; set; }
    /// <summary>
    /// 设备编号
    /// </summary>
    [SugarColumn(ColumnName = "EQUIPMENT_CODE")]
    public string EquipmentCode { get; set; }
    /// <summary>
    /// 接口方法参数
    /// </summary>
    [SugarColumn(ColumnName = "ACTION_ARGUMENTS")]
    public string ActionArguments { get; set; }
    /// <summary>
    /// HTTP请求类型
    /// </summary>
    [SugarColumn(ColumnName = "REQUEST_METHOD")]
    public string RequestMethod { get; set; }
    /// <summary>
    /// 接口返回内容
    /// </summary>
    [SugarColumn(ColumnName = "HTTP_RESULT")]
    public string HttpResult { get; set; }
    /// <summary>
    /// 接口执行时长（毫秒）
    /// </summary>
    [SugarColumn(ColumnName = "EXECUTION_DURATION")]
    public string ExecutionDuration { get; set; }
    ///// <summary>
    ///// 创建时间
    ///// </summary>
    //[SugarColumn(ColumnName = "CREATE_TIME")]
    //public DateTime CreateTime { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [SugarColumn(ColumnName = "CREATE_TIME1")]
    public DateTime CreateTime1 { get; set; }
    /// <summary>
    /// 服务器ip
    /// </summary>
    [SugarColumn(ColumnName = "SERVER_IP")]
    public string ServerIp { get; set; }
    /// <summary>
    /// 请求Ip
    /// </summary>
    [SugarColumn(ColumnName = "REQUEST_IP")]
    public string RequestIp { get; set; }
    /// <summary>
    /// 接口执行时长（毫秒）
    /// </summary>
    [SugarColumn(ColumnName = "MES_DURATION")]
    public string MesDuration { get; set; }
    /// <summary>
    /// 异常信息
    /// </summary>
    [SugarColumn(ColumnName = "EXCEPTION")]
    public string Exception { get; set; } = "";
}
