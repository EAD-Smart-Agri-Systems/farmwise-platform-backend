# Keycloak Configuration

## Overview
This directory contains Keycloak configuration files for the Farm Management Platform.

## Realm Configuration

**Realm Name:** `farm-management`

**Base URL:** `http://localhost:8080/realms/farm-management`

## Client Configuration

**Client ID:** `farm-management-api`

**Client Secret:** `farm-management-api-secret`

**Token Endpoint:** `http://localhost:8080/realms/farm-management/protocol/openid-connect/token`

**Authorization Endpoint:** `http://localhost:8080/realms/farm-management/protocol/openid-connect/auth`

## Roles

### Admin Role
- **Name:** `Admin`
- **Description:** Administrator role with full access
- **Permissions:**
  - Define `cropType`
  - Define `soilProfile`
  - All User role permissions

### User Role
- **Name:** `User`
- **Description:** Standard user role
- **Permissions:**
  - All operations EXCEPT:
    - Define `cropType`
    - Define `soilProfile`

## Default Users

### Admin User
- **Username:** `admin`
- **Password:** `admin123`
- **Email:** `admin@farmwise.com`
- **Roles:** Admin

### Standard User
- **Username:** `user`
- **Password:** `user123`
- **Email:** `user@farmwise.com`
- **Roles:** User

## API Integration

The API is configured to validate JWT tokens from Keycloak:

```json
{
  "Authentication": {
    "Authority": "http://localhost:8080/realms/farm-management",
    "Audience": "farm-management-api"
  }
}
```

## Role-Based Access Control (RBAC)

### Endpoints Requiring Admin Role
- `POST /api/crops/types` - Define new crop type
- `POST /api/soil/profiles` - Define new soil profile
- `PUT /api/crops/types/{id}` - Update crop type
- `PUT /api/soil/profiles/{id}` - Update soil profile
- `DELETE /api/crops/types/{id}` - Delete crop type
- `DELETE /api/soil/profiles/{id}` - Delete soil profile

### Endpoints Available to User Role
- All farm management operations
- All crop cycle operations
- All advisory report operations
- View crop types and soil profiles (read-only)

## Setup Instructions

1. Start Keycloak using Docker Compose:
   ```bash
   docker-compose up keycloak
   ```

2. The realm will be automatically imported from `realm-export.json`

3. Access Keycloak Admin Console:
   - URL: `http://localhost:8080`
   - Username: `admin`
   - Password: `admin`

4. Verify realm and client configuration

5. Test authentication:
   ```bash
   curl -X POST http://localhost:8080/realms/farm-management/protocol/openid-connect/token \
     -d "client_id=farm-management-api" \
     -d "client_secret=farm-management-api-secret" \
     -d "username=admin" \
     -d "password=admin123" \
     -d "grant_type=password"
   ```

## Token Validation

The API uses JWT Bearer token validation middleware. Tokens must:
- Be issued by the `farm-management` realm
- Have audience `farm-management-api`
- Contain valid roles in the `realm_access.roles` claim

## Security Notes

⚠️ **Important:** The default passwords and secrets in this configuration are for development only. 
For production:
1. Change all default passwords
2. Use strong client secrets
3. Enable HTTPS
4. Configure proper CORS policies
5. Review and restrict redirect URIs
