# 🔷 Proyecto: tareaSem9 - Arquitectura Hexagonal (Ports & Adapters)

Este proyecto implementa la **Arquitectura Hexagonal (Puertos y Adaptadores)** en **ASP.NET Core (.NET 10)** tomando como base funcional y de datos el sistema académico y de seguridad de **Lab06**.

---

## 🏛️ 1. Arquitectura Hexagonal Implementada

La aplicación aísla por completo el núcleo del negocio (Dominio y Casos de Uso) de las dependencias externas (Base de datos PostgreSQL, Entity Framework Core, HTTP REST y JWT):

```
tareaSem9/
├── Core/                                  # 🔶 EL HEXÁGONO (NÚCLEO)
│   ├── Domain/
│   │   ├── Entities/                      # Entidades POCO puras de negocio
│   │   │   ├── Asistencia.cs
│   │   │   ├── Curso.cs
│   │   │   ├── Estudiante.cs
│   │   │   ├── Evaluacione.cs
│   │   │   ├── Materia.cs
│   │   │   ├── Matricula.cs
│   │   │   ├── Profesore.cs
│   │   │   └── Usuario.cs
│   │   └── DTOs/                          # Contratos de transferencia (LoginDto, RegisterDto, AuthResponseDto)
│   │       └── AuthDtos.cs
│   ├── Ports/
│   │   ├── Inbound/                       # 🚪 PUERTOS DE ENTRADA (Driving Ports / Casos de Uso)
│   │   │   ├── IAuthUseCase.cs
│   │   │   ├── IEstudianteUseCase.cs
│   │   │   ├── ICursoUseCase.cs
│   │   │   ├── IMatriculaUseCase.cs
│   │   │   ├── IEvaluacioneUseCase.cs
│   │   │   ├── IAsistenciaUseCase.cs
│   │   │   ├── IMateriaUseCase.cs
│   │   │   └── IProfesoreUseCase.cs
│   │   └── Outbound/                      # 🔌 PUERTOS DE SALIDA (Driven Ports / Interfaces de Repositorio y Servicios)
│   │       ├── IGenericRepositoryPort.cs
│   │       ├── IUnitOfWorkPort.cs
│   │       ├── ITokenServicePort.cs
│   │       └── IPasswordHasherPort.cs
│   └── Application/
│       └── UseCases/                      # ⚙️ IMPLEMENTACIÓN DE CASOS DE USO (Lógica pura de negocio)
│           ├── AuthUseCase.cs
│           ├── EstudianteUseCase.cs
│           ├── CursoUseCase.cs
│           ├── MatriculaUseCase.cs
│           ├── EvaluacioneUseCase.cs
│           ├── AsistenciaUseCase.cs
│           ├── MateriaUseCase.cs
│           └── ProfesoreUseCase.cs
│
├── Adapters/                              # 🔌 ADAPTADORES (MUNDO EXTERNO)
│   ├── Inbound/                           # 🚗 ADAPTADORES DE ENTRADA (Driving Adapters)
│   │   └── Controllers/                   # Controladores REST API (HTTP -> Puertos Inbound)
│   │       ├── AuthController.cs
│   │       ├── EstudiantesController.cs
│   │       ├── CursosController.cs
│   │       ├── MatriculasController.cs
│   │       ├── EvaluacionesController.cs
│   │       ├── AsistenciasController.cs
│   │       ├── MateriasController.cs
│   │       └── ProfesoresController.cs
│   └── Outbound/                          # 🛠️ ADAPTADORES DE SALIDA (Driven Adapters)
│       ├── Persistence/                   # Persistencia con EF Core y PostgreSQL
│       │   ├── ApplicationDbContext.cs
│       │   ├── GenericRepositoryAdapter.cs
│       │   └── UnitOfWorkAdapter.cs
│       └── Security/                      # Generación de JWT y Hashing SHA-256
│           ├── JwtTokenServiceAdapter.cs
│           └── Sha256PasswordHasherAdapter.cs
│
├── Program.cs                             # Raíz de composición (Inyección de dependencias)
├── appsettings.json                       # Configuración de PostgreSQL y JWT
├── tareaSem9.sln / tareaSem9.slnx         # Solución para Visual Studio / JetBrains Rider
└── tareaSem9.http                         # Peticiones HTTP listas para probar
```

---

## 🔐 2. Guía de Pruebas en Swagger con JWT

### Paso 1: Ejecutar la API
Desde la terminal en `c:\Cnet\tareaSem9\tareaSem9`:
```powershell
dotnet run
```
Abre en tu navegador la URL de Swagger que muestra la consola (usualmente `https://localhost:7084/swagger` o el puerto asignado).

---

### Paso 2: Registrar Usuarios de Prueba

En Swagger, dirígete a `POST /api/Auth/register`:

#### 1. Crear Administrador:
```json
{
  "username": "admin_hex",
  "password": "Password123!",
  "role": "Admin"
}
```
*Respuesta:* `200 OK` (`"Usuario registrado exitosamente con contraseña encriptada"`).

#### 2. Crear Alumno / Usuario Regular:
```json
{
  "username": "alumno_hex",
  "password": "Password123!",
  "role": "User"
}
```

---

### Paso 3: Iniciar Sesión (Login) y Obtener el Token JWT

Ve a `POST /api/Auth/login`:
```json
{
  "username": "admin_hex",
  "password": "Password123!"
}
```
*Respuesta:*
```json
{
  "token": "eyJhbGciOiJIUzI1NiIs...",
  "username": "admin_hex",
  "role": "Admin"
}
```

---

### Paso 4: Autorizar en Swagger (Candado Verde)

1. Copia el valor del campo `token`.
2. Sube a la parte superior de Swagger y haz clic en el botón verde **Authorize 🔓**.
3. Pega el token directamente y haz clic en **Authorize**.
4. Ahora todas las peticiones incluirán la cabecera `Authorization: Bearer <token>`.

---

### Paso 5: Probar Endpoints de Seguridad y Roles

| Endpoint | Rol Permitido | Con Admin (`admin_hex`) | Con User (`alumno_hex`) | Sin Token |
| :--- | :--- | :---: | :---: | :---: |
| `GET /api/Auth/admin` | `Admin` | `200 OK` | `403 Forbidden` | `401 Unauthorized` |
| `GET /api/Auth/admin-politica` | Política `Administrador` | `200 OK` | `403 Forbidden` | `401 Unauthorized` |
| `GET /api/Auth/user` | `User` | `403 Forbidden` | `200 OK` | `401 Unauthorized` |
| `GET /api/Auth/perfil` | Autenticado | `200 OK` | `200 OK` | `401 Unauthorized` |

---

## 📚 3. Pruebas de Endpoints Académicos (CRUD)

### 1. Estudiantes (`/api/Estudiantes`)
* **`POST /api/Estudiantes`**:
```json
{
  "nombre": "Carlos Mendoza",
  "edad": 21,
  "direccion": "Av. Primavera 123",
  "telefono": "987654321",
  "correo": "carlos.mendoza@universidad.edu.pe"
}
```
* **`GET /api/Estudiantes`**: Lista todos los estudiantes.
* **`GET /api/Estudiantes/{id}`**: Obtiene el estudiante por ID.
* **`PUT /api/Estudiantes/{id}`**: Actualiza datos del estudiante.
* **`DELETE /api/Estudiantes/{id}`**: Elimina el estudiante.

### 2. Cursos (`/api/Cursos`)
```json
{
  "nombre": "Desarrollo con Arquitecturas Limpias",
  "descripcion": "Curso de Arquitectura Hexagonal y Microservicios",
  "creditos": 4
}
```

### 3. Profesores (`/api/Profesores`)
```json
{
  "nombre": "Ing. Julio Mayhua",
  "especialidad": "Arquitectura de Software y .NET",
  "correo": "jmayhua@universidad.edu.pe"
}
```

### 4. Matrículas (`/api/Matriculas`)
```json
{
  "idEstudiante": 1,
  "idCurso": 1,
  "semestre": "2026-I"
}
```

### 5. Evaluaciones (`/api/Evaluaciones`)
```json
{
  "idEstudiante": 1,
  "idCurso": 1,
  "calificacion": 19.5,
  "fecha": "2026-10-14"
}
```

### 6. Asistencias (`/api/Asistencias`)
```json
{
  "idEstudiante": 1,
  "idCurso": 1,
  "fecha": "2026-10-14",
  "estado": "PRESENTE"
}
```
