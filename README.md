# GladiatorsAPI
Welcome to GladiatorsAPI! This API allows you to create powerful gladiator characters and unleash their skills in thrilling single-player battles. Take control of your gladiators, customize their attributes, and witness epic clashes between mighty warriors.

# Getting Started
To get started with GladiatorsAPI, follow the steps below:

Clone the repository: git clone [https://github.com/your-username/gladiators-api.git](https://github.com/MikheiliChkhvirkia/GladiatorApi.git)
Navigate to the project directory: cd gladiators-api
Install the required dependencies: dotnet restore
Build the project: dotnet build
Run the application: dotnet run

# API Endpoints
The following are the available endpoints for interacting with the GladiatorsAPI:

GET /Gladiator/Character-Classes: Retrieve a list of all character classes.
POST /Gladiator/Fight-Characters: Create a two new characters.

# Creating a Gladiator Character
To create a new gladiator character, make a POST request to the /Gladiator/Fight-Characters endpoint with the necessary details. The API expects a JSON payload with the following properties:

json

{
  "characterOne": {
    "name": "Mishiko",
    "strength": 8,
    "vigor": 3,
    "intelligence": 5,
    "dexterity": 2,
    "crit": 1,
    "armor": 5,
    "class": 1
  },
  "characterTwo": {
    "name": "Beqa",
    "strength": 8,
    "vigor": 3,
    "intelligence": 5,
    "dexterity": 2,
    "crit": 1,
    "armor": 5,
    "class": 1
  }
}

The name field represents the character's name, while class determines the character's class or specialization (e.g., Warrior, Mage, Archer). The health, strength, agility, and intelligence properties define the character's attributes and can be adjusted based on your preferences.

# Battling Gladiators
Once you have created gladiator characters, battle will start in console app and follow the instructions.

# Characters
Every class has their own unique skills, which are randomly selected for each match using a Pseudo-Random algorithm.

If you have any questions or need further assistance, please don't hesitate to reach out to us. Enjoy the battles!
