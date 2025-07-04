﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TestController.Models
{
    public class Person
    {
        [JsonPropertyName("_id")] // Для соответствия полей с json
        public string Id { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("balance")]
        public string Balance { get; set; }

        [JsonPropertyName("age")]
        public int Age { get; set; }

        [JsonPropertyName("eyeColor")]
        public string EyeColor { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("company")]
        public string Company { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("friends")]
        public List<Friend> Friends { get; set; }

        [JsonPropertyName("favoriteFruit")]
        public string FavoriteFruit { get; set; }
    }
}
