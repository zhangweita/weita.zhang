using HeyRed.MarkdownSharp;
using Microsoft.Extensions.Caching.Memory;
using System.Text;
using Ude;

namespace MiddlewareDemo;

public class MarkDownViewerMiddleware(RequestDelegate next, IWebHostEnvironment environment, IMemoryCache memoryCache)
{
    private readonly RequestDelegate next = next;
    private readonly IWebHostEnvironment environment = environment;
    private readonly IMemoryCache memoryCache = memoryCache;

    public async Task InvokeAsync(HttpContext context)
    {
        string path = context.Request.Path.Value ?? "";
        if (!path.EndsWith(".md"))
        {
            await next(context);
            return;
        }

        var file = environment.WebRootFileProvider.GetFileInfo(path);
        if (!file.Exists)
        {
            await next(context);
            return;
        }

        context.Response.ContentType = $"text/html;charset=utf-8";
        context.Response.StatusCode = 200;
        string cacheKey = nameof(MarkDownViewerMiddleware) + path + file.LastModified;
        var html = await memoryCache.GetOrCreateAsync(cacheKey, async e =>
        {
            e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
            using Stream stream = file.CreateReadStream();
            string text = await ReadText(stream);
            Markdown markdown = new();
            return markdown.Transform(text);
        });

        await context.Response.WriteAsync(html!);
    }

    /// <summary>
    /// 探测流的编码
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    private static string DetectCharset(Stream stream)
    {
        CharsetDetector charsetDetector = new();
        charsetDetector.Feed(stream);
        charsetDetector.DataEnd();

        string charset = charsetDetector.Charset ?? "UTF-8";
        stream.Position = 0;

        return charset;
    }

    /// <summary>
    /// 从流中读取文本
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    private static async Task<string> ReadText(Stream stream)
    {
        string charset = DetectCharset(stream);
        using var reader = new StreamReader(stream, Encoding.GetEncoding(charset));
        return await reader.ReadToEndAsync();
    }
}
