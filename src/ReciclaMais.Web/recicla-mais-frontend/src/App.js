import './App.css';
import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Homepage from './components/pages/Homepage';
import ProductList from './components/pages/Produto/ProductList';
import ProductDetails from './components/pages/Produto/ProductDetails';
import ProductCreate from "./components/pages/Produto/ProductCreate";
import ProductUpdate from "./components/pages/Produto/ProductUpdate";
import AgendamentoCreate from './components/pages/Agendamento/AgendamentoCreate.jsx';
import AgendamentoList from './components/pages/Agendamento/AgendamentoList.jsx';
import AgendamentoEdit from './components/pages/Agendamento/AgendamentoEdit.jsx';
import Navbar from './components/layout/Navbar.jsx';
import NoticiasList from './components/pages/Noticias/NoticiasList';
import NoticiasCreate from './components/pages/Noticias/NoticiasCreate';
import NoticiasEdit from './components/pages/Noticias/NoticiasEdit'
import NoticiasDetails from './components/pages/Noticias/NoticiasDetails';
import NoticiasDelete from './components/pages/Noticias/NoticiasDelete';
import CadastroEscolha from './components/pages/Autenticacao/CadastroEscolha.jsx';
import RegistroAdministrador from './components/pages/Autenticacao/RegistroAdministrador.jsx';
import RegistroMunicipe from './components/pages/Autenticacao/RegistroMunicipe.jsx';
import RegistroOrgaoPublico from './components/pages/Autenticacao/RegistroOrgaoPublico.jsx';
import Login from './components/pages/Autenticacao/Login.jsx';
import FaleConoscoCreate from './components/pages/FaleConosco/FaleConoscoCreate.jsx';
import FaleConoscoDetails from './components/pages/FaleConosco/FaleConoscoDetails.jsx';
import FaleConoscoEdit from './components/pages/FaleConosco/FaleConoscoEdit.jsx';
import FaleConoscoDelete from './components/pages/FaleConosco/FaleConoscoDelete.jsx';
import FaleConoscoList from './components/pages/FaleConosco/FaleConoscoList.jsx';




function App() {
  return (
    <Router>
      <Navbar />
      <div className="App">
        <Routes>
          <Route path="/" element={<Homepage />} />
          <Route path="/produtos" element={<ProductList />} />
          <Route path="/detalhes/:id" element={<ProductDetails />} />
          <Route path="/criar" element={<ProductCreate />} />
          <Route path="/editar/:id" element={<ProductUpdate />} />
          <Route path="/agendamento" element={<AgendamentoCreate />} />
          <Route path="/agendamentos" element={<AgendamentoList />} />
          <Route path="/agendamento/:id" element={<AgendamentoEdit />} />
          <Route path="/noticias" element={<NoticiasList />} />
          <Route path="/noticias/criar" element={<NoticiasCreate />} />
          <Route path="/noticias/editar/:id" element={<NoticiasEdit />} />
          <Route path="/noticias/detalhes/:id" element={<NoticiasDetails />} />
          <Route path="/noticias/excluir/:id" element={<NoticiasDelete />} />

          <Route path="/cadastro-municipe" element={<RegistroMunicipe />} />
          <Route path="/cadastro-administrador" element={<RegistroAdministrador />} />
          <Route path="/cadastro-orgao-publico" element={<RegistroOrgaoPublico />} />
          <Route path="/cadastro" element={<CadastroEscolha />} />
          <Route path="/login" element={<Login onLoginSuccess={() => { /* your login success handler or redirect here */ }} />} />

          <Route path="/faleconosco" element={<FaleConoscoList />} />
          <Route path="/faleconosco/criar" element={<FaleConoscoCreate />} />
          <Route path="/faleconosco/:id/detalhe" element={<FaleConoscoDetails />} />
          <Route path="/faleconosco/:id/editar" element={<FaleConoscoEdit />} />
          <Route path="/faleconosco/:id/excluir" element={<FaleConoscoDelete />} />

        </Routes>
      </div>
    </Router>
  );
}

export default App;

