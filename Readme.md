#### Mysql Table configuration:
![alt text](https://github.com/TogetherWeWrite/AuthenticationService/blob/master/AuthenticationTableLayout.png)

#### mysql docker-compose.yml
![alt text](https://github.com/TogetherWeWrite/AuthenticationService/blob/master/MySqlDockerCompose.png)
##### reason
You will need to have a mysql service running you can do this easily with the help of docker, docker-compose this is the file you can easily copy. the settings that are highlighted are important to your appsettings.json of your project.
#### Corresponding data in appsettings.json
![alt text](https://github.com/TogetherWeWrite/AuthenticationService/blob/master/AppsettingjsonConnectionstring.png)

##### reason 
This is to indicate what certain variables mean in comparison the the docker-compose.yml file

#### Startup.cs ConfigureServices settings
This project uses Independecy injection with a repository pattern. This can be found in Startup.Configureservice(IServiceCollection).

##### database injection
the lines in the region database injection are used to make sure the database works with the settings that are in the appsettings.json

##### repository injection
This region is used for the injection of the repository to certain interfaces

##### Services injection
This region is used for the injection of the services to certain interfaces
