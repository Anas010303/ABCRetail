# ABC_Retail2

ABC_Retail2 is a web application built with **ASP.NET Core MVC** using **C#** and **.NET 8.0**. The application demonstrates a full-stack retail system integrated with **Azure Storage Services** and **Azure SQL Database**, allowing management of customers, products, media, orders, and files.

## Features

- **Customer & Product Management** using **Azure Table Storage**
- **Media Uploads** using **Azure Blob Storage**
- **Order Processing** using **Azure Queue Storage**
- **Contracts & Logs Storage** using **Azure File Share**
- **User Authentication & Roles** with **Azure SQL Database**
- **Shopping Cart Functionality** for customers
- **Admin Dashboard** for processing orders
- **Azure Functions Integration** for scalable storage and queue operations

---

## Project Structure

- `Controllers/` - MVC controllers for Customers, Products, Media, Orders, Files, Cart, and Account  
- `Models/` - Entity models for Azure Table Storage, Media, and SQL Database  
- `Services/` - Service classes for Blob, Table, Queue, and File Storage  
- `Views/` - Razor views for all pages  
- `Program.cs` - Application startup and service registration  
- `appsettings.json` - Configuration for Azure Storage and SQL connection strings  

---

## Setup Instructions

### 1. Clone the repository
```bash
git clone https://github.com/YourUsername/ABC_Retail2.git
cd ABC_Retail2
2. Configure Azure Storage
Update appsettings.json with your Azure Storage connection string:

json
Copy code
"AzureStorage": {
  "ConnectionString": "DefaultEndpointsProtocol=https;AccountName=YOUR_ACCOUNT_NAME;AccountKey=YOUR_ACCOUNT_KEY;EndpointSuffix=core.windows.net",
  "TableService_Customers": "Customers",
  "TableService_Products": "Products",
  "BlobContainer": "productimages",
  "QueueName": "processing-queue",
  "FileShare": "contracts"
}
Ensure the Blob container, File Share, and Queues exist in your Azure Storage account.

3. Configure Azure SQL Database
Create an Azure SQL Database to store user accounts, roles, and order details.

Update the connection string in appsettings.json:

json
Copy code
"ConnectionStrings": {
  "DefaultConnection": "Server=tcp:YOUR_SERVER.database.windows.net,1433;Initial Catalog=ABC_Retail2DB;Persist Security Info=False;User ID=YOUR_USER;Password=YOUR_PASSWORD;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}
4. Apply Entity Framework Migrations
bash
Copy code
dotnet ef database update
This creates the necessary tables for users, roles, orders, and order items.

5. Run the Application
bash
Copy code
dotnet run
Open your browser and navigate to https://localhost:5001.

6. Login
Use the Home Page to log in.

Admin accounts can update order statuses to Processed.

Customers can add products to their cart and confirm orders.

Azure Functions Integration
The application includes Azure Functions to enhance scalability and cloud capabilities:

Store information into Azure Tables

HTTP-triggered function to store customer and product data.

Example endpoint: POST https://<your-function-app>.azurewebsites.net/api/StoreToTable

Write to Azure Blob Storage

Upload media files via HTTP request.

Example endpoint: POST https://<your-function-app>.azurewebsites.net/api/UploadBlob

Queue Processing

Add messages to Azure Queue and read for order processing.

Example endpoint: POST https://<your-function-app>.azurewebsites.net/api/AddToQueue

Write to Azure File Share

Save contracts or log files to Azure File Storage.

Example endpoint: POST https://<your-function-app>.azurewebsites.net/api/UploadFile

Test functions using Postman or a browser by sending POST requests with JSON or form-data.

How to Use
Manage Customers & Products

Navigate to the Customers or Products page.

Add, edit, or delete records.

Manage Media Files

Navigate to Media.

Upload images, list blobs, or delete existing media.

Process Orders

Add items to cart as a customer.

Confirm order.

Admins can view orders and mark them as Processed.

Files & Logs

Navigate to Files page to upload contracts or logs.

Download or delete files as needed.

Screenshots
Add screenshots for:

Login page

Customers and Products pages

Media upload page

Files page

Azure Functions endpoints in action

Dependencies
.NET 8.0

ASP.NET Core MVC

Entity Framework Core

Azure.Storage.Blobs

Azure.Storage.Queues

Azure.Storage.Files.Shares

Azure.Data.Tables
