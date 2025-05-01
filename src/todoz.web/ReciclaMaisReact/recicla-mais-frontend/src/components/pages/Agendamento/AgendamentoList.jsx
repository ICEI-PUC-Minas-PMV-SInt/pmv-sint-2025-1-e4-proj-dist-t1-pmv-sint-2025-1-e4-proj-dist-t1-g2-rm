import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import { Card, Button, Pagination } from 'react-bootstrap';

const AgendamentoList = () => {
  const [agendamentos, setAgendamentos] = useState([]);
  const [paginaAtual, setPaginaAtual] = useState(1);
  const itensPorPagina = 10;

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
      <h3 className="mb-4">Todos os Agendamentos</h3>

      {agendamentosPaginados.map(ag => (
        <Card key={ag.id} className="mb-3">
          <Card.Body>
            <Card.Title>Agendamento #{ag.id}</Card.Title>
            <Card.Text>
              <strong>Data:</strong> {new Date(ag.data).toLocaleDateString()} <br />
              <strong>Hora:</strong> {ag.hora} <br />
              <strong>Pontuação:</strong> {ag.pontuacaoTotal}
            </Card.Text>
                <Link to={`/agendamento/${ag.id}`}>
                    <Button variant="primary">Ver Detalhes</Button>
                </Link>
          </Card.Body>
        </Card>
      ))}

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
