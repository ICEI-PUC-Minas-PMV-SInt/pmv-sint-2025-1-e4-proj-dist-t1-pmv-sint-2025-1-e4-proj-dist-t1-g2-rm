import React, { useEffect, useState } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import axios from 'axios';
import Datetime from 'react-datetime';
import "react-datetime/css/react-datetime.css";
import { Card, Button, Form, Row, Col } from 'react-bootstrap';
import './Agendamento.css';

const AgendamentoEdit = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [produtos, setProdutos] = useState([]);
  const [data, setData] = useState(null);
  const [hora, setHora] = useState('');
  const [itensColeta, setItensColeta] = useState([]);
  const [pontuacaoTotal, setPontuacaoTotal] = useState(0);

  useEffect(() => {
    axios.get(`https://localhost:7215/api/Agendamentos/${id}`)
      .then(res => {
        const ag = res.data;
        setData(new Date(ag.data));
        setHora(ag.hora);
        setItensColeta(ag.itensColeta.map(item => ({
          id: item.id,
          produtoId: item.produtoId,
          quantidade: item.quantidade,
          estado: item.estado
        })));
      })
      .catch(err => console.error('Erro ao buscar agendamento', err));

    axios.get('https://localhost:7215/api/Produtos')
      .then(res => setProdutos(res.data))
      .catch(err => console.error('Erro ao carregar produtos', err));
  }, [id]);

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
    const lista = [...itensColeta];
    lista[index][field] = value;
    setItensColeta(lista);
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
    const pad = n => String(n).padStart(2, '0');
    const dataFormatada = `${data.getFullYear()}-${pad(data.getMonth() + 1)}-${pad(data.getDate())}T${pad(data.getHours())}:${pad(data.getMinutes())}:00`;
    const horaFormatada = `${pad(data.getHours())}:${pad(data.getMinutes())}:00`;

    const payload = {
      id: parseInt(id),
      data: dataFormatada,
      hora: horaFormatada,
      itensColeta: itensColeta.map(item => ({
        id: item.id || 0,
        produtoId: parseInt(item.produtoId),
        quantidade: parseInt(item.quantidade),
        estado: parseInt(item.estado)
      }))
    };

    axios.put(`https://localhost:7215/api/Agendamentos/${id}`, payload)
      .then(() => {
        alert('Agendamento atualizado com sucesso!');
        navigate(`/agendamentos`);
      })
      .catch(err => alert('Erro ao atualizar: ' + err.response?.data || err.message));
  };

  if (!data) return <p className="text-center mt-5">Carregando detalhes...</p>;

  return (
    <div className="container mt-4">
      <h2 className="mb-4 text-center">Editar Agendamento</h2>
      <Card className="agendamento-card">
        <Card.Body>
          <h3 className="mb-4 text-center">Data e Horário do Agendamento</h3>
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

              <h4 className="fs-3 mb-3">Itens da Coleta</h4>
              {itensColeta.map((item, index) => (
                <Row key={index} className="align-items-end mb-3">
                  <Col md={4}>
                    <Form.Label className="form-label">Produto</Form.Label>
                    <Form.Select
                      value={item.produtoId}
                      onChange={(e) => handleChange(index, 'produtoId', e.target.value)}
                      required
                    >
                      <option value="">Selecione</option>
                      {produtos.map(p => (
                        <option key={p.id} value={p.id}>{p.nome}</option>
                      ))}
                    </Form.Select>
                  </Col>

                  <Col md={3}>
                    <Form.Label className="form-label">Quantidade</Form.Label>
                    <Form.Control
                      type="number"
                      min={1}
                      value={item.quantidade}
                      onChange={(e) => handleChange(index, 'quantidade', e.target.value)}
                      required
                    />
                  </Col>

                  <Col md={3}>
                    <Form.Label className="form-label">Estado</Form.Label>
                    <Form.Select
                      value={item.estado}
                      onChange={(e) => handleChange(index, 'estado', e.target.value)}
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

              <Button onClick={adicionarItem} className="btn-adicionar-item mb-4">+ Adicionar Item</Button>

              <div className="pontuacao-total text-success mb-3 fs-4">
                <strong>Pontuação Total:</strong> {pontuacaoTotal}
              </div>

              <div className="d-flex justify-content-center align-items-center gap-3 mt-4">
                <Button type="submit" className="btn-verde fs-4">Salvar Alterações</Button>
                <Link to="/agendamentos" className="btn btn-outline-secondary fs-4 text-decoration-none">
                  Cancelar
                </Link>
              </div>

            </Form>
          </div>
        </Card.Body>
      </Card>
    </div>
  );
};

export default AgendamentoEdit;
