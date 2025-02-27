# Funcionalidades

## Identificação de Ações e Interações das Personas

As funcionalidades do sistema foram definidas com base nas necessidades das personas identificadas durante o processo de pesquisa. Cada funcionalidade foi estruturada para resolver problemas específicos e agregar valor ao usuário final.

### Funcionalidades para Amanda Alves, Gerente de Farmácia

1. **Cadastro de Empresas Parceiras para Coleta**: Amanda pode cadastrar sua farmácias como pontos de coleta de pilhas e baterias.
2. **Recomendações de Pontos de Coleta**: Permite que Amanda visualize e locais próximos para descarte pessoal.
3. **Relatórios de Itens Coletados**: Exibição de estatísticas sobre os resíduos descartados na farmácia.
4. **Incentivos para Empresas Parceiras**: Cadastro de benefícios para farmácias que participam ativamente do projeto.

### Funcionalidades para Carlos Nogueira, Diretor de Escola Pública

1. **Criação de Avisos de Intenção de Receber Doações**: Carlos pode cadastrar sua escola para receber doações de equipamentos eletrônicos.
2. **Gerenciamento de Equipamentos Recebidos**: Permite monitorar os dispositivos doados para a escola.
3. **Agendamento de Coletas**: Organização de coletas de resíduos eletrônicos dentro da escola.
4. **Relatórios sobre Impacto Social**: Geração de estatísticas sobre as doações recebidas.

### Funcionalidades para Leonardo Soares, Empresário do Setor de Tecnologia

1. **Cadastro de Empresas Parceiras**: Empresas podem se registrar na plataforma para se tornarem pontos de coleta de resíduos eletrônicos.
2. **Gerenciamento de Coletas e Doações**: Empresas podem visualizar e gerenciar as doações e coletas agendadas.
3. **Relatórios de Sustentabilidade**: Geração de relatórios detalhados sobre a quantidade de resíduos coletados e seu impacto ambiental.
4. **Incentivos Fiscais para Empresas**: Informações e integração com programas de incentivo fiscal para empresas que participam ativamente do programa de reciclagem.
5. **Portal de Notícias sobre Meio Ambiente**: Acesso a notícias sobre reciclagem e impacto ambiental positivo.

### Funcionalidades para João Oliveira, Munícipe Comerciante

1. **Cadastro de Usuário**: Permite que João crie uma conta na plataforma e tenha acesso a funcionalidades como doação e agendamento de coleta de resíduos eletrônicos.
2. **Recomendações de Pontos de Coleta**: O sistema fornece sugestões de locais próximos onde João pode descartar seus resíduos eletrônicos de maneira adequada.
3. **Agendamento de Coleta**: João pode agendar a coleta de seus resíduos eletrônicos, facilitando o descarte responsável sem precisar se deslocar.
4. **Confirmação e Notificações de Agendamento**: O sistema envia alertas por e-mail ou WhatsApp confirmando o agendamento e informando detalhes da coleta.

### Funcionalidades para Ana Silveira, Secretária Municipal

1. **Educação Ambiental**: Portal de notícias e conteúdos para educar a população sobre descarte consciente.
2. **Estatísticas Ambientais**: Exibição de métricas sobre as contribuições, incluindo impacto ambiental e número de resíduos reciclados.
3. **Geração de Relatórios sobre Itens Coletados**: Permite que Ana visualize e baixe relatórios detalhados sobre suas doações e descartes.
4. **Promoção de Benefícios por Descarte**: Implementação de programas de incentivo (troca de pontos) para os cidadãos e empresas que realizam descartes responsáveis.

### Funcionalidades para o Administrador do Sistema

1. **Cadastro de Itens de Coleta**: Permite o registro e atualização dos tipos de resíduos eletrônicos aceitos na plataforma, garantindo opções adequadas para os usuários ao agendarem coletas.
2. **Gerenciamento de Benefícios**: Define os benefícios disponíveis para os usuários e empresas participantes que acumulam pontos com suas descartes, doações e coletas.
3. **Configuração de Políticas de Recompensa**: Estabelece critérios para a conversão de doações e descartes em pontos que podem ser trocados por benefícios.
4. **Manutenção de Registros de Atividades**: Acompanha todas as interações na plataforma para garantir um ambiente seguro e transparente.
5. **Relatórios de Uso do Sistema**: Gera estatísticas sobre doações, descartes, benefícios resgatados e a evolução da plataforma ao longo do tempo.
6. **Cadastro de Notícias**: Permite ao administrador criar e gerenciar notícias que serão disponibilizadas no portal de notícias da plataforma.
7. **Relatórios de Agendamentos**: Gera relatórios detalhados dos agendamentos de coleta realizados em um determinado dia.

## Tabela de Funcionalidades

| Funcionalidades                               | Amanda Alves | Carlos Nogueira | Leonardo Soares | João Oliveira | Ana Silveira | Administrador |
| --------------------------------------------- | ------------ | --------------- | --------------- | ------------- | ------------ | ------------- |
| Cadastro de Empresas Parceiras                | Sim          | Sim             | Sim             | -             | -            | Sim           |
| Recomendações de Pontos de Coleta             | Sim          | -               | -               | Sim           | -            | Sim           |
| Agendamento de Coleta                         | -            | Sim             | -               | Sim           | -            | Sim           |
| Cadastro de Materiais para Doação             | Sim          | Sim             | Sim             | -             | -            | Sim           |
| Confirmação e Notificações de Agendamento     | -            | -               | -               | Sim           | -            | Sim           |
| Relatórios sobre Impacto Social               | -            | Sim             | -               | -             | -            | Sim           |
| Relatórios de Sustentabilidade                | -            | -               | Sim             | -             | -            | Sim           |
| Incentivos para Empresas Parceiras            | Sim          | -               | Sim             | -             | -            | Sim           |
| Geração de Relatórios sobre Itens Coletados   | Sim          | -               | -               | -             | Sim          | Sim           |
| Promoção de Benefícios por Descarte           | -            | -               | -               | -             | Sim          | Sim           |
| Cadastro de Notícias                          | -            | -               | -               | -             | -            | Sim           |
| Relatórios de Agendamentos                    | -            | -               | -               | -             | -            | Sim           |
| Leitura de notícias (educação ambiental)      | -            | -               | Sim             | -             | Sim          | Sim           |
| Cadastro de usuário                           | -            | -               | -               | Sim           | -            | Sim           |
| Cadastro de resíduos permitidos na plataforma | -            | -               | -               | -             | -            | Sim           |
| Cadastro e gerenciamento de benefícios        | -            | -               | -               | -             | -            | Sim           |
| Configuração de Políticas de Recompensa       | -            | -               | -               | -             | -            | Sim           |
| Manutenção de Registros de Atividades         | -            | -               | -               | -             | -            | Sim           |
| Relatórios de Uso do Sistema                  | -            | -               | -               | -             | -            | Sim           |
