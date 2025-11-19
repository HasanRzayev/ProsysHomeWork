İmtahan Proqramı

Quraşdırma:

1. Repository-ni clone edin:
git clone https://github.com/HasanRzayev/ProsysHomeWork.git
cd ProsysHomeWork

2. Backend quraşdırması:

appsettings.json faylında connection string-i düzəldin:
Data Source=YOUR_SERVER\SQLEXPRESS;Integrated Security=True;TrustServerCertificate=True;

Sonra backend papkasına keçin:
cd backend/ProsysWork
dotnet restore
dotnet ef database update
dotnet run

3. Frontend quraşdırması:

frontend papkasında .env faylı yaradın və içərisinə yazın:
REACT_APP_API_URL=http://localhost:5195/api

Sonra:
cd frontend
npm install
npm start

İşə salınması:

Backend: backend/ProsysWork papkasında dotnet run
Frontend: frontend papkasında npm start
Browser: http://localhost:3000
