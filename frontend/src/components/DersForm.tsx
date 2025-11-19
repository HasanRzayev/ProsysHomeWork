import { useState, useEffect } from 'react';
import { Ders } from '../types';
import { api } from '../services/api';

interface DersFormProps {
  ders?: Ders | null;
  onClose: () => void;
  onSubmit: () => void;
}

const DersForm = ({ ders, onClose, onSubmit }: DersFormProps) => {
  const [formData, setFormData] = useState<Ders>({
    dersKodu: '',
    dersAdi: '',
    sinifi: 1,
    muellimAdi: '',
    muellimSoyadi: '',
  });
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (ders) {
      setFormData(ders);
    }
  }, [ders]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: name === 'sinifi' ? parseInt(value) || 0 : value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    setLoading(true);

    try {
      if (ders) {
        await api.updateDers(ders.dersKodu, formData);
      } else {
        await api.createDers(formData);
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
          <h3>{ders ? 'Dərs Redaktə Et' : 'Yeni Dərs Əlavə Et'}</h3>
          <button onClick={onClose} className="btn-close">&times;</button>
        </div>
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>
              Dərs Kodu <span className="required">*</span>
              <input
                type="text"
                name="dersKodu"
                value={formData.dersKodu}
                onChange={handleChange}
                maxLength={3}
                required
                disabled={!!ders}
                placeholder="Məs: MAT"
              />
            </label>
          </div>
          <div className="form-group">
            <label>
              Dərs Adı <span className="required">*</span>
              <input
                type="text"
                name="dersAdi"
                value={formData.dersAdi}
                onChange={handleChange}
                maxLength={30}
                required
                placeholder="Məs: Riyaziyyat"
              />
            </label>
          </div>
          <div className="form-group">
            <label>
              Sinif <span className="required">*</span>
              <input
                type="number"
                name="sinifi"
                value={formData.sinifi}
                onChange={handleChange}
                min={1}
                max={12}
                required
              />
            </label>
          </div>
          <div className="form-group">
            <label>
              Müəllim Adı <span className="required">*</span>
              <input
                type="text"
                name="muellimAdi"
                value={formData.muellimAdi}
                onChange={handleChange}
                maxLength={20}
                required
              />
            </label>
          </div>
          <div className="form-group">
            <label>
              Müəllim Soyadı <span className="required">*</span>
              <input
                type="text"
                name="muellimSoyadi"
                value={formData.muellimSoyadi}
                onChange={handleChange}
                maxLength={20}
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
              {loading ? 'Yüklənir...' : ders ? 'Yenilə' : 'Əlavə Et'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default DersForm;