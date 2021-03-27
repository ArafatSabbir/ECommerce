# E-Commerce
E-Commerce application written in ASP.NET Core 3.1 MVC and MSSQL.

## Architecture
This application is created using ASP.NET Core 3.1 Web Application <br />
*Languages*: C#, HTML, CSS, SCSS <br />
*Tools*: Visual Studion 2019, MSSQL Server Management Studio, Bootstrap <br />
*Type of Application*: Web Application <br />


## Project Directory
<br />
<pre> 
	e-Commerce[Solution]
		|
		|
		|------Libraries
		|	|	
		|	|---ECommerce.Core
		|	|
		|	|---ECommerce.Data
		|	
		|
		|------ECommerce.Web
				
</pre>

<h3> Edit database connection string in appsettings.json </h3>
<code> 
	"DefaultConnection": "server=localhost;userid=root;pwd=[database password];port=[your port number];
	database=ExamService;sslmode=none;charset=utf8;" 
</code>
<br /> <b> Example :</b> <br />
<code> 
	"DefaultConnection": "server=localhost;userid=root;pwd=;port=3306;
	database=ExamService;sslmode=none;charset=utf8;" 
</code>

## How to Setting, Build and Run
The following is required to run the program.
1. Open Visual Studio 2019 Community or Enterprise edition.
2. Open MSSQL Server Management Studio.
3. Set database connection string.
4. Update DataBase and Hit IIS Express.

**ENJOY!**

## Images
**1. Sign Up Form**
![Sign Up Form]<img src="https://github.com/ArafatSabbir/ECommerce/blob/31389530cef5106601e4c49b446a4904007f41d2/Demo%20of%20the%20Project/SignUpForm.png" height=300 align=center>



