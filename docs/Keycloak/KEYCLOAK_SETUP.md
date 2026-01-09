# Keycloak Setup Guide

## Quick Start

### 1. Start Keycloak with Docker Compose

```bash
docker-compose up keycloak -d
```

Wait for Keycloak to be healthy (check with `docker ps`).

### 2. Access Keycloak Admin Console

- **URL:** http://localhost:8080
- **Username:** `admin`
- **Password:** `admin`

### 3. Import Realm (Manual Method)

The automatic import via `--import-realm` flag may not work with the JSON format. Use manual import:

1. Login to Keycloak Admin Console
2. Click "Add realm" button
3. Click "Select file" and choose `docs/keycloak/realm-export.json`
4. Click "Create"

**OR** Create realm manually:

1. Click "Create Realm"
2. Name: `farm-management`
3. Click "Create"

### 4. Create Client

1. Go to "Clients" → "Create client"
2. Client ID: `farm-management-api`
3. Client authentication: **ON**
4. Click "Next"
5. Valid redirect URIs: `*`
6. Web origins: `*`
7. Click "Save"
8. Go to "Credentials" tab
9. Copy the "Client secret" (or set it to `farm-management-api-secret`)

### 5. Create Roles

1. Go to "Realm roles"
2. Click "Create role"
3. Role name: `Admin` → Save
4. Click "Create role" again
5. Role name: `User` → Save

### 6. Create Users

#### Admin User
1. Go to "Users" → "Add user"
2. Username: `admin`
3. Email: `admin@farmwise.com`
4. Email verified: **ON**
5. Click "Create"
6. Go to "Credentials" tab
7. Set password: `admin123`
8. Temporary: **OFF**
9. Click "Set password"
10. Go to "Role mapping" tab
11. Assign role: `Admin`

#### User User
1. Go to "Users" → "Add user"
2. Username: `user`
3. Email: `user@farmwise.com`
4. Email verified: **ON**
5. Click "Create"
6. Go to "Credentials" tab
7. Set password: `user123`
8. Temporary: **OFF**
9. Click "Set password"
10. Go to "Role mapping" tab
11. Assign role: `User`

## Testing Authentication

### Get Access Token (Admin)

```bash
curl -X POST http://localhost:8080/realms/farm-management/protocol/openid-connect/token \
  -H "Content-Type: application/x-www-form-urlencoded" \
  -d "client_id=farm-management-api" \
  -d "client_secret=farm-management-api-secret" \
  -d "username=admin" \
  -d "password=admin123" \
  -d "grant_type=password"
```

Response:
```json
{
  "access_token": "eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJ...",
  "expires_in": 300,
  "refresh_expires_in": 1800,
  "token_type": "Bearer"
}
```

### Get Access Token (User)

```bash
curl -X POST http://localhost:8080/realms/farm-management/protocol/openid-connect/token \
  -H "Content-Type: application/x-www-form-urlencoded" \
  -d "client_id=farm-management-api" \
  -d "client_secret=farm-management-api-secret" \
  -d "username=user" \
  -d "password=user123" \
  -d "grant_type=password"
```

### Test API with Token

```bash
# Replace <TOKEN> with the access_token from above
curl -H "Authorization: Bearer <TOKEN>" \
  http://localhost:5000/api/farms
```

### Test Admin-Only Endpoint

```bash
# This should work with Admin token
curl -X POST http://localhost:5000/api/crops/types \
  -H "Authorization: Bearer <ADMIN_TOKEN>" \
  -H "Content-Type: application/json" \
  -d '{
    "cropCode": 1,
    "name": "Maize",
    "typicalStages": "seed,germination,vegetative,flowering,harvest",
    "durationDays": 120
  }'

# This should FAIL with User token (403 Forbidden)
curl -X POST http://localhost:5000/api/crops/types \
  -H "Authorization: Bearer <USER_TOKEN>" \
  -H "Content-Type: application/json" \
  -d '{
    "cropCode": 1,
    "name": "Maize",
    "typicalStages": "seed,germination,vegetative,flowering,harvest",
    "durationDays": 120
  }'
```

## Swagger UI Testing

1. Start the application
2. Navigate to http://localhost:5000/swagger
3. Click "Authorize" button (top right)
4. Enter: `Bearer <your-token>`
5. Click "Authorize"
6. Now you can test endpoints directly from Swagger

## Troubleshooting

### Token Validation Fails

1. Check that `Authentication:Authority` in `appsettings.json` matches Keycloak realm URL
2. Verify client secret matches
3. Check that audience in token matches `farm-management-api`

### 403 Forbidden on Admin Endpoints

1. Verify user has the correct role assigned
2. Check token contains role in `realm_access.roles` claim
3. Decode JWT at https://jwt.io to inspect claims

### Keycloak Not Starting

1. Check Docker logs: `docker logs farmwise-keycloak`
2. Verify SQL Server is running and healthy
3. Check port 8080 is not in use

## Role Claims in JWT

The JWT token will contain roles in the `realm_access.roles` claim:

```json
{
  "realm_access": {
    "roles": ["Admin", "User"]
  }
}
```

The API validates these roles using the `[Authorize(Policy = "AdminOnly")]` or `[Authorize(Policy = "UserOrAdmin")]` attributes.
