using System.Security.Cryptography;
using System.Text;

namespace IPC.Web.Common;

public static class DesEncryption
{
    private static byte[] _rgbKey = Encoding.UTF8.GetBytes("r#QrZ7d(qM%1qYb2creU"[..8]);
    private static byte[] _rgbIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

    /// <summary>
    /// DES 加密
    /// </summary>
    /// <param name="text">需要加密的值</param>
    /// <returns>加密后的结果</returns>
    public static string Encrypt(string text)
    {
        byte[] toEncrypt = Encoding.UTF8.GetBytes(text);
        using MemoryStream mStream = new();

        using var cStream = new CryptoStream(mStream, DES.Create().CreateEncryptor(_rgbKey, _rgbIV), CryptoStreamMode.Write);

        cStream.Write(toEncrypt, 0, toEncrypt.Length);
        cStream.FlushFinalBlock();

        return Convert.ToBase64String(mStream.ToArray());
    }

    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string Decrypt(string text)
    {
        byte[] toDecrypt = Convert.FromBase64String(text);
        using MemoryStream mStream = new();
        using CryptoStream cStream = new(mStream, DES.Create().CreateDecryptor(_rgbKey, _rgbIV), CryptoStreamMode.Write);

        cStream.Write(toDecrypt, 0, toDecrypt.Length);
        cStream.FlushFinalBlock();

        return Encoding.UTF8.GetString(mStream.ToArray());
    }
}
