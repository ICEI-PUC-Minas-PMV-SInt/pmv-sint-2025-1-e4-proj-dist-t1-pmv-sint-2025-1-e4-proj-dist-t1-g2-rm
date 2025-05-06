import React from 'react';
import { Link } from 'react-router-dom';
import './Navbar.css';
import logo from '../../assets/logo-reciclamais.png';
import perfil from '../../assets/imagem-usuario.png';

function Navbar() {
  return (
    <nav className="navbar">
      <div className="navbar_logo">
        <Link to="/">
          <img src={logo} alt="Logo ReciclaMais" />
        </Link>
      </div>

      <div className="navbar_links">
        <Link to="/">Home</Link>
        <Link to="/noticias">Notícias</Link>
        <Link to="/agendamento">Agendamento</Link>
      </div>

      <div className="navbar_usuario">
        <Link to="/login">
          <img src={perfil} alt="Perfil do Usuário" />
        </Link>
      </div>
    </nav>
  );
}

export default Navbar;