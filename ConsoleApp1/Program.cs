using System.Net.Http.Headers;
using System.Text;


string url = "https://10.3.102.249:34569/WinCCRestService/TagManagement/Values";

string body = @"{
    ""variableNames"": [
        ""H_R2.AI_RealValue""
    ]
}";

string returnJson = await HttpPostAsync("MES1", "123456", url, body);
int a = 1;


/// <summary>
/// HTTP请求 POST
/// </summary>
/// <param name="username"></param>
/// <param name="password"></param>
/// <param name="url">请求路径</param>
/// <param name="body">参数</param>
/// <returns></returns>
static async Task<string> HttpPostAsync(string username, string password, string url, string body)
{
    using HttpClient authClient = new(new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (sender, cert, chain, error) => true
    });
    string parameter = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
    AuthenticationHeaderValue authentication = new("Basic", parameter);
    authClient.DefaultRequestHeaders.Authorization = authentication;

    HttpContent httpContent = new StringContent(body);
    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

    HttpResponseMessage response = await authClient.PostAsync(new Uri(url), httpContent);
    response.EnsureSuccessStatusCode();

    return await response.Content.ReadAsStringAsync();
}