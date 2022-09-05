# Kuro-Desserts

This a web app that uses ASP.NET as backend, and a Blazor Server as frontend.

You can see the API documentation in the "/swagger" endpoint.

The thematic of this project is a dessert store, you can add desserts to your cart, personalize the dessert size, flavor, and add toppings.

## Docker image

You can run this project with the [docker image](https://hub.docker.com/r/kurovale/kuro-desserts) I made.

## Quick Setup

1. Run ```git clone https://github.com/kuro-vale/kuro-desserts```
2. ```cd kuro-desserts```
3. Set environment variables (Connection string of a MySQL database)
   - JWT_KEY="Secure key with length of Int32 (string of 40 characters)"
   - CONNECTION_STRING=server=your_host;user=your_user;password=your_pass;database=your_database
4. Run ```dotnet run```