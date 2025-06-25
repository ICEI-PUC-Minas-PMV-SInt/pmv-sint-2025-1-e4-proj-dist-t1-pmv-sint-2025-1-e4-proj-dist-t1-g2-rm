import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import '../Produto/styles/ProductList.css';
import apiBaseUrl from '../../../apiconfig';

const BeneficiosList = () => {
  const [beneficios, setBeneficios] = useState([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    axios.get(`${apiBaseUrl}/beneficios`)
      .then(response => {
        setBeneficios(response.data);
        setLoading(false);
      })
      .catch(error => {
        console.error('Erro ao carregar benefícios:', error);
        setLoading(false);
      });
  }, []);

  const handleDelete = async (id) => {
    if (!window.confirm('Tem certeza que deseja excluir este benefício?')) return;

    try {
      await axios.delete(`${apiBaseUrl}/beneficios/${id}`);
      setBeneficios(beneficios.filter(b => b.id !== id));
      alert('Benefício excluído com sucesso.');
    } catch (error) {
      alert('Erro ao excluir benefício.');
      console.error(error);
    }
  };

  if (loading) return <p className="loading">Carregando benefícios...</p>;

  return (
    <div className="container">
      <h2 className="title">Lista de Benefícios</h2>
      <div className="button-container">
        <button className="create-button" onClick={() => navigate("/beneficios/criar")}>
          Criar Benefício
        </button>
      </div>
      <table className="table">
        <thead>
          <tr className="thead-row">
            <th className="th">ID</th>
            <th className="th">Nome</th>
            <th className="th">Pontos Necessários</th>
            <th className="th">Ações</th>
          </tr>
        </thead>
        <tbody>
          {beneficios.map(beneficio => (
            <tr key={beneficio.id}>
              <td className="td">{beneficio.id}</td>
              <td className="td">{beneficio.titulo}</td>
              <td className="td">{beneficio.pontosNecessarios}</td>
              <td className="td">
                <button onClick={() => navigate(`/beneficios/detalhes/${beneficio.id}`)} className="action-button">
                  Detalhes
                </button>
                <button onClick={() => navigate(`/beneficios/editar/${beneficio.id}`)} className="action-button">
                  Editar
                </button>
                <button onClick={() => handleDelete(beneficio.id)} className="action-button" style={{ backgroundColor: "#dc3545" }}>
                  Excluir
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default BeneficiosList;
