
# 🌊 Alagamenos - Projeto .NET (Monitoramento de Alagamentos)

### 👥 Integrantes do Projeto

- **Gustavo de Aguiar Lima Silva** - RM: 557707  
- **Julio Cesar Conceição Rodrigues** - RM: 557298  
- **Matheus de Freitas Silva** - RM: 552602  

---

### 💡 Descrição da Solução

O projeto **Alagamenos**, desenvolvido em **.NET 9**, tem como objetivo o monitoramento inteligente de alagamentos em centros urbanos. A aplicação utiliza **Minimal APIs**, **Entity Framework Core** com banco de dados **Oracle** e uma interface gráfica via **Swagger (OpenAPI)**.

A solução permite o gerenciamento completo de alertas de alagamento, com integração de dados de localização (Estado, Cidade, Bairro, Rua), registro de usuários, vínculos entre usuários e alertas, além de histórico e filtros.

---

### 🛠️ Como Executar o Projeto Localmente

#### ✅ Pré-requisitos

Certifique-se de ter instalado:

- [.NET SDK 9.0+](https://dotnet.microsoft.com/en-us/download)
- IDE como **Rider**, **Visual Studio 2022+** ou **VS Code** com extensão C#

---

#### 🚀 Executando o Projeto

1. Clone ou extraia o repositório:

```bash
git clone https://github.com/Challenge-MottuGJM/dotnet.git
cd Alagamenos
```

2. Execute o projeto:

```bash
dotnet run --project Alagamenos.csproj
```

2. Acesse a API e a interface web:

- API: `http://localhost:5144`

---

### 📦 Tecnologias Utilizadas

- .NET 9
- Entity Framework Core + Oracle
- Minimal APIs
- Swagger (OpenAPI)
- C#

---

### 📬 Como Usar a API Localmente

Você pode interagir com os endpoints da API usando **Swagger (OpenAPI)**, **Postman**, **curl** ou navegador.

---

## 📋 Tabela de Endpoints da API

| Entidade | Método HTTP | Rota | Descrição |
|----------|--------------|------|------------|
| Alerta | GET | /alertas | Retorna todos os alertas |
| Alerta | GET | /alertas/paginadas | Retorna todos os alertas paginados |
| Alerta | GET | /alertas/{id} | Retorna um alerta com base em seu ID |
| Alerta | POST | /alertas/inserir | Insere um novo alerta ao banco |
| Alerta | PUT | /alerta/delete/{id} | Deleta um alerta com base em seu ID |
| Bairro | GET | /bairros | Retorna todos os bairros |
| Bairro | GET | /bairros/paginadas | Retora todos os bairros paginados |
| Bairro | GET | /bairros/{id} | Retorna um bairro com base em seu ID |
| Bairro | POST | /bairros/inserir | Insere um novo Bairro ao banco |
| Bairro | PUT | /bairros/atualizar/{id} | Atualiza um bairro no banco |
| Bairro | DELETE | /bairros/deletar/{id} | Deleta um bairro com base em seu ID |
| Cidade | GET | /cidades | Retorna todas as cidades |
| Cidade | GET | /cidades/paginadas | Retorna todas as cidades paginadas |
| Cidade | GET | /cidades/{id} | Retorna uma cidade com base em seu ID |
| Cidade | POST | /cidades/inserir | Insere uma cidade ao banco |
| Cidade | PUT | /cidades/atualizar/{id} | Atualiza uma cidade no banco |
| Cidade | DELETE | /cidades/deletar/{id} | Deleta uma cidade com base em seu ID |
| Endereco | GET | /enderecos | Retorna todos os enderecos |
| Endereco | GET | /enderecos/paginadas | Retorna todos os enderecos paginados |
| Endereco | GET | /enderecos/{id} | Retorna um endereco com base em seu ID |
| Endereco | POST | /enderecos/inserir | Insere um endereco ao banco |
| Endereco | PUT | /enderecos/atualizar/{id} | Atualiza um endereco no banco |
| Endereco | DELETE | /enderecos/deletar/{id} | Delete um endereco com base em seu ID |
| Estado | GET | /estados | Retorna todos os estados |
| Estado | GET | /estados/paginadas | Retorna todos os estados paginados |
| Estado | GET | /estados/{id} | Retorna um estado com base em seu ID |
| Estado | POST | /estados/inserir | Insere um estado ao banco de dados |
| Estado | PUT | /estados/atualizar/{id} | Atualiza um estado no estado |
| Estado | DELETE | /estados/deletar/{id} | Deleta um endereco com base em seu ID |
| Rua | GET | /ruas | Retorna todas as ruas |
| Rua | GET | /ruas/paginadas | Retorna todas as ruas paginadas |
| Rua | GET | /ruas/{id} | Retorna uma rua com base no ID |
| Rua | POST | /ruas/inserir | Insere uma rua ao banco |
| Rua | PUT | /ruas/atualizar/{id} | Atualiza uma rua no banco |
| Rua | DELETE | /ruas/deletar/{id} | Deleta uma rua com base em seu ID |
| Usuario | GET | /usuarios | Retorna todos os usuarios |
| Usuario | GET | /usuarios/paginadas | Retorna todos os usuarios paginados |
| Usuario | GET | /usuarios/{id} | Retorna um usuario com base em seu ID |
| Usuario | GET | /usuarios/email/{email} | Retorna um usuario com base em seu Email |
| Usuario | GET | /usuarios/telefone/{telefone} | Retorna um usuario com base em seu telefone |
| Usuario | POST | /usuarios/inserir | Insere um usuario ao banco |
| Usuario | PUT | /usuarios/atualizar/{id} | Atualiza um usuario |
| Usuario | DELETE | /usuarios/deletar/{id} | Deleta um usuario com base em seu ID |
| UsuarioAlerta | GET | /usuario-alertas | Retorna todos os alertas de usuários |
| UsuarioAlerta | GET | /usuario-alertas/paginadas | Retorna todos os alertas de usuários paginados |
| UsuarioAlerta | GET | /usuario-alertas/usuario/{usuarioId}/alerta/{alertaId} | Retorna um alerta com base no ID de usuário e ID do alerta |
| UsuarioAlerta | POST | /usuario-alertas/inserir | Insere um alerta de usuário no banco |
| UsuarioAlerta | DELETE | /usuario-alertas/deletar/{usuarioId}/{alertaId} | Deleta um alerta de usuário com base no ID do usuário e ID do Alerta |

