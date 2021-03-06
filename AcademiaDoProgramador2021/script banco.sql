DROP DATABASE AcademiaDoProgramador2021

CREATE DATABASE AcademiaDoProgramador2021

USE [AcademiaDoProgramador2021]
GO
/****** Object:  Table [dbo].[Chamados]    Script Date: 19/03/2021 16:40:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chamados](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](40) NOT NULL,
	[Descricao] [varchar](255) NULL,
	[Equipamento] [varchar](40) NOT NULL,
	[Data] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Equipamentos]    Script Date: 19/03/2021 16:40:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipamentos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](40) NOT NULL,
	[Preco] [decimal](6, 2) NOT NULL,
	[Sn] [varchar](20) NOT NULL,
	[Data] [datetime] NOT NULL,
	[Fabricante] [varchar](40) NOT NULL
) ON [PRIMARY]
GO
