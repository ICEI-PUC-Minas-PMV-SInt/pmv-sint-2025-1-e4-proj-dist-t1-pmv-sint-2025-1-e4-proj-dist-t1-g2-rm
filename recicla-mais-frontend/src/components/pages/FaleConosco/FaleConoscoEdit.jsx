import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';
import apiBaseUrl from '../../../apiconfig';
import './styles/FaleConosco.css';


function FaleConoscoEdit() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [contato, setContato] = useState({
    nome: '',
    email: '',
    telefone: ''
  });

  useEffect(() => {
    axios.get(`${apiBaseUrl}/faleconosco/${id}`)
      .then((response) => setContato(response.data))
      .catch((error) => console.error('Erro ao buscar contato:', error));
  }, [id]);

  const handleChange = (e) => {
    setContato({ ...contato, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.put(`${apiBaseUrl}/faleconosco/${id}`, contato);
      alert('Contato atualizado com sucesso!');
      navigate('/faleconosco'); // redireciona para a lista
    } catch (error) {
      alert('Erro ao atualizar o contato.');
      console.error(error);
    }
  };

  return (
    <div>
      <h2>Editar Contato</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Nome:</label><br />
          <input name="nome" type="text" value={contato.nome} onChange={handleChange} required />
        </div>
        <div>
          <label>Email:</label><br />
          <input name="email" type="email" value={contato.email} onChange={handleChange} required />
        </div>
        <div>
          <label>Telefone:</label><br />
          <input name="telefone" type="text" value={contato.telefone} onChange={handleChange} required />
        </div>
        <button type="submit">Salvar</button>
      </form>
    </div>
  );
}

export default FaleConoscoEdit;
