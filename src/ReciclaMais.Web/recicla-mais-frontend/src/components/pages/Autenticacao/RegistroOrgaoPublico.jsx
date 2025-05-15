import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import './styles/Registro.css';
import apiBaseUrl from '../../../apiconfig';

const RegistroOrgaoPublico = () => {
  const [nome, setNome] = useState('');
  const [estado, setEstado] = useState('');
  const [cidade, setCidade] = useState('');
  const [bairro, setBairro] = useState('');
  const [rua, setRua] = useState('');
  const [cep, setCep] = useState('');
  const [numero, setNumero] = useState('');
  const [complemento, setComplemento] = useState('');
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [cnpj, setCnpj] = useState('');
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    const dataToSend = {
      nome,
      estado,
      cidade,
      bairro,
      rua,
      cep: parseInt(cep),
      numero: parseInt(numero),
      complemento,
      username,
      password,
      tipo: "OrgaoPublico",
      cnpj: String(cnpj),
    };

    console.log("Sending data:", dataToSend);

    try {
      await axios.post(`${apiBaseUrl}/Usuarios/registrar-orgaopublico`, dataToSend);
      alert("Órgão Público cadastrado com sucesso!");
      navigate("/");
    } catch (error) {
      alert("Erro ao cadastrar órgão público.");
      console.error(error);
      console.error(error.response?.data || error.message);
    }
  };

  return (
    <div className="form-container">
      <h2>Cadastro de Órgão Público</h2>
      <form onSubmit={handleSubmit} className="form">
        <label>Nome: <input value={nome} onChange={e => setNome(e.target.value)} required /></label>
        <label>Estado: <input value={estado} onChange={e => setEstado(e.target.value)} required /></label>
        <label>Cidade: <input value={cidade} onChange={e => setCidade(e.target.value)} required /></label>
        <label>Bairro: <input value={bairro} onChange={e => setBairro(e.target.value)} required /></label>
        <label>Rua: <input value={rua} onChange={e => setRua(e.target.value)} required /></label>
        <label>CEP: <input value={cep} onChange={e => setCep(e.target.value)} required /></label>
        <label>Número: <input value={numero} onChange={e => setNumero(e.target.value)} required /></label>
        <label>Complemento: <input value={complemento} onChange={e => setComplemento(e.target.value)} /></label>
        <label>Username: <input value={username} onChange={e => setUsername(e.target.value)} required /></label>
        <label>Senha: <input type="password" value={password} onChange={e => setPassword(e.target.value)} required /></label>
        <label>CNPJ: <input value={cnpj} onChange={e => setCnpj(e.target.value)} required /></label>
        <button type="submit">Cadastrar</button>
      </form>
    </div>
  );
};

export default RegistroOrgaoPublico;
