# ManageClient

- ManageClient is a project "File storage",which created in programming language C# and .Net Core MVC Framework.
- User data (password,email,username),is stored in DB MSSQL,where user password,is crypted in base64.
- User data is insert in the DB using EntityFramework.
- All files,which upload on the site,are stored in free cluster MongoDB.
- Files is insert in the MongoDB cluster  using library "MongoDB.Bson,MongoDB.Driver,MongoDB.DriverGridFs".
- ManageClient accept all type of file and can to open some file direct in the website.
