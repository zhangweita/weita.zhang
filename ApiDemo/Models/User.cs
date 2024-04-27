using Microsoft.AspNetCore.Identity;

namespace ApiDemo.Models
{
    public class User : IdentityUser<long>
    {
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public string? NickName { get; set; }
        public long JWTVersion { get; set; } = 0;
    }
}
