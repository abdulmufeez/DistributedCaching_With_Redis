# Distributed Caching Using Redis

In this project, Redis is utilized for distributed caching through the implementation of the IDistributedCache extension class, which provides asynchronous CRUD (Create, Read, Update, Delete) methods. The Redis instance is run as a separate Docker container, with the Redis port mapped to an external port for access from the application.

An additional static extension class has also been created to allow for reading values from the appsettings.json file without the use of dependency injection. This extension class can be used in any static class or static method, making it a versatile and convenient tool.

## Getting Started

To run the Redis instance as a Docker container, make sure that you have Docker installed and running on your system. Then, run the following command to start the Redis container:

docker run --name [container name] -p [host port]:6379 -d redis

Replace `[host port]` with the desired external port to map to the Redis container and `[container name]` with a name for the container.

## Using the Extension Class

The static extension class can be used in any part of the project by simply calling the relevant methods. No additional setup or dependency injection is required.

For example, to read a value from the appsettings.json file, you can use the following code:

var value = AppSettingsExtension.GetValue<T>("key");

Where "key" is the name of the desired value in the appsettings.json file.

## Conclusion

By using Redis for distributed caching and the static extension class for reading values from the appsettings.json file, you can simplify your code and ensure efficient, flexible caching and configuration management in your project.
