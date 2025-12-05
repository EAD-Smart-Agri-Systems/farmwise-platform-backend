# Farmwise Platform Backend

![.NET](https://img.shields.io/badge/.NET-9.0-blue)
![Architecture](https://img.shields.io/badge/Architecture-DDD%20Modular%20Monolith-brightgreen)
![Status](https://img.shields.io/badge/Status-In%20Development-yellow)

A modular DDD-based backend for the Smart Agriculture System. This backend manages farm data, crop records, advisory recommendations, and notifications using clearly defined Bounded Contexts and layered architecture. Designed for future transition into microservices with an event-driven backbone.

---

## 🚜 Core Features (Phase 1 - Modular Monolith)
- Domain-Driven Design with separate contexts  
- Farm and crop lifecycle management  
- Advisory recommendation workflow  
- Notification triggering  
- Secure authentication/authorization (Keycloak)  
- Domain events for eventual consistency  

---

## 🏛 Architecture Overview
This solution follows:

- **Strategic DDD**
  - Core, Supporting, Generic subdomains  
  - Bounded Context separation  

- **Tactical DDD**
  - Entities, Value Objects  
  - Aggregate Roots  
  - Domain Services  
  - Repositories  
  - Domain Events  

Folder structure (initial phase):

/src  
/FarmContext  
/CropContext  
/AdvisoryContext  
/NotificationContext  
/tests  
/docs  


---

## 🔧 Tech Stack
| Category | Technology |
|---|---|
| Language | C# (.NET 9) |
| Architecture | DDD + Clean Architecture |
| Authentication | Keycloak |
| Messaging | RabbitMQ |
| Persistence | EF Core (Infra Layer only) |
| AI/ML | Recommendation Model placeholder |

---

## 🚀 Running the Application
### Prerequisites
✔ .NET SDK 9+  
✔ Docker installed  
✔ PostgreSQL OR SQL Server  
✔ RabbitMQ  
✔ Keycloak configured  

### Setup Steps
```bash
git clone https://github.com/<org>/farmwise-platform-backend
cd farmwise-platform-backend
dotnet restore
dotnet build
dotnet run --project src/Farm.API/Farm.API.csproj

