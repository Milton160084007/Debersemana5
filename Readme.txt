Descripción
Aplicación Windows Forms que permite gestionar usuarios y roles con operaciones básicas CRUD (Crear, Leer, Actualizar, Eliminar) usando MySQL como base de datos.

Requisitos
✅ XAMPP (con MySQL activo)
✅ Visual Studio (o similar para C#)
✅ MySQL Connector/NET (instalado via NuGet)


Estructura del Proyecto

ProyectoCRUD/  
├── **Modelos/**  
│   ├── Usuario.cs  
│   └── Rol.cs  
├── **DAO/**  
│   ├── UsuarioDAO.cs  
│   └── RolDAO.cs  
├── **Util/**  
│   └── Conexion.cs  
└── **Form1.cs** (Ventana principal)  


 Configuración

Importa la base de datos (usa el script SQL proporcionado).

Abre el proyecto en Visual Studio.

Instala MySql.Data desde NuGet:

bash
Install-Package MySql.Data
Ejecuta el proyecto (F5).

 Funcionalidades
 Gestión de Usuarios
Agregar nuevos usuarios
Editar información existente
Eliminar usuarios
Listar todos los usuarios

Gestión de Roles
Crear nuevos roles (Ej: "Administrador", "Usuario")
Modificar roles existentes
Borrar roles no usados