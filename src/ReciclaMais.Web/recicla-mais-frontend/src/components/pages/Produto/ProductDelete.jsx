import React from 'react';
import axios from 'axios';
import apiBaseUrl from '../../../apiconfig';

const ProductDelete = ({ id, onDeleteSuccess }) => {
  const handleDelete = async () => {
    const confirm = window.confirm(`Tem certeza que deseja excluir o produto de ID ${id}?`);
    if (!confirm) return;

    try {
      await axios.delete(`${apiBaseUrl}/api/Produtos/${id}`);
      onDeleteSuccess(id); // callback para atualizar lista
      alert(`Produto ${id} excluído com sucesso!`);
    } catch (error) {
      console.error('Erro ao excluir produto:', error);
      alert('Erro ao excluir o produto. Tente novamente.');
    }
  };

  return (
    <button onClick={handleDelete} className="action-button delete-button">
      Excluir
    </button>
  );
};

export default ProductDelete;
