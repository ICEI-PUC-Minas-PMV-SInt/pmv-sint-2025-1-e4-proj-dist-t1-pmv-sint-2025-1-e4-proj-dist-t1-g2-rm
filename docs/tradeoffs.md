# Trade-offs de Características de Qualidade 

De acordo com o modelo FURPS+, as categorias de requisitos não-funcionais para o produto de software "Recicla Mais" seriam:

## Funcionalidade

- O sistema deve permitir que os usuários realizem agendamentos de coleta de resíduos eletrônicos de forma segura e eficiente.
- Deve garantir a rastreabilidade dos resíduos coletados, permitindo que os usuários acompanhem o status da coleta.
- O sistema deve permitir a atribuição de pontos aos usuários após a confirmação da coleta, possibilitando a troca por benefícios.
- O sistema deve integrar a API do Google Maps para auxiliar na exibição de endereços e cálculo de rotas para auxiliar e otimizar a logística de coleta.

## Usabilidade

- O sistema deve possuir uma interface intuitiva e acessível, garantindo que 90% dos usuários consigam realizar um agendamento sem precisar de assistência.
- Deve oferecer suporte a diferentes dispositivos, incluindo desktops e dispositivos móveis, com design responsivo.
- O processo de agendamento deve ser rápido e requerer o menor número possível de passos para garantir uma boa experiência do usuário.
- A API do Google Maps deve ser utilizada para fornecer uma interface amigável de seleção de endereços, permitindo a escolha do local exato da coleta.

## Confiabilidade

- O sistema deve ter uma disponibilidade mínima de 99,5%, evitando falhas que possam impedir os usuários de realizar agendamentos ou acompanhar suas coletas.
- Os dados dos usuários e seus agendamentos devem ser armazenados de forma segura, utilizando criptografia e backups regulares.
- O sistema deve possuir autenticação segura para evitar acessos não autorizados, garantindo a privacidade dos usuários.

## Desempenho

- O tempo de resposta para a realização de um agendamento não deve ultrapassar 2 segundos em 95% das interações.
- O sistema deve ser escalável para suportar um grande número de usuários simultaneamente sem degradação do desempenho.
- A API deve ser otimizada para processar as solicitações de agendamento e consulta de forma eficiente, garantindo baixa latência.
- A integração com a API do Google Maps deve ser otimizada para garantir carregamento rápido e evitar sobrecarga no sistema.

## Suportabilidade

- O sistema deve ser modular para facilitar futuras atualizações e manutenções sem impactar o funcionamento principal.
- A documentação do código e da API deve ser clara e bem estruturada para facilitar a compreensão por desenvolvedores futuros.
- A integração com a API do Google Maps deve ser bem documentada para facilitar manutenção e eventuais atualizações.

## A Importância Relativa de Cada Categoria

| Categoria       | Funcionalidade | Usabilidade | Confiabilidade | Desempenho | Suportabilidade |
|---------------|----------------|------------|---------------|------------|----------------|
| Funcionalidade | -              | >          | >             | >          | >              |
| Usabilidade    | <              | -          | >             | >          | >              |
| Confiabilidade | <              | <          | -             | >          | >              |
| Desempenho     | <              | <          | <             | -          | >              |
| Suportabilidade| <              | <          | <             | <          | -              |

> Nesta matriz, o sinal ">" indica que a categoria da linha é mais importante que a categoria da coluna, e o sinal "<" indica que a categoria da linha é menos importante que a categoria da coluna. No caso do "Recicla Mais", a **Funcionalidade** é a característica mais importante, seguida pela **Usabilidade**, garantindo que os usuários possam facilmente utilizar o sistema. A **Confiabilidade** é essencial para garantir que os dados e agendamentos sejam mantidos de forma segura e estável. O **Desempenho** e a **Suportabilidade** são priorizados de acordo com a necessidade de estabilidade e escalabilidade do sistema.

A utilização da **API do Google Maps** contribui para a usabilidade e eficiência do sistema, facilitando a inserção e visualização de endereços e otimizando a logística de coleta, garantindo uma experiência mais fluida para os usuários e maior eficiência operacional.

