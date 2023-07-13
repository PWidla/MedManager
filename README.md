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
Collection of postman requests:
