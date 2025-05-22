import React, { useState } from 'react';
import axios from 'axios';
import './styles/FaleConosco.css';
import apiBaseUrl from '../../../apiconfig';

function FaleConoscoCreate() {
  const [nome, setNome] = useState('');
  const [email, setEmail] = useState('');
  const [telefone, setTelefone] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.post(`${apiBaseUrl}/faleconosco`, {
        nome,
        email,
        telefone
      });
      alert('Mensagem enviada com sucesso!');
      setNome('');
      setEmail('');
      setTelefone('');
    } catch (error) {
      alert('Erro ao enviar a mensagem.');
      console.error(error);
    }
  };

  return (
    <div className="container">
      <h2>Fale Conosco</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Nome:</label><br />
          <input type="text" value={nome} onChange={(e) => setNome(e.target.value)} required />
        </div>
        <div>
          <label>Email:</label><br />
          <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} required />
        </div>
        <div>
          <label>Telefone:</label><br />
          <input type="text" value={telefone} onChange={(e) => setTelefone(e.target.value)} required />
        </div>
        <button type="submit">Enviar</button>
      </form>
    </div>
  );
}

export default FaleConoscoCreate;
