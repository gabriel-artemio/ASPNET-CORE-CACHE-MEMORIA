# Cache de Memória com ASP.NET Core

## 📋 Sobre o projeto

Este projeto foi desenvolvido com o objetivo de demonstrar a criação de uma **Minimal API utilizando ASP.NET Core**, implementando uma arquitetura organizada em camadas e realizando operações CRUD utilizando o banco de dados **SQLite**.

A aplicação utiliza o padrão:

```
Endpoint
   ↓
BLL
   ↓
DAL
   ↓
SQLite
```

##  🚀 Tecnologias utilizadas
* .NET 8
* ASP.NET Core Minimal API
* SQLite
* Microsoft.Data.Sqlite
* IMemoryCache

## ⚡ Memory Cache

O projeto utiliza IMemoryCache para armazenar temporariamente os produtos em memória.

O fluxo funciona da seguinte forma:

```
GET /produtos
   ↓
Verifica o Cache
   ↓
┌───────────────┴───────────────┐
│                               │
Cache encontrado             Cache não encontrado
│                               │
↓                               ↓
Retorna dados                Consulta SQLite
do Cache                         ↓
                                Salva no Cache
                                    ↓
                              Retorna produtos
```

## 📚 Conceitos abordados

Este projeto demonstra:

* Criação de Minimal API
* Organização de endpoints em classes
* Agrupamento de rotas com MapGroup
* Arquitetura em camadas
* BLL
* DAL
* SQLite
* Injeção de Dependência
* CRUD
* Memory Cache
* Cache Invalidation
* Sliding Expiration
* Absolute Expiration
