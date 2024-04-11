namespace IPC.Model.DTO;

public class ApiLogDTO
{
    /// <summary>
    /// 接口地址
    /// </summary>
    public string Url { get; set; }
    /// <summary>
    /// HTTP请求类型
    /// </summary>
    public string RequestMethod { get; set; }
    /// <summary>
    /// 接口执行方法
    /// </summary>
    public string ActionName { get; set; }
    /// <summary>
    /// 设备编号
    /// </summary>
    public string EquipmentCode { get; set; }
    /// <summary>
    /// 接口方法参数
    /// </summary>
    public string RequestJson { get; set; }
    /// <summary>
    /// 接口返回内容
    /// </summary>
    public string ResponseJson { get; set; }
    /// <summary>
    /// 请求时间
    /// </summary>
    public DateTime RequestTime { get; set; }
    /// <summary>
    /// 接口执行时长（毫秒）
    /// </summary>
    public string MesDuration { get; set; }
    /// <summary>
    /// 接口执行时长（毫秒）
    /// </summary>
    public string TotalDuration { get; set; }
    /// <summary>
    /// 异常信息
    /// </summary>
    public string Exception { get; set; }
    /// <summary>
    /// 服务器ip
    /// </summary>
    public string ServerIp { get; set; }
    /// <summary>
    /// 请求Ip
    /// </summary>
    public string RequestIp { get; set; }
}
