# Projeto Amigo Secreto

O Projeto Amigo Secreto é um sistema de gerenciamento de amigo secreto desenvolvido em SQL. Ele permite criar e gerenciar participantes, listas de desejos e sorteios para eventos de amigo secreto.

## Estrutura do Banco de Dados

O sistema utiliza um banco de dados relacional com as seguintes tabelas:

### Tabela `listadesejos`

Esta tabela armazena os itens da lista de desejos de cada participante.

- `id`: Identificador único do item (BIGSERIAL)
- `nome`: Nome do item (varchar(255))
- `descricao`: Descrição do item (varchar(255))
- `id_participante`: Identificador do participante ao qual o item pertence (int8)

### Tabela `login`

Nesta tabela são registrados os dados de login dos participantes.

- `id`: Identificador único do login (BIGSERIAL)
- `nome`: Nome do participante (varchar(255))
- `email`: Endereço de e-mail do participante (varchar(255))
- `senha`: Senha do participante (varchar(255))

### Tabela `participante`

A tabela `participante` armazena informações detalhadas sobre cada participante.

- `id`: Identificador único do participante (BIGSERIAL)
- `nome`: Nome do participante (varchar(255))
- `email`: Endereço de e-mail do participante (varchar(255))
- `senha`: Senha do participante (varchar(255))
- `id_login_service`: Identificador do serviço de login associado ao participante (int8)

### Tabela `sorteio`

Esta tabela registra os resultados dos sorteios, incluindo a data em que foram realizados.

- `id`: Identificador único do sorteio (BIGSERIAL)
- `data_sorteio`: Data em que o sorteio foi realizado (date)
- `id_participante_sorteado`: Identificador do participante sorteado (int8)

## Relacionamentos

O banco de dados possui as seguintes restrições de chave estrangeira (FK):

- `FKlistadesej52748`: Chave estrangeira na tabela `listadesejos` referenciando `participante(id)`
- `FKparticipan215456`: Chave estrangeira na tabela `participante` referenciando `login(id)`
- `FKsorteio203699`: Chave estrangeira na tabela `sorteio` referenciando `participante(id)`

## Funcionalidades

- **Cadastro de Participantes**: Os participantes podem se cadastrar no sistema fornecendo nome, e-mail e senha.
- **Gerenciamento de Listas de Desejos**: Cada participante pode criar uma lista de desejos, adicionando itens com nome e descrição.
- **Realização de Sorteios**: O sistema realiza sorteios automaticamente para determinar quem deve presentear quem em um evento de amigo secreto.

## Contribuição

Contribuições são bem-vindas! Se você deseja contribuir para o projeto Amigo Secreto, sinta-se à vontade para abrir um pull request ou criar uma issue para discutir novas funcionalidades, correções de bugs ou melhorias.

## Licença

Este projeto é distribuído sob a [Licença MIT](LICENSE).
