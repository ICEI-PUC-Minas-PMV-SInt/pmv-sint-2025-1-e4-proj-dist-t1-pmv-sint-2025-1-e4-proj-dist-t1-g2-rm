import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import '../Produto/styles/ProductList.css';     // Reutilizando o estilo do ProductList

const NoticiasList = () => {
  const [noticias, setNoticias] = useState([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    axios.get('https://localhost:7215/api/noticias')
      .then(response => {
        setNoticias(response.data);
        setLoading(false);
      })
      .catch(error => {
        console.error('Erro ao carregar notícias:', error);
        setLoading(false);
      });
  }, []);

  const handleDelete = async (id) => {
    if (!window.confirm('Tem certeza que deseja excluir esta notícia?')) return;

    try {
      await axios.delete(`https://localhost:7215/api/noticias/${id}`);
      setNoticias(noticias.filter(n => n.id !== id));
      alert('Notícia excluída com sucesso.');
    } catch (error) {
      alert('Erro ao excluir notícia.');
      console.error(error);
    }
  };

  if (loading) return <p className="loading">Carregando notícias...</p>;

  return (
    <div className="container">
      <h2 className="title">Lista de Notícias</h2>

      <div className="button-container">
        <button
          className="create-button"
          onClick={() => navigate("/noticias/criar")}
        >
          Criar Notícia
        </button>
      </div>

      <table className="table">
        <thead>
          <tr className="thead-row">
            <th className="th">ID</th>
            <th className="th">Título</th>
            <th className="th">Autor</th>
            <th className="th">Publicado em</th>
            <th className="th">Ações</th>
          </tr>
        </thead>
        <tbody>
          {noticias.map((noticia) => (
            <tr key={noticia.id}>
              <td className="td">{noticia.id}</td>
              <td className="td">{noticia.titulo}</td>
              <td className="td">{noticia.autor}</td>
              <td className="td">{new Date(noticia.dataPublicacao).toLocaleDateString()}</td>
              <td className="td">
                <button
                  onClick={() => navigate(`/noticias/detalhes/${noticia.id}`)}
                  className="action-button"
                >
                  Detalhes
                </button>
                <button
                  onClick={() => navigate(`/noticias/editar/${noticia.id}`)}
                  className="action-button"
                >
                  Atualizar
                </button>
                <button
                  onClick={() => handleDelete(noticia.id)}
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

export default NoticiasList;
