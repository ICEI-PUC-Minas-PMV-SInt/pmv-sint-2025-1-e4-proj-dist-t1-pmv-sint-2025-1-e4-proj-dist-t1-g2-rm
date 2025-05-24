import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';
import './styles/Beneficios.css';
import apiBaseUrl from '../../../apiconfig';

const BeneficiosEdit = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [titulo, setTitulo] = useState('');
  const [descricao, setDescricao] = useState('');
  const [pontosNecessarios, setPontosNecessarios] = useState('');

  useEffect(() => {
    axios.get(`${apiBaseUrl}/beneficios/${id}`)
      .then(res => {
        const b = res.data;
        setTitulo(b.titulo);
        setDescricao(b.descricao);
        setPontosNecessarios(b.pontosNecessarios);
      })
      .catch(err => console.error('Erro ao carregar benefício:', err));
  }, [id]);

  const handleUpdate = async (e) => {
    e.preventDefault();
    try {
      await axios.put(`${apiBaseUrl}/beneficios/${id}`, {
        Id: id,
        Titulo: titulo,
        Descricao: descricao,
        PontosNecessarios: parseInt(pontosNecessarios)
      });
      alert('Benefício atualizado com sucesso!');
      navigate('/beneficios');
    } catch (error) {
      alert('Erro ao atualizar benefício.');
      console.error(error);
    }
  };

  return (
    <div className="container">
      <h2>Editar Benefício</h2>
      <form onSubmit={handleUpdate}>
        <div>
          <label>titulo:</label><br />
          <input type="text" value={titulo} onChange={e => setTitulo(e.target.value)} required />
        </div>
        <div>
          <label>Descrição:</label><br />
          <textarea value={descricao} onChange={e => setDescricao(e.target.value)} required />
        </div>
        <div>
          <label>Pontos Necessários:</label><br />
          <input type="number" value={pontosNecessarios} onChange={e => setPontosNecessarios(e.target.value)} required />
        </div>
        <button type="submit">Salvar Alterações</button>
      </form>
    </div>
  );
};

export default BeneficiosEdit;
