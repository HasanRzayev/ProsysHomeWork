import { useState, useEffect } from 'react';
import { Shagird } from '../types';
import { api } from '../services/api';

interface ShagirdFormProps {
  shagird?: Shagird | null;
  onClose: () => void;
  onSubmit: () => void;
}

const ShagirdForm = ({ shagird, onClose, onSubmit }: ShagirdFormProps) => {
  const [formData, setFormData] = useState<Shagird>({
    nomresi: 0,
    adi: '',
    soyadi: '',
    sinifi: 1,
  });
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (shagird) {
      setFormData(shagird);
    }
  }, [shagird]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: name === 'nomresi' || name === 'sinifi' ? parseInt(value) || 0 : value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    setLoading(true);

    try {
      if (shagird) {
        await api.updateShagird(shagird.nomresi, formData);
      } else {
        await api.createShagird(formData);
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
          <h3>{shagird ? 'Şagird Redaktə Et' : 'Yeni Şagird Əlavə Et'}</h3>
          <button onClick={onClose} className="btn-close">&times;</button>
        </div>
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>
              Nömrə <span className="required">*</span>
              <input
                type="number"
                name="nomresi"
                value={formData.nomresi || ''}
                onChange={handleChange}
                min={1}
                max={99999}
                required
                disabled={!!shagird}
                placeholder="Məs: 10001"
              />
            </label>
          </div>
          <div className="form-group">
            <label>
              Ad <span className="required">*</span>
              <input
                type="text"
                name="adi"
                value={formData.adi}
                onChange={handleChange}
                maxLength={30}
                required
              />
            </label>
          </div>
          <div className="form-group">
            <label>
              Soyad <span className="required">*</span>
              <input
                type="text"
                name="soyadi"
                value={formData.soyadi}
                onChange={handleChange}
                maxLength={30}
                required
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
          {error && <div className="error">{error}</div>}
          <div className="form-actions">
            <button type="button" onClick={onClose} className="btn btn-secondary">
              Ləğv Et
            </button>
            <button type="submit" className="btn btn-primary" disabled={loading}>
              {loading ? 'Yüklənir...' : shagird ? 'Yenilə' : 'Əlavə Et'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default ShagirdForm;
