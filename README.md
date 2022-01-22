## Docker Compose with two Api and database.
Run 'docker-compose up'

First Api:
Insert new users and autheticate.
Documentation: http://localhost:5000/index.html

Second Api:
Management othes CRUD with autheticated.
Documentation: http://localhost:5001/index.html

 * [Technologies]
	 * [Microsoft Visual Studio Code v. 1.63.2]
	 * [.NET Core v. 6.0.101]
		* [Entity Framework Core v. 6.0.1]
		* [Microsoft.EntityFrameworkCore.Design v. 6.0.1]
		* [Microsoft.EntityFrameworkCore.SqlServer v. 6.0.1]
		* [Microsoft.AspNetCore.Authentication v. 2.2.0]
    	* [Microsoft.AspNetCore.Authentication.JwtBearer v 6.0.1]
		* [Documentation SwaggerUI]
			* [Swashbuckle.AspNetCore v 6.2.3]
			* [Swashbuckle.AspNetCore.Annotations v 6.2.3]
	* [Docker Desktop 4.3.2]
		* [Images]
			* [SQL Server 2019]
				* [mcr.microsoft.com/mssql/server:2019-latest]
			* [mcr.microsoft.com/dotnet/sdk:6.0-alpine]