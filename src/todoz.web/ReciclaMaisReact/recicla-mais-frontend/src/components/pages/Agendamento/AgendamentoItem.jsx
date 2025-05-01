import React from 'react';
import { Card, Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import './AgendamentoItem.css';

const AgendamentoItem = ({ id, data, hora, pontuacaoTotal }) => {
  return (
    <Card className="agendamento-item mx-3">
      <Card.Body>
        <Card.Title>Agendamento #{id}</Card.Title>
        <Card.Text>
          <strong>Data:</strong> {new Date(data).toLocaleDateString()} <br />
          <strong>Hora:</strong> {hora} <br />
          <strong>Pontuação:</strong> {pontuacaoTotal}
        </Card.Text>
        <Link to={`/agendamento/${id}`}>
          <Button className="btn-verde">Ver Detalhes</Button>
        </Link>
      </Card.Body>
    </Card>
  );
};

export default AgendamentoItem;
