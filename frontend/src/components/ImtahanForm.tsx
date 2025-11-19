import { useState, useEffect } from 'react';
import { Imtahan, Ders, Shagird } from '../types';
import { api } from '../services/api';

interface ImtahanFormProps {
  imtahan?: Imtahan | null;
  dersler: Ders[];
  shagirdler: Shagird[];
  onClose: () => void;
  onSubmit: () => void;
}

const ImtahanForm = ({ imtahan, dersler, shagirdler, onClose, onSubmit }: ImtahanFormProps) => {
  const [formData, setFormData] = useState<Imtahan>({
    dersKodu: '',
    shagirdNomresi: 0,
    imtahanTarixi: new Date().toISOString().split('T')[0],
    qiymeti: 1,
  });
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (imtahan) {
      setFormData({
        ...imtahan,
        imtahanTarixi: imtahan.imtahanTarixi.split('T')[0],
      });
    }
  }, [imtahan]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: name === 'shagirdNomresi' || name === 'qiymeti' 
        ? parseInt(value) || 0 
        : value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    setLoading(true);

    try {
      const submitData = {
        ...formData,
        imtahanTarixi: new Date(formData.imtahanTarixi).toISOString(),
      };

      if (imtahan) {
        await api.updateImtahan(
          imtahan.dersKodu,
          imtahan.shagirdNomresi,
          imtahan.imtahanTarixi,
          submitData
        );
      } else {
        await api.createImtahan(submitData);
      }
      onSubmit();
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Xəta baş verdi');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="modal-content" onClick={(e) => e.stopPropagation()}>
        <div className="modal-header">
          <h3>{imtahan ? 'İmtahan Redaktə Et' : 'Yeni İmtahan Əlavə Et'}</h3>
          <button onClick={onClose} className="btn-close">&times;</button>
        </div>
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>
              Dərs <span className="required">*</span>
              <select
                name="dersKodu"
                value={formData.dersKodu}
                onChange={handleChange}
                required
                disabled={!!imtahan}
              >
                <option value="">Seçin...</option>
                {dersler.map((ders) => (
                  <option key={ders.dersKodu} value={ders.dersKodu}>
                    {ders.dersKodu} - {ders.dersAdi}
                  </option>
                ))}
              </select>
            </label>
          </div>
          <div className="form-group">
            <label>
              Şagird <span className="required">*</span>
              <select
                name="shagirdNomresi"
                value={formData.shagirdNomresi || ''}
                onChange={handleChange}
                required
                disabled={!!imtahan}
              >
                <option value="">Seçin...</option>
                {shagirdler.map((shagird) => (
                  <option key={shagird.nomresi} value={shagird.nomresi}>
                    {shagird.nomresi} - {shagird.adi} {shagird.soyadi}
                  </option>
                ))}
              </select>
            </label>
          </div>
          <div className="form-group">
            <label>
              İmtahan Tarixi <span className="required">*</span>
              <input
                type="date"
                name="imtahanTarixi"
                value={formData.imtahanTarixi}
                onChange={handleChange}
                required
                disabled={!!imtahan}
              />
            </label>
          </div>
          <div className="form-group">
            <label>
              Qiymət <span className="required">*</span>
              <input
                type="number"
                name="qiymeti"
                value={formData.qiymeti}
                onChange={handleChange}
                min={1}
                max={5}
                required
              />
            </label>
          </div>
          {error && <div className="error">{error}</div>}
          <div className="form-actions">
            <button type="button" onClick={onClose} className="btn btn-secondary">
              Ləğv Et
            </button>
            <button type="submit" className="btn btn-primary" disabled={loading}>
              {loading ? 'Yüklənir...' : imtahan ? 'Yenilə' : 'Əlavə Et'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default ImtahanForm;
