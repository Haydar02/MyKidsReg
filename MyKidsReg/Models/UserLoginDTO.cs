using System.Text.Json.Serialization;

namespace MyKidsReg.Models
{
    public class UserLoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public User_type user_Type { get; set; }
    }
}
