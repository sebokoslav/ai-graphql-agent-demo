# AI GraphQL Agent Demo

**AI-ready GraphQL playground over a classic PostgreSQL sample DB — explore, query, and visualize relational data with HotChocolate and EF Core.**

## Overview

This repository demonstrates a full-stack sample project combining a classic example database with a modern .NET 8 GraphQL API. It’s designed for experimenting with AI agents, data querying, and visualization in a Kubernetes-friendly environment.

## Features

- **Database:** Uses the [Pagila](https://github.com/devrimgunduz/pagila) PostgreSQL sample database (films, actors, customers, rentals, payments, categories).  
- **GraphQL API:** Built with [HotChocolate](https://hotchocolate.io/) and EF Core, exposing all main entities and their relationships.  
- **Queryable:** Full support for filtering, sorting, and paging over all entities.  
- **Navigation:** Traverse relationships such as `Actor → FilmActors → Film` or `Customer → Rentals → Payments`.  
- **Docker-Ready:** Database runs in Docker (Pagila container), can be extended to run GraphQL API container.  
- **Kubernetes-Ready:** Can be deployed locally with Minikube or on any k8s cluster.  

## Setup

### 1. Clone the repository

```bash
git clone https://github.com/sebokoslav/ai-graphql-agent-demo.git
cd ai-graphql-agent-demo
```

### 2. Spin up the database

```bash
cd pagila-docker
docker-compose up -d
```

### 3. Run the GraphQL API
```bash
dotnet run --project PagilaGraphQL
```

### 4. Explore GraphQL
Open http://localhost:5287/graphql with Banana Cake Pop or any GraphQL client.

## Example GraphQL Queries
### Actors with films
```graphql
{
  actors(first: 10, order: { lastName: ASC }) {
    nodes {
      actorId
      firstName
      lastName
      filmActors {
        film {
          title
        }
      }
    }
  }
}
```

### Films with categories
```graphql
{
  films(first: 10) {
    nodes {
      title
      description
      filmCategories {
        category {
          name
        }
      }
    }
  }
}
```

### Customers with Rentals and Payments
```graphql
{
  customers(first: 10) {
    nodes {
      firstName
      lastName
      rentals {
        rentalDate
        film {
          title
        }
      }
      payments {
        amount
      }
    }
  }
}
```
## AI Agent Integration Ideas
- Use an AI agent to query the GraphQL API and automatically generate reports or visualizations.
- Implement natural language prompts that translate into GraphQL queries.
- Build a dashboard with charts powered by AI-driven insights from the Pagila data.
- Run the whole solution in the k8s cluster
