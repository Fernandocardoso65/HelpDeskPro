# 🎫 HelpDesk Pro

Sistema web de gerenciamento de chamados de TI desenvolvido com **C# e ASP.NET Core MVC**.

O projeto simula o fluxo de uma central de atendimento de TI, permitindo que usuários abram chamados, administradores façam a distribuição dos atendimentos e técnicos acompanhem e solucionem os tickets.

## 🚀 Funcionalidades

- Autenticação de usuários
- Controle de acesso baseado em perfis
- Perfis de Administrador, Técnico e Usuário
- Abertura de chamados
- Definição de prioridade
- Acompanhamento de status
- Atribuição de chamados para técnicos
- Dashboard específico para cada perfil
- Registro da solução técnica
- Data de conclusão do atendimento
- Histórico de interações
- Comunicação entre usuário e técnico
- Controle de acesso aos chamados
- Persistência dos dados em banco SQLite

## 👥 Perfis do sistema

### Usuário

Pode:

- abrir chamados;
- visualizar seus próprios chamados;
- acompanhar o andamento;
- enviar mensagens no histórico;
- visualizar a solução apresentada pelo técnico.

### Técnico

Pode:

- visualizar chamados atribuídos a ele;
- acompanhar informações do ticket;
- interagir com o usuário;
- registrar procedimentos realizados;
- alterar o status do atendimento;
- registrar a solução do chamado.

### Administrador

Pode:

- visualizar todos os chamados;
- administrar o fluxo de atendimento;
- atribuir e reatribuir chamados aos técnicos;
- editar chamados;
- acessar o painel administrativo.

## 🛠️ Tecnologias

- C#
- .NET 8
- ASP.NET Core MVC
- ASP.NET Core Identity
- Entity Framework Core
- SQLite
- Razor
- Bootstrap
- HTML
- CSS

## 🏗️ Arquitetura

O projeto utiliza o padrão **MVC (Model-View-Controller)**.

A aplicação utiliza o Entity Framework Core para persistência dos dados e ASP.NET Core Identity para autenticação e gerenciamento de usuários e perfis.

## 🔐 Segurança

O sistema implementa autenticação e autorização baseada em roles.

Os chamados possuem regras de acesso para impedir que usuários ou técnicos visualizem tickets que não pertencem a eles.

Credenciais utilizadas para criação de contas administrativas e técnicas não são armazenadas diretamente no código-fonte.

O banco de dados local também não é versionado no repositório.

## 📌 Fluxo de atendimento

```text
Usuário
   ↓
Abre chamado
   ↓
Administrador
   ↓
Atribui técnico
   ↓
Técnico
   ↓
Atendimento
   ↕
Histórico de interações
   ↕
Usuário
   ↓
Solução
   ↓
Chamado resolvido
