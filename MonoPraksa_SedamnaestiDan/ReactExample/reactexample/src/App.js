import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Header from './components/Header';
import Main from './pages/Main';
import CarDashboard from './pages/CarDashboard';
import './index.css';

function App() {
  return (
    <Router>
      <div>
        <Header />
        <Routes>
          <Route path="/" element={<Main />} />
          <Route path="/car-dashboard" element={<CarDashboard />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
