// Copyright (c) 2025, Rafael Melo
// Todos os direitos reservados.
// All rights reserved.
//
// Este código é licenciado sob a licença BSD 3-Clause.
// This code is licensed under the BSD 3-Clause License.
//
// Consulte o arquivo LICENSE para obter mais informações.
// See the LICENSE file for more information.

namespace rame.study.sqlperformance.Internal.Models
{
    internal class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    internal class Carga
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public decimal Peso { get; set; }
        public string Dimensoes { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public bool Seguro { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal? Altura { get; set; }
        public decimal? Largura { get; set; }
        public decimal? Profundidade { get; set; }
        public DateTime? DataCriacao { get; set; }
        public string Observacoes { get; set; } = string.Empty;
    }

    internal class Motorista
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Cnh { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Endereco { get; set; } = string.Empty;
        public decimal Salario { get; set; }
        public string Observacoes { get; set; } = string.Empty;
    }

    internal class Transportadora
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string InscricaoEstadual { get; set; } = string.Empty;
        public DateTime? DataCadastro { get; set; }
        public string Observacoes { get; set; } = string.Empty;
    }

    internal class Veiculo
    {
        public int Id { get; set; }
        public string Placa { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Cor { get; set; } = string.Empty;
        public int IdMotorista { get; set; }
        public int AnoFabricacao { get; set; }
        public decimal CapacidadeCarga { get; set; }
        public DateTime? DataRevisao { get; set; }
        public string Observacoes { get; set; } = string.Empty;
        public Motorista Motorista { get; set; } = new();
    }

    internal class Despacho
    {
        public int Id { get; set; }
        public DateTime DataPartida { get; set; }
        public DateTime DataProcessamento { get; set; }
        public DateTime DataEntrega { get; set; }
        public string EnderecoOrigem { get; set; } = string.Empty;
        public string EnderecoDestino { get; set; } = string.Empty;
        public string CoordenadasOrigem { get; set; } = string.Empty;
        public string CoordenadasDestino { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Log { get; set; } = string.Empty;
        public int ClienteId { get; set; }
        public int CargaId { get; set; }
        public int VeiculoId { get; set; }
        public int TransportadoraId { get; set; }
        public decimal? ValorFrete { get; set; }
        public decimal? DistanciaKM { get; set; }
        public int? Rota { get; set; }
        public DateTime? PrevisaoEntrega { get; set; }
        public string Responsavel { get; set; } = string.Empty;
        public int Prioridade { get; set; }
        public string Observacoes { get; set; } = string.Empty;
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataCancelamento { get; set; }
        public Cliente Cliente { get; set; } = new();
        public Carga Carga { get; set; } = new();
        public Veiculo Veiculo { get; set; } = new();
        public Transportadora Transportadora { get; set; } = new();
    }
}
