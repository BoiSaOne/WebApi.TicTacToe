# Задача #1 на стажировку backend developer C#. Реализация web api для игры в крестики нолики. 
### Проект содержит следующие контроллеры с endpoints:

**POST**

*/api/Auth/Register* - регистрация пользователя

Принимает json
```
{
  "login": "string", 
  "password": "string"
}
```
возвращает json
```
{
  "user": {
    "id": 14,
    "name": "stringnew"
  },
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic3RyaW5nbmV3IiwiZXhwIjoxNjc5NDgwNDc1LCJpc3MiOiJCb2lrb3YgQS5TLiIsImF1ZCI6IldlYkFwaS5UaWNUYWNUb2UifQ.IUabLIkR0m9Kgv9RCnVvMkJqes82UQ98t5fJRXLTik0"
}
```
**POST**

*/api/Auth/Login* - авторизация пользователя

принимает json
```
{
  "login": "string",
  "password": "string"
}
```
возвращает json
```
{
  "user": {
    "id": 11,
    "name": "string"
  },
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic3RyaW5nIiwiZXhwIjoxNjc5NDgwNjI2LCJpc3MiOiJCb2lrb3YgQS5TLiIsImF1ZCI6IldlYkFwaS5UaWNUYWNUb2UifQ.LfiP8fuhbbc3k02uCa2ukVeRXYH8FKTJE4XBeOXZ0ao"
}
```
**POST**

*/api/Rooms?userName* - создание комнаты для пользователя

*userName* - имя пользователя

возвращает json
```
{
  "id": 349,
  "nameRoom": "string",
  "statusRoom": 0
}
```
**DELETE**

*/api/Rooms/{userName}* - удаление комнаты

*userName* - имя пользователя, для которого удаляем комнату

возвращает json
```
{
  "message": "Комната пользователя string удалена"
}
```
**GET**

*/api/Rooms* - получить список доступных комнат

возвращает json
```
[
  {
    "id": 350,
    "nameRoom": "string",
    "statusRoom": 0
  }
]
```
**GET**

*/api/Rooms/{roomName}?userName* - подключиться к комнате

*roomName* - имя комнаты, к которой подключается пользователь

*userName* - имя подключаемого пользователя

при заполнение комнаты нужным количеством игроков происходит редирект на адрес */api/Game?groupName*

**GET**

*/api/Game?groupName* - создание и получение игры

*groupName* - название комнаты

возвращет json
```
{
  "game": {
    "id": 91,
    "creationDate": "2023-03-22T13:53:41.0450707+03:00"
  },
  "playerMakeMove": "testov",
  "room": "testov",
  "queuePlayers": [
    "testov",
    "string"
  ],
  "numberOfMoves": 0,
  "column": 0,
  "row": 0
}
```
**POST**

*/api/Game* - регистрация хода игрока

принимает json
```
{
  "game": {
    "id": 91,
    "creationDate": "2023-03-22T13:53:41.0450707+03:00"
  },
  "playerMakeMove": "testov",
  "room": "testov",
  "queuePlayers": [
    "testov",
    "string"
  ],
  "numberOfMoves": 1,
  "column": 1,
  "row": 3
}
```
возвращает json через hub, используется библиотека SiganlR для синхронизации ходов игроков.

Для этого был добавлен Hub, обмен данными происходит по пути */gameHub*.

Для подключения и отключения пользователей присутствуют следующие методы:
```
Task AddUserInGroupAsync(string groupName)
Task RemoveUserFromGroupAsync(string groupName)
```
Используются следующие методы для отправки json:
```
Task NotifyGroupAsync(GameDTO? gameDTO) //отправляется пользователям о начале игры
Task NotifyGroupPlayerMadeMoveAsync(GameDTO? gameDTO) //отправляется ход игрока 
//пример json файла:
{
  "game": {
    "id": 91,
    "creationDate": "2023-03-22T13:53:41.0450707+03:00"
  },
  "playerMakeMove": "testov",
  "room": "testov",
  "queuePlayers": [
    "testov",
    "string"
  ],
  "numberOfMoves": 1,
  "column": 1,
  "row": 3
}
Task NotifyResultGame(IEnumerable<ResultGamePlayer> resultGamePlayers) //отправляется результат игры
//пример json файла:
[
  {
    "game": {
      "id": 91,
      "creationDate": "2023-03-22T13:53:41.0450707+03:00"
    },
    "player": {
      id: 9,
      name: testov    
    },
    resultGame: 0
  },
  {
    "game": {
      "id": 91,
      "creationDate": "2023-03-22T13:53:41.0450707+03:00"
    },
    "player": {
      id: 5,
      name: testovNew    
    },
    resultGame: 2
  },
]

```
При неудачных запросах будет возвращаться следующий json файл
```
{
  "message": "Здесь описание ошибки"
}
```
Для тестирования API был подключен swagger, можно проверить по следующему пути:

*https://localhost:7013/swagger/index.html* 

Так же я написал небольшой front для тестирования одновременных ходов, можно проверить по следующему пути:

*https://localhost:7013/*

7013 - это порт у вас возможно отличается





