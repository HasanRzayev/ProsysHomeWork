import { useState } from 'react';
import DersList from './components/DersList';
import ShagirdList from './components/ShagirdList';
import ImtahanList from './components/ImtahanList';
import './App.css';

type TabType = 'dersler' | 'shagirdler' | 'imtahanlar';

function App() {
  const [activeTab, setActiveTab] = useState<TabType>('dersler');

  return (
    <div className="app">
      <header className="app-header">
        <h1>İmtahan Proqramı</h1>
        <p>Orta məktəb imtahan nəticələrinin idarəetmə sistemi</p>
      </header>

      <nav className="tabs">
        <button
          className={activeTab === 'dersler' ? 'tab active' : 'tab'}
          onClick={() => setActiveTab('dersler')}
        >
          Dərslər
        </button>
        <button
          className={activeTab === 'shagirdler' ? 'tab active' : 'tab'}
          onClick={() => setActiveTab('shagirdler')}
        >
          Şagirdlər
        </button>
        <button
          className={activeTab === 'imtahanlar' ? 'tab active' : 'tab'}
          onClick={() => setActiveTab('imtahanlar')}
        >
          İmtahanlar
        </button>
      </nav>

      <main className="app-main">
        {activeTab === 'dersler' && <DersList />}
        {activeTab === 'shagirdler' && <ShagirdList />}
        {activeTab === 'imtahanlar' && <ImtahanList />}
      </main>
    </div>
  );
}

export default App;
