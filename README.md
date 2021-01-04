# TodoApi
This is a Asp .Net Core Web Api project that serves as Todo data CRUD operations.

> ## Tools stack:
- ASP .NET CORE
- C#
- MS SQL SERVER
- CASSANDRA DB
- POSTMAN

> ## Prerequisites:
Need to install
```SQL SERVER``` 
```CASSANDRA SERVER```
```POSTMAN```.
It will be better if you install 
```Visual Studio 2019```.

> ## Run and Build:
Clone or Download this repository. Change Connection strings for database connection in 
the properties folder at ```laucnhSettings.json```. After that we need to start the cassandra server.
Then Run the solution file and it will start the server and you are ready to request in the api endpoints.

> ### API ENDPOINTS / URL:
- GET (http://localhost:{port}/todo/all) => It returns all the todo items.
- GET http://localhost:{port}/todo/{id} => It returns single {id} the todo items.
- GET http://localhost:{port}/todo/?datetime = <serchValue> => It returns all the todo items filter by {dateTime}.
- DELETE http://localhost:{port}/todo/{id} => It delete single {id} the todo items.
- PUT http://localhost:{port}/todo/{id} => It alter single {id} the todo items.
- PATCH http://localhost:{port}/todo/{id} => It update single {id}'s date for todo items.
