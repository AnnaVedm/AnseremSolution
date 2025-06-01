const path = require('path');
const fs = require('fs').promises;
const axios = require('axios');

const resultFilePath = path.resolve(__dirname, 'result.txt');
const testJsonPath = path.resolve(__dirname, 'test.json');

// Отправка данных на API и запись результата в файл
async function getAnalize(data) {
    try {
        const response = await axios.post('http://localhost:5222/api/Analize/GetAnalize', data, {
            headers: { 'Content-Type': 'application/json; charset=UTF-8' }
        });
        await createResponseFile(response.data);
        console.log('Результаты сохранены в', resultFilePath);
    } catch (error) {
        console.error('Ошибка запроса или записи:', error);
    }
}

// Форматирование и запись ответа в файл
async function createResponseFile(users) {
    const formattedText = users.map(user => 
        `Имя: ${user.name}
         Номер телефона: ${user.phone}
         Почта: ${user.email}
         Количество друзей: ${user.friendCount}
         Дружеские пары: ${user.mutualFriends.length ? user.mutualFriends.join(', ') : 'Отсутствуют'}
`).join('\n');

    await fs.writeFile(resultFilePath, formattedText, 'utf8');
}

// Главная функция: чтение файла, фильтрация и запуск анализа через асинхронность
async function root() {
    try {
        const data = await fs.readFile(testJsonPath, 'utf8');
        const users = JSON.parse(data);
        const activeUsers = users.filter(u => u.isActive);

        if (!activeUsers.length) {
            console.log('Нет активных пользователей.');
            return;
        }

        await getAnalize(activeUsers); // Вызываем функцию getAnalize с активными пользователями
    } catch (error) {
        console.error('Ошибка чтения или парсинга JSON:', error);
    }
}

root();
