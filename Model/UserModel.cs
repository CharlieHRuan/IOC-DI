using System;

namespace Model
{
    public class UserModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
