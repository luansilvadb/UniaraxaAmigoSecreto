# Projeto Amigo Secreto

O Projeto Amigo Secreto é um sistema de gerenciamento de amigo secreto desenvolvido em SQL. Ele permite criar e gerenciar participantes, listas de desejos e sorteios para eventos de amigo secreto.

## Estrutura do Banco de Dados

A estrutura do banco de dados é ilustrada abaixo:

![Estrutura do Banco de Dados](https://i.imgur.com/UDVN4Te.png)

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
