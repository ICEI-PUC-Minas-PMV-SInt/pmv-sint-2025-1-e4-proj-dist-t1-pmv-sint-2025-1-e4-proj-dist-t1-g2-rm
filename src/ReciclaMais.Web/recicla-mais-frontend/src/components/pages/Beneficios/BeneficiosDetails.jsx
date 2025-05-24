import React, { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import axios from 'axios';
import './styles/Beneficios.css';
import apiBaseUrl from '../../../apiconfig';

const BeneficiosDetails = () => {
  const { id } = useParams();
  const [beneficio, setBeneficio] = useState(null);

  useEffect(() => {
    axios.get(`${apiBaseUrl}/beneficios/${id}`)
      .then(response => setBeneficio(response.data))
      .catch(error => {
        console.error('Erro ao carregar detalhes do benefício:', error);
        alert('Benefício não encontrado.');
      });
  }, [id]);

  if (!beneficio) return <p>Carregando...</p>;

  return (
    <div className="container">
      <h2>{beneficio.titulo}</h2>
      <p><strong>Descrição:</strong> {beneficio.descricao}</p>
      <p><strong>Pontos Necessários:</strong> {beneficio.pontosNecessarios}</p>
      <Link to="/beneficios"><button>Voltar para lista</button></Link>
    </div>
  );
};

export default BeneficiosDetails;
