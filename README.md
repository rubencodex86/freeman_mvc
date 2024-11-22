## Exercises from "Pro ASP.NET Core 6" by Adam Freeman


### Annotations

### <u>Chapter 2 | Getting Started</u>

##### Good to know

- HTML, CSS
- C#
##### Must Have

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
==TODO==: HowTo: add https (later). [Youtube](https://youtu.be/AopeJjkcRvU?si=a_-5jz8tf-OucuAG&t=1387)

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

<u>Note 2</u>: When I have used "FirstProject" and door 5000 I was not able to connect and start the project.

<u>Note 3</u>: The problem was with port 5000, not the profile name.

==TODO== ~~Fix connection problem.~~
I was able to debugged the issue, myself.

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

```html
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
```html
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


### <u>Chapter 3 | First ASP.NET Core Application</u>

#### History

> Imagine that a friend has decided to host a New Year’s Eve party and that she has asked me to create a web app that allows her invitees to electronically RSVP. She has asked for these four key features:
>
> - A home page that shows information about the party  
> - A form that can be used to RSVP  
> - Validation for the RSVP form, which will display a thank-you page
> - A summary page that shows who is coming to the party

##### Step 1: Create new project.
```bash
$ dotnet new globaljson --sdk-version 9.0.100-rc.2.24474.11 --output PartyInvites
$ dotnet new mvc --no-https --output PartyInvites --framework net9.0
$ dotnet new sln -o PartyInvites
$ dotnet sln PartyInvites add PartyInvites
```

##### Step 2: Setting ports in the launchSettings.json
```json
{  
  "$schema": "https://json.schemastore.org/launchsettings.json",  
  "iisSettings": {  
    "windowsAuthentication": false,  
    "anonymousAuthentication": true,  
    "iisExpress": {  
      "applicationUrl": "http://localhost:8080",  
      "sslPort": 0  
    }  
  },  
  "profiles": {  
    "PartyInvites": {  
      "commandName": "Project",  
      "dotnetRunMessages": true,  
      "launchBrowser": true,  
      "applicationUrl": "http://localhost:8080",  
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

##### Step 3: Replacing the contents of the HomeController.cs
```cs
using Microsoft.AspNetCore.Mvc;  
  
namespace PartyInvites.Controllers;  
  
public class HomeController : Controller  
{  
    public IActionResult Index() => View();  
}
```

##### Step 4: Replacing the contents of the Index.cshtml
```html
@{  
    Layout = null;  
}  
  
<!doctype html>  
<html lang="en">  
<head>  
    <meta name="viewport"  
          content="width=device-width" />  
    <title>Party!</title>  
</head>  
<body>  
    <div>  
        <div>  
            We're going to have an exciting party. <br/>  
            (To do: sell it better. Add pictures or something.)  
        </div>  
    </div>  
</body>  
</html>
```

```bash
dotnet watch
```

##### Step 5: Adding a Data Model

Create a new class inside Models.

GuestResponse.cs
```cs
namespace PartyInvites.Models;  
  
public class GuestResponse  
{  
    public string? Name { get; set; }  
    public string? Email { get; set; }  
    public string? Phone { get; set; }  
    public bool? WillAttend { get; set; }  
}
```

##### Step 6: Creating 2nd Action and View

Adding an Action Method in HomeController.cs
```cs
public class HomeController : Controller  
{  
    public IActionResult Index() => View();  
    public ViewResult RsvpForm() => View();  
}
```

Create /Home/View RsvpForm.cshtml
```html
@{  
    Layout = null;  
}  
  
<!DOCTYPE html>  
  
<!doctype html>  
<html lang="en">  
<head>  
    <meta name="viewport"  
          content="width=device-width" />  
    <title>RsvpForm</title>  
</head>  
<body>  
    <div>  
        This is the RsvpForm.cshtml View  
    </div>  
</body>  
</html>
```

##### Step 7: Linking Action Methods (HowTo: Create a link)

```html
@{  
    Layout = null;  
}  
  
<!doctype html>  
<html lang="en">  
<head>  
    <meta name="viewport"  
          content="width=device-width" />  
    <title>Party!</title>  
</head>  
<body>  
    <div>  
        <div>  
            We're going to have an exciting party. <br/>  
            (To do: sell it better. Add pictures or something.)  
        </div>
          
        <a asp-action="RsvpForm">RSVP Now</a> 
         
    </div>  
</body>  
</html>
```

##### Step 8: Building the Form

RsvpForm.cshtml
```html
@model PartyInvites.Models.GuestResponse  
@{  
    Layout = null;  
}  
  
<!doctype html>  
<html lang="en">  
    <head>  
        <meta name="viewport"  
              content="width=device-width">  
        <title>RsvpForm</title>  
    </head>  
    <body>  
    <form asp-action="RsvpForm" method="post">  
        <div>  
            <label asp-for="Name">Your Name:</label>  
            <input asp-for="Name">  
        </div>  
        <div>  
            <label asp-for="Email">Your email:</label>  
            <input asp-for="Email">  
        </div>  
        <div>  
            <label asp-for="Phone">Your phone:</label>  
            <input asp-for="Phone">  
        </div>  
        <div>  
            <label asp-for="WillAttend">Will you attend?</label>  
            <select asp-for="WillAttend">  
                <option value="">Choose an option</option>  
                <option value="true">Yes, I'll be there</option>  
                <option value="false">No, I can't come</option>  
            </select>  
        </div>  
        <button type="submit">Submit RSVP</button>  
    </form>  
    <a asp-action="Index">Back Now</a>  
    </body>  
</html>
```

##### Step 9: Receiving Form Data

> I have not yet told ASP.NET Core what I want to do when the form is posted to the server. As things stand, clicking the Submit RSVP button just clears any values you have entered in the form. That is because the form posts back to the RsvpForm action method in the Home controller, which just renders the view again. To receive and process submitted form data, I am going to use an important feature of controllers. I will add a second RsvpForm action method to create the following:
> 
> - A method that responds to HTTP GET requests: A GET request is what a browser issues normally each time someone clicks a link. This version of the action will be responsible for displaying the initial blank form when someone first visits /Home/RsvpForm.
> - A method that responds to HTTP POST requests: The form element defined in Listing 3-10 sets the method attribute to post, which causes the form data to be sent to the server as a POST request. This version of the action will be responsible for receiving submitted data and deciding what to do with it.

HomeController.cs
```cs
using Microsoft.AspNetCore.Mvc;  
using PartyInvites.Models;  
  
namespace PartyInvites.Controllers;  
  
public class HomeController : Controller  
{  
    public IActionResult Index() => View();  
      
    [HttpGet]  
    public ViewResult RsvpForm() => View();  
      
    [HttpPost]  
    public ViewResult RsvpForm(GuestResponse guestResponse) => View(); 
	    // TODO: store response from guest  
}
```

> I have added the HttpGet attribute to the existing RsvpForm action method, which declares that this method should be used only for GET requests. I then added an overloaded version of the RsvpForm method, which accepts a GuestResponse object. I applied the HttpPost attribute to this method, which declares it will deal with POST requests. I explain how these additions to the listing work in the following sections. I also imported the PartyInvites.Models namespace—this is just so I can refer to the GuestResponse model type without needing to qualify the class name.

