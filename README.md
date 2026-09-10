# 🎫 HelpDesk Pro

Sistema web para **gerenciamento de chamados de suporte técnico**, desenvolvido com **C# e ASP.NET Core MVC**.

O HelpDesk Pro simula o fluxo de atendimento de uma central de Help Desk, permitindo abertura, acompanhamento, atribuição e resolução de chamados de TI com diferentes níveis de acesso.

🌐 **Aplicação publicada:**  
https://helpdeskpro-production.up.railway.app/

---

## 🚀 Tecnologias utilizadas

- C#
- .NET 8
- ASP.NET Core MVC
- ASP.NET Core Identity
- Entity Framework Core
- Razor Pages
- SQLite
- HTML5
- CSS3
- Docker
- Git e GitHub
- Railway

---

## ✨ Funcionalidades

O sistema possui um fluxo completo para gerenciamento de chamados:

- Cadastro e autenticação de usuários
- Autorização baseada em perfis (Roles)
- Abertura de chamados
- Edição e acompanhamento de chamados
- Definição de prioridade
- Controle de status
- Atribuição de técnico
- Atendimento de chamados
- Registro da solução aplicada
- Data de conclusão
- Histórico de interações
- Dashboard administrativo
- Indicadores de chamados
- Controle de acesso de acordo com o perfil autenticado
- Interface responsiva
- Deploy utilizando Docker

---

## 🔐 Acesso de demonstração

A aplicação possui contas de demonstração para permitir a avaliação dos diferentes níveis de acesso.

### 👤 Usuário

**E-mail:** `usuario@demo.com`  
**Senha:** `Demo@123456`

O perfil de usuário permite testar a experiência do solicitante, incluindo a abertura e o acompanhamento dos próprios chamados.

### 🛠️ Técnico

**E-mail:** `tecnico@demo.com`  
**Senha:** `Demo@123456`

O perfil técnico permite testar o fluxo de atendimento dos chamados, registrar interações, adicionar a solução aplicada e concluir atendimentos.

> As contas acima são destinadas exclusivamente à demonstração da aplicação.

---

## 👥 Perfis de acesso

O HelpDesk Pro utiliza diferentes níveis de autorização para controlar as funcionalidades disponíveis.

### 👑 Administrador

O administrador possui uma visão mais ampla da operação.

Entre suas funcionalidades estão:

- Visualização geral dos chamados
- Dashboard administrativo
- Acompanhamento dos chamados
- Visualização de chamados críticos
- Atribuição de técnicos
- Gerenciamento do fluxo de atendimento

### 🛠️ Técnico

Responsável pelo atendimento dos chamados.

Entre suas funcionalidades estão:

- Acompanhamento dos chamados
- Atendimento de solicitações
- Registro de interações
- Inclusão da solução aplicada
- Alteração do status
- Conclusão do atendimento

### 👤 Usuário

Representa o solicitante do suporte.

Entre suas funcionalidades estão:

- Abertura de novos chamados
- Descrição do problema
- Definição da prioridade
- Acompanhamento dos próprios chamados
- Visualização do histórico
- Interação durante o atendimento

---

## 🎫 Fluxo de atendimento

O fluxo básico de um chamado funciona da seguinte maneira:

```text
Usuário abre um chamado
        ↓
      Aberto
        ↓
Técnico é atribuído ao chamado
        ↓
  Em atendimento
        ↓
Técnico registra interações
        ↓
Solução é registrada
        ↓
     Resolvido
```

Esse fluxo permite acompanhar o chamado desde a abertura até sua conclusão.

---

## 📊 Dashboard

O sistema possui um dashboard para acompanhamento rápido da situação dos chamados.

Entre os indicadores disponíveis estão:

- Chamados abertos
- Chamados em atendimento
- Chamados resolvidos
- Chamados críticos

Isso permite visualizar rapidamente o estado atual da operação de suporte.

---

## 💬 Histórico de interações

Os chamados possuem um sistema de interações que mantém o histórico do atendimento.

Cada interação pode registrar informações como:

- Mensagem
- Data
- Usuário responsável
- E-mail
- Perfil do usuário
- Chamado relacionado

Dessa forma, as comunicações realizadas durante o atendimento permanecem associadas ao chamado.

---

## 🔒 Autenticação e autorização

A autenticação foi implementada utilizando **ASP.NET Core Identity**.

O sistema trabalha com três Roles:

```text
Administrador
Tecnico
Usuario
```

As permissões e visualizações são controladas de acordo com o perfil autenticado.

Por exemplo, um usuário comum visualiza seus próprios chamados, enquanto os perfis responsáveis pelo atendimento possuem acesso às funcionalidades necessárias para gerenciar as solicitações.

---

## 🗄️ Banco de dados

O projeto utiliza **SQLite** em conjunto com o **Entity Framework Core**.

O Entity Framework é responsável pelo mapeamento das entidades e gerenciamento das alterações no banco através de migrations.

### Entidade Chamado

Entre as informações armazenadas estão:

- ID
- Título
- Descrição
- Status
- Prioridade
- Data de abertura
- Usuário responsável pela abertura
- E-mail do usuário
- Técnico responsável
- E-mail do técnico
- Solução
- Data de conclusão

### Entidade InteracaoChamado

Responsável pelo histórico das interações realizadas durante o atendimento.

Entre os dados armazenados estão:

- ID
- Chamado
- Mensagem
- Data
- Usuário
- E-mail
- Perfil

---

## 🏗️ Arquitetura

O projeto utiliza o padrão **MVC (Model-View-Controller)**.

A aplicação é organizada separando responsabilidades entre modelos, controladores, interface e persistência dos dados.

```text
Usuário
   ↓
Views / Razor
   ↓
Controllers
   ↓
Models
   ↓
Entity Framework Core
   ↓
SQLite
```

Essa separação facilita a organização, manutenção e evolução da aplicação.

---

## 🧩 Estrutura do projeto

A aplicação é dividida em componentes responsáveis por diferentes partes do sistema.

```text
HelpDeskWeb/
│
├── Controllers/
│   └── Regras e fluxo da aplicação
│
├── Models/
│   └── Entidades do sistema
│
├── Views/
│   └── Interface MVC
│
├── Data/
│   └── Contexto do Entity Framework
│
├── Areas/
│   └── Recursos relacionados ao Identity
│
├── wwwroot/
│   └── CSS, JavaScript e arquivos estáticos
│
├── Migrations/
│   └── Histórico de alterações do banco
│
├── Program.cs
├── appsettings.json
└── Dockerfile
```

---

## ⚙️ Entity Framework Core

O banco de dados é gerenciado através de migrations.

Durante o desenvolvimento, migrations foram utilizadas para evoluir a estrutura da aplicação conforme novas funcionalidades eram implementadas, incluindo:

- Estrutura inicial do banco
- Integração com Identity
- Associação entre usuário e chamado
- Associação de técnico ao chamado
- Solução e data de conclusão
- Histórico de interações

---

## 🐳 Docker

O projeto possui suporte a **Docker**, permitindo executar a aplicação em um ambiente containerizado.

Para criar a imagem:

```bash
docker build -t helpdeskpro .
```

Para executar o container:

```bash
docker run -p 8080:8080 helpdeskpro
```

Depois, a aplicação pode ser acessada em:

```text
http://localhost:8080
```

---

## 💻 Executando localmente

Para executar o projeto é necessário possuir o **.NET 8 SDK** instalado.

Clone o repositório:

```bash
git clone LINK_DO_REPOSITORIO
```

Entre na pasta:

```bash
cd HelpDeskWeb
```

Restaure as dependências:

```bash
dotnet restore
```

Aplique as migrations:

```bash
dotnet ef database update
```

Compile o projeto:

```bash
dotnet build
```

Execute:

```bash
dotnet run
```

O endereço local utilizado pela aplicação será informado pelo ASP.NET Core no terminal.

---

## 🌐 Deploy

A aplicação foi preparada para execução em container e está publicada no **Railway**.

**Aplicação online:**

https://helpdeskpro-production.up.railway.app/

---

## 🎯 Objetivo do projeto

O HelpDesk Pro foi desenvolvido como projeto de estudo e portfólio com o objetivo de aplicar conceitos de desenvolvimento web em uma aplicação funcional.

Durante o desenvolvimento foram trabalhados conceitos como:

- Desenvolvimento back-end com C#
- ASP.NET Core
- MVC
- CRUD
- Entity Framework Core
- Banco de dados
- Relacionamentos entre entidades
- Autenticação
- Autorização baseada em Roles
- Regras de negócio
- ASP.NET Core Identity
- Interface responsiva
- Docker
- Deploy de aplicações web

Mais do que uma demonstração visual, o objetivo foi construir um sistema com **fluxo de negócio, persistência de dados, autenticação, diferentes níveis de acesso e funcionamento completo de ponta a ponta**.

---

## 📚 Aprendizados

Este projeto também representa minha evolução prática no ecossistema **C# e .NET**.

Durante o desenvolvimento trabalhei desde a estruturação das entidades e do banco de dados até autenticação, autorização, controllers, views, migrations, Docker e publicação da aplicação.

O projeto foi desenvolvido de forma incremental, adicionando funcionalidades e corrigindo problemas durante o processo, o que permitiu aprofundar conhecimentos tanto em desenvolvimento back-end quanto no funcionamento completo de uma aplicação web.

---

## 👨‍💻 Autor

**Fernando Cardoso**

Estudante de Segurança da Informação e desenvolvedor web com interesse em **desenvolvimento back-end, C#, .NET e construção de aplicações web**.
