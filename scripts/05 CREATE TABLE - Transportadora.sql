USE [rame.study.mock]
GO

/****** Object:  Table [dbo].[Transportadora]    Script Date: 15/01/2025 15:31:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Transportadora](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](255) NOT NULL,
	[Telefone] [nvarchar](25) NULL,
	[Endereco] [varchar](255) NULL,
	[Cnpj] [varchar](20) NULL,
	[InscricaoEstadual] [varchar](20) NULL,
	[DataCadastro] [datetime] NULL,
	[Observacoes] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

