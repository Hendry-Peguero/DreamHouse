name: DreamHouse
description: >
  Plataforma para publicar y gestionar propiedades inmobiliarias con API REST en .NET y arquitectura limpia,
  enfocada en mantenibilidad, seguridad y escalabilidad.
estructura:
  - path: DreamHouse.Core.Domain/
    descripcion: Entidades, Value Objects, invariantes de negocio
  - path: DreamHouse.Core.Application/
    descripcion: Casos de uso, DTOs, validaciones, contratos
  - path: DreamHouse.Infrastructure.Identity/
    descripcion: Autenticación JWT, roles, usuarios
  - path: DreamHouse.Infrastructure.Persistence/
    descripcion: EF Core: DbContext, mapeos, migraciones
  - path: DreamHouse.Infrastructure.Shared/
    descripcion: Servicios y utilidades transversales
  - path: WebApi.DreamHouse/
    descripcion: Capa de presentación (API REST, Swagger)
  - path: DreamHouse.sln
    descripcion: Solución principal .NET
tecnologias:
  lenguaje: "C# (.NET, ASP.NET Core)"
  frameworks:
    - "ASP.NET Core Web API"
    - "Entity Framework Core"
    - "Swagger / OpenAPI"
  autenticacion:
    - "JWT Bearer Tokens"
    - "Roles y Policies"
  base_datos: "SQL Server (o RDBMS compatible)"
  utilidades: "Proyecto Infrastructure.Shared para servicios comunes"
patrones:
  - "Clean Architecture / Onion Architecture"
  - "CQRS / Commands & Queries en Application"
  - "Repository y Unit of Work con EF Core"
  - "DTOs para requests/responses"
  - "Validaciones en Application y reglas de negocio en Domain"
endpoints:
  - metodo: POST
    ruta: /api/auth/login
    descripcion: Autenticación, retorna JWT
  - metodo: POST
    ruta: /api/auth/register
    descripcion: Registro de usuarios
  - metodo: GET
    ruta: /api/properties
    query: "city, minPrice, maxPrice"
    descripcion: Listado con filtros
  - metodo: GET
    ruta: /api/properties/{id}
    descripcion: Detalle de propiedad
  - metodo: POST
    ruta: /api/properties
    descripcion: Crear propiedad (requiere rol)
  - metodo: PUT
    ruta: /api/properties/{id}
    descripcion: Actualizar propiedad
  - metodo: DELETE
    ruta: /api/properties/{id}
    descripcion: Eliminar propiedad
sugerencias_pruebas:
  unitarias: "En Domain y Application"
  integracion: "En la API usando WebApplicationFactory, InMemory DB o contenedor efímero"
seguridad:
  - "JWT de corta duración y (opcional) refresh tokens"
  - "Autorización basada en roles y policies"
  - "Validación estricta de DTOs y ModelState"
  - "CORS configurado para frontends permitidos"
  - "Rate limiting y headers de seguridad en Program/Startup"
roadmap:
  - "Paginación y ordenamiento en listados"
  - "Gestión y carga de imágenes (storage en la nube)"
  - "Auditoría de cambios y soft delete"
  - "Búsqueda avanzada (amenidades, m², tipo de operación)"
  - "Integración de CI/CD con GitHub Actions"
  - "Contenerización con Docker y despliegue en cloud"
colaboradores:
  - nombre: "Rafael Oscar Gómez Ruiz"
    rol: "Colaborador"
    perfil: "https://www.linkedin.com/in/rafael-oscar-gomez-ruiz-2271a029a/"
  - nombre: "Moises Girón"
    rol: "Colaborador"
    perfil: "https://www.linkedin.com/in/moises-giron/"
licencia: >
  Si no especificas una licencia, por defecto todos los derechos quedan reservados.
  Se recomienda incluir una licencia como MIT o Apache-2.0 para permitir contribu
