## Exercises from "Pro ASP.NET Core 6" by Adam Freeman


### **Annotations**

### <u>Chapter 2 | Getting Started</u>

#### Good to know

- HTML, CSS
- C#
#### Must Have

- IDE (Visual Studio, Rider, VS Code),
- dotnet-sdk,
- mySQL server,
- MySQL Workbench

**My settings**
- Rider
- 9.0.100-rc.2.24474.11

#### Creating a New Project

Listing the Installed SDKs
```bash
dotnet --list-sdks
```

Starting a  new project
```bash
$ dotnet new globaljson --sdk-version <sdk-version> --output <ProjectName>
$ dotnet new mvc --no-https --output <ProjectName> --framework <netX.0>
$ dotnet new sln -o <ProjectName>
$ dotnet sln <ProjectName> add <ProjectName>
```

Note: I'm not sure about the --no-https parameter.
==TODO==: How to add https later

##### Setting the HTTP Port in the launchSettings.json File in the Properties Folder

> When the project is created, a file named launchSettings.json is created in the Properties folder, and it is this file that determines which HTTP port ASP.NET Core will use to listen for HTTP requests.

```json
{
	"iisSettings": {
	    "windowsAuthentication": false,
	    "anonymousAuthentication": true,
	    "iisExpress": {
			"applicationUrl": "http://localhost:5000",
		    "sslPort": 0
	    }
	},
	"profiles": {
		"FirstProject": {
			"commandName": "Project",
			"dotnetRunMessages": true,
			"launchBrowser": true,
			"applicationUrl": "http://localhost:5000",
			"environmentVariables": {
		        "ASPNETCORE_ENVIRONMENT": "Development"
		    }
	    },
	    "IIS Express": {
		    "commandName": "IISExpress",
		    "launchBrowser": true,
		    "environmentVariables": {
		        "ASPNETCORE_ENVIRONMENT": "Development"
		    }
		} 
	}
}
```

<u>Note 1</u>: I have changed profile name "FirstProject" to "http". I have used 8080 instead of 5000.

==TODO== Check [Youtube](https://youtu.be/AopeJjkcRvU?si=a_-5jz8tf-OucuAG&t=1387)

<u>Note 2</u>: When I have used "FirstProject" and door 5000 I was not able to connect and start the project.

<u>Note 3</u>: The problem was with port 5000, not the profile name.

Starting the Application
```bash
dotnet run
```

> In an ASP.NET Core application, incoming requests are handled by endpoints. The endpoint that produced the response ~~in Figure 2-14~~ is an action, which is a method that is written in C#. An action is defined in a controller, which is a C# class that is derived from the Microsoft.AspNetCore.Mvc.Controller class, the built-in controller base class. 
>
> Each public method defined by a controller is an action, which means you can invoke the action method to handle an HTTP request.

##### Changing the HomeController.cs file

```cs
using Microsoft.AspNetCore.Mvc;  
  
namespace FirstProject.Controllers;  
  
public class HomeController : Controller  
{  
    public string Index()  
    {  
        return "Hello World!";  
    }  
}
```

> I have changed the method named Index so that it returns the string Hello World.

##### Routes

Any of the following URLs will be dispatched to the Index action defined by the Home controller:

- /
- /Home
- /Home/Index

> The output from the previous example wasn’t HTML—it was just the string Hello World. To produce an HTML response to a browser request, I need a view, which tells ASP.NET Core how to process the result produced by the Index method into an HTML response that can be sent to the browser.

##### Rendering a View in the HomeController.cs File in the Controllers Folder

```cs
using Microsoft.AspNetCore.Mvc;  
  
namespace FirstProject.Controllers;  
  
public class HomeController : Controller  
{  
    public ViewResult Index() => View("MyView");  
}
```

Inside Views/Home create a Razor View called MyView.cshtml

```cshtml
@{  
    Layout = null;  
}  
  
<!doctype html>  
<html lang="en">  
<head>  
    <meta name="viewport"  
          content="width=device-width" />  
    <title>Index</title>  
</head>  
<body>  
    <div>  
        Hello World (fromt the view)  
    </div>  
</body>  
</html>
```


> Standard naming convention. Put view files in a folder whose name matched the controller that contains the action method. In this case, this meant putting the view file in the Views/Home folder, since the action method is defined by the Home controller.

##### Adding Dynamic Output

HomeController.cs
```cs
using Microsoft.AspNetCore.Mvc;  
  
namespace FirstProject.Controllers;  
  
public class HomeController : Controller  
{  
    public ViewResult Index()  
    {  
        int hour = DateTime.Now.Hour;  
        string viewModel = hour < 12 ? "Good Morning" : "Good Afternoon";  
        return View("MyView", viewModel);  
    }  
}
```

MyView.cshtml
```cshtml
@model string  
@{  
    Layout = null;  
}  
<!doctype html>  
<html lang="en">  
<head>  
    <meta name="viewport"  
          content="width=device-width" />  
    <title>Index</title>  
</head>  
<body>  
    <div>  
        @Model World (fromt the view)  
    </div>  
</body>  
</html>
```

##### Summary

> It is a simple result, but this example reveals all the building blocks you need to create a simple ASP.NET Core web application and to generate a dynamic response. The ASP.NET Core platform receives an HTTP request and uses the routing system to match the request URL to an endpoint. The endpoint, in this case, is the Index action method defined by the Home controller. The method is invoked and produces a ViewResult object that contains the name of a view and a view model object. The Razor view engine locates and processes the view, evaluating the @Model expression to insert the data provided by the action method into the response, which is returned to the browser and displayed to the user. There are, of course, many other features available, but this is the essence of ASP.NET Core, and it is worth bearing this simple sequence in mind as you read the rest of the book.

