Web API 2.2 OWIN Microservice Boilerplate 
==========

### Introduction
Ready to build a microservice on .NET and want to get started fast? Use this project template 
to bootstrap the new application and start implementing your RESTful webservice without hassle.

The solution does not rely on a webserver such as IIS or Apache because it is based on the OWIN 
middleware for self hosting. Because the project uses [Topshelf](http://topshelf-project.com), 
the service can be launched as a console application and it can be installed as a Windows service. 
The project template features an example implementation of a RESTful web service including support for 
GET, POST, PUT and DELETE methods.

The solution also provides out of the box support for logging with Log4Net and JSON configuration 
with JsonConfig.

### Running the application
Start the application from Visual Studio. The get all operation of the TestRestController will be 
opened with the default URL and port at http://localhost:9013/api/testrest in the browser.

Supported operations:  
  
Operation | Description  
--------- | -------------  
GET /api/testrest | Get all values  
GET /api/testrest/1 | Get value with id 1  
POST /api/testrest { "value":"My new value" } | Create new value, the service returns the URL of the new resource as { "url":"http://localhost:9013/api/testrest/newid" }  
PUT /api/testrest/1 { "value":"My updated value" } | Update value with id 1  
DELETE /api/testrest/1 | Delete value with id 1  

### Logging and Exception handling
The application is pre configured to log debug, info, warnings and errors with Log4Net. All incoming web api requests 
are logged with the DEBUG level. All unhandled exceptions are logged with the ERROR level by the 
Structura.WebApiOwinBoilerPlate.WebService.WebApiInstrumentation.ExceptionLogger action filter and a global exception 
handler in Structura.WebApiOwinBoilerPlate.WebService.Program. 

Example of adding DEBUG level message to the log:

```
Structura.Shared.Utilities.FormatLoggerAccessor.Locate().Debug("My log message.");
```

### Configuration
The service is configured with the settings defined in the file configuration.json. The supported settings for 
are BaseUrl and Port. The configuration data is retrieved by 
Structura.WebApiOwinBoilerPlate.WebService.JsonConfiguration.JsonConfigAccessor using the [JsonConfig](https://github.com/Dynalon/JsonConfig) library.

### Install as a Windows service
In the method Structura.WebApiOwinBoilerPlate.WebService.Program.Main, Topshelf is configured with 
Windows service properties and it is executed. To install the application as a Windows service, run 
the following command in a command prompt:

```
Structura.WebApiOwinBoilerPlate.WebService.exe install
```

The service will be installed with service name WebApiOwinBoilerplateService. The service can be removed 
with the command:

```
Structura.WebApiOwinBoilerPlate.WebService.exe uninstall
```

Maintained by Alexander van Trijffel of [Software Development Consultancy company Structura](http://structura.ws)

