import React, { useEffect, useState } from 'react';
import { useParams, useNavigate, Link } from 'react-router-dom';
import axios from 'axios';
import './styles/Beneficios.css';
import apiBaseUrl from '../../../apiconfig';

const BeneficiosDelete = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [beneficio, setBeneficio] = useState(null);

  useEffect(() => {
    axios.get(`${apiBaseUrl}/beneficios/${id}`)
      .then(response => setBeneficio(response.data))
      .catch(error => {
        console.error('Erro ao carregar benefício:', error);
        alert('Benefício não encontrado.');
        navigate('/beneficios');
      });
  }, [id, navigate]);

  const handleDelete = async () => {
    try {
      await axios.delete(`${apiBaseUrl}/beneficios/${id}`);
      alert('Benefício excluído com sucesso!');
      navigate('/beneficios');
    } catch (error) {
      alert('Erro ao excluir o benefício.');
      console.error(error);
    }
  };

  if (!beneficio) return <p>Carregando...</p>;

  return (
    <div className="container">
      <h2>Confirmar Exclusão</h2>
      <p>Tem certeza que deseja excluir o benefício <strong>{beneficio.titulo}</strong>?</p>
      <button onClick={handleDelete} style={{ backgroundColor: 'red' }}>Confirmar Exclusão</button>
      <Link to="/beneficios"><button style={{ marginLeft: '10px' }}>Cancelar</button></Link>
    </div>
  );
};

export default BeneficiosDelete;
