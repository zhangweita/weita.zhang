using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Uinfor.Scada.Common;

public class HttpHelper
{
//    /// <summary>    
//    /// HTTP POST方式请求数据    
//    /// </summary>    
//    /// <param name="url">接口路径</param>    
//    /// <param name="postText">POST报文</param>    
//    /// <returns></returns>    
//    public static string HttpPost(string url, string postText)
//    {
//#pragma warning disable SYSLIB0014 // 类型或成员已过时
//        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
//#pragma warning restore SYSLIB0014 // 类型或成员已过时
//        request.Method = "POST";
//        request.ContentType = "application/json";
//        request.Accept = "*/*";
//        request.Timeout = 15000;
//        request.AllowAutoRedirect = false;

//        StreamWriter requestStream = null;
//        WebResponse response = null;
//        string responseStr = null;

//        try
//        {
//            requestStream = new StreamWriter(request.GetRequestStream());
//            requestStream.Write(postText);
//            requestStream.Close();

//            response = request.GetResponse();
//            if (response != null)
//            {
//                StreamReader reader = new(stream: response.GetResponseStream(), Encoding.UTF8);
//                responseStr = reader.ReadToEnd();
//                reader.Close();
//            }
//        }
//        catch (Exception ex)
//        {
//            throw new Exception("发送数据到WebApi接口异常:" + ex.ToString());
//        }
//        finally
//        {
//            requestStream?.Dispose();
//            response?.Dispose();
//        }
//        return responseStr;
//    }

    //private static object objLock = new();
    /// <summary>
    ///  Http Post 方式调用数据
    /// </summary>
    /// <param name="url"></param>
    /// <param name="body"></param>
    /// <returns></returns>
    public static string HttpPost<T>(string url, T body)
    {
        //lock (objLock)
        //{
        if (body == null || string.IsNullOrEmpty(url)) return null;
        string strJsonBody = JsonConvert.SerializeObject(body);
        Encoding encoding = Encoding.UTF8;
#pragma warning disable SYSLIB0014 // 类型或成员已过时
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
#pragma warning restore SYSLIB0014 // 类型或成员已过时
        request.Method = "POST";
        request.Accept = "text/html, application/xhtml+xml, */*";
        request.ContentType = "application/json";

        byte[] buffer = encoding.GetBytes(strJsonBody);
        request.ContentLength = buffer.Length;
        using (Stream stream = request.GetRequestStream())
        {
            stream.Write(buffer, 0, buffer.Length);
        }

        string responseContent = string.Empty;
        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            using (StreamReader reader = new(response.GetResponseStream(), Encoding.UTF8))
            {
                responseContent = reader.ReadToEnd();
            }
        }
        return responseContent;
        //}
    }

    //static readonly HttpClient client = new();
    ///// <summary>
    ///// HTTP请求 POST
    ///// </summary>
    ///// <typeparam name="T">实体名</typeparam>
    ///// <param name="url">请求路径</param>
    ///// <param name="Params">参数</param>
    ///// <returns></returns>
    //public static async Task<string> HttpPostAsync<T>(string url, T t)
    //{
    //    string body = JsonConvert.SerializeObject(t);
    //    return await HttpPostAsync<T>(url, body);
    //}

    ///// <summary>
    ///// HTTP请求 POST
    ///// </summary>
    ///// <typeparam name="T">实体名</typeparam>
    ///// <param name="url">请求路径</param>
    ///// <param name="Params">参数</param>
    ///// <returns></returns>
    //public static async Task<string> HttpPostAsync<T>(string url, string body)
    //{
    //    HttpContent httpContent = new StringContent(body);
    //    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
    //    //清除 Http 缓存
    //    httpContent.Headers.TryAddWithoutValidation("Cache-Control", "no-cache");
    //    httpContent.Headers.TryAddWithoutValidation("Pragma", "no-cache");
    //    httpContent.Headers.TryAddWithoutValidation("Expires", "0");

    //    HttpResponseMessage httpResponse = await client.PostAsync(url, httpContent);
    //    httpResponse.EnsureSuccessStatusCode();

    //    return await httpResponse.Content.ReadAsStringAsync();
    //}


    //static readonly HttpClient authClient = new();

    ///// <summary>
    ///// HTTP请求 POST
    ///// </summary>
    ///// <param name="url">请求路径</param>
    ///// <param name="body">参数</param>
    ///// <returns></returns>
    //public static async Task<string> HttpPostAsync(string username, string password, string url, string body)
    //{
    //    string parameter = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
    //    AuthenticationHeaderValue authentication = new("Basic", parameter);
    //    authClient.DefaultRequestHeaders.Authorization = authentication;

    //    HttpContent httpContent = new StringContent(body);
    //    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
    //    //清除 Http 缓存
    //    httpContent.Headers.TryAddWithoutValidation("Cache-Control", "no-cache");
    //    httpContent.Headers.TryAddWithoutValidation("Pragma", "no-cache");
    //    httpContent.Headers.TryAddWithoutValidation("Expires", "0");

    //    HttpResponseMessage httpResponse = await authClient.PostAsync(url, httpContent);
    //    httpResponse.EnsureSuccessStatusCode();

    //    return await httpResponse.Content.ReadAsStringAsync();
    //}


    ///// <summary>
    ///// HTTP请求 GET
    ///// </summary>
    ///// <param name="url">请求路径</param>
    ///// <returns></returns>
    //public static string HttpGetAsync(string url) => client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;

    ///// <summary>
    ///// 将DateTime转为DateTimeOffset 东八区时间
    ///// 2023-03-01 liugongkui
    ///// </summary>
    ///// <param name="time"></param>
    ///// <returns></returns>
    //public static DateTimeOffset DateTimeConvertDateTimeOffset(DateTime thisDate)
    //{
    //    const string tzName = "China Standard Time";
    //    TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(tzName);
    //    //通过时区，找到时间的offset
    //    TimeSpan offset = timeZone.GetUtcOffset(thisDate);
    //    //将时间转为DateTimeOffset
    //    DateTimeOffset newTime = new(thisDate, offset);
    //    return newTime;
    //}
}
