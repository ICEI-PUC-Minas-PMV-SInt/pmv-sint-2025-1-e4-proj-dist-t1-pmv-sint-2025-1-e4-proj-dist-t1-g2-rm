import React, { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import "./styles/ProductCreate.css"; 
import apiBaseUrl from '../../../apiconfig';

const ProductCreate = () => {
  const [nome, setNome] = useState("");
  const [descricao, setDescricao] = useState("");
  const [pontuacao, setPontuacao] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      await axios.post(`${apiBaseUrl}/Produtos`, {
        nome,
        descricao,
        pontuacao: parseInt(pontuacao),
      });

      alert("Produto criado com sucesso!");
      navigate("/"); // Redireciona de volta pra lista de produtos
    } catch (error) {
      alert("Erro ao criar produto.");
      console.error(error);
    }
  };

  return (
    <div className="form-container">
      <h2>Cadastrar Produto</h2>
      <form onSubmit={handleSubmit} className="form">
        <label>
          Nome:
          <input value={nome} onChange={(e) => setNome(e.target.value)} required />
        </label>
        <label>
          Descrição:
          <input value={descricao} onChange={(e) => setDescricao(e.target.value)} required />
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
    </div>
  );
};

export default ProductCreate;
