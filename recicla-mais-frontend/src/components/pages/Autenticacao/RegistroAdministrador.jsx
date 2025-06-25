import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import './styles/Registro.css';
import apiBaseUrl from '../../../apiconfig';

const RegistroAdministrador = () => {
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
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    // Preparing the data to send to the backend
    const dataToSend = {
      nome,
      estado,
      cidade,
      bairro,
      rua,
      cep: parseInt(cep), // Ensure it's an integer
      numero: parseInt(numero), // Ensure it's an integer
      complemento,
      username,
      password,
      tipo: "Administrador", // Set the type to "Administrador"
    };

    // Log the data you're sending to verify
    console.log("Sending data:", dataToSend);

    try {
      await axios.post(`${apiBaseUrl}/Usuarios/registrar-administrador`, dataToSend);
      alert("Administrador cadastrado com sucesso!");
      navigate("/"); // Navigate to a different page (e.g., home or list)
    } catch (error) {
      alert("Erro ao cadastrar administrador.");
      console.error(error);
      console.error(error.response?.data || error.message); // Exibe detalhes do erro
    }
  };

  return (
    <div className="form-container">
      <h2>Cadastro de Administrador</h2>
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
        <button type="submit">Cadastrar</button>
      </form>
    </div>
  );
};

export default RegistroAdministrador;
