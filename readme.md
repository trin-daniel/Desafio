# Objetivo

- Desenvolver um sistema de controle de gastos residenciais, com **front-end em React com TypeScript** e **back-end em .NET**.

## Ressalvas

- Embora minha vontade fosse construir o projeto totalmente em inglês — pois as palavras reservadas da linguagem também são escritas nesse idioma e acredito que a mistura de termos em inglês e português cause uma grande inconsistência estrutural — optei por seguir, detalhe por detalhe, a especificação fornecida.

## Pontos de atenção

1. Certifique-se de ter o **.NET 10** e o **Node.js** instalados na máquina. Caso contrário, nem o back-end nem o front-end irão funcionar.
2. Ao clonar o repositório, é necessário restaurar as dependências de ambos os projetos:
   - **Back-end**:  
     Acesse a pasta `backend` pelo terminal e execute o comando `dotnet restore` para baixar as dependências. Em seguida, execute `dotnet build` para compilar os binários da aplicação.
   - **Front-end**:  
     Acesse a pasta `frontend` pelo terminal e execute o comando `npm install` para que o gerenciador de pacotes baixe as dependências do projeto.

## Como subir o servidor do Back-end e do Front-end

1. É importante que o servidor do **back-end** seja inicializado antes do **front-end**.
2. Para subir o servidor do back-end:
   - Acesse a pasta `backend` pelo terminal;
   - Em seguida, acesse a pasta `src` e depois `Backend.WebApi`;
   - Execute o comando `dotnet run`.
   
   Caso considere esse processo demorado ou pouco prático, basta abrir o projeto de back-end em sua IDE e inicializá-lo da forma habitual.
3. Para subir o servidor do front-end:
   - Acesse a pasta `frontend` pelo terminal;
   - Execute o comando `npm run dev`.

## Funcionalidades

- Cadastro, listagem e remoção de pessoas;
- Cadastro e listagem de categorias;
- Cadastro e listagem de transações;
- Relatório de receitas, despesas e saldo por pessoa, além do total geral de receitas, despesas e saldo líquido de todas as pessoas.

## Documentação da API com Scalar

- É possível interagir com a **API** por meio da ferramenta de documentação **Scalar** (alternativa ao Swagger).  
  Basta acessar: `http://localhost:5051/scalar`.  
  Essa rota está disponível apenas em ambiente de desenvolvimento.

