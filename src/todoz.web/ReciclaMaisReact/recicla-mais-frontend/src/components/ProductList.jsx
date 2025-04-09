import React, { useEffect, useState } from 'react';
import axios from 'axios';

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

  if (loading) return <p style={{ fontSize: '20px' }}>Carregando produtos...</p>;

  return (
    <div style={{ padding: '20px' }}>
      <h2 style={{ fontSize: '28px' }}>Lista de Produtos</h2>

      <button style={{
        marginBottom: '20px',
        padding: '10px 20px',
        fontSize: '16px',
        backgroundColor: '#4CAF50',
        color: 'white',
        border: 'none',
        borderRadius: '5px',
        cursor: 'pointer'
      }}
      onClick={() => alert('Criar componente e chamar api na nova tela.')}>
        Criar Produto
      </button>

      <table style={{ width: '100%', borderCollapse: 'collapse', fontSize: '18px' }}>
        <thead>
          <tr style={{ backgroundColor: '#f2f2f2' }}>
            <th style={thStyle}>ID</th>
            <th style={thStyle}>Nome</th>
            <th style={thStyle}>Descrição</th>
            <th style={thStyle}>Pontuação</th>
            <th style={thStyle}>Ações</th>
          </tr>
        </thead>
        <tbody>
          {produtos.map(produto => (
            <tr key={produto.id}>
              <td style={tdStyle}>{produto.id}</td>
              <td style={tdStyle}>{produto.nome}</td>
              <td style={tdStyle}>{produto.descricao}</td>
              <td style={tdStyle}>{produto.pontuacao}</td>
              <td style={tdStyle}>
                <button onClick={() => alert(`Ver detalhes do ID ${produto.id}`)} style={buttonStyle}>Detalhes</button>
                <button onClick={() => alert(`Atualizar ID ${produto.id}`)} style={buttonStyle}>Atualizar</button>
                <button onClick={() => alert(`Excluir ID ${produto.id}`)} style={deleteButtonStyle}>Excluir</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

const thStyle = {
  border: '1px solid #ddd',
  padding: '12px',
  textAlign: 'left'
};

const tdStyle = {
  border: '1px solid #ddd',
  padding: '12px'
};

const buttonStyle = {
  marginRight: '8px',
  padding: '6px 12px',
  fontSize: '14px',
  backgroundColor: '#2196F3',
  color: 'white',
  border: 'none',
  borderRadius: '4px',
  cursor: 'pointer'
};

const deleteButtonStyle = {
  ...buttonStyle,
  backgroundColor: '#f44336'
};

export default ProductList;
