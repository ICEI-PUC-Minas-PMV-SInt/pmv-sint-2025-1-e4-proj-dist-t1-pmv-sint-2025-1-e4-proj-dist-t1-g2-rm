import React, { useEffect, useState } from 'react';
import { useParams, useNavigate, Link } from 'react-router-dom';
import axios from 'axios';
import './styles/Noticias.css';

const NoticiasDelete = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [noticia, setNoticia] = useState(null);

  useEffect(() => {
    axios.get(`http://localhost:7215/api/noticias/${id}`)
      .then(response => setNoticia(response.data))
      .catch(error => {
        console.error('Erro ao carregar notícia:', error);
        alert('Notícia não encontrada.');
        navigate('/noticias');
      });
  }, [id, navigate]);

  const handleDelete = async () => {
    try {
      await axios.delete(`http://localhost:5000/api/noticias/${id}`);
      alert('Notícia excluída com sucesso!');
      navigate('/noticias');
    } catch (error) {
      alert('Erro ao excluir a notícia.');
      console.error(error);
    }
  };

  if (!noticia) return <p>Carregando...</p>;

  return (
    <div className="container">
      <h2>Confirmar Exclusão</h2>
      <p>Tem certeza que deseja excluir a seguinte notícia?</p>
      <h3>{noticia.titulo}</h3>
      <p><strong>Autor:</strong> {noticia.autor}</p>
      <p>{noticia.conteudo}</p>

      <button onClick={handleDelete} style={{ backgroundColor: 'red' }}>
        Confirmar Exclusão
      </button>
      <Link to="/noticias">
        <button style={{ marginLeft: '10px' }}>Cancelar</button>
      </Link>
    </div>
  );
};

export default NoticiasDelete;
