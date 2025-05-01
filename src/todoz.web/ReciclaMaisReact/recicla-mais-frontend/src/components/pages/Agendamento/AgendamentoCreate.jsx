import React, { useEffect, useState } from 'react';
import axios from 'axios';
import Datetime from 'react-datetime';
import "react-datetime/css/react-datetime.css";
import { Form, Button, Row, Col, Card } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import './Agendamento.css';

function AgendamentoCreate() {
  const [data, setData] = useState(null);
  const [produtos, setProdutos] = useState([]);
  const [itensColeta, setItensColeta] = useState([{ produtoId: '', quantidade: '', estado: 100 }]);
  const [pontuacaoTotal, setPontuacaoTotal] = useState(0);
  const navigate = useNavigate(); // USO DO NAVIGATE.

  useEffect(() => {
    axios.get('https://localhost:7215/api/Produtos')
      .then(res => setProdutos(res.data))
      .catch(err => console.error('Erro ao carregar produtos', err));
  }, []);

  useEffect(() => {
    let total = 0;
    for (const item of itensColeta) {
      const produto = produtos.find(p => p.id === parseInt(item.produtoId));
      if (produto && item.quantidade && item.estado) {
        total += (produto.pontuacao * parseInt(item.quantidade) * parseInt(item.estado)) / 100;
      }
    }
    setPontuacaoTotal(total);
  }, [itensColeta, produtos]);

  const handleChange = (index, field, value) => {
    const updated = [...itensColeta];
    updated[index][field] = value;
    setItensColeta(updated);
  };

  const adicionarItem = () => {
    setItensColeta([...itensColeta, { produtoId: '', quantidade: '', estado: 100 }]);
  };

  const removerItem = (index) => {
    const lista = [...itensColeta];
    lista.splice(index, 1);
    setItensColeta(lista);
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    if (!(data instanceof Date) || isNaN(data.getTime())) {
      alert('Preencha a data e hora corretamente.');
      return;
    }

    const pad = (n) => String(n).padStart(2, '0');
    const dataFormatada = `${data.getFullYear()}-${pad(data.getMonth() + 1)}-${pad(data.getDate())}T${pad(data.getHours())}:${pad(data.getMinutes())}:00`;
    const horaFormatada = `${pad(data.getHours())}:${pad(data.getMinutes())}:00`;

    const payload = {
      data: dataFormatada,
      hora: horaFormatada,
      itensColeta: itensColeta.map(item => ({
        produtoId: parseInt(item.produtoId),
        quantidade: parseInt(item.quantidade),
        estado: parseInt(item.estado)
      }))
    };

    axios.post('https://localhost:7215/api/Agendamentos', payload)
    .then(res => {
      const novoId = res.data.id;
      alert('Agendamento realizado com sucesso!');
      navigate(`/agendamento/${novoId}`);
    })
      .catch(err => alert('Erro ao agendar: ' + err.response?.data || err.message));
  };

  return (
    <div className="container mt-4">
      <Card>
        <Card.Body>
          <h3 className="mb-4">Efetuar Agendamento</h3>
            <div className="agendamento-container">
              <Form onSubmit={handleSubmit}>
              <Row className="mb-3">
                <Form.Label className="form-label fs-5">Data e Hora</Form.Label>
                <Datetime
                  value={data}
                  onChange={value => setData(value.toDate())}
                  inputProps={{ className: "form-control fs-5" }}
                  dateFormat="DD/MM/YYYY"
                  timeFormat="HH:mm"
                />
              </Row>

              <h4 className="fs-3">Itens da Coleta</h4>
              {itensColeta.map((item, index) => (
                <Row key={index} className="align-items-end mb-3">
                  <Col md={4}>
                    <Form.Label className="form-label fs-5">Produto</Form.Label>
                    <Form.Select
                      value={item.produtoId}
                      onChange={(e) => handleChange(index, 'produtoId', e.target.value)}
                      required
                      className="fs-5"
                    >
                      <option value="">Selecione</option>
                      {produtos.map(p => (
                        <option key={p.id} value={p.id}>{p.nome}</option>
                      ))}
                    </Form.Select>
                  </Col>

                  <Col md={3}>
                    <Form.Label className="form-label fs-5">Quantidade</Form.Label>
                    <Form.Control
                      type="number"
                      min={1}
                      value={item.quantidade}
                      onChange={(e) => handleChange(index, 'quantidade', e.target.value)}
                      required
                      className="fs-5"
                    />
                  </Col>

                  <Col md={3}>
                    <Form.Label className="form-label fs-5">Estado</Form.Label>
                    <Form.Select
                      value={item.estado}
                      onChange={(e) => handleChange(index, 'estado', e.target.value)}
                      className="fs-5"
                    >
                      <option value={100}>Doação</option>
                      <option value={50}>Descarte</option>
                    </Form.Select>
                  </Col>

                  <Col md={2}>
                    <Button variant="danger" onClick={() => removerItem(index)} disabled={itensColeta.length === 1}>Remover</Button>
                  </Col>
                </Row>
              ))}

              <Button variant="secondary" onClick={adicionarItem} className="btn-adicionar-item mb-3">+ Adicionar Item</Button>

              <div className="pontuacao-total mb-3 fs-4">
                <strong>Pontuação Total:</strong> {pontuacaoTotal}
              </div>

              <Button type="submit" variant="success" className="fs-4">Agendar</Button>
            </Form>
          </div>

        </Card.Body>
      </Card>
    </div>
  );
}

export default AgendamentoCreate;
