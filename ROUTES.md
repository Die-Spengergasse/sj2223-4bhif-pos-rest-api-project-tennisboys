# Routes

## API

## /api/user

```bat
POST /api/user/create
{
    "username": "string",
    "password": "string",
    "email": "string",
    "firstname": "string",
    "lastname": "string",
    "phone_prefix": "string",
    "phone_number": "string",
    "address": "string",
}
```

```bat
POST /api/user/login
{
  "username": "string",
  "password": "string",
}
```

```bat
POST /api/user/change/password
{
    "password": "string",
    "newPassword": "string",
}
```

```bat
Post /api/user/change/email
{
    "email": "string",
    "newEmail": "string",
}
```
    
```bat
POST /api/user/change/phone
{
    "phone_prefix": "string",
    "phone_number": "string",
    "newPhone_prefix": "string",
    "newPhone_number": "string",
}
```

```bat
POST /api/user/change/address
{
    "address": "string",
    "newAddress": "string",
}
```

```bat
POST /api/user/change/firstname
{
    "firstname": "string"
    "newFirstname": "string"
}
```

## Root

```bat
GET /

Homepage
```

```bat
GET /login

Login page
```

```bat
GET /register

Register page
```
