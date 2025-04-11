import React, { useEffect, useState } from 'react';
import axios from 'axios';
import './ProductList.css';
import ProductDelete from './ProductDelete';


const ProductList = () => {
  const [produtos, setProdutos] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    axios.get('https://localhost:7215/api/Produtos')
      .then(response => {
        setProdutos(response.data);
        setLoading(false);
      })
      .catch(error => {
        console.error('Erro ao buscar produtos:', error);
        setLoading(false);
      });
  }, []);

  if (loading) return <p className="loading">Carregando produtos...</p>;

  return (
    <div className="container">
      <h2 className="title">Lista de Produtos</h2>

      <div className="button-container">
        <button className="create-button"
          onClick={() => alert('Criar componente e chamar api na nova tela.')}>
          Criar Produto
        </button>
      </div>

      <table className="table">
        <thead>
          <tr className="thead-row">
            <th className="th">ID</th>
            <th className="th">Nome</th>
            <th className="th">Descrição</th>
            <th className="th">Pontuação</th>
            <th className="th">Ações</th>
          </tr>
        </thead>
        <tbody>
          {produtos.map(produto => (
            <tr key={produto.id}>
              <td className="td">{produto.id}</td>
              <td className="td">{produto.nome}</td>
              <td className="td">{produto.descricao}</td>
              <td className="td">{produto.pontuacao}</td>
              <td className="td">
                <button onClick={() => alert(`Ver detalhes do ID ${produto.id}`)} className="action-button">Detalhes</button>
                <button onClick={() => alert(`Atualizar ID ${produto.id}`)} className="action-button">Atualizar</button>
                <ProductDelete id={produto.id} onDeleteSuccess={(deletedId) => {setProdutos(produtos.filter(p => p.id !== deletedId));}} />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ProductList;
