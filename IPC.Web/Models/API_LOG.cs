using System.ComponentModel.DataAnnotations.Schema;

namespace IPC.Presentation.Web.Models;

[Table("API_LOG")]
public class API_LOG
{
    public int ID { get; set; }
    /// <summary>
    /// 接口地址
    /// </summary>
    public string URL { get; set; }
    /// <summary>
    /// HTTP请求类型
    /// </summary>
    public string REQUEST_METHOD { get; set; }
    /// <summary>
    /// 接口执行方法
    /// </summary>
    public string ACTION_NAME { get; set; }
    /// <summary>
    /// 设备编号
    /// </summary>
    public string EQUIPMENT_CODE { get; set; }
    /// <summary>
    /// 接口方法参数
    /// </summary>
    public string ACTION_ARGUMENTS { get; set; }
    /// <summary>
    /// 接口返回内容
    /// </summary>
    public string HTTP_RESULT { get; set; }
    /// <summary>
    /// 请求时间
    /// </summary>
    public DateTime REQUEST_TIME { get; set; }
    /// <summary>
    /// 接口执行时长（毫秒）
    /// </summary>
    public string MES_DURATION { get; set; }
    /// <summary>
    /// 接口执行时长（毫秒）
    /// </summary>
    public string EXECUTION_DURATION { get; set; }
    /// <summary>
    /// 异常信息
    /// </summary>
    public string EXCEPTION { get; set; }
    /// <summary>
    /// 服务器ip
    /// </summary>
    public string SERVER_IP { get; set; }
    /// <summary>
    /// 请求Ip
    /// </summary>
    public string REQUEST_IP { get; set; }
}
