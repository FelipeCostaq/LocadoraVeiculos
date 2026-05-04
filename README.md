# Locadora de Veículos

API desenvolvida com **.NET 10**, **Entity Framework**, **Clean Architecture**, **DDD** e autenticação via **ASP.NET Identity** com cookies.

> **Autenticação:** Endpoints marcados com 🔒 exigem que o usuário esteja autenticado via cookie de sessão (Identity API).

---

## Autenticação

### Admin credenciais padrão.

user: admin@locadora.com
password: Admin_Acesso_2026!

**Base URL:** `/auth`

### `POST /login?useCookies=true`

Realiza o login na API.

- **Autenticação:** Não requerida
- **Resposta de sucesso:** `200 OK`

---

## Categorias de Veículo

**Base URL:** `/categoria`

### `GET /categoria`

Lista todas as categorias de veículo cadastradas.

- **Autenticação:** Não requerida
- **Resposta de sucesso:** `200 OK`

```json
[
  {
    "id": "uuid",
    "nome": "string"
  }
]
```

---

### `POST /categoria` 🔒

Adiciona uma nova categoria de veículo.

- **Autenticação:** Requerida
- **Corpo da requisição:**

```json
{
  "nome": "string"
}
```

| Campo | Tipo   | Obrigatório | Descrição             |
|-------|--------|-------------|-----------------------|
| nome  | string | ✅           | Nome da categoria     |

- **Resposta de sucesso:** `201 Created`
- **Resposta de erro:** `400 Bad Request`

```json
{
  "mensagem": "Descrição do erro"
}
```

---

### `PUT /categoria?id={id}` 🔒

Edita uma categoria de veículo existente.

- **Autenticação:** Requerida
- **Parâmetro de query:**

| Parâmetro | Tipo | Obrigatório | Descrição                    |
|-----------|------|-------------|------------------------------|
| id        | Guid | ✅           | ID da categoria a ser editada |

- **Corpo da requisição:**

```json
{
  "nome": "string"
}
```

- **Resposta de sucesso:** `201 Created`
- **Resposta de erro:** `400 Bad Request`

```json
{
  "mensagem": "Descrição do erro"
}
```

---

## Clientes

**Base URL:** `/clientes`

> Todos os endpoints deste recurso exigem autenticação. 🔒

### `GET /clientes`

Lista todos os clientes cadastrados.

- **Autenticação:** Requerida
- **Resposta de sucesso:** `200 OK`

```json
[
  {
    "id": "uuid",
    "nome": "string",
    "cpf": "string",
    "email": "string"
  }
]
```

---

### `GET /clientes/{id}`

Retorna um cliente específico pelo ID.

- **Autenticação:** Requerida
- **Parâmetro de rota:**

| Parâmetro | Tipo | Obrigatório | Descrição          |
|-----------|------|-------------|--------------------|
| id        | Guid | ✅           | ID do cliente      |

- **Resposta de sucesso:** `200 OK`
- **Resposta de erro:** `404 Not Found` — quando o cliente não é encontrado

---

### `POST /clientes/add`

Adiciona um novo cliente.

- **Autenticação:** Requerida
- **Corpo da requisição:**

```json
{
  "nome": "string",
  "cpf": "string",
  "email": "string"
}
```

| Campo | Tipo   | Obrigatório | Descrição          |
|-------|--------|-------------|--------------------|
| nome  | string | ✅           | Nome do cliente    |
| cpf   | string | ✅           | CPF do cliente     |
| email | string | ✅           | E-mail do cliente  |

- **Resposta de sucesso:** `201 Created`
- **Resposta de erro:** `400 Bad Request`

```json
{
  "mensagem": "Descrição do erro"
}
```

---

### `PUT /clientes/edit?id={id}`

Edita os dados de um cliente existente.

- **Autenticação:** Requerida
- **Parâmetro de query:**

| Parâmetro | Tipo | Obrigatório | Descrição                  |
|-----------|------|-------------|----------------------------|
| id        | Guid | ✅           | ID do cliente a ser editado |

- **Corpo da requisição:**

```json
{
  "nome": "string",
  "email": "string"
}
```

- **Resposta de sucesso:** `201 Created`
- **Resposta de erro:** `400 Bad Request`

```json
{
  "mensagem": "Descrição do erro"
}
```

---

### `DELETE /clientes?id={id}`

Exclui um cliente pelo ID.

- **Autenticação:** Requerida
- **Parâmetro de query:**

| Parâmetro | Tipo | Obrigatório | Descrição                   |
|-----------|------|-------------|-----------------------------|
| id        | Guid | ✅           | ID do cliente a ser excluído |

- **Resposta de sucesso:** `204 No Content`
- **Resposta de erro:** `400 Bad Request`

```json
{
  "mensagem": "Descrição do erro"
}
```

---

## Veículos

**Base URL:** `/veiculos`

### `GET /veiculos`

Lista todos os veículos cadastrados.

- **Autenticação:** Não requerida
- **Resposta de sucesso:** `200 OK`

```json
[
  {
    "placa": "string",
    "modelo": "string",
    "categoriaId": "uuid",
    "disponivel": true
  }
]
```

---

### `GET /veiculos/disponivel`

Lista apenas os veículos disponíveis para aluguel.

- **Autenticação:** Não requerida
- **Resposta de sucesso:** `200 OK`

```json
[
  {
    "placa": "string",
    "modelo": "string",
    "categoriaId": "uuid"
  }
]
```

---

### `POST /veiculos` 🔒

Adiciona um novo veículo.

- **Autenticação:** Requerida
- **Corpo da requisição:**

```json
{
  "placa": "string",
  "modelo": "string",
  "categoriaId": "uuid"
}
```

| Campo       | Tipo   | Obrigatório | Descrição                    |
|-------------|--------|-------------|------------------------------|
| placa       | string | ✅           | Placa do veículo             |
| modelo      | string | ✅           | Modelo do veículo            |
| categoriaId | Guid   | ✅           | ID da categoria do veículo   |

- **Resposta de sucesso:** `201 Created`
- **Resposta de erro:** `400 Bad Request`

```json
{
  "mensagem": "Descrição do erro"
}
```

---

### `PUT /veiculos?placa={placa}` 🔒

Edita os dados de um veículo existente.

- **Autenticação:** Requerida
- **Parâmetro de query:**

| Parâmetro | Tipo   | Obrigatório | Descrição                   |
|-----------|--------|-------------|-----------------------------|
| placa     | string | ✅           | Placa do veículo a ser editado |

- **Corpo da requisição:**

```json
{
  "modelo": "string",
  "categoriaId": "uuid"
}
```

- **Resposta de sucesso:** `200 OK`
- **Resposta de erro:** `400 Bad Request`

```json
{
  "mensagem": "Descrição do erro"
}
```

---

## Veículos Alocados

**Base URL:** `/veiculoalocado`

### `GET /veiculoalocado` 🔒

Lista todos os registros de alocação de veículos.

- **Autenticação:** Requerida
- **Resposta de sucesso:** `200 OK`

```json
[
  {
    "id": "uuid",
    "veiculoId": "uuid",
    "clienteId": "uuid",
    "dataInicio": "datetime",
    "dataFim": "datetime",
    "status": "string"
  }
]
```

---

### `GET /veiculoalocado/disponibilidade`

Lista veículos alocados com informações de disponibilidade.

- **Autenticação:** Não requerida
- **Resposta de sucesso:** `200 OK`

```json
[
  {
    "veiculoId": "uuid",
    "disponivel": true
  }
]
```

---

### `POST /veiculoalocado/add` 🔒

Registra uma nova alocação de veículo.

- **Autenticação:** Requerida
- **Corpo da requisição:**

```json
{
  "veiculoId": "uuid",
  "clienteId": "uuid",
  "dataInicio": "datetime",
  "dataFim": "datetime"
}
```

| Campo      | Tipo     | Obrigatório | Descrição                    |
|------------|----------|-------------|------------------------------|
| veiculoId  | Guid     | ✅           | ID do veículo a ser alocado  |
| clienteId  | Guid     | ✅           | ID do cliente                |
| dataInicio | datetime | ✅           | Data de início da alocação   |
| dataFim    | datetime | ✅           | Data de fim da alocação      |

- **Resposta de sucesso:** `201 Created`
- **Resposta de erro:** `400 Bad Request`

```json
{
  "mensagem": "Descrição do erro"
}
```

---

### `PUT /veiculoalocado/darbaixa?id={id}` 🔒

Dá baixa em uma alocação, finalizando o aluguel.

- **Autenticação:** Requerida
- **Parâmetro de query:**

| Parâmetro | Tipo | Obrigatório | Descrição              |
|-----------|------|-------------|------------------------|
| id        | Guid | ✅           | ID da alocação         |

- **Resposta de sucesso:** `200 OK`
- **Resposta de erro:** `400 Bad Request`

```json
{
  "mensagem": "Descrição do erro"
}
```

---

### `PUT /veiculoalocado/cancelar?id={id}` 🔒

Cancela uma alocação de veículo.

- **Autenticação:** Requerida
- **Parâmetro de query:**

| Parâmetro | Tipo | Obrigatório | Descrição              |
|-----------|------|-------------|------------------------|
| id        | Guid | ✅           | ID da alocação         |

- **Resposta de sucesso:** `200 OK`
- **Resposta de erro:** `400 Bad Request`

```json
{
  "mensagem": "Descrição do erro"
}
```

---

## Códigos de Resposta

| Código | Significado                                      |
|--------|--------------------------------------------------|
| 200    | OK — requisição bem-sucedida                     |
| 201    | Created — recurso criado com sucesso             |
| 204    | No Content — operação realizada sem retorno      |
| 400    | Bad Request — dados inválidos ou erro de negócio |
| 401    | Unauthorized — autenticação necessária           |
| 404    | Not Found — recurso não encontrado               |
