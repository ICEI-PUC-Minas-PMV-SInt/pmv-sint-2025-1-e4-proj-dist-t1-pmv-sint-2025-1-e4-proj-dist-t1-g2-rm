import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";
import apiBaseUrl from '../../../apiconfig';


const ProductDetails = () => {
  const { id } = useParams();
  const [produto, setProduto] = useState(null);
  const [erro, setErro] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    axios
      .get(`${apiBaseUrl}/Produtos/${id}`)
      .then((response) => {
        setProduto(response.data);
        setErro("");
      })
      .catch((error) => {
        console.error("Erro ao buscar produto:", error);
        setErro("Produto não encontrado.");
      });
  }, [id]);

  return (
    <div className="get-container">
      <h2>Detalhes do Produto</h2>

      {erro && <p className="erro">{erro}</p>}

      {produto && (
        <div className="produto-info">
          <p>
            <strong>ID:</strong> {produto.id}
          </p>
          <p>
            <strong>Nome:</strong> {produto.nome}
          </p>
          <p>
            <strong>Descrição:</strong> {produto.descricao}
          </p>
          <p>
            <strong>Pontuação:</strong> {produto.pontuacao}
          </p>
        </div>
      )}

      <button
        onClick={() => navigate(-1)}
        className="buscar-button"
        style={{ marginTop: "20px" }}
      >
        Voltar
      </button>
    </div>
  );
};

export default ProductDetails;
