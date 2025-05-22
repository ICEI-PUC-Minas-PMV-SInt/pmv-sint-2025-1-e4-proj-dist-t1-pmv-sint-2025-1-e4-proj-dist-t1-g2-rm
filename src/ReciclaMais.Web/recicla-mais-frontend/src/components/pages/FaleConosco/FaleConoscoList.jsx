// ./components/pages/FaleConosco/FaleConoscoList.jsx
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import './styles/FaleConosco.css'; 
import apiBaseUrl from '../../../apiconfig';

const FaleConoscoList = () => {
  const [mensagens, setMensagens] = useState([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    axios.get(`${apiBaseUrl}/faleconosco`)
      .then(response => {
        setMensagens(response.data);
        setLoading(false);
      })
      .catch(error => {
        console.error('Erro ao carregar mensagens:', error);
        setLoading(false);
      });
  }, []);

  const handleDelete = async (id) => {
    if (!window.confirm('Tem certeza que deseja excluir esta mensagem?')) return;

    try {
      await axios.delete(`${apiBaseUrl}/faleconosco/${id}`);
      setMensagens(mensagens.filter(m => m.id !== id));
      alert('Mensagem excluída com sucesso.');
    } catch (error) {
      alert('Erro ao excluir mensagem.');
      console.error(error);
    }
  };

  if (loading) return <p className="loading">Carregando mensagens...</p>;

  return (
    <div className="container">
      <h2 className="title">Mensagens - Fale Conosco</h2>

      <div className="button-container">
        <button
          className="create-button"
          onClick={() => navigate("/faleconosco/criar")}
        >
          Nova Mensagem
        </button>
      </div>

      <table className="table">
        <thead>
          <tr className="thead-row">
            <th className="th">ID</th>
            <th className="th">Nome</th>
            <th className="th">Email</th>
            <th className="th">Telefone</th>
            
          </tr>
        </thead>
        <tbody>
          {mensagens.map((msg) => (
            <tr key={msg.id}>
              <td className="td">{msg.id}</td>
              <td className="td">{msg.nome}</td>
              <td className="td">{msg.email}</td>
              <td className="td">{msg.telefone}</td>
              <td className="td">
                <button
                  onClick={() => navigate(`/faleconosco/${msg.id}/detalhe`)}
                  className="action-button"
                >
                  Detalhes
                </button>
                <button
                  onClick={() => navigate(`/faleconosco/${msg.id}/editar`)}
                  className="action-button"
                >
                  Atualizar
                </button>
                <button
                  onClick={() => handleDelete(msg.id)}
                  className="action-button"
                  style={{ backgroundColor: "#dc3545" }}
                >
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

export default FaleConoscoList;

