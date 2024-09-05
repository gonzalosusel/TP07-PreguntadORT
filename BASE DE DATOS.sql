USE [master]
GO
/****** Object:  Database [PreguntadOrt]    Script Date: 5/9/2024 15:59:44 ******/
CREATE DATABASE [PreguntadOrt]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PreguntadOrt', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\PreguntadOrt.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PreguntadOrt_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\PreguntadOrt_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PreguntadOrt] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PreguntadOrt].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PreguntadOrt] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PreguntadOrt] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PreguntadOrt] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PreguntadOrt] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PreguntadOrt] SET ARITHABORT OFF 
GO
ALTER DATABASE [PreguntadOrt] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PreguntadOrt] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PreguntadOrt] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PreguntadOrt] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PreguntadOrt] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PreguntadOrt] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PreguntadOrt] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PreguntadOrt] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PreguntadOrt] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PreguntadOrt] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PreguntadOrt] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PreguntadOrt] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PreguntadOrt] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PreguntadOrt] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PreguntadOrt] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PreguntadOrt] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PreguntadOrt] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PreguntadOrt] SET RECOVERY FULL 
GO
ALTER DATABASE [PreguntadOrt] SET  MULTI_USER 
GO
ALTER DATABASE [PreguntadOrt] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PreguntadOrt] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PreguntadOrt] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PreguntadOrt] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PreguntadOrt] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'PreguntadOrt', N'ON'
GO
ALTER DATABASE [PreguntadOrt] SET QUERY_STORE = OFF
GO
USE [PreguntadOrt]
GO
/****** Object:  User [alumno]    Script Date: 5/9/2024 15:59:45 ******/
CREATE USER [alumno] FOR LOGIN [alumno] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 5/9/2024 15:59:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[IdCategoria] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](20) NOT NULL,
	[Foto] [varchar](500) NULL,
 CONSTRAINT [PK_Categorias] PRIMARY KEY CLUSTERED 
(
	[IdCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dificultades]    Script Date: 5/9/2024 15:59:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dificultades](
	[IdDificultad] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](20) NOT NULL,
	[Puntaje] [int] NOT NULL,
 CONSTRAINT [PK_Dificultades] PRIMARY KEY CLUSTERED 
(
	[IdDificultad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Preguntas]    Script Date: 5/9/2024 15:59:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Preguntas](
	[IdPregunta] [int] IDENTITY(1,1) NOT NULL,
	[IdCategoria] [int] NOT NULL,
	[IdDificultad] [int] NOT NULL,
	[Enunciado] [varchar](250) NOT NULL,
	[Foto] [varchar](500) NULL,
 CONSTRAINT [PK_Preguntas] PRIMARY KEY CLUSTERED 
(
	[IdPregunta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Respuestas]    Script Date: 5/9/2024 15:59:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Respuestas](
	[IdRespuesta] [int] IDENTITY(1,1) NOT NULL,
	[IdPregunta] [int] NOT NULL,
	[Opcion] [int] NOT NULL,
	[Contenido] [varchar](50) NOT NULL,
	[Correcta] [bit] NOT NULL,
 CONSTRAINT [PK_Respuestas] PRIMARY KEY CLUSTERED 
(
	[IdRespuesta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 5/9/2024 15:59:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Password] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categorias] ON 

INSERT [dbo].[Categorias] ([IdCategoria], [Nombre], [Foto]) VALUES (1, N'Geografia', N'https://t3.ftcdn.net/jpg/01/70/53/70/360_F_170537083_gA1RBTufrZRvHPmlHfZtUeUnjeb4gcUD.jpg')
INSERT [dbo].[Categorias] ([IdCategoria], [Nombre], [Foto]) VALUES (2, N'Historia', N'https://i.pinimg.com/474x/4b/db/b2/4bdbb2656c468fb09c2ca73c27a6cf51.jpg')
INSERT [dbo].[Categorias] ([IdCategoria], [Nombre], [Foto]) VALUES (3, N'Biología', N'https://www.pngplay.com/wp-content/uploads/12/DNA-Background-PNG-Clip-Art.png')
INSERT [dbo].[Categorias] ([IdCategoria], [Nombre], [Foto]) VALUES (4, N'Entretenimiento', N'https://i.pinimg.com/originals/e3/d7/33/e3d7338298172f4e04f095571ed3107c.png')
INSERT [dbo].[Categorias] ([IdCategoria], [Nombre], [Foto]) VALUES (5, N'Deporte', N'https://www.clipartqueen.com/image-files/football-clipart-of-ball.png')
INSERT [dbo].[Categorias] ([IdCategoria], [Nombre], [Foto]) VALUES (6, N'Arte', N'https://i.pinimg.com/originals/7e/6a/4a/7e6a4aa291bbeff854a7326b943889c5.png')
INSERT [dbo].[Categorias] ([IdCategoria], [Nombre], [Foto]) VALUES (7, N'Ciencia', N'https://lh3.googleusercontent.com/proxy/97k5RgB1_PAfUlBrsS9tyjRMy-U0e62K1_StwEdVE-OQKCvGq1Drcu8Xn3hkmr2evx2awyn82pzuh2CJ8_8-hPVRvOKcntmddXQH')
SET IDENTITY_INSERT [dbo].[Categorias] OFF
GO
SET IDENTITY_INSERT [dbo].[Dificultades] ON 

INSERT [dbo].[Dificultades] ([IdDificultad], [Nombre], [Puntaje]) VALUES (1, N'Fácil', 1)
INSERT [dbo].[Dificultades] ([IdDificultad], [Nombre], [Puntaje]) VALUES (2, N'Medio', 2)
INSERT [dbo].[Dificultades] ([IdDificultad], [Nombre], [Puntaje]) VALUES (3, N'Difícil', 3)
SET IDENTITY_INSERT [dbo].[Dificultades] OFF
GO
SET IDENTITY_INSERT [dbo].[Preguntas] ON 

INSERT [dbo].[Preguntas] ([IdPregunta], [IdCategoria], [IdDificultad], [Enunciado], [Foto]) VALUES (1, 1, 2, N'¿Qué país tiene la mayor cantidad de islas en el mundo?', N'https://i.pinimg.com/originals/80/3e/3d/803e3d2860404aeb8d33e157b95768f1.png')
INSERT [dbo].[Preguntas] ([IdPregunta], [IdCategoria], [IdDificultad], [Enunciado], [Foto]) VALUES (2, 2, 2, N'¿Qué sucedió un 20 de junio de 1969?', NULL)
INSERT [dbo].[Preguntas] ([IdPregunta], [IdCategoria], [IdDificultad], [Enunciado], [Foto]) VALUES (5, 2, 3, N'¿Cuándo se inauguró el metro de Londres?', N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQUOW79EaTvZ-fC7d1CpqxjMnz2dESXrzDoWA&s')
INSERT [dbo].[Preguntas] ([IdPregunta], [IdCategoria], [IdDificultad], [Enunciado], [Foto]) VALUES (6, 3, 1, N'¿Cuántos litros de sangre tiene una persona adulta?', N'https://static.vecteezy.com/system/resources/previews/024/097/589/non_2x/donation-blood-bag-free-png.png')
INSERT [dbo].[Preguntas] ([IdPregunta], [IdCategoria], [IdDificultad], [Enunciado], [Foto]) VALUES (7, 3, 1, N'¿Cuántos litros de sangre tiene una persona adulta?', N'https://static.vecteezy.com/system/resources/previews/024/097/589/non_2x/donation-blood-bag-free-png.png')
INSERT [dbo].[Preguntas] ([IdPregunta], [IdCategoria], [IdDificultad], [Enunciado], [Foto]) VALUES (9, 2, 1, N'¿Quién es el autor de la frase "Pienso, luego existo"?', NULL)
INSERT [dbo].[Preguntas] ([IdPregunta], [IdCategoria], [IdDificultad], [Enunciado], [Foto]) VALUES (10, 1, 1, N'¿Cuál es el país más grande y el más pequeño del mundo?', NULL)
INSERT [dbo].[Preguntas] ([IdPregunta], [IdCategoria], [IdDificultad], [Enunciado], [Foto]) VALUES (11, 4, 2, N'¿Cuál es el libro más vendido en el mundo después de la Biblia?', NULL)
INSERT [dbo].[Preguntas] ([IdPregunta], [IdCategoria], [IdDificultad], [Enunciado], [Foto]) VALUES (12, 5, 1, N'¿Cuántos jugadores por equipo participan en un partido de voleibol?', N'https://globalsymbols.com/uploads/production/image/imagefile/14082/17_14083_7007ce89-1586-43b3-8f72-1765df6a7507.png')
INSERT [dbo].[Preguntas] ([IdPregunta], [IdCategoria], [IdDificultad], [Enunciado], [Foto]) VALUES (13, 6, 1, N'¿Quién pintó la obra "Guernica"?', N'https://estaticosgn-cdn.deia.eus/clip/5a9227a2-fb75-4a5a-89f4-f54fbb0641b6_16-9-aspect-ratio_default_0.jpg')
INSERT [dbo].[Preguntas] ([IdPregunta], [IdCategoria], [IdDificultad], [Enunciado], [Foto]) VALUES (14, 2, 3, N'¿Cuál es la nacionalidad de Jorge Mario Bergoglio?', NULL)
INSERT [dbo].[Preguntas] ([IdPregunta], [IdCategoria], [IdDificultad], [Enunciado], [Foto]) VALUES (15, 7, 3, N'¿En qué orden se presentaron los modelos atómicos?', N'https://i.pinimg.com/originals/59/3a/e4/593ae4e924b9b9d9f6bfbf6c78ff5b11.png')
INSERT [dbo].[Preguntas] ([IdPregunta], [IdCategoria], [IdDificultad], [Enunciado], [Foto]) VALUES (16, 2, 2, N'¿En qué periodo de la prehistoria fue descubierto el fuego?', N'https://static.vecteezy.com/system/resources/previews/019/907/695/original/fire-graphic-clipart-design-free-png.png')
INSERT [dbo].[Preguntas] ([IdPregunta], [IdCategoria], [IdDificultad], [Enunciado], [Foto]) VALUES (17, 1, 2, N'¿Cuál es la montaña más alta del continente americano?', NULL)
INSERT [dbo].[Preguntas] ([IdPregunta], [IdCategoria], [IdDificultad], [Enunciado], [Foto]) VALUES (18, 1, 1, N'¿Cuál es la capital de Mongolia?', N'https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/968afb90-ac92-44de-a6d9-bbd220b7fa64/dbjft75-bbffa421-05e9-4b58-8288-bdcdffcad804.png/v1/fill/w_1024,h_525/mongolia_silhouette_by_paperlightbox_dbjft75-fullview.png?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwiaXNzIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsIm9iaiI6W1t7ImhlaWdodCI6Ijw9NTI1IiwicGF0aCI6IlwvZlwvOTY4YWZiOTAtYWM5Mi00NGRlLWE2ZDktYmJkMjIwYjdmYTY0XC')
SET IDENTITY_INSERT [dbo].[Preguntas] OFF
GO
SET IDENTITY_INSERT [dbo].[Respuestas] ON 

INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (1, 1, 1, N'Italia', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (2, 1, 2, N'Suecia', 1)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (3, 1, 3, N'República Dominicana', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (4, 1, 4, N'Japón', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (5, 2, 1, N'Nació Carlos III', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (6, 2, 2, N'Richard Nixon asume como presidente', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (7, 2, 3, N'Ocurre el primer alunizaje', 1)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (8, 2, 4, N'La sonda soviética Venera 6 aterriza en Venus', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (12, 5, 1, N'1912', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (13, 5, 2, N'1924', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (14, 5, 3, N'1892', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (15, 5, 4, N'1863', 1)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (17, 7, 1, N'Entre 2 y 4 litros', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (18, 7, 2, N'Entre 4 y 6 litros', 1)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (20, 7, 3, N'10 litros', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (22, 7, 4, N'7 litros', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (23, 9, 1, N'Platón', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (24, 9, 2, N'Galileo Galilei', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (25, 9, 3, N'Descartes', 1)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (26, 9, 4, N'Sócrates', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (27, 10, 1, N'Rusia y Vaticano', 1)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (28, 10, 2, N'China y Nauru', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (29, 10, 3, N'Canadá y Mónaco', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (30, 10, 4, N'India y San Marino', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (31, 11, 1, N'El Señor de los Anillos', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (32, 11, 2, N'Don Quijote de la Mancha', 1)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (33, 11, 3, N'Cien años de Soledad', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (34, 11, 4, N'La Odisea', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (35, 12, 1, N'3', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (36, 12, 2, N'4', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (37, 12, 3, N'5', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (38, 12, 4, N'6', 1)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (39, 13, 1, N'Pablo Picasso', 1)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (40, 13, 2, N'Diego Rivera', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (41, 13, 3, N'Salvador Dalí', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (42, 13, 4, N'Frida Kahlo', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (43, 14, 1, N'Mexicana', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (44, 14, 2, N'Italiana', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (45, 14, 3, N'Argentina', 1)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (46, 14, 4, N'Española', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (47, 15, 1, N'Thomson, Dalton, Rutherford, Bohr', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (48, 15, 2, N'Dalton, Thomson, Rutherford, cuántico', 1)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (49, 15, 3, N'Dalton, Einstein, cuántico, Sheldon Cooper', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (50, 15, 4, N'Rutherford, Dalton, Thomson, cuántico', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (51, 16, 1, N'Neolítico', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (52, 16, 2, N'Paleolítico', 1)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (53, 16, 3, N'Edad de piedra', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (54, 16, 4, N'Edad media', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (55, 17, 1, N'Monte Everest', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (56, 17, 2, N'Pico Bolívar', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (57, 17, 3, N'Monte Fuji', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (58, 17, 4, N'Aconcagua', 1)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (59, 18, 1, N'Muharin Den', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (60, 18, 2, N'Ulán Bator', 1)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (61, 18, 3, N'Daca', 0)
INSERT [dbo].[Respuestas] ([IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta]) VALUES (62, 18, 4, N'Katmandú', 0)
SET IDENTITY_INSERT [dbo].[Respuestas] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([IdUsuario], [Nombre], [Password]) VALUES (1, N'Susel', N'123banfield')
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
ALTER TABLE [dbo].[Preguntas]  WITH CHECK ADD  CONSTRAINT [FK_Preguntas_Categorias] FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[Categorias] ([IdCategoria])
GO
ALTER TABLE [dbo].[Preguntas] CHECK CONSTRAINT [FK_Preguntas_Categorias]
GO
ALTER TABLE [dbo].[Preguntas]  WITH CHECK ADD  CONSTRAINT [FK_Preguntas_Dificultades] FOREIGN KEY([IdDificultad])
REFERENCES [dbo].[Dificultades] ([IdDificultad])
GO
ALTER TABLE [dbo].[Preguntas] CHECK CONSTRAINT [FK_Preguntas_Dificultades]
GO
ALTER TABLE [dbo].[Respuestas]  WITH CHECK ADD  CONSTRAINT [FK_Respuestas_Preguntas] FOREIGN KEY([IdPregunta])
REFERENCES [dbo].[Preguntas] ([IdPregunta])
GO
ALTER TABLE [dbo].[Respuestas] CHECK CONSTRAINT [FK_Respuestas_Preguntas]
GO
/****** Object:  StoredProcedure [dbo].[cargarpregunta]    Script Date: 5/9/2024 15:59:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cargarpregunta]
@categoria varchar(20),
@dificultad varchar(20),
@enunciado varchar(250),
@foto varchar(500)
as begin
declare @IdCategoria int;
declare @IdDificultad int;

select @IdCategoria=IdCategoria from Categorias where Nombre=@categoria;
select @IdDificultad=IdDificultad from Dificultades where Nombre=@dificultad;
insert into Preguntas(IdCategoria, IdDificultad, Enunciado, Foto) values(@IdCategoria, @IdDificultad, @enunciado, @foto);

select top 1 IdPregunta from Preguntas order by IdPregunta desc; 
end;
GO
/****** Object:  StoredProcedure [dbo].[cargarrespuestas]    Script Date: 5/9/2024 15:59:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[cargarrespuestas]
@idpregunta int,
@contenido1 varchar(50),
@contenido2 varchar(50),
@contenido3 varchar(50),
@contenido4 varchar(50),
@correcta int
as begin
insert into Respuestas(IdPregunta, Opcion, Contenido, Correcta) values
(@idpregunta, 1, @contenido1, 0),
(@idpregunta, 2, @contenido2, 0),
(@idpregunta, 3, @contenido3, 0),
(@idpregunta, 4, @contenido4, 0);

update Respuestas set Correcta=1 where IdPregunta=@idpregunta and Opcion=@correcta;

end;
GO
USE [master]
GO
ALTER DATABASE [PreguntadOrt] SET  READ_WRITE 
GO
