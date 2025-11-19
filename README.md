# İmtahan Proqramı

Orta məktəb imtahan nəticələrinin idarəetmə sistemi

## Texnologiyalar

- **Backend**: C# .NET 8.0, Entity Framework Core, MS SQL Server
- **Frontend**: React, TypeScript

## Quraşdırma və İşə Salınması

### 1. Repository-ni clone edin

```bash
git clone https://github.com/HasanRzayev/ProsysHomeWork.git
cd ProsysHomeWork
```

### 2. Backend

```bash
cd backend/ProsysWork
```

**appsettings.json** faylında connection string-i düzəldin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=YOUR_SERVER\\SQLEXPRESS;Integrated Security=True;..."
  }
}
```

```bash
dotnet restore
dotnet ef database update
dotnet run
```

Backend `https://localhost:7238` və `http://localhost:5195` ünvanlarında işləyəcək.

### 3. Frontend

Yeni terminal açın:

```bash
cd frontend
npm install
npm start
```

Frontend `http://localhost:3000` ünvanında açılacaq.

## Database

Database yaradılanda avtomatik olaraq fake data əlavə olunur:
- 8 dərs
- 10 şagird
- 10 imtahan

## API Endpoints

- `GET /api/Ders` - Bütün dərsləri gətir
- `POST /api/Ders` - Yeni dərs əlavə et
- `PUT /api/Ders/{dersKodu}` - Dərs yenilə
- `DELETE /api/Ders/{dersKodu}` - Dərs sil

- `GET /api/Shagird` - Bütün şagirdləri gətir
- `POST /api/Shagird` - Yeni şagird əlavə et
- `PUT /api/Shagird/{nomresi}` - Şagird yenilə
- `DELETE /api/Shagird/{nomresi}` - Şagird sil

- `GET /api/Imtahan` - Bütün imtahanları gətir
- `POST /api/Imtahan` - Yeni imtahan əlavə et
- `PUT /api/Imtahan/{dersKodu}/{shagirdNomresi}/{tarix}` - İmtahan yenilə
- `DELETE /api/Imtahan/{dersKodu}/{shagirdNomresi}/{tarix}` - İmtahan sil

