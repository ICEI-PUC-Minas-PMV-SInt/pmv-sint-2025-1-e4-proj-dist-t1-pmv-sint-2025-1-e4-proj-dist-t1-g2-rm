import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';
import apiBaseUrl from '../../../apiconfig';
import './styles/FaleConosco.css';


function FaleConoscoDelete() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [contato, setContato] = useState(null);

  useEffect(() => {
    axios.get(`${apiBaseUrl}/faleconosco/${id}`)
      .then((response) => setContato(response.data))
      .catch((error) => console.error('Erro ao buscar contato:', error));
  }, [id]);

  const handleDelete = async () => {
    try {
      await axios.delete(`${apiBaseUrl}/faleconosco/${id}`);
      alert('Contato excluído com sucesso!');
      navigate('/faleconosco'); // redireciona para a lista
    } catch (error) {
      alert('Erro ao excluir o contato.');
      console.error(error);
    }
  };

  if (!contato) return <p>Carregando...</p>;

  return (
    <div>
      <h2>Excluir Contato</h2>
      <p>Tem certeza que deseja excluir este contato?</p>
      <p><strong>Nome:</strong> {contato.nome}</p>
      <p><strong>Email:</strong> {contato.email}</p>
      <p><strong>Telefone:</strong> {contato.telefone}</p>
      <button onClick={handleDelete}>Confirmar Exclusão</button>
    </div>
  );
}

export default FaleConoscoDelete;
