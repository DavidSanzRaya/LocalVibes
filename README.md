

# Local Vibes 🎵  

Local Vibes es un proyecto final desarrollado para el Bootcamp Enfocat de Fundació Esplai.  
Nuestro objetivo es abordar un **Objetivo de Desarrollo Sostenible (ODS)** del plan de acción global **Agenda 2030**, enfocado en la promoción de la **inclusión cultural y el acceso equitativo a las artes**.  

Esta aplicación web, creada con **ASP.NET Core MVC**, conecta a pequeños artistas y bandas emergentes con sus comunidades locales.  
A través de un **mapa interactivo** 🗺️, los artistas pueden anunciar sus próximos eventos, facilitando a los usuarios descubrir y asistir a actuaciones cercanas.  

<img src="https://github.com/DavidSanzRaya/LocalVibes/blob/master/LocalVibes/wwwroot/Assets/Logo-white.png" alt="Logo" width="150" />


## 🤝 Objetivo de Local Vibes  

El objetivo principal de Local Vibes es dar visibilidad al talento de bandas emergentes o desconocidas. Según los datos:

- Aproximadamente **el 90% de los 30 millones de proyectos musicales en el mundo** se encuentran en este rango.
- Tan solo **el 2% de ellos viven exclusivamente de la música**.  

Queremos acercar las bandas locales a su audiencia más cercana, ayudándolas a destacar y crecer. De ahí surge nuestro nombre: **Local Vibes**.  

Para lograr este objetivo, nos basamos en tres fundamentos clave:

- **Alegría**: Promover un ambiente positivo y vibrante para los artistas y sus seguidores.
- **Conexión**: Facilitar la interacción genuina entre bandas y su comunidad local.
- **Satisfacción**: Asegurar que tanto los artistas como los usuarios encuentren lo que buscan y disfruten del proceso.


## 🛠️ ¿Cómo lo hacemos?  

Hemos diseñado Local Vibes con dos herramientas clave para apoyar a los artistas emergentes:  

1. **Sistema de reseñas:** 🌟✍️
Los usuarios pueden opinar sobre las bandas, lo que les permite destacar por su **calidad y reconocimiento del público**, en lugar de solo basarse en visitas o reproducciones.  

2. **Mapa interactivo:** 🗺️📍
Un sistema visual que facilita a los usuarios encontrar nuevos eventos musicales cerca de su ubicación.  

## 📝 Descripción del Proyecto y Roles de Usuario  

En Local Vibes, los usuarios pueden registrarse en dos modalidades, cada una diseñada para satisfacer necesidades específicas.  

### **1. Usuario** 👤
Un **usuario común** que actúa como cliente principal de las bandas y artistas.  

#### **Datos requeridos para el registro:**  
- Nombre de usuario  
- Nombre y apellido  
- Email  
- Teléfono  
- Foto de perfil (opcional)  

#### **Funciones disponibles:**  
- Guardar grupos y géneros musicales como favoritos.  
- Ver eventos y añadirlos a su perfil personal.  
- Escribir reseñas sobre eventos o bandas.  

### **2. Banda/Artista** 🎤🎸
Un tipo de usuario diseñado para proyectos musicales (bandas o artistas individuales).  

#### **Datos requeridos para el registro:**  
- Nombre del proyecto  
- Biografía  
- Fecha de fundación  
- Géneros musicales que toca/n  
- Foto del proyecto (opcional)  

#### **Funciones adicionales:**  
- Gestión por un **usuario administrador** (con acceso a todas las opciones de un usuario común).  
- Crear eventos y publicarlos en el mapa interactivo.  
- Añadir miembros a la banda.  


## 🚀🎨 Características principales y diseño

- **Diseño**:
La página web tiene un diseño **moderno y accesible**, con una interfaz **UI/UX** intuitiva que facilita la navegación para los usuarios.

- **Colores**:
Utilizamos una paleta de colores que combina **tonos grises oscuros con matices morados**, creando una atmósfera **elegante y relajante**, a la vez que resalta la identidad de la marca.

- **Mapa interactivo**:
Una de las características clave de la aplicación es su mapa interactivo. Los usuarios pueden **explorar eventos musicales cercanos, ver detalles de los conciertos y localizaciones, y agregar eventos a su perfil personal**. Este sistema facilita el descubrimiento de nuevas bandas y eventos en la comunidad local.

- **Sistema de reseñas y valoraciones**:
El sistema de reseñas permite a los usuarios valorar y comentar sobre las bandas o eventos a los que asisten. Las reseñas no solo fomentan la interacción, sino que ayudan a las bandas a **ganar visibilidad y a construir una reputación dentro de su comunidad**.

## 🛠 **Tecnologías utilizadas**

- **ASP.NET CORE MVC**:  
  Utilizado para la creación de la aplicación web en su totalidad, aprovechando la arquitectura **Model-View-Controller (MVC)**. Esto incluye el uso de **HTML**, **C#**, **JavaScript** y **CSS** para el desarrollo de las vistas, controladores y modelos de la aplicación.

- **Microsoft SQL Server**:  
  Para la gestión de la base de datos, se utiliza **SQL Server** mediante **Microsoft Management Studio**. Esto permite la administración eficiente de los datos, con consultas SQL para interactuar con las tablas de usuarios, bandas, eventos, entre otros.

- **Visual Studio y Vim**:  
  **Visual Studio** es el principal **IDE** utilizado para el desarrollo de la aplicación, especialmente por su integración con .NET. **Vim**, un editor de texto ligero, se emplea para tareas más rápidas o cuando se prefiere trabajar en entornos más minimalistas.

- **Leaflet**:  
  Biblioteca de **JavaScript** para la creación de **mapas interactivos**. Se utiliza para mostrar eventos y ubicaciones de bandas de forma visual, permitiendo que los usuarios descubran eventos cercanos a su ubicación.

- **wicg-inert**:  
  Librería para implementar el atributo `inert` en navegadores que no lo soportan de manera nativa. Se usa para mejorar la **accesibilidad** de la aplicación, deshabilitando temporalmente elementos de la interfaz (como fondos o formularios) cuando se muestran modales o ventanas emergentes.


## 📦 Instalación.

Actualmente la web no esta desplegada en ningun servidor, lo qual significa que hay que iniciarla manualmente. Para ello se puede descargar todos los paquetes de este repositorio.

**1. Abrir el proyecto en Visual Studio o IDE similar:**

```bash
  git clone https://github.com/DavidSanzRaya/LocalVibes.git
  cd LocalVibes
```

**2. Compila e inicia** el programa con http. 

## 👥 Contribuciones.

¡Las contribuciones son bienvenidas! Si deseas colaborar:

- Haz un fork del repositorio.

- Crea una nueva rama para tu característica o corrección.

- Realiza los cambios y haz un commit.

- Envíe una solicitud de extracción.

## 🗂 Estructura del proyecto.

```plaintext
└───LocalVibes
    ├───bin
    ├───Controllers
    ├───DALs
    ├───DTOs
    ├───Models
    ├───obj
    ├───Properties
    ├───TagHelpers
    ├───Views
    └───wwwroot
```

## 📌 Estado del Proyecto.

El proyecto está finalizado en una version Demo para poderla presentar, existen algunos detalles a pulir, especialmente en diseño y muchas funcionalidades que han quedado pendientes.

## ✨ Capturas de pantalla.

> [!IMPORTANT]
> No hay captura actual de la pagina ni del mapa.

 ## 📜 Licencia

Este proyecto no tiene licencia.

## 🔮 Futuras Mejoras

- **Solucionar problemas de lógica y visuales**: Mejorar la funcionalidad y el diseño de la aplicación, corrigiendo cualquier error o inconsistencia en la interfaz y la interacción de los usuarios.
  
- **Implementar un sistema de detección de tema oscuro o claro**: Adaptar la aplicación para que reconozca automáticamente la preferencia de tema (oscuro o claro) del usuario, mejorando la experiencia de uso según sus configuraciones personales.
  
- **Generador de tickets propio**: Desarrollar un sistema interno para la creación y gestión de tickets de eventos, de modo que no dependamos de plataformas de terceros como Ticketmaster, facilitando una experiencia más integrada para los usuarios y artistas.

- **Sistema de rating de bandas y niveles de usuarios**: Introducir un sistema de valoraciones para las bandas, donde los usuarios puedan calificar los eventos y artistas. Además, implementar un sistema de niveles (tiers) para usuarios, que permita ofrecer beneficios exclusivos a los más activos o comprometidos.

- **Filtros personalizados por gustos musicales, géneros y bandas**: Desarrollar un sistema de filtrado avanzado que permita a los usuarios encontrar eventos y bandas según sus preferencias musicales, géneros específicos, y otras opciones personalizadas.

- **Expandir la cobertura geográfica**: Ampliar la plataforma para cubrir más zonas más allá de Barcelona, permitiendo que más artistas y comunidades locales se conecten a través de la aplicación. También se incluirá la traducción al inglés y otros idiomas para facilitar el acceso internacional.


## 👩‍💻 Coders

[@Abel Cumbreño](https://www.github.com/llavefija)
[@Alexia Sabate](https://www.github.com/alexiasabate11)
[@Joel Adorno](https://www.github.com/joelforworks)
[@David Sanz](https://www.github.com/DavidSanzRaya)




