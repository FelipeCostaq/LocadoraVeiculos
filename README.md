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

---

### `POST /categoria` 🔒

Adiciona uma nova categoria de veículo.

- **Autenticação:** Requerida

- **Resposta de sucesso:** `201 Created`
- **Resposta de erro:** `400 Bad Request`

---

### `PUT /categoria?id={id}` 🔒

Edita uma categoria de veículo existente.

- **Autenticação:** Requerida
- **Parâmetro de query:**

- **Resposta de sucesso:** `201 Created`
- **Resposta de erro:** `400 Bad Request`
- 
---

## Clientes

**Base URL:** `/clientes`

> Todos os endpoints deste recurso exigem autenticação. 🔒

### `GET /clientes`

Lista todos os clientes cadastrados.

- **Autenticação:** Requerida
- **Resposta de sucesso:** `200 OK`

---

### `GET /clientes/{id}`

Retorna um cliente específico pelo ID.

- **Autenticação:** Requerida
- **Parâmetro de rota:**

- **Resposta de sucesso:** `200 OK`
- **Resposta de erro:** `404 Not Found` — quando o cliente não é encontrado

---

### `POST /clientes/add`

Adiciona um novo cliente.

- **Autenticação:** Requerida

- **Resposta de sucesso:** `201 Created`
- **Resposta de erro:** `400 Bad Request`

---

### `PUT /clientes/edit?id={id}`

Edita os dados de um cliente existente.

- **Autenticação:** Requerida
- **Parâmetro de query:**

- **Resposta de sucesso:** `201 Created`
- **Resposta de erro:** `400 Bad Request`

---

### `DELETE /clientes?id={id}`

Exclui um cliente pelo ID.

- **Autenticação:** Requerida
- **Parâmetro de query:**
- 
- **Resposta de sucesso:** `204 No Content`
- **Resposta de erro:** `400 Bad Request`

---

## Veículos

**Base URL:** `/veiculos`

### `GET /veiculos`

Lista todos os veículos cadastrados.

- **Autenticação:** Não requerida
- **Resposta de sucesso:** `200 OK`

---

### `GET /veiculos/disponivel`

Lista apenas os veículos disponíveis para aluguel.

- **Autenticação:** Não requerida
- **Resposta de sucesso:** `200 OK`
  
---

### `POST /veiculos` 🔒

Adiciona um novo veículo.

- **Autenticação:** Requerida

- **Resposta de sucesso:** `201 Created`
- **Resposta de erro:** `400 Bad Request`

---

### `PUT /veiculos?placa={placa}` 🔒

Edita os dados de um veículo existente.

- **Autenticação:** Requerida
- **Parâmetro de query:**

- **Resposta de sucesso:** `200 OK`
- **Resposta de erro:** `400 Bad Request`

---

## Veículos Alocados

**Base URL:** `/veiculoalocado`

### `GET /veiculoalocado` 🔒

Lista todos os registros de alocação de veículos.

- **Autenticação:** Requerida
- **Resposta de sucesso:** `200 OK`

---

### `GET /veiculoalocado/disponibilidade`

Lista veículos alocados com informações de disponibilidade.

- **Autenticação:** Não requerida
- **Resposta de sucesso:** `200 OK`

---

### `POST /veiculoalocado/add` 🔒

Registra uma nova alocação de veículo.

- **Autenticação:** Requerida

- **Resposta de sucesso:** `201 Created`
- **Resposta de erro:** `400 Bad Request`

---

### `PUT /veiculoalocado/darbaixa?id={id}` 🔒

Dá baixa em uma alocação, finalizando o aluguel.

- **Autenticação:** Requerida
- **Parâmetro de query:**

- **Resposta de sucesso:** `200 OK`
- **Resposta de erro:** `400 Bad Request`

---

### `PUT /veiculoalocado/cancelar?id={id}` 🔒

Cancela uma alocação de veículo.

- **Autenticação:** Requerida
- **Parâmetro de query:**

- **Resposta de sucesso:** `200 OK`
- **Resposta de erro:** `400 Bad Request`

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
