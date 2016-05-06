# Software-development-and-process-management CA1

This project  is a car selling system that will allow car buyers to browse cars and car dealers to add update and delete cars for sale. The system maintains data in a no-SQL database and provides access to the data to client applications via a RESTful web service. Car Dealers will have the functionality to log in and upload cars for sale including functionality to upload images of the cars. Additional functionality will allow car dealers to schedule price reduction sales.
GOALS 
*	Console application for customer and car dealers to manipulate cars
*	Ability to store dealer and car details.  
*	Ability to store car images
*	Ability to schedule price reduction sales on cars
*	Ability for App administrators to monitor activity. 

#ARCHITECTURE 
A RESTful web service deployed to Azure, implemented in ASP.Net Web API provides CRUD operations with respect to Car data. Data will be stored in Azure Table storage containing two entity types storing details about car dealer and cars. Car Dealers will have the functionality to upload cars for sale including functionality to upload images of the cars. These images will be stored on Azure blob storage with a reference to the images stored in Azure Table storage. 
A scheduled job using Azure scheduler will implement a PUT request that hits the Cars RESTful web service to allow car dealers to schedule price reduction sales using Azure scheduler.
A simple console application is used to call the RESTFul web service and perform HTTP requests.
