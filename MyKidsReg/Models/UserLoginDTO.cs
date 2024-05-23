using MyKidsReg.Models;
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
public class UpdateUserDTO
{
    public string User_Name { get; set; }
    public string Name { get; set; }
    public string Last_name { get; set; }
    public string Address { get; set; }
    public int Zip_code { get; set; }
    public string E_mail { get; set; }
    public long Mobil_nr { get; set; }
    public User_type Usertype { get; set; }
}
