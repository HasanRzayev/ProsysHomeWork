import { useState, useEffect } from 'react';
import { Shagird } from '../types';
import { api } from '../services/api';
import ShagirdForm from './ShagirdForm';

const ShagirdList = () => {
  const [shagirdler, setShagirdler] = useState<Shagird[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [editingShagird, setEditingShagird] = useState<Shagird | null>(null);
  const [showForm, setShowForm] = useState(false);

  useEffect(() => {
    loadShagirdler();
  }, []);

  const loadShagirdler = async () => {
    try {
      setLoading(true);
      const data = await api.getShagirdler();
      setShagirdler(data);
      setError(null);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Xəta baş verdi');
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (nomresi: number) => {
    if (!window.confirm('Bu şagirdi silmək istədiyinizə əminsiniz?')) {
      return;
    }

    try {
      await api.deleteShagird(nomresi);
      loadShagirdler();
    } catch (err) {
      window.alert(err instanceof Error ? err.message : 'Silinmə xətası');
    }
  };

  const handleEdit = (shagird: Shagird) => {
    setEditingShagird(shagird);
    setShowForm(true);
  };

  const handleFormClose = () => {
    setShowForm(false);
    setEditingShagird(null);
  };

  const handleFormSubmit = () => {
    loadShagirdler();
    handleFormClose();
  };

  if (loading) return <div>Yüklənir...</div>;
  if (error) return <div className="error">Xəta: {error}</div>;

  return (
    <div className="section">
      <div className="section-header">
        <h2>Şagirdlər</h2>
        <button onClick={() => setShowForm(true)} className="btn btn-primary">
          Yeni Şagird Əlavə Et
        </button>
      </div>

      {showForm && (
        <ShagirdForm
          shagird={editingShagird}
          onClose={handleFormClose}
          onSubmit={handleFormSubmit}
        />
      )}

      <table className="table">
        <thead>
          <tr>
            <th>Nömrə</th>
            <th>Ad</th>
            <th>Soyad</th>
            <th>Sinif</th>
            <th>Əməliyyatlar</th>
          </tr>
        </thead>
        <tbody>
          {shagirdler.length === 0 ? (
            <tr>
              <td colSpan={5} style={{ textAlign: 'center' }}>
                Şagird tapılmadı
              </td>
            </tr>
          ) : (
            shagirdler.map((shagird) => (
              <tr key={shagird.nomresi}>
                <td>{shagird.nomresi}</td>
                <td>{shagird.adi}</td>
                <td>{shagird.soyadi}</td>
                <td>{shagird.sinifi}</td>
                <td>
                  <button
                    onClick={() => handleEdit(shagird)}
                    className="btn btn-sm btn-edit"
                  >
                    Redaktə
                  </button>
                  <button
                    onClick={() => handleDelete(shagird.nomresi)}
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

export default ShagirdList;
