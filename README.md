# 🟣 Figurou

> Uma plataforma inteligente para gerenciamento de álbuns de figurinhas, controle de coleção e conexão entre colecionadores.

O **Figurou** nasceu de uma dificuldade real durante a experiência de colecionar figurinhas: controlar quais figurinhas já estavam na coleção, quais ainda faltavam e quais estavam repetidas.

Com álbuns cada vez maiores, fazer esse gerenciamento manualmente se torna trabalhoso e sujeito a erros.

A proposta do Figurou é centralizar toda essa experiência em uma única plataforma, permitindo **gerenciar a coleção, realizar leituras com IA, encontrar outros colecionadores, negociar trocas e acompanhar a evolução do álbum**.

---

## 🎥 Demonstração

O projeto possui uma interface responsiva desenvolvida para utilização tanto em desktop quanto em dispositivos móveis.

> 📌 Adicione aqui o link para o vídeo de demonstração publicado no LinkedIn.

---

## ✨ Funcionalidades

### 📚 Álbum Virtual

Visualização completa da coleção do usuário.

É possível acompanhar:

- Figurinhas que possui;
- Figurinhas faltantes;
- Figurinhas repetidas;
- Quantidade de cada figurinha;
- Progresso por página;
- Progresso geral do álbum;
- Diferentes raridades de figurinhas.

O usuário também pode atualizar manualmente as quantidades diretamente pelo álbum virtual.

---

### 📷 Leitura de Figurinhas com IA

Permite utilizar a câmera do dispositivo para identificar uma figurinha.

O fluxo de leitura realiza o processamento da imagem e identifica o código da figurinha, permitindo adicioná-la diretamente à coleção.

A funcionalidade utiliza **OCR e integração com serviços de Inteligência Artificial** para auxiliar no reconhecimento.

---

### 📄 Leitura de Página

Além da leitura individual, o sistema possui funcionalidade para processamento de páginas do álbum.

A proposta é facilitar a atualização da coleção através da identificação das figurinhas presentes na página.

---

### 🤝 Match Inteligente

O sistema cruza automaticamente as coleções dos usuários procurando oportunidades de troca.

O algoritmo considera:

**Figurinhas que eu tenho repetidas e o outro usuário precisa**

×  

**Figurinhas que o outro usuário possui repetidas e eu preciso**

A partir desse cruzamento, são apresentados usuários compatíveis para uma possível negociação.

---

### 📍 Busca por Localização

Os matches podem considerar a localização dos colecionadores.

Isso permite encontrar usuários próximos que possuem figurinhas compatíveis para troca, facilitando negociações presenciais.

---

### 🔄 Gerenciamento de Trocas

Após encontrar um usuário compatível, é possível iniciar uma negociação.

O sistema permite:

- Selecionar as figurinhas da negociação;
- Solicitar uma troca;
- Aprovar uma solicitação;
- Cancelar uma negociação;
- Validar a disponibilidade das figurinhas;
- Reservar figurinhas envolvidas em negociações;
- Acompanhar o status da troca;
- Finalizar a negociação.

As regras de disponibilidade e quantidade são tratadas pela aplicação para evitar inconsistências durante as trocas.

---

### 💬 Chat em Tempo Real

Usuários envolvidos em uma negociação podem conversar diretamente pela plataforma.

A comunicação em tempo real utiliza **SignalR**, permitindo troca de mensagens sem necessidade de atualizar a página.

---

### 🔔 Notificações

O sistema possui notificações para acompanhar eventos importantes da plataforma, como:

- Novas solicitações de troca;
- Alterações em negociações;
- Novos matches;
- Mensagens e interações.

---

### 👤 Perfil do Colecionador

Cada usuário possui seu próprio perfil dentro da plataforma, permitindo acompanhar informações relacionadas à conta e sua evolução no sistema.

---

### 📖 Suporte a diferentes álbuns

O Figurou não foi desenvolvido especificamente para um único álbum.

Os álbuns são cadastrados dinamicamente, permitindo que novos álbuns sejam adicionados pelo administrador e posteriormente selecionados pelos usuários.

Dessa forma, a estrutura do sistema pode ser reutilizada para diferentes coleções.

---

## 🛠️ Tecnologias

O projeto foi desenvolvido utilizando principalmente:

### Back-end

- C#
- .NET / ASP.NET Core MVC
- Entity Framework Core
- ASP.NET Core Identity
- SignalR
- FluentValidation
- SQL Server

### Front-end

- Razor Views
- HTML
- CSS
- JavaScript
- jQuery
- AJAX
- Bootstrap

### Inteligência Artificial

- OCR
- Azure AI / Document Intelligence
- Processamento de imagens para reconhecimento de figurinhas

### Desenvolvimento

- Git
- GitHub
- Visual Studio
- Entity Framework Migrations

---

## 🏗️ Arquitetura

O projeto foi estruturado buscando separar responsabilidades entre as diferentes partes da aplicação.

```text
Figurou
│
├── Figurou.WebApp
│   ├── Controllers
│   ├── Views
│   ├── ViewModels
│   └── InputModels
│
├── Figurou.Business
│   ├── Models
│   ├── Services
│   ├── Interfaces
│   ├── DTOs
│   └── Validações
│
├── Figurou.Data
│   ├── Context
│   ├── Mappings
│   ├── Repositories
│   └── Migrations
│
└── Figurou.Tests
```

A aplicação utiliza conceitos como:

- Separação em camadas;
- Dependency Injection;
- Repository Pattern;
- Services;
- DTOs;
- ViewModels e InputModels;
- Domain Rules;
- Validações;
- Programação assíncrona;
- Separação entre regras de negócio e apresentação.

---

## 🔐 Segurança e configurações

Informações sensíveis, como chaves de APIs e credenciais externas, **não devem ser armazenadas diretamente no repositório**.

Durante o desenvolvimento, utilize:

```bash
dotnet user-secrets
```

Exemplo:

```bash
dotnet user-secrets set "AzureDocumentIntelligence:Key" "SUA_CHAVE"
```

> ⚠️ Nunca publique chaves de API, connection strings de produção ou outras credenciais no GitHub.

---

## 🚀 Executando o projeto

### Pré-requisitos

Antes de executar a aplicação, tenha instalado:

- .NET SDK
- SQL Server
- Visual Studio ou Visual Studio Code
- Git

Clone o repositório:

```bash
git clone URL_DO_REPOSITORIO
```

Acesse o projeto:

```bash
cd Figurou
```

Restaure as dependências:

```bash
dotnet restore
```

Configure a connection string e as chaves necessárias utilizando `appsettings.Development.json` e/ou User Secrets.

Execute as migrations:

```bash
dotnet ef database update
```

Execute a aplicação:

```bash
dotnet run
```

---

## 🗺️ Próximas melhorias

O Figurou continua em desenvolvimento.

Algumas melhorias planejadas incluem:

- [ ] Evolução do Dashboard;
- [ ] Novos indicadores sobre a coleção;
- [ ] Melhorias na experiência de leitura por IA;
- [ ] Evolução do sistema de notificações;
- [ ] Melhorias no sistema de Match;
- [ ] Novas funcionalidades para gerenciamento de álbuns;
- [ ] Melhorias de responsividade e experiência mobile;
- [ ] Ampliação da cobertura de testes;
- [ ] Novas funcionalidades administrativas.

---

## 💡 Motivação

Mais do que desenvolver uma aplicação para portfólio, o objetivo do projeto foi transformar um **problema que encontrei no mundo real em uma solução de software**.

Durante o desenvolvimento foi necessário trabalhar diferentes desafios, desde modelagem e regras de negócio até comunicação em tempo real, geolocalização, processamento de imagens e integração com serviços de IA.

O projeto continua sendo utilizado como ambiente de estudo e evolução das minhas habilidades no ecossistema **.NET**.

---

## 🤝 Contribuições

Contribuições e sugestões são bem-vindas.

Caso encontre alguma melhoria ou tenha alguma ideia para o projeto, fique à vontade para:

1. Abrir uma **Issue**;
2. Criar um **Fork** do projeto;
3. Criar uma branch para sua alteração;
4. Implementar a melhoria;
5. Abrir um **Pull Request**.

Feedbacks sobre **arquitetura, código, regras de negócio, interface ou novas funcionalidades** também são muito bem-vindos.

---

## 👨‍💻 Autor

Desenvolvido por **Rafael Nascimento**.

Projeto desenvolvido com foco em aprendizado, evolução profissional e aplicação prática de tecnologias do ecossistema .NET.

⭐ Se você gostou do projeto, considere deixar uma **Star** no repositório.
