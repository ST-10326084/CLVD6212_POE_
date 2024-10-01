# CLVD6212_POE: ABC Retail Online Store

## Project Overview
The CLVD6212_POE project is a web application developed for ABC Retail, designed to showcase and sell a wide range of products. Built using ASP.NET Core MVC, the application integrates with Azure Blob Storage for hosting and displaying product images. The application features an intuitive interface for product browsing, viewing previous orders, managing customer profiles, and adding products to a shopping cart.

## Features
- **Product Display**: View a curated collection of products, each with a name, description, price, and high-resolution image.
- **Azure Blob Storage Integration**: Product images are hosted on Azure Blob Storage and accessed securely using SAS (Shared Access Signature) tokens.
- **Azure File Storage, Azure Queue Storage, Azure Table Storage.**
- **Shopping Cart**: Users can add products to their cart, review the items, and proceed to checkout.
- **Previous Orders**: View a list of previous orders with details such as order ID, date, total amount, and status.
- **User Authentication**: Sign up, log in, and manage customer profiles.
- **Responsive Design**: The application is optimized for both desktop and mobile devices, ensuring a seamless experience across all platforms.
- **Product Filtering**: Easily filter products based on categories, price ranges, and other attributes. **NOT IMPLEMENTED YET**
- **Order Management**: Users can track the status of their orders and view order history.
- **Contact Form**: A simple and secure form for users to reach out to customer support.

## Technologies Used
- **ASP.NET Core MVC**: The primary framework used for building the web application.
- **Azure Blob Storage**: Used for storing and serving product images.
- **C#**: The main programming language used for backend development.
- **HTML/CSS**: For structuring and styling the web pages.
- **JavaScript**: For enhancing interactivity on the client side.
- **Bootstrap**: For responsive and modern UI design.

## Prerequisites
Before running the project, ensure you have the following installed:

- [.NET Core SDK](https://dotnet.microsoft.com/download) (version 6.0 or higher)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or another suitable IDE
- An [Azure Storage Account](https://portal.azure.com/), with a container for storing product images

## Setup Instructions

### Clone the Repository:

git clone https://github.com/yourusername/CLVD6212_POE.git

cd CLVD6212_POE


## Project Structure
- Controllers: Handles the logic for managing products, orders, and user authentication.
- Models: Contains the Product, Order, and Customer classes, which define the structure of the data.
- Views: Contains the Razor views for displaying products, previous orders, the shopping cart, and customer profiles.
- Services: Includes the AzureBlobService class for interacting with Azure Blob Storage.
- wwwroot: Contains static files such as images, CSS, and JavaScript.

## Run the Application:
- Open the project in Visual Studio.
- Build the solution to restore all dependencies.
- Press F5 to build and launch program.


## Links:

## Part 1
- Youtube: https://youtu.be/NSyIwb66F10
- Azure: https://clvd6212poe20240825.azurewebsites.net
- https://github.com/ST-10326084/CLVD6212_POE_.git

## Part 2
- GITHUB: https://github.com/ST-10326084/CLVD6212_POE_.git 
- Website: https://clvd6212poe20240825.azurewebsites.net/
- YouTube: https://youtu.be/31uZRn755ws 

