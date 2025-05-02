import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';
import './styles/Noticias.css';

const NoticiasEdit = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const [titulo, setTitulo] = useState('');
  const [conteudo, setConteudo] = useState('');
  const [autor, setAutor] = useState('');

  useEffect(() => {
    axios.get(`http://localhost:7215/api/noticias/${id}`)
      .then(response => {
        const noticia = response.data;
        setTitulo(noticia.titulo);
        setConteudo(noticia.conteudo);
        setAutor(noticia.autor);
      })
      .catch(error => console.error('Erro ao carregar notícia:', error));
  }, [id]);

  const handleUpdate = async (e) => {
    e.preventDefault();
    try {
      await axios.put(`http://localhost:5000/api/noticias/${id}`, {
        id,
        titulo,
        conteudo,
        autor,
        dataPublicacao: new Date().toISOString()
      });
      alert('Notícia atualizada com sucesso!');
      navigate('/noticias');
    } catch (error) {
      alert('Erro ao atualizar notícia.');
      console.error(error);
    }
  };

  return (
    <div className="container">
      <h2>Editar Notícia</h2>
      <form onSubmit={handleUpdate}>
        <div>
          <label>Título:</label><br />
          <input
            type="text"
            value={titulo}
            onChange={(e) => setTitulo(e.target.value)}
            required
          />
        </div>
        <div>
          <label>Conteúdo:</label><br />
          <textarea
            value={conteudo}
            onChange={(e) => setConteudo(e.target.value)}
            required
          />
        </div>
        <div>
          <label>Autor:</label><br />
          <input
            type="text"
            value={autor}
            onChange={(e) => setAutor(e.target.value)}
            required
          />
        </div>
        <button type="submit">Salvar Alterações</button>
      </form>
    </div>
  );
};

export default NoticiasEdit;
