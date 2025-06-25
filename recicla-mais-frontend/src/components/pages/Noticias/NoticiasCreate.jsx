import React, { useState } from 'react';
import axios from 'axios';
import './styles/Noticias.css';
import apiBaseUrl from '../../../apiconfig';


const NoticiasCreate = () => {
  const [titulo, setTitulo] = useState('');
  const [conteudo, setConteudo] = useState('');
  const [autor, setAutor] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.post(`${apiBaseUrl}/noticias`, {
        titulo,
        conteudo,
        autor
      });
      alert('Notícia cadastrada com sucesso!');
      setTitulo('');
      setConteudo('');
      setAutor('');
    } catch (error) {
      alert('Erro ao cadastrar notícia.');
      console.error(error);
    }
  };

  return (
    <div className="container">
      <h2>Cadastrar Nova Notícia</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Título:</label><br />
          <input type="text" value={titulo} onChange={(e) => setTitulo(e.target.value)} required />
        </div>
        <div>
          <label>Conteúdo:</label><br />
          <textarea value={conteudo} onChange={(e) => setConteudo(e.target.value)} required />
        </div>
        <div>
          <label>Autor:</label><br />
          <input type="text" value={autor} onChange={(e) => setAutor(e.target.value)} required />
        </div>
        <button type="submit">Salvar</button>
      </form>
    </div>
  );
};

export default NoticiasCreate;
