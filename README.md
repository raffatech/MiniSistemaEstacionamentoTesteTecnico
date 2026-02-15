# 🚗 Mini-Sistema de Estacionamento (webAPI + Angular)

Este projeto é uma solução Full-Stack desenvolvida para o controle de entrada e saída de veículos, atendendo requisitos de precificação para operações no Brasil e Argentina.

## 🛠️ Tecnologias Utilizadas
- **Back-End:** ASP.NET Core Web API (NET 10.0) C#
- **Front-End:** Angular 19+
- **Banco de Dados:** SQLite (com Entity Framework Core)
- **Arquitetura:** Injeção de Dependência para cálculo de impostos/taxas (Strategy Pattern).

## 🧠 Lógica do Sistema
A estrutura foi modelada focando na separação de responsabilidades:
- **Veículo**: Cadastro único das informações.
- **Sessão de Estacionamento**: Gerencia o estado do veículo (Entrada/Saída).
- **Fatura (Invoice)**: Gerada no momento da saída, calculando o valor baseado no tempo de permanência e nas regras de precificação vigentes.

### Regra de Precificação (Arredondamento)
Para este desafio, adotei a seguinte lógica de arredondamento:
* **Hora Cheia:** Após os primeiros 60 minutos, qualquer fração de hora (ex: 1h 10min) é contabilizada como uma hora cheia adicional. Isso garante a sustentabilidade financeira da operação e simplifica o entendimento do cliente final.

## 🚀 Como Rodar o Projeto

### Back-End
1. Navegue até a pasta `BackEnd/MiniSistemaEstacionamentoAPI`.
2. Execute o comando: `dotnet run`.
3. A API estará disponível em: `http://localhost:5000` (ou conforme configurado no `launchSettings.json`).

### Front-End
1. Navegue até a pasta `FrontEnd/projectangular`.
2. Execute: `npm install` e depois `npm start`.
3. Acesse: `http://localhost:4200`.
