import { Ders, Shagird, Imtahan } from '../types';

const API_BASE_URL = process.env.REACT_APP_API_URL || 'http://localhost:5195/api';

async function fetchApi<T>(endpoint: string, options?: RequestInit): Promise<T> {
  const response = await fetch(`${API_BASE_URL}${endpoint}`, {
    ...options,
    headers: {
      'Content-Type': 'application/json',
      ...options?.headers,
    },
  });

  if (!response.ok) {
    const error = await response.text();
    throw new Error(error || `HTTP error! status: ${response.status}`);
  }

  if (response.status === 204) {
    return undefined as T;
  }

  return response.json();
}

export const api = {
  getDersler: () => fetchApi<Ders[]>('/Ders'),
  getDers: (dersKodu: string) => fetchApi<Ders>(`/Ders/${dersKodu}`),
  createDers: (data: Ders) => fetchApi<Ders>('/Ders', {
    method: 'POST',
    body: JSON.stringify(data),
  }),
  updateDers: (dersKodu: string, data: Ders) => fetchApi<void>(`/Ders/${dersKodu}`, {
    method: 'PUT',
    body: JSON.stringify(data),
  }),
  deleteDers: (dersKodu: string) => fetchApi<void>(`/Ders/${dersKodu}`, {
    method: 'DELETE',
  }),

  getShagirdler: () => fetchApi<Shagird[]>('/Shagird'),
  getShagird: (nomresi: number) => fetchApi<Shagird>(`/Shagird/${nomresi}`),
  createShagird: (data: Shagird) => fetchApi<Shagird>('/Shagird', {
    method: 'POST',
    body: JSON.stringify(data),
  }),
  updateShagird: (nomresi: number, data: Shagird) => fetchApi<void>(`/Shagird/${nomresi}`, {
    method: 'PUT',
    body: JSON.stringify(data),
  }),
  deleteShagird: (nomresi: number) => fetchApi<void>(`/Shagird/${nomresi}`, {
    method: 'DELETE',
  }),

  getImtahanlar: () => fetchApi<Imtahan[]>('/Imtahan'),
  getImtahan: (dersKodu: string, shagirdNomresi: number, tarix: string) => {
    const encodedDate = encodeURIComponent(tarix);
    return fetchApi<Imtahan>(`/Imtahan/${dersKodu}/${shagirdNomresi}/${encodedDate}`);
  },
  createImtahan: (data: Imtahan) => fetchApi<Imtahan>('/Imtahan', {
    method: 'POST',
    body: JSON.stringify(data),
  }),
  updateImtahan: (dersKodu: string, shagirdNomresi: number, tarix: string, data: Imtahan) => {
    const encodedDate = encodeURIComponent(tarix);
    return fetchApi<void>(`/Imtahan/${dersKodu}/${shagirdNomresi}/${encodedDate}`, {
      method: 'PUT',
      body: JSON.stringify(data),
    });
  },
  deleteImtahan: (dersKodu: string, shagirdNomresi: number, tarix: string) => {
    const encodedDate = encodeURIComponent(tarix);
    return fetchApi<void>(`/Imtahan/${dersKodu}/${shagirdNomresi}/${encodedDate}`, {
      method: 'DELETE',
    });
  },
};
