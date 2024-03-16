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

<!-- # TODO
- Correlation ID
- .NET8
- Event Sourcing -->

## Sumário

- [Sobre](#sobre)
- [Tecnologias](#tecnologias)
- [Suporte ao Browser](#suporte-ao-browser)
- [Arquitetura](#arquitetura)
    - [Web App](#web-app)
    - [Core API](#core-api)
    - [Auth API (Authorization Server)](#auth-api-authorization-server)
- [Segurança](#segurança)
    - [Rotação das Chaves](#rotação-das-chaves)
    - [Prevenção contra possíveis ataques](#prevenção-contra-possíveis-ataques)
- [Testes](#testes)
    - [Testes Unitários](#testes-unitários)
    - [Testes de Integração](#testes-de-integração)
    - [Testes de UI/UAT (Interface/Aceitação do Usuário)](#testes-de-uiuat-interfaceaceitação-do-usuário)
- [CI / CD](#ci--cd)
- [Executando a aplicação](#executando-a-aplicação)
    - [Docker](#docker)


# Sobre
Este projeto foi desenvolvido para atender aos requisitos de uma demanda de processamento de imagens da empresa [FIAP X].<br>
- [A plataforma Tech Box consiste em três aplicações principais:]
- Uma aplicação WEB APP MVC que permite ao usuário fazer o upload de vídeos e o download de um arquivo ZIP contendo os frames extraídos.
- Uma WEB API que gerencia o upload dos vídeos no armazenamento, cria registros no banco de dados e adiciona mensagens à fila. Ao receber a resposta do processamento, atualiza a interface do usuário com a URL de download do arquivo .zip .
- Uma aplicação WORKER que consome a fila, processa os vídeos e extrai os frames. Em seguida, salva os frames em um arquivo ZIP no armazenamento e atualiza o banco de dados com as informações do arquivo ZIP e o status do processamento.
-[Além disso, o projeto inclui:]
- Um Broker do tipo RabbitMQ para gerenciar a fila de mensagens.
- Um banco de dados do tipo SQL Server, onde são armazenadas informações sobre o processamento, o status do processo, a URL do vídeo e a URL do arquivo ZIP.
- Um armazenamento com dois containers, um para vídeos e outro para arquivos ZIP. Esses containers permitem o upload do vídeo a ser processado e do arquivo ZIP para disponibilização do download na interface do usuário.

# Tecnologias

| Web App | API's | ORM | Database | WORKER |
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

Escolhemos uma arquitetura mais simples para a API de notícias, adotado um estilo arquitetural de CRUD.

# Testes
Para este tech challenge o projeto inclui testes em diferentes níveis para garantir a qualidade e o funcionamento correto do software.

<p align="center">
  <a href="">
    <img src=".github\images\tests-diagram.png" alt="api-architecture">
  </a>
</p>

## Testes Unitários
Os testes unitários visam validar a funcionalidade de unidades individuais de código, como métodos ou funções.

- <b>Frameworks Utilizados:</b> xUnit, FakeItEasy (para mocks) e Bogus (para geração automática de dados fake)
- <b>Localização dos Testes:</b> tests/unit/

## Testes de UI/UAT (Interface/Aceitação do Usuário)
Os testes de UI/UAT (User Acceptance Testing) são realizados para validar o aplicativo quanto à usabilidade, experiência do usuário e para garantir que atende aos requisitos do usuário final. Para este teste todo um ambiente é criado e depois descartado após execução do teste.

- <b>Frameworks Utilizado:</b> xUnit, Bogus, Specflow e Selenium
- <b>Localização dos Testes:</b> tests/user-interface/

# CI / CD

O CI / CD desse Tech Challenge consiste nos pipelines: <b>Create Azure Resources</b>, <b>Main</b> e <b>UI Test</b>.

O pipeline de <b>Create Azure Resources</b> cria todos os recursos descritos na [Arquitetura](#arquitetura), fazendo uso dos ARM Templates disponíveis na pasta "azure". Os recursos criados são: Key Vault, Container Registry, SQL Databases e Blob Storage.

O pipeline <b>Main</b> assume o papel de Integração Contínua (CI) e Entrega Contínua (CD), realizando os seguintes passos:
- A compilação das aplicações juntamente com suas dependências; 
- A execução dos testes Unitários, de Integração e UI/UAT;
- A compilação das imagens com base os dockerfiles, gerando os artefatos que são publicados no Container Registry;
- Criação das instâncias dos containers no Azure Container Instance com base as imagens;

Já o pipeline <b>UI Test</b> serve para preparar o ambiente de teste e executar os testes de interface e aceitação do usuário (UI/UAT). Os passos são:
- Criação do Resource Group na Azure de teste para facilitar o gerenciamento do ambiente;
- Deploy das bases de testes;
- Compilação das imagens com base os dockerfiles, gerando os artefatos que são publicados no Container Registry com a tag de teste;
- Criação das instâncias dos containers no Azure Container Instance com base as imagens de teste;
- Execução do teste de UI/UAT;
- Descarte do ambiente de teste deletando o Resource Group;

Abaixo um diagrama que demonstra como o pipeline Main e UI Test se integram.

<p align="center">
  <a href="">
    <img src="docs\pipelines-diagram.jpg" alt="pipeline-diagram">
  </a>
</p>

As migrations do banco são realizadas por cada aplicação (Auth API e Core API), no momento em que a aplicação é executada no container. Isso garante que as bases estão atualizadas automaticamente através das migrations do Entity Framework. Também existe a opção de executar os scripts gerados manualmente. Eles se encontram na pasta "sql".

# Mensageria

Para a implementação de um Message Bus, escolhemos como Broker o RabbitMQ.

O Produtor ao criar uma mensagem criará também uma Exchange com o nome do evento e uma fila de DeadLetter, se caso não existirem, para armazenar as mensagens.

<p align="center">
  <a href="">
    <img src=".github\images\dead-letter.gif" alt="dead-letter">
  </a>
</p>

O Consumidor ao ser executado irá: criar as filas, vincular (Bind) à Exchange do evento e desvincular a fila de DeadLetter. As mensagens armazenadas na fila de DeadLetter serão posteriormente reenviadas à Exchange para serem consumidas e processadas.

<p align="center">
  <a href="">
    <img src=".github\images\unbind-deadletter.gif" alt="dead-letter">
  </a>
</p>

Abaixo um exemplo de uma Exchange de "UserRegisteredEvent" redirecionando mensagens para as filas, que por sua vez, são nomeadas a partir de suas responsabilidades.

<p align="center">
  <a href="">
    <img src=".github\images\exchanges-and-queues.gif" alt="exchanges-and-queues">
  </a>
</p>

# Executando a aplicação
É possível executar a aplicação realizando a configuração manualmente, ou utilizando Docker (recomendado).

## Docker
Para rodar localmente, é possível utilizar o Docker.  
Abaixo o passo a passo para executar a aplicação localmente:
- Realizar o clone do projeto na pasta desejada:
    ```bash
        git clone https://github.com/pistoladas-group/tech-challenge-02.git
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