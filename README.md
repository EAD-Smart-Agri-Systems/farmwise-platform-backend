# enterprise-application-Group-2
| Name| ID Number|
|---|---|
|BEAMLAK FEKADU|UGR/8928/15|
|BELEN BERHANU|UGR/0379/15|
|BEMNET ASSEGED|UGR/2591/15|
|ETSUB NADEW|UGR/4283/15|
|HASET WENDESEN|UGR/4331/15|

# Farmwise Platform Backend

![.NET](https://img.shields.io/badge/.NET-10.0-blue)
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
 
- **Tactical DDD**
  - Entities, Value Objects  
  - Aggregate Roots  
  - Domain Services  
  - Repositories  
  - Domain Events  

Folder structure (initial phase):
/docs/
     /Keycloak
     /outbox
/src/  
    /FarmManagement.Modules.Farm
    /FarmManagement.Modules.Crop 
    /FarmManagement.Modules.Advisory 
    /FarmManagement.SharedKernel
    /FarmManagement.Shared.Infrastructure
    /FarmManagement.WebAPI
      
---

## 🔧 Tech Stack
| Category | Technology |
|---|---|
| Language | C# (.NET 10) |
| Architecture | DDD + Clean Architecture |
| Authentication | Keycloak |
| Messaging | RabbitMQ |
| Persistence | EF Core (Infra Layer only) |

---

## 🚀 Running the Application
### Prerequisites
✔ .NET SDK 10  
✔ Docker installed  
✔ SQL Server  
✔ RabbitMQ  
✔ Keycloak configured  

### Setup Steps
```bash
git clone https://github.com/EAD-Smart-Agri-Systems/farmwise-platform-backend
cd farmwise-platform-backend
dotnet restore
dotnet build
dotnet run --project src/Farm.API/Farm.API.csproj

