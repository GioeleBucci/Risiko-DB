<p align="center">
  <img src="https://imgur.com/saJegA8.png" width=400>
</p>

## About
An application that provides a user interface for managing and storing information related to
the popular strategy board game [Risk](https://en.wikipedia.org/wiki/Risk_(game)). \
This was created as a project for the Databases course (2023/24).

## Installing

### Setting up the database
Ensure you have a MySQL server running on your machine.
Load the content of the `RisiKoDB.sql` dump file to create and populate the database.
> Note: **Do not change any database configuration parameters**. 
> The application is pre-configured to connect to the database using the following connection string: \
> `"server=localhost ; database=RisiKoDB; user=root ; password= ; charset=utf8 ; SslMode=None;"` \
> If you want to change any of these parameters you can edit the connection string [here](Assets/Scripts/utils/SqlUtils.cs).
### Running the application
You can find the executable file in the project's releases tab. After setting up the database, if you did not change any parameter, you can launch the application without further setup. \
If you changed any parameters you will need to rebuild the project from scratch using Unity (*version should be >= 2022.3.20f1*).
Clone the repo, open it with Unity and modify the connection string.

## Screenshots

<p align="center">
  <img src="https://imgur.com/uhh7out.png" width=400>
</p>

<p align="center">
  <img src="https://imgur.com/tRFmCdi.png" width=400>
</p>
