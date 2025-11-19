import { useState, useEffect } from 'react';
import { Imtahan, Ders, Shagird } from '../types';
import { api } from '../services/api';
import ImtahanForm from './ImtahanForm';

const ImtahanList = () => {
  const [imtahanlar, setImtahanlar] = useState<Imtahan[]>([]);
  const [dersler, setDersler] = useState<Ders[]>([]);
  const [shagirdler, setShagirdler] = useState<Shagird[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [editingImtahan, setEditingImtahan] = useState<Imtahan | null>(null);
  const [showForm, setShowForm] = useState(false);

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      setLoading(true);
      const [imtahanData, dersData, shagirdData] = await Promise.all([
        api.getImtahanlar(),
        api.getDersler(),
        api.getShagirdler(),
      ]);
      setImtahanlar(imtahanData);
      setDersler(dersData);
      setShagirdler(shagirdData);
      setError(null);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Xəta baş verdi');
    } finally {
      setLoading(false);
    }
  };

  const getDersAdi = (dersKodu: string) => {
    const ders = dersler.find((d) => d.dersKodu === dersKodu);
    return ders ? ders.dersAdi : dersKodu;
  };

  const getShagirdAdi = (nomresi: number) => {
    const shagird = shagirdler.find((s) => s.nomresi === nomresi);
    return shagird ? `${shagird.adi} ${shagird.soyadi}` : nomresi.toString();
  };

  const handleDelete = async (imtahan: Imtahan) => {
    if (!window.confirm('Bu imtahanı silmək istədiyinizə əminsiniz?')) {
      return;
    }

    try {
      await api.deleteImtahan(imtahan.dersKodu, imtahan.shagirdNomresi, imtahan.imtahanTarixi);
      loadData();
    } catch (err) {
      window.alert(err instanceof Error ? err.message : 'Silinmə xətası');
    }
  };

  const handleEdit = (imtahan: Imtahan) => {
    setEditingImtahan(imtahan);
    setShowForm(true);
  };

  const handleFormClose = () => {
    setShowForm(false);
    setEditingImtahan(null);
  };

  const handleFormSubmit = () => {
    loadData();
    handleFormClose();
  };

  if (loading) return <div>Yüklənir...</div>;
  if (error) return <div className="error">Xəta: {error}</div>;

  return (
    <div className="section">
      <div className="section-header">
        <h2>İmtahanlar</h2>
        <button onClick={() => setShowForm(true)} className="btn btn-primary">
          Yeni İmtahan Əlavə Et
        </button>
      </div>

      {showForm && (
        <ImtahanForm
          imtahan={editingImtahan}
          dersler={dersler}
          shagirdler={shagirdler}
          onClose={handleFormClose}
          onSubmit={handleFormSubmit}
        />
      )}

      <table className="table">
        <thead>
          <tr>
            <th>Dərs</th>
            <th>Şagird</th>
            <th>Tarix</th>
            <th>Qiymət</th>
            <th>Əməliyyatlar</th>
          </tr>
        </thead>
        <tbody>
          {imtahanlar.length === 0 ? (
            <tr>
              <td colSpan={5} style={{ textAlign: 'center' }}>
                İmtahan tapılmadı
              </td>
            </tr>
          ) : (
            imtahanlar.map((imtahan, index) => (
              <tr key={`${imtahan.dersKodu}-${imtahan.shagirdNomresi}-${imtahan.imtahanTarixi}-${index}`}>
                <td>{getDersAdi(imtahan.dersKodu)}</td>
                <td>{getShagirdAdi(imtahan.shagirdNomresi)}</td>
                <td>{new Date(imtahan.imtahanTarixi).toLocaleDateString('az-AZ')}</td>
                <td>{imtahan.qiymeti}</td>
                <td>
                  <button
                    onClick={() => handleEdit(imtahan)}
                    className="btn btn-sm btn-edit"
                  >
                    Redaktə
                  </button>
                  <button
                    onClick={() => handleDelete(imtahan)}
                    className="btn btn-sm btn-delete"
                  >
                    Sil
                  </button>
                </td>
              </tr>
            ))
          )}
        </tbody>
      </table>
    </div>
  );
};

export default ImtahanList;
