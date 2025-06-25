import React, { useState } from 'react';
import './styles/Registro.css';
import apiBaseUrl from '../../../apiconfig';

function Login({ onLoginSuccess }) {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [success, setSuccess] = useState(''); //viewbag

  async function handleSubmit(e) {
    e.preventDefault();

    setError('');   //limpar setError
    setSuccess('');  //limpar setSuccess

    const response = await fetch(`${apiBaseUrl}/usuarios/authenticate`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ username, password }),
    });

    if (!response.ok) {
      setError('Usuário ou senha inválidos');
      return;
    }

    const data = await response.json();
    localStorage.setItem('token', data.token);
    
    setSuccess('Usuário encontrado! Login realizado com sucesso.');
    onLoginSuccess();
  }

  return (
    <div className="form-container">
      <h2>Login</h2>
      <form onSubmit={handleSubmit} className="form">
        <label>Usuário</label>
        <input
          type="text"
          placeholder="Digite seu usuário"
          value={username}
          onChange={e => setUsername(e.target.value)}
          required
        />
        <label>Senha</label>
        <input
          type="password"
          placeholder="Digite sua senha"
          value={password}
          onChange={e => setPassword(e.target.value)}
          required
        />
        <button type="submit">Entrar</button>
        {error && <p style={{ color: 'red' }}>{error}</p>}
        {success && <p style={{ color: 'green' }}>{success}</p>}
      </form>
    </div>
  );
}

export default Login;
