# MedManager

This is a system built using .NET 6. It allows users to manage users (patients, doctors and admins), notices, prescriptions and appointments). The application uses MS SQL Database.
Authentication is done by using a bearer token.

## Requirements

- .NET 6
- SQL Server Management Studio Management Studio 19

## Installation

1. Clone the repository: `git clone https://github.com/PWidla/MedManager.git`

2. In Package Manager Console initialize the database by using command: 
- Update-Database

3. Set all microservices to run together
4. 
  ![image](https://github.com/PWidla/MedManager/assets/89644623/7c663656-21bf-4978-ac25-851ed4458e07)
  ![image](https://github.com/PWidla/MedManager/assets/89644623/65be97fe-7d26-4832-a05d-eebd275fc4c7)

5. Run the application by clicking
   ![image](https://github.com/PWidla/MedManager/assets/89644623/76f5cc6f-192e-4a8e-b499-bbb841a7e352)


## Configuration

- Connection string: `Server=localhost;Database=onionDb;Trusted_Connection=True;`
- Admin user credentials: `EmailAddress: admin@example.com`, `Password: admin123!`

## User Guide

After running the application, users can work with API for instance via Postman. 
Collection of postman requests is stored in MedManager.postman_collection.json file.
You can import it by clicking Import button 

![image](https://github.com/PWidla/MedManager/assets/89644623/8ae75c7f-323c-4718-bf09-5f06904d8d58)
and for instance dragging there the MedManager.postman_collection.json file.

![image](https://github.com/PWidla/MedManager/assets/89644623/3ad7cff6-f5de-47ad-bc0b-229f0b51e91f)
![image](https://github.com/PWidla/MedManager/assets/89644623/d8d282c0-8294-4b15-9a4c-14e2f66998ed)

Collections looks like below

![image](https://github.com/PWidla/MedManager/assets/89644623/384eeb5e-a2eb-43a4-9480-f51b703fcf12)
Via auth request you may receive bearer token for tested user.

