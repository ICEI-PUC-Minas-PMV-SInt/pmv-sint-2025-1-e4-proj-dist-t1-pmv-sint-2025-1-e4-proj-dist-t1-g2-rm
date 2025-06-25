import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';
import apiBaseUrl from '../../../apiconfig';
import './styles/FaleConosco.css';


function FaleConoscoDetails() {
  const { id } = useParams();
  const [contato, setContato] = useState(null);

  useEffect(() => {
    axios.get(`${apiBaseUrl}/faleconosco/${id}`)
      .then((response) => setContato(response.data))
      .catch((error) => console.error('Erro ao carregar os dados:', error));
  }, [id]);

  if (!contato) return <p>Carregando...</p>;

  return (
    <div>
      <h2>Detalhes do Contato</h2>
      <p><strong>Nome:</strong> {contato.nome}</p>
      <p><strong>Email:</strong> {contato.email}</p>
      <p><strong>Telefone:</strong> {contato.telefone}</p>
      <p><strong>Data de Envio:</strong> {new Date(contato.dataEnvio).toLocaleString()}</p>
    </div>
  );
}

export default FaleConoscoDetails;
