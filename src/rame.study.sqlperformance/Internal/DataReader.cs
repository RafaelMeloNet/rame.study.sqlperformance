// Copyright (c) 2025, Rafael Melo
// Todos os direitos reservados.
// All rights reserved.
//
// Este código é licenciado sob a licença BSD 3-Clause.
// This code is licensed under the BSD 3-Clause License.
//
// Consulte o arquivo LICENSE para obter mais informações.
// See the LICENSE file for more information.

using Microsoft.Data.SqlClient;
using rame.study.sqlperformance.Internal.Models;
using System.Data;

namespace rame.study.sqlperformance.Internal
{
    internal class DataReader(string connectionString)
    {
        private readonly string connectionString = connectionString;

        public List<Despacho> GetAllData()
        {
            List<Despacho> despachos = [];

            string sql = @"
                SELECT 
                    d.Id AS DespachoId, 
                    d.DataPartida, 
                    d.DataProcessamento, 
                    d.DataEntrega, 
                    d.EnderecoOrigem, 
                    d.EnderecoDestino, 
                    d.CoordenadasOrigem, 
                    d.CoordenadasDestino,
	                d.Status,
                    d.Log, 
                    d.ClienteId, 
                    d.CargaId, 
                    d.VeiculoId, 
                    d.TransportadoraId, 
                    d.ValorFrete, 
                    d.DistanciaKM, 
                    d.Rota, 
                    d.PrevisaoEntrega, 
                    d.Responsavel, 
                    d.Prioridade, 
                    d.Observacoes, 
                    d.DataCriacao, 
                    d.DataAtualizacao, 
                    d.DataCancelamento,
                    c.Id AS ClienteId, 
                    c.Nome AS ClienteNome, 
                    c.Telefone AS ClienteTelefone, 
                    c.Email AS ClienteEmail,
                    cg.Id AS CargaId, 
                    cg.Tipo AS CargaTipo, 
                    cg.Peso AS CargaPeso, 
                    cg.Dimensoes AS CargaDimensoes, 
                    cg.Valor AS CargaValor, 
                    cg.Seguro AS CargaSeguro, 
                    cg.Descricao AS CargaDescricao, 
                    cg.Altura AS CargaAltura, 
                    cg.Largura AS CargaLargura, 
                    cg.Profundidade AS CargaProfundidade, 
                    cg.DataCriacao AS CargaDataCriacao, 
                    cg.Observacoes AS CargaObservacoes,
                    v.Id AS VeiculoId, 
                    v.Placa AS VeiculoPlaca, 
                    v.Modelo AS VeiculoModelo, 
                    v.Cor AS VeiculoCor, 
                    v.IdMotorista AS VeiculoIdMotorista, 
                    v.AnoFabricacao AS VeiculoAnoFabricacao, 
                    v.CapacidadeCarga AS VeiculoCapacidadeCarga, 
                    v.DataRevisao AS VeiculoDataRevisao, 
                    v.Observacoes AS VeiculoObservacoes,
                    m.Id AS MotoristaId, 
                    m.Nome AS MotoristaNome, 
                    m.Telefone AS MotoristaTelefone, 
                    m.Cnh AS MotoristaCnh, 
                    m.DataNascimento AS MotoristaDataNascimento, 
                    m.Endereco AS MotoristaEndereco, 
                    m.Salario AS MotoristaSalario, 
                    m.Observacoes AS MotoristaObservacoes,
                    t.Id AS TransportadoraId, 
                    t.Nome AS TransportadoraNome, 
                    t.Telefone AS TransportadoraTelefone, 
                    t.Endereco AS TransportadoraEndereco, 
                    t.Cnpj AS TransportadoraCnpj, 
                    t.InscricaoEstadual AS TransportadoraInscricaoEstadual, 
                    t.DataCadastro AS TransportadoraDataCadastro, 
                    t.Observacoes AS TransportadoraObservacoes
                FROM Despacho d
                INNER JOIN Cliente c ON d.ClienteId = c.Id
                INNER JOIN Carga cg ON d.CargaId = cg.Id
                INNER JOIN Veiculo v ON d.VeiculoId = v.Id
                INNER JOIN Motorista m ON v.IdMotorista = m.Id
                INNER JOIN Transportadora t ON d.TransportadoraId = t.Id
            ";

            using (SqlConnection conn = new (connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            using (SqlDataAdapter da = new (cmd))
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                DataTable dt = new();

                da.Fill(dt);
            }

            using (SqlConnection connection = new (connectionString))
            {
                using (SqlCommand command = new (sql, connection))
                {
                    connection.Open();
                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Despacho despacho = new()
                        {
                            Id = (int)reader["DespachoId"],
                            DataPartida = (DateTime)reader["DataPartida"],
                            DataProcessamento = (DateTime)reader["DataProcessamento"],
                            DataEntrega = (DateTime)reader["DataEntrega"],
                            EnderecoOrigem = reader["EnderecoOrigem"] as string ?? string.Empty,
                            EnderecoDestino = reader["EnderecoDestino"] as string ?? string.Empty,
                            CoordenadasOrigem = reader["CoordenadasOrigem"] as string ?? string.Empty,
                            CoordenadasDestino = reader["CoordenadasDestino"] as string ?? string.Empty,
                            Status = reader["Status"] as string ?? string.Empty,
                            Log = reader["Log"] as string ?? string.Empty,
                            ClienteId = (int)reader["ClienteId"],
                            CargaId = (int)reader["CargaId"],
                            VeiculoId = (int)reader["VeiculoId"],
                            TransportadoraId = (int)reader["TransportadoraId"],
                            ValorFrete = reader["ValorFrete"] as decimal?,
                            DistanciaKM = reader["DistanciaKM"] as decimal?,
                            Rota = reader["Rota"] as int?,
                            PrevisaoEntrega = reader["PrevisaoEntrega"] as DateTime?,
                            Responsavel = reader["Responsavel"] as string ?? string.Empty,
                            Prioridade = (int)reader["Prioridade"],
                            Observacoes = reader["Observacoes"] as string ?? string.Empty,
                            DataCriacao = reader["DataCriacao"] as DateTime?,
                            DataAtualizacao = reader["DataAtualizacao"] as DateTime?,
                            DataCancelamento = reader["DataCancelamento"] as DateTime?,
                            Cliente = new Cliente
                            {
                                Id = (int)reader["ClienteId"],
                                Nome = (string)reader["ClienteNome"],
                                Telefone = reader["ClienteTelefone"] as string ?? string.Empty,
                                Email = reader["ClienteEmail"] as string ?? string.Empty,
                            },
                            Carga = new Carga
                            {
                                Id = (int)reader["CargaId"],
                                Tipo = (string)reader["CargaTipo"],
                                Peso = (decimal)reader["CargaPeso"],
                                Dimensoes = (string)reader["CargaDimensoes"],
                                Valor = (decimal)reader["CargaValor"],
                                Seguro = (bool)reader["CargaSeguro"],
                                Descricao = (string)reader["CargaDescricao"],
                                Altura = reader["CargaAltura"] as decimal?,
                                Largura = reader["CargaLargura"] as decimal?,
                                Profundidade = reader["CargaProfundidade"] as decimal?,
                                DataCriacao = reader["CargaDataCriacao"] as DateTime?,
                                Observacoes = reader["CargaObservacoes"] as string ?? string.Empty,
                            },
                            Veiculo = new Veiculo
                            {
                                Id = (int)reader["VeiculoId"],
                                Placa = (string)reader["VeiculoPlaca"],
                                Modelo = (string)reader["VeiculoModelo"],
                                Cor = (string)reader["VeiculoCor"],
                                IdMotorista = (int)reader["VeiculoIdMotorista"],
                                AnoFabricacao = (int)reader["VeiculoAnoFabricacao"],
                                CapacidadeCarga = (decimal)reader["VeiculoCapacidadeCarga"],
                                DataRevisao = reader["VeiculoDataRevisao"] as DateTime?,
                                Observacoes = reader["VeiculoObservacoes"] as string ?? string.Empty,
                                Motorista = new Motorista
                                {
                                    Id = (int)reader["MotoristaId"],
                                    Nome = (string)reader["MotoristaNome"],
                                    Telefone = reader["MotoristaTelefone"] as string ?? string.Empty,
                                    Cnh = reader["MotoristaCnh"] as string ?? string.Empty,
                                    DataNascimento = (DateTime)reader["MotoristaDataNascimento"],
                                    Endereco = reader["MotoristaEndereco"] as string ?? string.Empty,
                                    Salario = (decimal)reader["MotoristaSalario"],
                                    Observacoes = reader["MotoristaObservacoes"] as string ?? string.Empty,
                                }
                            },
                            Transportadora = new Transportadora
                            {
                                Id = (int)reader["TransportadoraId"],
                                Nome = (string)reader["TransportadoraNome"],
                                Telefone = reader["TransportadoraTelefone"] as string ?? string.Empty,
                                Endereco = reader["TransportadoraEndereco"] as string ?? string.Empty,
                                Cnpj = reader["TransportadoraCnpj"] as string ?? string.Empty,
                                InscricaoEstadual = reader["TransportadoraInscricaoEstadual"] as string ?? string.Empty,
                                DataCadastro = reader["TransportadoraDataCadastro"] as DateTime?,
                                Observacoes = reader["TransportadoraObservacoes"] as string ?? string.Empty,
                            }
                        };
                        despachos.Add(despacho);
                    }
                }
                connection.Close();
            }
            return despachos;
        }
    }
}