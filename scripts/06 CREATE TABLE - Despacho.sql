USE [rame.study.mock]
GO

/****** Object:  Table [dbo].[Despacho]    Script Date: 15/01/2025 15:31:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Despacho](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DataPartida] [datetime] NOT NULL,
	[DataProcessamento] [datetime] NOT NULL,
	[DataEntrega] [datetime] NOT NULL,
	[EnderecoOrigem] [varchar](255) NOT NULL,
	[EnderecoDestino] [varchar](255) NOT NULL,
	[CoordenadasOrigem] [varchar](255) NULL,
	[CoordenadasDestino] [varchar](255) NULL,
	[Status] [varchar](50) NULL,
	[Log] [varchar](max) NULL,
	[ClienteId] [int] NOT NULL,
	[CargaId] [int] NOT NULL,
	[VeiculoId] [int] NOT NULL,
	[TransportadoraId] [int] NOT NULL,
	[ValorFrete] [decimal](18, 2) NULL,
	[DistanciaKM] [decimal](18, 2) NULL,
	[Rota] [int] NULL,
	[PrevisaoEntrega] [datetime] NULL,
	[Responsavel] [varchar](255) NULL,
	[Prioridade] [int] NULL,
	[Observacoes] [varchar](max) NULL,
	[DataCriacao] [datetime] NULL,
	[DataAtualizacao] [datetime] NULL,
	[DataCancelamento] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Despacho]  WITH CHECK ADD FOREIGN KEY([CargaId])
REFERENCES [dbo].[Carga] ([Id])
GO

ALTER TABLE [dbo].[Despacho]  WITH CHECK ADD FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Cliente] ([Id])
GO

ALTER TABLE [dbo].[Despacho]  WITH CHECK ADD FOREIGN KEY([TransportadoraId])
REFERENCES [dbo].[Transportadora] ([Id])
GO

ALTER TABLE [dbo].[Despacho]  WITH CHECK ADD FOREIGN KEY([VeiculoId])
REFERENCES [dbo].[Veiculo] ([Id])
GO

