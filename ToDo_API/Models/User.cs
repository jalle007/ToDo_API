using System.Text.Json.Serialization;

namespace ToDo_API.Models
{
    public enum Role
    {
        USER,
        ADMIN
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public Role Role { get; set; }

    }
}
