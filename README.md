The app works with ASP.NET and react. They run separately and need to be started separately. In order to run the server you need to install Visual Studio with ASP.NET support and in order to run the client, you need to install Node JS and npm. The react app will automatically connect to the location of the ASP.NET project, which is preset to be on localhost:5178. To change that, you should change the of Server in the react app (under the name Server.js), and also change it in the ASP project in Properties\launchSettings.json. To start the project, first go to the "Tools" option in visual studio, then to NuGet, and then to console, and run update-database. Then, run the program. After that, go to the assignment 1 folder in cmd. Run the command "npm install react-scripts@latest" to download the necessary files to run the react. Run the command "npm start" and the react will start.
In the homepage, there is a log in page. If the login info is correct, it will link to the mainpage. There is also a registration part, where you can register a new account, with username, password, and nickname (username and nickname mustn't be empty and password must have at least 6 characters and a letter and a number).
In the main page there is a contact part, where you can search for contacts by name, and add new contacts by their username (not nickname).
There is also a chat part, where you can send messages to your contacts and search messages.
