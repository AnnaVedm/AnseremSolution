using System.Collections.Generic;
namespace TestController.Models
{

    public class PersonResponse
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int FriendCount { get; set; }
        public List<string> MutualFriends { get; set; }
    }

}
