USE [rame.study.mock]
GO

/****** Object:  Table [dbo].[Carga]    Script Date: 15/01/2025 15:30:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Carga](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Tipo] [varchar](50) NULL,
	[Peso] [decimal](18, 2) NOT NULL,
	[Dimensoes] [varchar](255) NULL,
	[Valor] [decimal](18, 2) NOT NULL,
	[Seguro] [bit] NOT NULL,
	[Descricao] [varchar](max) NULL,
	[Altura] [decimal](18, 2) NULL,
	[Largura] [decimal](18, 2) NULL,
	[Profundidade] [decimal](18, 2) NULL,
	[DataCriacao] [datetime] NULL,
	[Observacoes] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

