# DotNetCoreJwt

Contains simple JWT implementation, with MiddleWare to validate the caller

## Getting Started


Use the Index.html file in wwwroot folder to test JWT.<br>
GetToken fetches a token from the Token Controller.<br>
T test the JWT tken call the Values Controller wich contains the Authorise attribute<br>


### Prerequisites

Visual Studio 2017, DOT NET CORE 2 SDK

### Installing

Clone the repository, open the solution file DotNetCoreJwt
Nuget will restore the packages automatically

### Troubleshooting
There is built in error handling Middleware this traps error in the delegate next, its purpose is to help find any errors in middleware<br>
See AuthorizeMiddleWare.cs, also check tne comments in the startup to place it correctly in the pipeline

### Logging

Logging standard dot net core logging is registerd in Program.cs see Token controller for an instance of logger

### Documentation
Uese Swagger, go to api/swagger for API doc's

## Running the tests

The project uses XUNIT, testrunner is included.

### Break down into end to end tests

TODO


### And coding style tests

TODO

## Deployment

TODO

## Built With

* [DOT NET CORE2](https://www.microsoft.com/net/learn/get-started/windows) - The web framework used


## Contributing

Feel Free to do a pull request

## Versioning

## Authors

* **Sajid Rashid** - *Initial work* 

See also the list of [contributors](https://github.com/sajrashid/DotNetCoreJwt/graphs/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Hat tip to anyone who's code was used


