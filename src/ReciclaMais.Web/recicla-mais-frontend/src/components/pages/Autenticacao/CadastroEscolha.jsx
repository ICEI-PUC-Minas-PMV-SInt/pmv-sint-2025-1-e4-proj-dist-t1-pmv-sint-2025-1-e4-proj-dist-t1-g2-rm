import React from 'react';
import { Link } from 'react-router-dom';
import './styles/Registro.css'; 

const CadastroEscolha = () => {
  return (
    <div className="form-container">
      <h2>Escolha o tipo de cadastro</h2>
      <div className="form">
        <Link to="/cadastro-municipe" className="button">
          Munícipe
        </Link>
        <Link to="/cadastro-administrador" className="button">
          Administrador
        </Link>
        <Link to="/cadastro-orgao-publico" className="button">
          Órgão Público
        </Link>
      </div>
    </div>
  );
};

export default CadastroEscolha;
