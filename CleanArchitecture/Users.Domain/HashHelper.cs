using System.Security.Cryptography;
using System.Text;

namespace Users.Domain;

public class HashHelper
{
    public static string ComputeMd5Hash(string s)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(s);
        return Encoding.UTF8.GetString(MD5.HashData(bytes));
    }
}
