# MinimalAPI

This is a minimal API that can GET and POST to a local database which stores persons, their interests and URL links associated with a person and its interest

## Requirements
- Microsoft Visual Studio
- Microsoft SQL Server
- SQL Server Management Studio
- Insomnia

## Installation

1. Clone the repo https://github.com/mlvestlund/MinimalAPI.git
2. Create a database and change the connections string "MinimalDbContext" in appsettings.json to your own
3. Update-Database in Package Manager Console

## Usage
Start program in VS, copy URL with domain and port number from the console and copy it to Insomnia. Below is each possible path to add to your URL described, including how to use them and what they do.

### Fetch all persons in database
HTTP method: GET  
Path: "/persons"  
Example: GET https://localhost:1234/persons  

### Fetch all intrests for a chosen person
HTTP method: GET  
Path: "/interests/{PersonID}"  
Example: GET https://localhost:1234/interests/1  

### Fetch all links for a chosen person
HTTP method: GET  
Path: "/links/{PersonID}"  
Example: GET https://localhost:1234/links/1  

### Creates new interests and links it to a chosen person
HTTP method: POST  
Path: "/interest"  
JSON format:  
{  
	"PersonID": {PersonID},  
 	"Interest": {  
  		"InterestName": "",  
    		"InterestDescription": ""  
	}  
 }  
 Example: POST https://localhost:1234/interest/1  
{  
	"PersonID": 1,  
 	"Interest": {  
  		"InterestName": "Fishing",  
    		"InterestDescription": "Tricking fishes to bite hooks"  
	}  
 }  

### Links a chosen person to a chosen interest
HTTP method: POST  
Path: "/personinterest"  
JSON format:  
{  
	"PersonID": {PersonID},  
 	"InterestID": {InterestID}  
}  
Example: POST https://localhost:1234/personinterest  
{  
	"PersonID": 1,  
 	"InterestID": 2  
}  

### Add new URL links associated with a person and interest
HTTP method: POST  
Path: "/link"  
JSON format:  
{  
	"LinkURL": "",  
 	"PersonID": {PersonID},  
  	"InterestID": {InterestID}  
}  
Example: POST https://localhost:1234/link  
{  
	"LinkURL": "www.fishing.com",  
 	"PersonID": 1,  
  	"InterestID": 1  
}

### ER Diagram
![Image of ER Diagram for MinimalAPI DB](ER_diagram.png)

### Sequence Diagram
![Image of Sequence Diagram for MinimalAPI](Sequence_diagram.png)
