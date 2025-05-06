import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";

const ProductUpdate = () => {
  const { id } = useParams(); 
  const [produto, setProduto] = useState(null);
  const [nome, setNome] = useState("");
  const [descricao, setDescricao] = useState("");
  const [pontuacao, setPontuacao] = useState("");
  const [erro, setErro] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    axios
      .get(`https://localhost:7215/api/Produtos/${id}`)
      .then((response) => {
        setProduto(response.data);
        setNome(response.data.nome);
        setDescricao(response.data.descricao);
        setPontuacao(response.data.pontuacao);
        setErro("");
      })
      .catch((error) => {
        console.error("Erro ao buscar produto:", error);
        setErro("Produto não encontrado.");
      });
  }, [id]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!nome || !descricao || !pontuacao) {
      setErro("Por favor, preencha todos os campos.");
      return;
    }

    if (isNaN(pontuacao) || pontuacao <= 0) {
      setErro("A pontuação deve ser um número válido maior que zero.");
      return;
    }

    try {
      await axios.put(`https://localhost:7215/api/Produtos/${id}`, {
        id: parseInt(id), // <-- This is the fix
        nome,
        descricao,
        pontuacao: parseInt(pontuacao),
      });

      alert("Produto atualizado com sucesso!");
      navigate("/");
    } catch (error) {
      console.error("Erro ao atualizar o produto:", error);
      setErro("Erro ao atualizar o produto. Tente novamente.");
    }
  };

  return (
    <div className="get-container">
      <h2>Atualizar Produto</h2>

      {erro && <p className="erro">{erro}</p>}

      {/* Form para atualizar o produto */}
      {produto && (
        <form onSubmit={handleSubmit} className="form">
          <label>
            Nome:
            <input
              type="text"
              value={nome}
              onChange={(e) => setNome(e.target.value)}
              required
            />
          </label>

          <label>
            Descrição:
            <input
              type="text"
              value={descricao}
              onChange={(e) => setDescricao(e.target.value)}
              required
            />
          </label>

          <label>
            Pontuação:
            <input
              type="number"
              value={pontuacao}
              onChange={(e) => setPontuacao(e.target.value)}
              required
            />
          </label>

          <button type="submit">Salvar</button>
        </form>
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

export default ProductUpdate;
