<h1 align="center">Tech Box</h1>
<h4 align="center">Uma plataforma de cortes de videos para capturas frames.</h4>

<p align="center">
  <a href="">
    <img src="https://img.shields.io/badge/version-1.0.0-blue"
         alt="version">
  </a>
  <a href="">
    <img src="https://img.shields.io/badge/license-MIT-green"
         alt="license">
  </a>
</p>

<!-- Pegar o front depois de pronto -->
<p align="center">
  <a href="">
    <img src=".github\images\website-demo.png" alt="website-demo">
  </a>
</p>

## Sumário

- [Sobre](#sobre)
- [Tecnologias](#tecnologias)
- [Suporte ao Browser](#suporte-ao-browser)
- [Arquitetura](#arquitetura)
    - [Web App](#web-app)
    - [Core API](#core-api)
- [Testes Unitários](#testes-unitários)
- [Executando a aplicação](#executando-a-aplicação)
    - [Docker](#docker)


# Sobre
Este projeto foi desenvolvido para atender aos requisitos de uma demanda de processamento de imagens da empresa [FIAP X].<br>
- <b>A plataforma Tech Box consiste em três aplicações principais:</b>
    - Uma aplicação WEB APP MVC que permite ao usuário fazer o upload de vídeos e o download de um arquivo ZIP contendo os frames extraídos.
    - Uma WEB API que gerencia o upload dos vídeos no armazenamento, cria registros no banco de dados e adiciona mensagens à fila. Ao receber a resposta do processamento, atualiza a interface do usuário com a URL de download do arquivo .zip .
    - Uma aplicação WORKER que consome a fila, processa os vídeos e extrai os frames. Em seguida, salva os frames em um arquivo ZIP no armazenamento e atualiza o banco de dados com as informações do arquivo ZIP e o status do processamento.
- <b>Além disso, o projeto inclui:</b>
    - Um Broker do tipo RabbitMQ para gerenciar a fila de mensagens.
    - Um banco de dados do tipo SQL Server, onde são armazenadas informações sobre o processamento, o status do processo, a URL do vídeo e a URL do arquivo ZIP.
    - Um armazenamento com dois containers, um para vídeos e outro para arquivos ZIP. Esses containers permitem o upload do vídeo a ser processado e do arquivo ZIP para disponibilização do download na interface do usuário.

# Tecnologias

| Web App | API | ORM | Database | WORKER |
| --- | --- | --- | --- | -- |
| [![bootstrap-version](https://img.shields.io/badge/Bootstrap-5.3.1-purple)](https://getbootstrap.com/)<br>[![fontawesome-version](https://img.shields.io/badge/Font_Awesome-6.4.0-yellow)](https://fontawesome.com/)<br>[![aspnetcore-version](https://img.shields.io/badge/ASP.NET_Core_MVC-7.0-blue)](https://learn.microsoft.com/pt-br/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-7.0)| [![aspnetcore-version](https://img.shields.io/badge/ASP.NET_Core-7.0-blue)](https://learn.microsoft.com/pt-br/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-7.0) | [![dapper-version](https://img.shields.io/badge/EF_Core-7.0-red)](https://learn.microsoft.com/en-us/ef/core/) | ![database](https://img.shields.io/badge/SQL_Server-gray) |<br>[![aspnetcore-version](https://img.shields.io/badge/ASP.NET_Core_MVC-7.0-blue)](https://learn.microsoft.com/pt-br/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-7.0)

# Suporte ao Browser

| <img src="https://user-images.githubusercontent.com/1215767/34348387-a2e64588-ea4d-11e7-8267-a43365103afe.png" alt="Chrome" width="16px" height="16px" /> Chrome | <img src="https://user-images.githubusercontent.com/1215767/34348590-250b3ca2-ea4f-11e7-9efb-da953359321f.png" alt="IE" width="16px" height="16px" /> Internet Explorer | <img src="https://user-images.githubusercontent.com/1215767/34348380-93e77ae8-ea4d-11e7-8696-9a989ddbbbf5.png" alt="Edge" width="16px" height="16px" /> Edge | <img src="https://user-images.githubusercontent.com/1215767/34348394-a981f892-ea4d-11e7-9156-d128d58386b9.png" alt="Safari" width="16px" height="16px" /> Safari | <img src="https://user-images.githubusercontent.com/1215767/34348383-9e7ed492-ea4d-11e7-910c-03b39d52f496.png" alt="Firefox" width="16px" height="16px" /> Firefox |
| :---------: | :---------: | :---------: | :---------: | :---------: |
| Yes | 11+ | Yes | Yes | Yes |


# Arquitetura
Esta é uma visão geral da arquitetura do Tech Box.

<p align="center">
  <a href="">
    <img src=".github\images\architecture-overview.png" alt="overview-architecture">
  </a>
</p>

## Web App

A concepção da aplicação foi fundamentada no padrão arquitetural MVC (Model View Controller), sendo implementada por meio do ASP.NET Core.

No âmbito do negócio, sua responsabilidade é enviar o video para ser processado e posteriormente disponibiliza o link para download do zip contendo os frames.

<p align="center">
  <a href="">
    <img src=".github\images\webapp-architecture.png" alt="webapp-architecture">
  </a>
</p>

## Core API

Escolhemos uma arquitetura em camadas para a API, adotado um estilo arquitetural de CRUD.

## Testes Unitários
Os testes unitários visam validar a funcionalidade de unidades individuais de código, como métodos ou funções.

- <b>Frameworks Utilizados:</b> xUnit, FakeItEasy (para mocks).
- <b>Localização dos Testes:</b> tests/unit/

# Mensageria

Para a implementação de um Message Bus, escolhemos como Broker o RabbitMQ.

O Produtor que é a WebAPI criará uma mensagem na fila do RabbitMQ e o Worker consumirá essa fila com os dados para o processamento do video.

# Executando a aplicação
É possível executar a aplicação realizando a configuração manualmente, ou utilizando Docker (recomendado).

## Docker
Para rodar localmente, é possível utilizar o Docker.  
Abaixo o passo a passo para executar a aplicação localmente:
- Realizar o clone do projeto na pasta desejada:
    ```bash
        git clone https://github.com/pistoladas-group/hackaton-fiap.git
    ```
- Configurar certificados para habilitar conexão via https:
    ```bash
        dotnet dev-certs https -ep "$env:USERPROFILE\.aspnet\https\technews.pfx"  -p "OVmTv9lykb0)>m=wWcQaJ"
        dotnet dev-certs https --trust
    ```
- Utilizar o comando abaixo para subir a aplicação utilizando docker-compose:
    ```bash
        docker-compose -f docker-compose.debug.yml up --build
    ```