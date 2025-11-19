import { useState, useEffect } from 'react';
import { Ders } from '../types';
import { api } from '../services/api';
import DersForm from './DersForm';

const DersList = () => {
  const [dersler, setDersler] = useState<Ders[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [editingDers, setEditingDers] = useState<Ders | null>(null);
  const [showForm, setShowForm] = useState(false);

  useEffect(() => {
    loadDersler();
  }, []);

  const loadDersler = async () => {
    try {
      setLoading(true);
      const data = await api.getDersler();
      setDersler(data);
      setError(null);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Xəta baş verdi');
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (dersKodu: string) => {
    if (!window.confirm('Bu dərsi silmək istədiyinizə əminsiniz?')) {
      return;
    }

    try {
      await api.deleteDers(dersKodu);
      loadDersler();
    } catch (err) {
      window.alert(err instanceof Error ? err.message : 'Silinmə xətası');
    }
  };

  const handleEdit = (ders: Ders) => {
    setEditingDers(ders);
    setShowForm(true);
  };

  const handleFormClose = () => {
    setShowForm(false);
    setEditingDers(null);
  };

  const handleFormSubmit = () => {
    loadDersler();
    handleFormClose();
  };

  if (loading) return <div>Yüklənir...</div>;
  if (error) return <div className='error'>Xəta: {error}</div>;

  return (
    <div className='section'>
      <div className='section-header'>
        <h2>Dərslər</h2>
        <button onClick={() => setShowForm(true)} className='btn btn-primary'>
          Yeni Dərs Əlavə Et
        </button>
      </div>

      {showForm && (
        <DersForm
          ders={editingDers}
          onClose={handleFormClose}
          onSubmit={handleFormSubmit}
        />
      )}

      <table className='table'>
        <thead>
          <tr>
            <th>Dərs Kodu</th>
            <th>Dərs Adı</th>
            <th>Sinif</th>
            <th>Müəllim Adı</th>
            <th>Müəllim Soyadı</th>
            <th>Əməliyyatlar</th>
          </tr>
        </thead>
        <tbody>
          {dersler.length === 0 ? (
            <tr>
              <td colSpan={6} style={{ textAlign: 'center' }}>
                Dərs tapılmadı
              </td>
            </tr>
          ) : (
            dersler.map((ders) => (
              <tr key={ders.dersKodu}>
                <td>{ders.dersKodu}</td>
                <td>{ders.dersAdi}</td>
                <td>{ders.sinifi}</td>
                <td>{ders.muellimAdi}</td>
                <td>{ders.muellimSoyadi}</td>
                <td>
                  <button
                    onClick={() => handleEdit(ders)}
                    className='btn btn-sm btn-edit'
                  >
                    Redaktə
                  </button>
                  <button
                    onClick={() => handleDelete(ders.dersKodu)}
                    className='btn btn-sm btn-delete'
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

export default DersList;
