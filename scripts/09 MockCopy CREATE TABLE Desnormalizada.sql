USE [rame.study.mockCopy]
GO

/****** Object:  Table [dbo].[DespachoDesnormalizado]    Script Date: 15/01/2025 15:38:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DespachoDesnormalizado](
	[DespachoId] [int] NULL,
	[DataPartida] [datetime] NULL,
	[DataProcessamento] [datetime] NULL,
	[DataEntrega] [datetime] NULL,
	[EnderecoOrigem] [varchar](255) NULL,
	[EnderecoDestino] [varchar](255) NULL,
	[CoordenadasOrigem] [varchar](255) NULL,
	[CoordenadasDestino] [varchar](255) NULL,
	[Status] [varchar](50) NULL,
	[Log] [varchar](max) NULL,
	[ClienteId] [int] NULL,
	[CargaId] [int] NULL,
	[VeiculoId] [int] NULL,
	[TransportadoraId] [int] NULL,
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
	[ClienteNome] [varchar](255) NULL,
	[ClienteTelefone] [nvarchar](25) NULL,
	[ClienteEmail] [varchar](255) NULL,
	[CargaTipo] [varchar](50) NULL,
	[CargaPeso] [decimal](18, 2) NULL,
	[CargaDimensoes] [varchar](255) NULL,
	[CargaValor] [decimal](18, 2) NULL,
	[CargaSeguro] [bit] NULL,
	[CargaDescricao] [varchar](max) NULL,
	[CargaAltura] [decimal](18, 2) NULL,
	[CargaLargura] [decimal](18, 2) NULL,
	[CargaProfundidade] [decimal](18, 2) NULL,
	[CargaDataCriacao] [datetime] NULL,
	[CargaObservacoes] [varchar](max) NULL,
	[VeiculoPlaca] [varchar](20) NULL,
	[VeiculoModelo] [varchar](100) NULL,
	[VeiculoCor] [varchar](50) NULL,
	[VeiculoIdMotorista] [int] NULL,
	[VeiculoAnoFabricacao] [int] NULL,
	[VeiculoCapacidadeCarga] [decimal](18, 2) NULL,
	[VeiculoDataRevisao] [datetime] NULL,
	[VeiculoObservacoes] [varchar](max) NULL,
	[MotoristaNome] [varchar](255) NULL,
	[MotoristaTelefone] [nvarchar](25) NULL,
	[MotoristaCnh] [varchar](20) NULL,
	[MotoristaDataNascimento] [date] NULL,
	[MotoristaEndereco] [varchar](255) NULL,
	[MotoristaSalario] [decimal](18, 2) NULL,
	[MotoristaObservacoes] [varchar](max) NULL,
	[TransportadoraNome] [varchar](255) NULL,
	[TransportadoraTelefone] [nvarchar](25) NULL,
	[TransportadoraEndereco] [varchar](255) NULL,
	[TransportadoraCnpj] [varchar](20) NULL,
	[TransportadoraInscricaoEstadual] [varchar](20) NULL,
	[TransportadoraDataCadastro] [datetime] NULL,
	[TransportadoraObservacoes] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

