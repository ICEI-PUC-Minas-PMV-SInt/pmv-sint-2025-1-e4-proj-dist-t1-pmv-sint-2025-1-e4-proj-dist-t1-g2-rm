import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Row, Col, Pagination } from 'react-bootstrap';
import AgendamentoItem from './AgendamentoItem';
import './Agendamento.css';

const AgendamentoList = () => {
  const [agendamentos, setAgendamentos] = useState([]);
  const [paginaAtual, setPaginaAtual] = useState(1);
  const itensPorPagina = 9;

  useEffect(() => {
    axios.get('https://localhost:7215/api/Agendamentos')
      .then(res => setAgendamentos(res.data))
      .catch(err => console.error('Erro ao buscar agendamentos', err));
  }, []);

  const totalPaginas = Math.ceil(agendamentos.length / itensPorPagina);

  const agendamentosPaginados = agendamentos.slice(
    (paginaAtual - 1) * itensPorPagina,
    paginaAtual * itensPorPagina
  );

  const mudarPagina = (numero) => {
    if (numero >= 1 && numero <= totalPaginas) {
      setPaginaAtual(numero);
    }
  };

  return (
    <div className="container mt-4">
      <h3 className="mb-4 text-center">Todos os Agendamentos</h3>

      <Row className="g-4">
        {agendamentosPaginados.map(ag => (
          <Col key={ag.id} md={4}>
            <AgendamentoItem
              id={ag.id}
              data={ag.data}
              hora={ag.hora}
              pontuacaoTotal={ag.pontuacaoTotal}
            />
          </Col>
        ))}
      </Row>

      <Pagination className="justify-content-center mt-4">
        <Pagination.First onClick={() => mudarPagina(1)} disabled={paginaAtual === 1} />
        <Pagination.Prev onClick={() => mudarPagina(paginaAtual - 1)} disabled={paginaAtual === 1} />
        {[...Array(totalPaginas)].map((_, i) => (
          <Pagination.Item
            key={i + 1}
            active={i + 1 === paginaAtual}
            onClick={() => mudarPagina(i + 1)}
          >
            {i + 1}
          </Pagination.Item>
        ))}
        <Pagination.Next onClick={() => mudarPagina(paginaAtual + 1)} disabled={paginaAtual === totalPaginas} />
        <Pagination.Last onClick={() => mudarPagina(totalPaginas)} disabled={paginaAtual === totalPaginas} />
      </Pagination>
    </div>
  );
};

export default AgendamentoList;
