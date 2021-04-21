# Language Adventures by Moths
Language Adventures is a story-based scavenger-hunt game system that is comprised of a mobile application and a web application.

## Mobile Application
Users of the mobile application are the players, and will be presented with snippets of a story. The players will form teams and need to move around in the real world to reach special story-related locations which will unlock the next part of the story. Players are presented with challenges at specific story-points in the adventure. The Language Adventures mobile app has the capability to house many different stories.

## Website Application
Users of the website applcation are the game masters. The game masters monitor and assist the players (users of the mobile application). The webstie application allows the game master to see location of any of the currently playing teams and send messages to them. The website will also control the creation of teams and stories. 

# Repository Contents
MobileSourceCode contains a visual studio project that implements the mobile application. Written in C# and using the Xamarin platform, specifically Xamarin forms to allow cross-platform. 

GameController conatins a visual studio project that implements the website application and the Web API. The Web API facilitates communication between the database and applications. The database is stored in the GameController folder. Both Web API and website are written in C#. The Web API uses the ASP.NET framework. The Website application uses the .NET core framework.   
