import './App.css';
import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Homepage from './components/pages/Homepage';
import ProductList from './components/pages/Produto/ProductList';
import ProductDetails from './components/pages/Produto/ProductDetails';
import ProductCreate from "./components/pages/Produto/ProductCreate";
import ProductUpdate from "./components/pages/Produto/ProductUpdate";
import AgendamentoCreate from './components/pages/Agendamento/AgendamentoCreate.jsx';
import AgendamentoList from './components/pages/Agendamento/AgendamentoList.jsx';
import AgendamentoDetail from './components/pages/Agendamento/AgendamentoDetail.jsx';
import Navbar from './components/layout/Navbar.jsx';

function App() {
  return (
    <Router>
      <Navbar />
      <div className="App">
        <Routes>
          <Route path="/" element={<Homepage />} />
          <Route path="/produtos" element={<ProductList />} />
          <Route path="/detalhes/:id" element={<ProductDetails />} />
          <Route path="/criar" element={<ProductCreate />} />
          <Route path="/editar/:id" element={<ProductUpdate />} />
          <Route path="/Agendamento" element={<AgendamentoCreate />} />
          <Route path="/agendamentos" element={<AgendamentoList />} />
          <Route path="/agendamentos/:id" element={<AgendamentoDetail />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;

