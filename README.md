# Aplicação de Registro de transações e consolidado
Esta aplicação tem como objetivo registrar transações de débito e crédito em uma API e consolidar o saldo por dia em outra API. A comunicação entre as APIs é feita por meio do RabbitMQ.

A aplicação foi configurada para rodar em containers Docker, utilizando o docker-compose.

## Pré-requisitos
- Docker
- Docker Compose

## Instalação

Clone este repositório em sua máquina local.
```bash
git clone https://github.com/rafaelguinho/DailyCashFlowControl.git
```
Navegue até a pasta do projeto.
```bash
cd \DailyCashFlowControl
```
## Utilização

- Inicie a aplicação.
```bash
docker-compose up
```
- Acesse a aplicação web http://localhost:3000/

A API de saldo consolidado será atualizada automaticamente, processando o saldo do dia até aquele momento a cada nova transação registrada.

- Endereço da api de transações: http://localhost:5000/
- Endereço da api de dados consolidados: http://localhost:5001/

## Collection do postman:

   Use o link abaixo e importe a collection no seu Postman:

- [DailyCashFlowControl.postman_collection](DailyCashFlowControl.postman_collection.json)
