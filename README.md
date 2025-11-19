# İmtahan Proqramı

## Quraşdırma

### 1. Clone edin
```bash
git clone https://github.com/HasanRzayev/ProsysHomeWork.git
cd ProsysHomeWork
```

### 2. Backend

**appsettings.json** faylında connection string-i düzəldin:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=YOUR_SERVER\\SQLEXPRESS;Integrated Security=True;TrustServerCertificate=True;"
  }
}
```

```bash
cd backend/ProsysWork
dotnet restore
dotnet ef database update
dotnet run
```

### 3. Frontend

**frontend/.env** faylı yaradın:
```
REACT_APP_API_URL=http://localhost:5195/api
```

```bash
cd frontend
npm install
npm start
```

## İşə Salınması

1. Backend: `cd backend/ProsysWork` → `dotnet run`
2. Frontend: `cd frontend` → `npm start`
3. Browser: `http://localhost:3000`
