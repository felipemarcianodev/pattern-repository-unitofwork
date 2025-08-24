
# 📋 Repository Pattern com Unit of Work - POC

[![.NET](https://img.shields.io/badge/.NET-9.0-blue)](https://dotnet.microsoft.com/)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-9.0.5-green)](https://docs.microsoft.com/en-us/ef/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-LocalDB-orange)](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)

Uma **Prova de Conceito (POC)** demonstrando a implementação dos padrões **Repository** e **Unit of Work** em uma API C# com Entity Framework Core. O projeto simula um sistema de vendas com controle transacional, mostrando na prática a diferença entre operações com e sem gerenciamento de transações.

## 🎯 Objetivo

Demonstrar como implementar e utilizar os padrões Repository e Unit of Work para:
- Garantir **integridade dos dados** através de controle transacional
- **Abstrair a camada de dados** para facilitar manutenção e testes
- **Centralizar operações** relacionadas em uma única transação
- Comparar cenários **com e sem** controle transacional

## 🚀 Tecnologias Utilizadas

- **.NET 9.0** - Framework principal
- **ASP.NET Core Web API** - API REST
- **Entity Framework Core 9.0.5** - ORM
- **SQL Server LocalDB** - Banco de dados
- **Repository Pattern** - Abstração da camada de dados
- **Unit of Work Pattern** - Controle transacional

## 📊 Modelo de Dados

O sistema possui 3 entidades principais:

```
Cliente (1) -----> (N) Venda (N) <----- (1) Produto
   |                     |                    |
   ├─ Id                 ├─ Id                ├─ Id
   ├─ Nome               ├─ ClienteId         ├─ Nome
   ├─ Email              ├─ ProdutoId         ├─ Valor
   ├─ Celular            ├─ Quantidade        ├─ SaldoEstoque
   ├─ Endereco           └─ DataCadastro      └─ DataCadastro
   ├─ DataCadastro
   └─ DataUltimaCompra
```

## 🏗️ Arquitetura do Projeto

```
ExemploUnitOfWork.API/
├── Controllers/          # Controladores da API
├── Models/              # Entidades/Modelos de dados
├── Context/             # DbContext do Entity Framework
├── Interfaces/          # Contratos (Repository, Services, UoW)
│   ├── Repositories/    # Interfaces dos repositórios
│   └── Services/        # Interfaces dos serviços
├── Repositories/        # Implementações dos repositórios
├── Services/           # Lógica de negócio
├── Migrations/         # Migrações do banco
└── UnitOfWork.cs       # Implementação do Unit of Work
```

## ⚡ Pré-requisitos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [SQL Server LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb) (incluído no Visual Studio)
- IDE: Visual Studio 2022 ou VS Code

## 🛠️ Instalação e Configuração

### 1. Clone o repositório
```bash
git clone https://github.com/felipemarcianodev/pattern-repository-unitofwork.git
cd pattern-repository-unitofwork
```

### 2. Instale as dependências
```bash
# Instalar Entity Framework CLI globalmente
dotnet tool install --global dotnet-ef --version 9.0.5 

# Navegar para o projeto
cd ExemploUnitOfWork.API

# Restaurar pacotes NuGet
dotnet restore
```

### 3. Configure o banco de dados
```bash
# Criar e aplicar migrations
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Execute a aplicação
```bash
dotnet run
```

A API estará disponível em: `https://localhost:7xxx` (a porta será exibida no console)

## 📚 Funcionalidades Principais

### ✅ **Endpoint COM Transação** (`POST /api/vendas/com-transacao`)
Demonstra o **uso correto** do Unit of Work:
1. Cria uma venda
2. Atualiza a data da última compra do cliente  
3. Diminui o estoque do produto
4. **Se qualquer operação falha, TODAS são desfeitas**

### ❌ **Endpoint SEM Transação** (`POST /api/vendas/sem-transacao`)
Demonstra o **problema da falta** de controle transacional:
1. Cria uma venda ✅ (salva no banco)
2. Atualiza o cliente ✅ (salva no banco)
3. **Erro simulado** ❌
4. Estoque não é atualizado, mas venda e cliente ficam salvos

## 🧪 Testando a Aplicação

### Exemplo de requisição - Venda COM transação:
```bash
POST /api/vendas/com-transacao
Content-Type: application/json

{
  "ClienteId": 1,
  "ProdutoId": 1,
  "Quantidade": 2
}
```

### Exemplo de requisição - Venda SEM transação:
```bash
POST /api/vendas/sem-transacao
Content-Type: application/json

{
  "ClienteId": 1,
  "ProdutoId": 1,
  "Quantidade": 2
}
```

## 🎨 Padrões Implementados

### **Repository Pattern**
- **GenericRepository**: Operações CRUD básicas para qualquer entidade
- **Repositories específicos**: Consultas customizadas (ex: ClienteRepository.GetByEmailAsync)
- **Abstração**: Isola a lógica de negócio dos detalhes do Entity Framework

### **Unit of Work Pattern**
- **Controle transacional**: BeginTransaction, Commit, Rollback
- **Coordenação**: Gerencia múltiplos repositories em uma única transação
- **Consistência**: Garante que operações relacionadas sejam executadas juntas

## 📈 Vantagens Demonstradas

| Aspecto | Com UoW | Sem UoW |
|---------|---------|---------|
| **Consistência** | ✅ Garantida | ❌ Pode falhar |
| **Rollback** | ✅ Automático | ❌ Impossível |
| **Performance** | ✅ Uma transação | ⚠️ Múltiplas transações |
| **Testabilidade** | ✅ Fácil mock | ❌ Difícil |

## 🔧 Estrutura dos Principais Arquivos

- **`UnitOfWork.cs`**: Implementação do padrão Unit of Work
- **`GenericRepository.cs`**: Repository base com operações CRUD
- **`VendaController.cs`**: Demonstra uso com/sem transação
- **`ApplicationDbContext.cs`**: Configuração do Entity Framework
- **`Models/`**: Entidades com lógica de negócio encapsulada

## 📝 Próximos Passos

- [ ] Implementar logs estruturados
- [ ] Adicionar testes unitários
- [ ] Configurar Docker
- [ ] Implementar cache
- [ ] Adicionar documentação Swagger

## 🤝 Contribuindo

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/nova-feature`)
3. Commit suas mudanças (`git commit -m 'Adiciona nova feature'`)
4. Push para a branch (`git push origin feature/nova-feature`)
5. Abra um Pull Request

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## 👨‍💻 Autor

**Felipe Santos Marciano**
- GitHub: [@felipemarcianodev](https://github.com/felipemarcianodev)
- LinkedIn: [Felipe Santos Marciano](https://www.linkedin.com/in/felipe-santos-marciano/)

---

⭐ **Se este projeto foi útil para você, considere dar uma estrela!**
