# Api
Projeto de Api Mínima com Controlador em Asp Net Core + Aplicação FrontEnd Angular

# Manual de Instruções do Projeto

Este manual fornece instruções detalhadas sobre como configurar o ambiente de desenvolvimento para o backend e o frontend, restaurar as dependências e compilar o código-fonte.

## Configuração do Ambiente Backend

### Pré-requisitos

1. [.NET 5.0 SDK](https://dotnet.microsoft.com/download) ou superior
2. [Visual Studio 2019](https://visualstudio.microsoft.com/vs/) ou [Visual Studio Code](https://code.visualstudio.com/)

### Passos

1. Clone o repositório do projeto para o seu ambiente local usando o comando `git clone https://github.com/ifirmos/Api.git`.
2. Abra a pasta do projeto no Visual Studio ou no Visual Studio Code.
3. Restaure as dependências do projeto executando o comando `dotnet restore` no terminal integrado.
4. Compile o projeto executando o comando `dotnet build`.
5. Inicie o servidor de desenvolvimento executando o comando `dotnet run`.

O servidor de desenvolvimento estará disponível nos seguintes endereços:

- `https://localhost:7001`
- `http://localhost:5183`

## Configuração do Ambiente Frontend

### Pré-requisitos

1. [Node.js](https://nodejs.org/en/download/) versão 14.0 ou superior
2. [Angular CLI](https://cli.angular.io/) versão 16.1.3 ou superior

### Passos

1. Clone o repositório do projeto para o seu ambiente local.
2. Navegue até a pasta do projeto frontend no terminal.
3. Restaure as dependências do projeto executando o comando `npm install`.
4. Inicie o servidor de desenvolvimento executando o comando `npm start` ou `ng serve`.

O servidor de desenvolvimento estará disponível no seguinte endereço:

- `http://localhost:4200`

## Utilizando a API

A API possui as seguintes rotas:

1.	POST /api/Contratos: Cria um novo contrato.
2.	GET /api/Contratos: Retorna uma lista de todos os contratos.
3.	GET /api/Contratos/{id}: Retorna um contrato específico pelo ID.
4.	PUT /api/Contratos/{id}: Atualiza um contrato específico pelo ID.
5.	DELETE /api/Contratos/{id}: Exclui um contrato específico pelo ID.
6.	GET /api/Contratos/nome/{nome}: Retorna uma lista de contratos filtrada por nome.
7.	GET /api/Contratos/numero/{numero}: Retorna uma lista de contratos filtrada por número.
8.	POST /Documento/upload/{idContrato}: Faz o upload de um documento PDF relacionado a um contrato específico pelo ID do contrato.
9.	GET /Documento/{idContrato}: Faz o download de um documento PDF relacionado a um contrato específico pelo ID do contrato.
10.	DELETE /Documento/{idContrato}: Remove um documento PDF relacionado a um contrato específico pelo ID do contrato.


## Documentação da API com Swagger

A documentação da API está disponível no Swagger UI, que pode ser acessado através do seguinte endereço:

- `https://localhost:7001/swagger`
- `http://localhost:5183/swagger`

A documentação do Swagger fornece informações detalhadas sobre cada rota da API, incluindo os parâmetros de entrada, os formatos de resposta e os códigos de status HTTP. Você também pode experimentar as rotas diretamente na interface do Swagger.
