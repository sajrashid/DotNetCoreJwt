# DotNetCoreJwt
## Contains simple JWT implementation 
Simple JWT example </br>
Get token from token controller /api/token CreateToken method uses post from body username / password </br>
Test Authorise on api/vlaues controller (sends bearer header + token) </br>
Test page /index.html </br>
Contains swagger http://localhost:57425/swagger/ </br>
Test Post Token from swagger paste into parameters  { "username": "mario", "password": "secret" }</br>
</br>
NB: update the ports in the wwwroot main js file  </br>


# DotNetCoreJwt

ontains simple JWT implementation, with MiddleWare to validate the caller

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.<br>
Use the Index.html file in wwwroot folder to test JWT.
GetToken fetches a token from the Token Controller
Test Token calls the Values Controller wich contains the Authorise attribute

### Prerequisites

Visual Studio 2017, DOT NET CORE 2 SDK

### Installing

Clone the repository, open the solution file DotNetCoreJwt
Nuget will restore the packages automatically

## Running the tests

Explain how to run the automated tests for this system

### Break down into end to end tests

Explain what these tests test and why

```
Give an example
```

### And coding style tests

Explain what these tests test and why

```
Give an example
```

## Deployment

Add additional notes about how to deploy this on a live system

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
* Inspiration
* etc

