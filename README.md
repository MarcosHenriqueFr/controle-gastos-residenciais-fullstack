# Gastos Residenciais 💰 🏠
### Um sistema para controle de receitas e despesas entre membros de uma residência

## Tecnologias 🖥️
<ul>
    <li>C# / .NET 10
    <li>ASP.NET Core Web API
    <li>Entity Framework Core
    <li>SQLite
    <li>Swagger / OpenAPI
    <li>React 19
    <li>TypeScript
    <li>Vite
    <li>React Router DOM
</ul>

## Como testar o projeto 🚀

### Pré-requisitos
Antes de iniciar o projeto, é necessário baixar os itens a seguir:
<ul>
    <li>.NET SDK 10
    <li>Node.js 19
    <li>Git
    <li>SQLite CLI (<code>sqlite3</code>)
</ul>

Para instalar o SQLite CLI:

**Windows:**
```powershell
winget install SQLite.SQLite
```

**Linux:**
```bash
sudo apt install sqlite3
```

**macOS:**
```bash
brew install sqlite3
```

### Clonando
Primeiro clone o projeto para uma pasta da sua máquina:
```bash
git clone https://github.com/MarcosHenriqueFr/controle-gastos-residenciais-fullstack.git
```
Depois entre na pasta criada:
```bash
cd controle-gastos-residenciais-fullstack
```

### Configurando e rodando o backend

Entre na pasta do backend:
```bash
cd backend/GastosResidenciais
```

Restaure as dependências:
```bash
dotnet restore
```

#### Aplicando as migrations e populando o banco com dados de exemplo

Um script já está pronto para aplicar as migrations do Entity Framework
e popular o banco SQLite com pessoas e transações de exemplo, facilitando
os testes manuais da API.

**Linux/macOS:**
```bash
./Scripts/Seeds/seed.sh
```

**Windows (PowerShell):**
```powershell
.\Scripts\Seeds\seed.ps1
```

<em>Obs: se o Windows bloquear a execução do script com um erro de política
de execução, rode antes:</em>
```powershell
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
```

#### Rodando a API
```bash
dotnet run
```

A API vai subir em <kbd>http://localhost:5241</kbd>, com o Swagger disponível em
<kbd>http://localhost:5241/swagger</kbd>.

### Configurando e rodando o frontend

Em outro terminal, entre na pasta do frontend:
```bash
cd frontend
```

Instale as dependências:
```bash
npm install
```

Rode o projeto:
```bash
npm run dev
```

O frontend vai subir em <kbd>http://localhost:5173</kbd>, já configurado para
se comunicar com o backend em <kbd>http://localhost:5241</kbd>.

## Endpoints 🚩

### PessoaController

| Endpoint                              | Descrição                                     |
|----------------------------------------|------------------------------------------------|
| <kbd>POST api/pessoa</kbd>              | Cadastra uma nova pessoa                        |
| <kbd>GET api/pessoa</kbd>               | Lista todas as pessoas cadastradas              |
| <kbd>GET api/pessoa/{id}</kbd>          | Retorna os dados de uma pessoa específica       |
| <kbd>DELETE api/pessoa/{id}</kbd>       | Remove uma pessoa do sistema                    |
| <kbd>GET api/pessoa/{id}/transacoes</kbd> | Lista as transações de uma pessoa específica  |

### TransacaoController

| Endpoint                       | Descrição                                                       |
|---------------------------------|--------------------------------------------------------------|
| <kbd>POST api/transacao</kbd>   | Cadastra uma nova transação (menores de 18 anos só podem cadastrar despesas) |
| <kbd>GET api/transacao</kbd>    | Lista todas as transações cadastradas                          |
| <kbd>GET api/transacao/{id}</kbd> | Retorna os dados de uma transação específica                  |

### ResumoController

| Endpoint            | Descrição                                                                 |
|-----------------------|----------------------------------------------------------------------|
| <kbd>GET api/total</kbd> | Retorna o resumo financeiro de todas as pessoas, com totais de receitas, despesas e saldo, além do total geral |

<br>

O frontend consome todos esses endpoints, oferecendo telas para cadastro
de pessoas, cadastro de transações e visualização do resumo financeiro.

## O que foi aprendido 📝
<ul>
    <li> Aplicação de regras de negócio na camada de serviço, mantendo os controllers enxutos;
    <li> Tratamento global de exceções com middleware, evitando repetição de try/catch nos controllers;
    <li> Uso de Entity Framework Core com SQLite, incluindo relacionamentos entre entidades;
    <li> Documentação de endpoints com XML comments e atributos <code>[ProducesResponseType]</code> para o Swagger;
    <li> Configuração de CORS para permitir a comunicação entre back-end e front-end em portas diferentes no C#;
    <li> Serialização de enums como string via <code>JsonStringEnumConverter</code>, facilitando o uso da API;
</ul>

## Mudanças futuras 📈
<ul>
    <li> Validação client-side no formulário de transações, impedindo a seleção de "Receita" para pessoas menores de idade antes mesmo da chamada à API;
    <li> Autenticação e autorização, permitindo múltiplas residências isoladas entre si;
    <li> Filtros e paginação na listagem de transações;
    <li> Testes automatizados no frontend;
    <li> Validação nos DTOs de request e response.
</ul>

<br><br>

**Obrigado pela sua atenção. Qualquer feedback é bem-vindo!**