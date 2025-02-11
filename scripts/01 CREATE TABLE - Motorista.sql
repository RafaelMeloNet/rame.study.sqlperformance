USE [rame.study.mock]
GO

/****** Object:  Table [dbo].[Motorista]    Script Date: 15/01/2025 15:29:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Motorista](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](255) NOT NULL,
	[Telefone] [nvarchar](25) NULL,
	[Cnh] [varchar](20) NULL,
	[DataNascimento] [date] NULL,
	[Endereco] [varchar](255) NULL,
	[Salario] [decimal](18, 2) NULL,
	[Observacoes] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

