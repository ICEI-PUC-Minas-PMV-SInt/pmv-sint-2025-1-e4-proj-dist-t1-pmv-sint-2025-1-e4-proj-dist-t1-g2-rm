import logo from './logo.svg';
import './App.css';
import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import ProductList from './components/pages/Produto/ProductList';
import ProductDetails from './components/pages/Produto/ProductDetails';
import ProductCreate from "./components/pages/Produto/ProductCreate";
import ProductUpdate from "./components/pages/Produto/ProductUpdate";

function App() {
  return (
    <Router>
      <div className="App">
        <h1>Recicla Mais - Produtos</h1>
        <Routes>
          <Route path="/" element={<ProductList />} />
          <Route path="/detalhes/:id" element={<ProductDetails />} />
          <Route path="/criar" element={<ProductCreate />} />
          <Route path="/editar/:id" element={<ProductUpdate />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;

