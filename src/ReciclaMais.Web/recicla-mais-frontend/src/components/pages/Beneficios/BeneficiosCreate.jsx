import React, { useState } from 'react';
import axios from 'axios';
import './styles/Beneficios.css';
import apiBaseUrl from '../../../apiconfig';

const BeneficiosCreate = () => {
  const [nome, setNome] = useState('');
  const [descricao, setDescricao] = useState('');
  const [pontosNecessarios, setPontosNecessarios] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.post(`${apiBaseUrl}/beneficios`, {
        Titulo: nome,
  Descricao: descricao,
  PontosNecessarios: parseInt(pontosNecessarios)
      });
      alert('Benefício criado com sucesso!');
      setNome('');
      setDescricao('');
      setPontosNecessarios('');
    } catch (error) {
      alert('Erro ao criar benefício.');
      console.error(error);
    }
  };

  return (
    <div className="container">
      <h2>Criar Novo Benefício</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Nome:</label><br />
          <input type="text" value={nome} onChange={e => setNome(e.target.value)} required />
        </div>
        <div>
          <label>Descrição:</label><br />
          <textarea value={descricao} onChange={e => setDescricao(e.target.value)} required />
        </div>
        <div>
          <label>Pontos Necessários:</label><br />
          <input type="number" value={pontosNecessarios} onChange={e => setPontosNecessarios(e.target.value)} required />
        </div>
        <button type="submit">Salvar</button>
      </form>
    </div>
  );
};

export default BeneficiosCreate;
