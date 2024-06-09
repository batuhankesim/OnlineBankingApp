# Online Banking App


## Features

- This application allows a new customer to register in the banking system via an API.
- Generates a unique key by entering the registered customer information correctly.
- In this way, the customer can perform basic banking transactions.
- These transactions are: Opening an Account, Depositing Money, Withdrawing Money, Viewing Account Information and Viewing Account Balance.

## Guides
- Swagger documentation will help you use the API.

## Layers

The layers included in the project are given in the table below.

| Project  | Type | Feature|
| ------------- | ------------- | -------------|
| OnlineBankingApp  | Main API Project  | It is a project that contains API endpoints consumed by external applications.
| OnlineBankingApp.Common  | Common  | Contains DTO and Interface elements commonly used in the project
| OnlineBankingApp.Service  | Service  | Contains the service layer where business rules are implemented.
| OnlineBankingApp.Entity  | Database  | It is the project where database operations are implemented.
