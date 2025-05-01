import React, { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import axios from 'axios';
import { Card, Table, Button } from 'react-bootstrap';

const AgendamentoDetail = () => {
  const { id } = useParams();
  const [agendamento, setAgendamento] = useState(null);

  useEffect(() => {
    axios.get(`https://localhost:7215/api/Agendamentos/${id}`)
      .then(res => setAgendamento(res.data))
      .catch(err => console.error('Erro ao buscar agendamento', err));
  }, [id]);

  if (!agendamento) return <p className="text-center mt-5">Carregando detalhes...</p>;

  return (
    <div className="container mt-4">
      <Card>
        <Card.Body>
          <Card.Title>Agendamento #{agendamento.id}</Card.Title>
          <p><strong>Data:</strong> {new Date(agendamento.data).toLocaleDateString()}</p>
          <p><strong>Hora:</strong> {agendamento.hora}</p>
          <p><strong>Pontuação Total:</strong> {agendamento.pontuacaoTotal}</p>

          <h5>Itens da Coleta</h5>
          <Table striped bordered>
            <thead>
              <tr>
                <th>Produto</th>
                <th>Quantidade</th>
                <th>Estado</th>
                <th>Pontuação</th>
              </tr>
            </thead>
            <tbody>
              {agendamento.itensColeta.map((item, index) => (
                <tr key={index}>
                  <td>{item.produtoNome}</td>
                  <td>{item.quantidade}</td>
                  <td>{item.estado === 100 ? 'Doação' : 'Descarte'}</td>
                  <td>{item.pontuacao}</td>
                </tr>
              ))}
            </tbody>
          </Table>

          <Link to="/agendamentos">
            <Button variant="secondary">Voltar para a Lista</Button>
          </Link>
        </Card.Body>
      </Card>
    </div>
  );
};

export default AgendamentoDetail;
