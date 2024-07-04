# Ohpen assignment 🏦👷

This REST API application was created to allow clients to process thier data in Ohpen infrastructure. Application was created with ASP.NET Framework and tests with XUnit. SOLID, KISS and DRY have been followed as much as possible.

## 🪶 Features

+ Process data. Application supports **Bulk** and **Batch** jobs, API validates inputs and returns status.

+ Get logs. API is also possible to return logs with results of a processed jobs.

+ User register and login. To save time, I've simplified the logic and did not use ASP.NET.Identity.

### ↗️ Endpoints: 
+ http://localhost:64626/api/job/startJob - starts job
    
    + Example of request body: 


        ``````json
        {
            "JobType": "bulk",
            "Items": [
                {
                    "AccountId": "a7ffbeb2-2ad8-46fe-9d28-96d1dc8b579a",
                    "DestinationAccountId": "7b8be36a-1240-4ff8-9117-a635178cfa0e",
                    "Amount": 500
                },
                {
                    "AccountId": "a7ffbeb2-2ad8-46fe-9d28-96d1dc8b579a",
                    "DestinationAccountId": "7b8be36a-1240-4ff8-9117-a635178cfa0e",
                    "Amount": 200
                }
            ]
        }
        ``````
    + Example of a response when successful:

        ``````json
        {
            "Id": "23b93997-c3d5-4e3f-b37b-bcae99ad81b0",
            "ErrorMessage": "Job 23b93997-c3d5-4e3f-b37b-bcae99ad81b0 was processed successfully"
        }     
        ``````   
    + Example of a response when failed:
        ``````json
        {
            "Id": "150ee2d4-93a9-445d-93b1-eea32a59b0d6",
            "ErrorMessage": "Account by id 1f5caa7d-5052-4543-85af-a8d7ffd65e6a has not enough money in job 150ee2d4-93a9-445d-93b1-eea32a59b0d6"
        }
        ``````

+ http://localhost:64626/api/job/jobLogs?id={jobId} - returns logs of job.


    + Example of request body: 


        ``````query
        http://localhost:64626/api/job/jobLogs?id=91feba91-a8f8-4119-94ca-cc1d9eb00ae4
        ``````
    + Example of a response:

        ``````json
        [
            {
                "ItemId": "76bc951d-0daf-4de7-a847-6ff119bcfb07",
                "Success": true,
                "Description": ""
            },
            {
                "ItemId": "70a4ccc9-d4c3-4ad1-804f-f4dbb5b27d7a",
                "Success": false,
                "Description": "Account by id 3f70f55c-348a-4773-b665-b9436043118c has not enough money in job 150ee2d4-93a9-445d-93b1-eea32a59b0d6"
            }
        ]    
        ``````   

+ http://localhost:64626/api/account/login - login
  
+ http://localhost:64626/api/account/register - registers new user
  
  + Example of a request body: 
  
    ``````json
    {
        "Username": "Daniel",
        "Password": "12345",
        "Amount": 1000
    }
    ``````

   + Example of a response when successful:

        ``````json
        "User by name Daniel was successfully registered 1f5caa7d-5052-4543-85af-a8d7ffd65e6a"   
        ``````   
    + Example of a response when failed:
        ``````json
        "User with this name already exists"
        ``````

## 🚀 How to run

1. Unzip Assignment.zip.
2. Run sql command to create necessary job types:
    ``````sql
    insert into JobTypes (Id, Name) 
    values 
        (NEWID(), 'bulk'),
        (NEWID(), 'batch');
    ``````
3. Run the application using your preferred method (e.g., Visual Studio, command-line).