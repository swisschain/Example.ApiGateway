# Example.ApiGateway

This is example of implementation api gateway solution with Ocelot gateway

![solution](https://github.com/swisschain/Example.ApiGateway/blob/master/Docs/Api%20Gateway%20Solution.jpg)

Start all 3 service from solution and test work by next calls:

**Create session**

```
curl -X POST "http://localhost:5002/api/Auth/Auth" -H "accept: */*" -H "Content-Type: application/json" -d "{\"name\":\"alex\",\"password\":\"12345\"}"
```

**Get request with cashe and rate limit**

```
curl -X GET "http://localhost:5002/api/service/WeatherForecast" -H "accept: text/plain" -H "Authorization: Bearer <PUT HERE SESSUIN TOKEN>"
```

**Get request rate limit**

```
curl -X GET "http://localhost:5002/api/service/WeatherForecast/id" -H "accept: text/plain" -H "Authorization: Bearer <PUT HERE SESSUIN TOKEN>"
```

Configuration of gateway keep in json format: [ocelot.json](https://github.com/swisschain/Example.ApiGateway/blob/master/Gateway/ocelot.json)
