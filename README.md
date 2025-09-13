"DreamHouse"
  # DreamHouse

  Plataforma para **publicar y gestionar propiedades inmobiliarias** (listings) con API REST en .NET y arquitectura limpia, enfocada en mantenibilidad, seguridad y escalabilidad.

  ---

  ## **🧠 Tecnologías principales**

  | Área | Tecnología / Herramienta |
  |------|--------------------------|
  | Lenguaje / Plataforma | C# (.NET, ASP.NET Core) |
  | Persistencia de datos | Entity Framework Core, SQL Server (u otro RDBMS compatible) |
  | Autenticación / Seguridad | JWT (JSON Web Tokens), Roles |
  | API / Documentación | ASP.NET Core Web API, Swagger / OpenAPI |
  | Estructura / Arquitectura | Clean Architecture, separación por capas |
  | Utilidades comunes | Proyecto `Infrastructure.Shared` |
  | Testing | `dotnet test`, mocks, pruebas de integración |

  ---

  ## **🏗 Patrones y Prácticas**

  - Clean Architecture / Onion Architecture  
  - CQRS para casos de uso (Commands & Queries)  
  - Repository + Unit of Work para persistencia  
  - DTOs para requests/responses  
  - Validaciones en Application y reglas en Domain  

  ---

  ## **📂 Estructura del proyecto**
    estructura_proyecto:
      - carpeta: "DreamHouse.Core.Domain"
        descripcion: "Entidades, Value Objects, invariantes de negocio"
      - carpeta: "DreamHouse.Core.Application"
        descripcion: "Casos de uso, DTOs, validaciones, contratos"
      - carpeta: "DreamHouse.Infrastructure.Identity"
        descripcion: "Autenticación JWT, roles, usuarios"
      - carpeta: "DreamHouse.Infrastructure.Persistence"
        descripcion: "EF Core: DbContext, mapeos, migraciones"
      - carpeta: "DreamHouse.Infrastructure.Shared"
        descripcion: "Servicios/utilidades transversales"
      - carpeta: "WebApi.DreamHouse"
        descripcion: "Capa de presentación (API REST, Swagger)"
      - carpeta: "DreamHouse.sln"
        descripcion: "Solución .NET"

  ## **🗺 API**

| Método | Ruta | Descripción |
|-------|------|-------------|
| POST | /api/auth/login | Autenticación, retorna JWT |
| POST | /api/auth/register | Registro de usuarios |
| GET | /api/properties?city=...&minPrice=...&maxPrice=... | Listado con filtros |
| GET | /api/properties/{id} | Detalle |
| POST | /api/properties | Crear propiedad (requiere rol) |
| PUT | /api/properties/{id} | Actualizar |
| DELETE | /api/properties/{id} | Eliminar |

---

## **🧪 Pruebas sugeridas**

- Unitarias en `Domain` y `Application`
- Pruebas de integración en API usando `WebApplicationFactory` o DB InMemory

---

## **🔐 Seguridad**

- JWT de corta duración (opcional refresh tokens)  
- Autorización por roles/policies  
- Validación estricta de DTOs y ModelState  
- CORS configurado para frontends permitidos  
- Rate limiting y headers de seguridad en Program/Startup  

---

## **🧭 Roadmap**

- Paginación y ordenamiento en listados  
- Gestión de imágenes de propiedades (storage en nube)  
- Auditoría de cambios y soft delete  
- Búsqueda avanzada (amenidades, m², tipo de operación)  
- CI/CD con GitHub Actions  
- Contenerización con Docker y despliegue en cloud  

---

## **👥 Colaboradores**

| Nombre | Rol | Perfil |
|-------|-----|--------|
| Rafael Oscar Gómez Ruiz | Colaborador | [LinkedIn](https://www.linkedin.com/in/rafael-oscar-gomez-ruiz-2271a029a/) |
| Moises Girón | Colaborador | [LinkedIn](https://www.linkedin.com/in/moises-giron/) |
| Hendry Peguero Valdez | Colaborador | [[LinkedIn](https://www.linkedin.com/in/hendry-peguero-valdez-b6a02236b/) |

---

