USE [rame.study.mock]
GO

/****** Object:  Table [dbo].[Veiculo]    Script Date: 15/01/2025 15:29:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Veiculo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Placa] [varchar](20) NOT NULL,
	[Modelo] [varchar](100) NULL,
	[Cor] [varchar](50) NULL,
	[IdMotorista] [int] NOT NULL,
	[AnoFabricacao] [int] NULL,
	[CapacidadeCarga] [decimal](18, 2) NULL,
	[DataRevisao] [datetime] NULL,
	[Observacoes] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Veiculo]  WITH CHECK ADD FOREIGN KEY([IdMotorista])
REFERENCES [dbo].[Motorista] ([Id])
GO

