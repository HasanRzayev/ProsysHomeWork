export interface Ders {
  dersKodu: string;
  dersAdi: string;
  sinifi: number;
  muellimAdi: string;
  muellimSoyadi: string;
}

export interface Shagird {
  nomresi: number;
  adi: string;
  soyadi: string;
  sinifi: number;
}

export interface Imtahan {
  dersKodu: string;
  shagirdNomresi: number;
  imtahanTarixi: string; // ISO date string
  qiymeti: number;
}

export interface ImtahanWithDetails extends Imtahan {
  dersAdi?: string;
  shagirdAdi?: string;
  shagirdSoyadi?: string;
}
