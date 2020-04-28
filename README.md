# Swashbuckle.AspNetCore.SwaggerPlugin
Swashbuckle plugin to easy starting using Swagger and [ApiVersion](https://github.com/microsoft/aspnet-api-versioning) on ASP.NET Core Web Api projects

## Add Swagger to your Asp.net Core Web Api in 4 steps:

## Getting Started 

1. Add Swashbuckle.AspNetCore.SwaggerPlugin reference in your Asp.Net Core Wep Api

2. Add the SwaggerConfiguration section into your appsettings.json file.

    ```
    "SwaggerConfiguration": {
      "PageTitle": "Page Title",
      "Title": "Swagger API docs",
      "Description": "Simple sample to demonstrate how to use the Swagger plugin.",
      "ContactName": "Contact Name",
      "ContactEmail": "contact@contact.com",
      "DeprecatedMessage": "THIS VERSION WAS DEPRECATED.",
      "RoutePrefix": "apidocs",
      "JsonRoute": "apidocs/{documentName}/swagger.json",
      "Authentication": "AppIdAndApiKey"
    }
    ```
3. In Startup.cs file, add the following code inside of ConfigureServices method just after services.AddControllers();

  a.
      ```
      services.AddSwaggerService(Configuration);
      ```
    
      or

  b.    
      ```
      services.AddSwaggerService(Configuration, typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml");
      ```
    
      Second parameter in this case is the XML Documentation file name.
    
4. In Startup.cs file, modify the Configure method as following:

  a. Add new two parameters "IApiVersionDescriptionProvider provider, ISwaggerConfiguration swaggerConfig" so the method signature should look something like:
      ```
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, ISwaggerConfiguration swaggerConfig)      
      ```
  b. Add the following code before app.UseEndpoints();
      ```
      app.AddUseSwagger(provider, swaggerConfig);      
      ```
  
 ## Optional 
 
 5. In case you decide to use the step 3.b, you will required to insert the following code inside of your .csproj file:
 
 ```
 <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
    <DocumentationFile>bin\$(Configuration)\$(TargetFramework)\$(MSBuildThisFileName).xml</DocumentationFile>
  </PropertyGroup>
  ```
  
  ---
  
  After doing the above steps, you should be able to see the documentation at: [http://localhost:port/apidocs](http://localhost:port/apidocs)
