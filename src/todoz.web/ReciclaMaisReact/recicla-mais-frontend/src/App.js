import logo from './logo.svg';
import './App.css';
import React from 'react';
import ProductList from './components/ProductList';

function App() {
  return (
    <div className="App">
      <h1>Recicla Mais - Produtos</h1>
      <ProductList />
    </div>
  );
}

export default App;

