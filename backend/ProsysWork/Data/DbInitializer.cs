using Microsoft.EntityFrameworkCore;
using ProsysWork.Models;

namespace ProsysWork.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Dersler.Any() || context.Shagirdler.Any() || context.Imtahanlar.Any())
            {
                return;
            }

            var dersler = new Ders[]
            {
                new Ders { DersKodu = "MAT", DersAdi = "Riyaziyyat", Sinifi = 9, MuellimAdi = "Əli", MuellimSoyadi = "Məmmədov" },
                new Ders { DersKodu = "FIZ", DersAdi = "Fizika", Sinifi = 9, MuellimAdi = "Nərgiz", MuellimSoyadi = "Qasımova" },
                new Ders { DersKodu = "KIM", DersAdi = "Kimya", Sinifi = 10, MuellimAdi = "Rəşad", MuellimSoyadi = "Həsənov" },
                new Ders { DersKodu = "BIO", DersAdi = "Biologiya", Sinifi = 10, MuellimAdi = "Leyla", MuellimSoyadi = "İbrahimova" },
                new Ders { DersKodu = "AZE", DersAdi = "Azərbaycan Dili", Sinifi = 11, MuellimAdi = "Gülnar", MuellimSoyadi = "Rəhimova" },
                new Ders { DersKodu = "ING", DersAdi = "İngilis Dili", Sinifi = 11, MuellimAdi = "Samir", MuellimSoyadi = "Əliyev" },
                new Ders { DersKodu = "TAR", DersAdi = "Tarix", Sinifi = 9, MuellimAdi = "Nigar", MuellimSoyadi = "Məlikova" },
                new Ders { DersKodu = "COG", DersAdi = "Coğrafiya", Sinifi = 10, MuellimAdi = "Elvin", MuellimSoyadi = "Cabbarov" }
            };

            context.Dersler.AddRange(dersler);
            context.SaveChanges();

            var shagirdler = new Shagird[]
            {
                new Shagird { Nomresi = 10001, Adi = "Aysel", Soyadi = "Məmmədova", Sinifi = 9 },
                new Shagird { Nomresi = 10002, Adi = "Rəşad", Soyadi = "Həsənov", Sinifi = 9 },
                new Shagird { Nomresi = 10003, Adi = "Leyla", Soyadi = "Qasımova", Sinifi = 9 },
                new Shagird { Nomresi = 10004, Adi = "Elvin", Soyadi = "İbrahimov", Sinifi = 10 },
                new Shagird { Nomresi = 10005, Adi = "Gülnar", Soyadi = "Rəhimova", Sinifi = 10 },
                new Shagird { Nomresi = 10006, Adi = "Samir", Soyadi = "Əliyev", Sinifi = 10 },
                new Shagird { Nomresi = 10007, Adi = "Nigar", Soyadi = "Məlikova", Sinifi = 11 },
                new Shagird { Nomresi = 10008, Adi = "Tural", Soyadi = "Cabbarov", Sinifi = 11 },
                new Shagird { Nomresi = 10009, Adi = "Aygün", Soyadi = "Vəliyeva", Sinifi = 11 },
                new Shagird { Nomresi = 10010, Adi = "Ruslan", Soyadi = "Nəbiyev", Sinifi = 9 }
            };

            context.Shagirdler.AddRange(shagirdler);
            context.SaveChanges();

            var imtahanlar = new Imtahan[]
            {
                new Imtahan { DersKodu = "MAT", ShagirdNomresi = 10001, ImtahanTarixi = new DateTime(2024, 10, 15), Qiymeti = 5 },
                new Imtahan { DersKodu = "FIZ", ShagirdNomresi = 10001, ImtahanTarixi = new DateTime(2024, 10, 16), Qiymeti = 4 },
                new Imtahan { DersKodu = "MAT", ShagirdNomresi = 10002, ImtahanTarixi = new DateTime(2024, 10, 15), Qiymeti = 4 },
                new Imtahan { DersKodu = "KIM", ShagirdNomresi = 10004, ImtahanTarixi = new DateTime(2024, 10, 20), Qiymeti = 5 },
                new Imtahan { DersKodu = "BIO", ShagirdNomresi = 10004, ImtahanTarixi = new DateTime(2024, 10, 21), Qiymeti = 4 },
                new Imtahan { DersKodu = "AZE", ShagirdNomresi = 10007, ImtahanTarixi = new DateTime(2024, 11, 1), Qiymeti = 5 },
                new Imtahan { DersKodu = "ING", ShagirdNomresi = 10007, ImtahanTarixi = new DateTime(2024, 11, 2), Qiymeti = 5 },
                new Imtahan { DersKodu = "TAR", ShagirdNomresi = 10003, ImtahanTarixi = new DateTime(2024, 10, 18), Qiymeti = 4 },
                new Imtahan { DersKodu = "COG", ShagirdNomresi = 10005, ImtahanTarixi = new DateTime(2024, 10, 25), Qiymeti = 5 },
                new Imtahan { DersKodu = "MAT", ShagirdNomresi = 10010, ImtahanTarixi = new DateTime(2024, 10, 15), Qiymeti = 3 }
            };

            context.Imtahanlar.AddRange(imtahanlar);
            context.SaveChanges();
        }
    }
}

