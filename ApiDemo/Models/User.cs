using Microsoft.AspNetCore.Identity;

namespace ApiDemo.Models
{
    public class User : IdentityUser<long>
    {
        public DateTime CreationTime { get; set; }
        public string? NickName { get; set; }
    }
}
