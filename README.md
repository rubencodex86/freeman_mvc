## Exercises from "Pro ASP.NET Core 6" by Adam Freeman


### Annotations


#### Good to know

- HTML, CSS
- C#

#### Must Have

- IDE (Visual Studio, Rider, VS Code),
- dotnet-sdk,
- mySQL server,
- MySQL Workbench


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


