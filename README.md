# E-Commerce
E-Commerce application written in ASP.NET Core 3.1 MVC and MSSQL.

## Architecture
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

## Features
**1. At first sight **
At first run a Sign Up / Log In form will open if you are not logged in. 

***1.1 Sign Up Form***
<img src="https://github.com/ArafatSabbir/ECommerce/blob/31389530cef5106601e4c49b446a4904007f41d2/Demo%20of%20the%20Project/SignUpForm.png" height=300 align=center>

***1.2 Log In Form***
<img src="https://github.com/ArafatSabbir/ECommerce/blob/ef09c28397dbad083e1fb73b401c25f37c3ed31f/Demo%20of%20the%20Project/LoginForm.PNG" height=300 align=center>

***1.3 Home Page***
If you are logged in Home Page will open. 
<img src="https://github.com/ArafatSabbir/ECommerce/blob/ef09c28397dbad083e1fb73b401c25f37c3ed31f/Demo%20of%20the%20Project/Home%20Page.PNG" height=300 align=center>

**2. Admin DashBoard**
<img src="https://github.com/ArafatSabbir/ECommerce/blob/ef09c28397dbad083e1fb73b401c25f37c3ed31f/Demo%20of%20the%20Project/Admin%20Dashboard.PNG" height=300 align=center>

***2.1 Add Category***
<img src="https://github.com/ArafatSabbir/ECommerce/blob/ef09c28397dbad083e1fb73b401c25f37c3ed31f/Demo%20of%20the%20Project/AddCatForm.png" height=300 align=center>

***2.2 Add Medicine***
<img src="https://github.com/ArafatSabbir/ECommerce/blob/ef09c28397dbad083e1fb73b401c25f37c3ed31f/Demo%20of%20the%20Project/Add%20New%20Medicine.PNG" height=300 align=center>

**3. Web** 
***3.1 Shop***
<img src="https://github.com/ArafatSabbir/ECommerce/blob/ef09c28397dbad083e1fb73b401c25f37c3ed31f/Demo%20of%20the%20Project/Medicine%20Store.PNG" height=300 align=center>

***3.2 Product Details***
<img src="https://github.com/ArafatSabbir/ECommerce/blob/ef09c28397dbad083e1fb73b401c25f37c3ed31f/Demo%20of%20the%20Project/Detatils%20of%20a%20Medicine.PNG" height=300 align=center>

***3.3 Shopping Cart***
<img src="https://github.com/ArafatSabbir/ECommerce/blob/ef09c28397dbad083e1fb73b401c25f37c3ed31f/Demo%20of%20the%20Project/Shopping%20Cart.PNG" height=300 align=center>

***3.4 Invoice***
<img src="https://github.com/ArafatSabbir/ECommerce/blob/ef09c28397dbad083e1fb73b401c25f37c3ed31f/Demo%20of%20the%20Project/Invoice.PNG" height=300 align=center>



