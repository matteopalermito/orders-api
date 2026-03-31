# orders-api

**Tech**: .NET 8, C#, EF Core (SQLite), Swagger/OpenAPI, Docker, Kubernetes manifests, GitHub Actions, SonarQube (template).

## Run locally
```bash
dotnet run --project src/Orders.Api/Orders.Api.csproj
# Swagger UI: http://localhost:5195/swagger
```

## Docker
```bash
docker build -t orders-api:local .
docker run -p 8080:8080 orders-api:local
# Swagger: http://localhost:8080/swagger
```

## Kubernetes
```bash
kubectl apply -f k8s/deployment.yml
kubectl apply -f k8s/service.yml
```


