using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using TestController.Models;

namespace TestController.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalizeController : ControllerBase
    {
        // Обрабатываем POST-запрос с данными людей, возвращаем анализ
        [HttpPost("GetAnalize")]
        public ActionResult<List<PersonResponse>> GetAnalize([FromBody] List<Person> people)
        {
            var responses = new List<PersonResponse>();

            foreach (var person in people)
            {
                var response = new PersonResponse
                {
                    Name = person.Name,
                    Phone = ValidatePhone(person.Phone),  
                    Email = ValidateEmail(person.Email),   
                    FriendCount = person.Friends?.Count ?? 0,
                    MutualFriends = new List<string>()
                };

                if (person.Friends != null)
                {
                    foreach (var friend in person.Friends)
                    {
                        var friendPerson = people.FirstOrDefault(p => p.Name == friend.Name);
                        if (friendPerson != null && friendPerson.Friends != null)
                        {
                            if (friendPerson.Friends.Any(f => f.Name == person.Name))
                            {
                                response.MutualFriends.Add($"{person.Name} - {friend.Name}");
                            }
                        }
                    }
                }

                if (response.MutualFriends.Count == 0)
                {
                    response.MutualFriends.Add("Дружеские пары отсутствуют");
                }

                responses.Add(response);
            }

            return Ok(responses);
        }

        // Проверяем формат телефона
        private string ValidatePhone(string phone)
        {
            // Извлекаем из строки только цифры, убирая все остальные символы
            var digits = new string(phone.Where(char.IsDigit).ToArray());
            if (digits.Length < 11)
                return "Номер телефона содержит ошибки";

            var phonePattern = @"^\+1 \(\d{3}\) \d{3}-\d{4}$";
            if (!Regex.IsMatch(phone, phonePattern))
                return "Номер телефона содержит ошибки";

            return phone;
        }

        // Проверяем формат email
        private string ValidateEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailPattern))
                return "Почта содержит ошибки";

            return email;
        }
    }
}
