# Kuro-Desserts

[![PWD](https://raw.githubusercontent.com/play-with-docker/stacks/master/assets/images/button.png)](https://labs.play-with-docker.com/?stack=https://raw.githubusercontent.com/kuro-vale/kuro-desserts/main/pwd-stack.yml)

This a e-commerce web app that uses ASP.NET as backend, and a Blazor Server as frontend.

You can see the API documentation in the "/swagger" endpoint.

The thematic of this project is a dessert store, you can add desserts to your cart, personalize the dessert size, flavor, and add toppings.

## Screenshots

#### Desserts
![Screenshot_20230104_201109](https://user-images.githubusercontent.com/87244716/210679265-21b2928e-da9d-4a43-bf5d-748560130037.png)
#### Profile
![Screenshot_20230104_201156](https://user-images.githubusercontent.com/87244716/210679342-0a229c65-d898-43f6-8a84-ebc34fda29ba.png)
#### Adding items to cart
![Screenshot_20230104_201229](https://user-images.githubusercontent.com/87244716/210679400-ce26c573-7008-440e-b51a-8e70a3854f2b.png)
#### Checkout
![Screenshot_20230104_201346](https://user-images.githubusercontent.com/87244716/210679551-be62b3e0-af45-4b1f-8593-e0d03fc135db.png)
#### Swagger docs
![Screenshot_20230104_201557](https://user-images.githubusercontent.com/87244716/210679737-f50c1d0b-9453-47fd-abea-800cec116b33.png)



## Deploy

Follow any of these methods and open http://localhost:5000/ to see the WebApp or http://localhost:5000/swagger to see the API docs in Swagger.

### Docker

Run the command below to quickly deploy this project on your machine, see the [docker image](https://hub.docker.com/r/kurovale/kuro-desserts) for more info.

```bash
docker run -d -p 5000:5000 kurovale/kuro-desserts:sqlite
```

### Quick Setup

1. Run ```git clone https://github.com/kuro-vale/kuro-desserts```
2. ```cd kuro-desserts```
3. Set environment variables (Connection string of a MySQL database)
   - JWT_KEY="Secure key with length of Int32 (string of 40 characters)"
   - CONNECTION_STRING=server=your_host;user=your_user;password=your_pass;database=your_database
4. Run ```dotnet run```
