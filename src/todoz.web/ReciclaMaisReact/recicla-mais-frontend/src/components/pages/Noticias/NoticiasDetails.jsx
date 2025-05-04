import React, { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import axios from 'axios';
import './styles/Noticias.css';

const NoticiasDetails = () => {
  const { id } = useParams();
  const [noticia, setNoticia] = useState(null);

  useEffect(() => {
    axios.get(`https://localhost:7215/api/noticias/${id}`)
      .then(response => setNoticia(response.data))
      .catch(error => {
        console.error('Erro ao carregar os detalhes da notícia:', error);
        alert('Notícia não encontrada.');
      });
  }, [id]);

  if (!noticia) return <p>Carregando...</p>;

  return (
    <div className="container">
      <h2>{noticia.titulo}</h2>
      <p><strong>Autor:</strong> {noticia.autor}</p>
      <p>{noticia.conteudo}</p>
      <p><em>Publicado em: {new Date(noticia.dataPublicacao).toLocaleDateString()}</em></p>
      {noticia.imagemUrl && (
        <img src={noticia.imagemUrl} alt="Imagem da notícia" style={{ maxWidth: '100%', marginTop: '20px' }} />
      )}
      <br /><br />
      <Link to="/noticias">
        <button>Voltar para lista</button>
      </Link>
    </div>
  );
};

export default NoticiasDetails;
