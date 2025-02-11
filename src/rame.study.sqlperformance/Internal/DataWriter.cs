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
    internal class DataWriter(Configs configs)
    {
        List<Cliente> clientes = [];
        List<Carga> cargas = [];
        List<Transportadora> transportadoras = [];
        List<Veiculo> veiculos = [];
        List<Motorista> motoristas = [];
        List<Despacho> despachos = [];

        #region Bulk Copy

        public void WriteOriginToTargetBulk()
        {
            string sql = @$"
                SELECT TOP {configs.CountToCopy}
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
                    c.Nome AS ClienteNome, 
                    c.Telefone AS ClienteTelefone, 
                    c.Email AS ClienteEmail,
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
                    v.Placa AS VeiculoPlaca, 
                    v.Modelo AS VeiculoModelo, 
                    v.Cor AS VeiculoCor, 
                    v.IdMotorista AS VeiculoIdMotorista, 
                    v.AnoFabricacao AS VeiculoAnoFabricacao, 
                    v.CapacidadeCarga AS VeiculoCapacidadeCarga, 
                    v.DataRevisao AS VeiculoDataRevisao, 
                    v.Observacoes AS VeiculoObservacoes,
                    m.Nome AS MotoristaNome, 
                    m.Telefone AS MotoristaTelefone, 
                    m.Cnh AS MotoristaCnh, 
                    m.DataNascimento AS MotoristaDataNascimento, 
                    m.Endereco AS MotoristaEndereco, 
                    m.Salario AS MotoristaSalario, 
                    m.Observacoes AS MotoristaObservacoes,
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
                ORDER BY d.Id DESC";

            DataTable table = new();

            using (SqlConnection conn = new(configs.ConnStrOrigem))
            using (SqlCommand cmd = conn.CreateCommand())
            using (SqlDataAdapter da = new(cmd))
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                da.Fill(table);
            }

            using SqlConnection connection = new(configs.ConnStrDestino);

            connection.Open();

            using SqlBulkCopy bulkCopy = new(connection);

            bulkCopy.DestinationTableName = "DespachoDesnormalizado";
            bulkCopy.WriteToServer(table);
        }

        #endregion

        #region Popula banco de dados Origem

        public void WriteDataToDbOrigem(List<Despacho> despachos)
        {
            this.despachos = despachos;

            clientes = despachos.Select(d => d.Cliente).DistinctBy(c => c.Id).ToList();
            cargas = despachos.Select(d => d.Carga).DistinctBy(c => c.Id).ToList();
            transportadoras = despachos.Select(d => d.Transportadora).DistinctBy(t => t.Id).ToList();
            veiculos = despachos.Select(d => d.Veiculo).DistinctBy(v => v.Id).ToList();
            motoristas = veiculos.Select(v => v.Motorista).DistinctBy(m => m.Id).ToList();

            using SqlConnection connection = new(configs.ConnStrOrigem);
            connection.Open();

            using SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                PersistClientes(connection, transaction);
                PersistCargas(connection, transaction);
                PersistTransportadoras(connection, transaction);
                PersistMotoristas(connection, transaction);
                PersistVeiculos(connection, transaction);
                PersistDespachos(connection, transaction);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();

                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        private void PersistClientes(SqlConnection connection, SqlTransaction transaction)
        {
            string sql = @"
                INSERT INTO Cliente (
                    Nome, 
                    Telefone, 
                    Email
                 ) 
                VALUES (
                    @Nome, 
                    @Telefone, 
                    @Email
                 ); 
                SELECT SCOPE_IDENTITY();";

            foreach (var item in clientes)
            {
                using SqlCommand command = new(sql, connection, transaction);
                command.Parameters.AddWithValue("@Nome", item.Nome);
                command.Parameters.AddWithValue("@Telefone", item.Telefone == null ? DBNull.Value : item.Telefone);
                command.Parameters.AddWithValue("@Email", item.Email == null ? DBNull.Value : item.Email);

                var insertedId = Convert.ToInt32(command.ExecuteScalar());

                foreach (var item1 in despachos.Where(x => x.ClienteId == item.Id))
                {
                    item1.ClienteId = insertedId;
                }

                item.Id = insertedId;
            }
        }

        private void PersistCargas(SqlConnection connection, SqlTransaction transaction)
        {
            string sql = @"
               INSERT INTO Carga (
                   Tipo,
                   Peso,
                   Dimensoes,
                   Valor,
                   Seguro,
                   Descricao,
                   Altura,
                   Largura,
                   Profundidade,
                   DataCriacao,
                   Observacoes
               )
               VALUES (
                 @Tipo,
                   @Peso,
                   @Dimensoes,
                   @Valor,
                   @Seguro,
                   @Descricao,
                   @Altura,
                   @Largura,
                   @Profundidade,
                   @DataCriacao,
                   @Observacoes
               );
               SELECT SCOPE_IDENTITY();";

            foreach (var item in cargas)
            {
                using SqlCommand command = new(sql, connection, transaction);
                command.Parameters.AddWithValue("@Tipo", item.Tipo == null ? DBNull.Value : item.Tipo);
                command.Parameters.AddWithValue("@Peso", item.Peso);
                command.Parameters.AddWithValue("@Dimensoes", item.Dimensoes == null ? DBNull.Value : item.Dimensoes);
                command.Parameters.AddWithValue("@Valor", item.Valor);
                command.Parameters.AddWithValue("@Seguro", item.Seguro);
                command.Parameters.AddWithValue("@Descricao", item.Descricao == null ? DBNull.Value : item.Descricao);
                command.Parameters.AddWithValue("@Altura", item.Altura.HasValue ? item.Altura.Value : DBNull.Value);
                command.Parameters.AddWithValue("@Largura", item.Largura.HasValue ? item.Largura.Value : DBNull.Value);
                command.Parameters.AddWithValue("@Profundidade", item.Profundidade.HasValue ? item.Profundidade.Value : DBNull.Value);
                command.Parameters.AddWithValue("@DataCriacao", item.DataCriacao ?? DateTime.Now);
                command.Parameters.AddWithValue("@Observacoes", item.Observacoes == null ? DBNull.Value : item.Observacoes);

                var insertedId = Convert.ToInt32(command.ExecuteScalar());

                foreach (var item1 in despachos.Where(x => x.CargaId == item.Id))
                {
                    item1.CargaId = insertedId;
                }

                item.Id = insertedId;
            }
        }

        private void PersistMotoristas(SqlConnection connection, SqlTransaction transaction)
        {
            string sql = @"
                INSERT INTO Motorista (
                    Nome, 
                    Telefone, 
                    Cnh, 
                    DataNascimento,
                    Endereco, 
                    Salario, 
                    Observacoes
                 )
                VALUES (
                   @Nome, 
                    @Telefone,
                    @Cnh,
                    @DataNascimento,
                    @Endereco,
                    @Salario,
                    @Observacoes
                  );
                 SELECT SCOPE_IDENTITY();";

            foreach (var item in motoristas)
            {
                using SqlCommand command = new(sql, connection, transaction);
                command.Parameters.AddWithValue("@Nome", item.Nome);
                command.Parameters.AddWithValue("@Telefone", item.Telefone == null ? DBNull.Value : item.Telefone);
                command.Parameters.AddWithValue("@Cnh", item.Cnh == null ? DBNull.Value : item.Cnh);
                command.Parameters.AddWithValue("@DataNascimento", item.DataNascimento);
                command.Parameters.AddWithValue("@Endereco", item.Endereco == null ? DBNull.Value : item.Endereco);
                command.Parameters.AddWithValue("@Salario", item.Salario);
                command.Parameters.AddWithValue("@Observacoes", item.Observacoes == null ? DBNull.Value : item.Observacoes);

                var insertedId = Convert.ToInt32(command.ExecuteScalar());

                foreach (var item1 in veiculos.Where(x => x.IdMotorista == item.Id))
                {
                    item1.IdMotorista = insertedId;
                }

                item.Id = insertedId;
            }
        }

        private void PersistTransportadoras(SqlConnection connection, SqlTransaction transaction)
        {
            string sql = @"
                INSERT INTO Transportadora (
                    Nome,
                    Telefone,
                    Endereco,
                    Cnpj,
                     InscricaoEstadual,
                    DataCadastro,
                    Observacoes
                 )
                VALUES (
                   @Nome,
                     @Telefone,
                    @Endereco,
                     @Cnpj,
                   @InscricaoEstadual,
                    @DataCadastro,
                    @Observacoes
                 );
                 SELECT SCOPE_IDENTITY();";

            foreach (var item in transportadoras)
            {
                using SqlCommand command = new(sql, connection, transaction);
                command.Parameters.AddWithValue("@Nome", item.Nome);
                command.Parameters.AddWithValue("@Telefone", item.Telefone == null ? DBNull.Value : item.Telefone);
                command.Parameters.AddWithValue("@Endereco", item.Endereco == null ? DBNull.Value : item.Endereco);
                command.Parameters.AddWithValue("@Cnpj", item.Cnpj == null ? DBNull.Value : item.Cnpj);
                command.Parameters.AddWithValue("@InscricaoEstadual", item.InscricaoEstadual == null ? DBNull.Value : item.InscricaoEstadual);
                command.Parameters.AddWithValue("@DataCadastro", item.DataCadastro ?? DateTime.Now);
                command.Parameters.AddWithValue("@Observacoes", item.Observacoes == null ? DBNull.Value : item.Observacoes);
                command.Parameters.AddWithValue("@DataCriacao", DateTime.Now);
                command.Parameters.AddWithValue("@DataAtualizacao", DateTime.Now);

                var insertedId = Convert.ToInt32(command.ExecuteScalar());

                foreach (var item1 in despachos.Where(x => x.TransportadoraId == item.Id))
                {
                    item1.TransportadoraId = insertedId;
                }

                item.Id = insertedId;
            }
        }

        private void PersistVeiculos(SqlConnection connection, SqlTransaction transaction)
        {
            string sql = @"
                INSERT INTO Veiculo (
                    Placa,
                    Modelo,
                    Cor,
                    IdMotorista,
                    AnoFabricacao,
                    CapacidadeCarga,
                    DataRevisao,
                    Observacoes
                 )
                VALUES (
                    @Placa,
                    @Modelo,
                    @Cor,
                    @IdMotorista,
                    @AnoFabricacao,
                    @CapacidadeCarga,
                    @DataRevisao,
                    @Observacoes
                  );
                 SELECT SCOPE_IDENTITY();";

            foreach (var item in veiculos)
            {
                using SqlCommand command = new(sql, connection, transaction);
                command.Parameters.AddWithValue("@Placa", item.Placa);
                command.Parameters.AddWithValue("@Modelo", item.Modelo == null ? DBNull.Value : item.Modelo);
                command.Parameters.AddWithValue("@Cor", item.Cor == null ? DBNull.Value : item.Cor);
                command.Parameters.AddWithValue("@IdMotorista", item.IdMotorista);
                command.Parameters.AddWithValue("@AnoFabricacao", item.AnoFabricacao);
                command.Parameters.AddWithValue("@CapacidadeCarga", item.CapacidadeCarga);
                command.Parameters.AddWithValue("@DataRevisao", item.DataRevisao.HasValue ? item.DataRevisao.Value : DBNull.Value);
                command.Parameters.AddWithValue("@Observacoes", item.Observacoes == null ? DBNull.Value : item.Observacoes);
                command.Parameters.AddWithValue("@DataCriacao", DateTime.Now);
                command.Parameters.AddWithValue("@DataAtualizacao", DateTime.Now);

                var insertedId = Convert.ToInt32(command.ExecuteScalar());

                foreach (var item1 in despachos.Where(x => x.VeiculoId == item.Id))
                {
                    item1.VeiculoId = insertedId;
                }

                item.Id = insertedId;
            }
        }

        private void PersistDespachos(SqlConnection connection, SqlTransaction transaction)
        {
            string sql = @"
               INSERT INTO Despacho (
                    DataPartida,
                    DataProcessamento,
                     DataEntrega,
                    EnderecoOrigem,
                     EnderecoDestino,
                    CoordenadasOrigem,
                   CoordenadasDestino,
                    Status,
                     Log,
                     ClienteId,
                     CargaId,
                     VeiculoId,
                     TransportadoraId,
                     ValorFrete,
                    DistanciaKM,
                     Rota,
                   PrevisaoEntrega,
                    Responsavel,
                     Prioridade,
                   Observacoes,
                     DataCriacao,
                     DataAtualizacao,
                    DataCancelamento
                )
                 VALUES (
                    @DataPartida,
                   @DataProcessamento,
                     @DataEntrega,
                    @EnderecoOrigem,
                    @EnderecoDestino,
                    @CoordenadasOrigem,
                   @CoordenadasDestino,
                    @Status,
                    @Log,
                    @ClienteId,
                    @CargaId,
                    @VeiculoId,
                   @TransportadoraId,
                     @ValorFrete,
                    @DistanciaKM,
                     @Rota,
                   @PrevisaoEntrega,
                    @Responsavel,
                    @Prioridade,
                     @Observacoes,
                    @DataCriacao,
                   @DataAtualizacao,
                     @DataCancelamento
                );";

            foreach (var item in despachos)
            {
                using var command = new SqlCommand(sql, connection, transaction);
                command.Parameters.AddWithValue("@DataPartida", item.DataPartida);
                command.Parameters.AddWithValue("@DataProcessamento", item.DataProcessamento);
                command.Parameters.AddWithValue("@DataEntrega", item.DataEntrega);
                command.Parameters.AddWithValue("@EnderecoOrigem", item.EnderecoOrigem == null ? DBNull.Value : item.EnderecoOrigem);
                command.Parameters.AddWithValue("@EnderecoDestino", item.EnderecoDestino == null ? DBNull.Value : item.EnderecoDestino);
                command.Parameters.AddWithValue("@CoordenadasOrigem", item.CoordenadasOrigem == null ? DBNull.Value : item.CoordenadasOrigem);
                command.Parameters.AddWithValue("@CoordenadasDestino", item.CoordenadasDestino == null ? DBNull.Value : item.CoordenadasDestino);
                command.Parameters.AddWithValue("@Status", item.Status);
                command.Parameters.AddWithValue("@Log", item.Log == null ? DBNull.Value : item.Log);
                command.Parameters.AddWithValue("@ClienteId", item.ClienteId);
                command.Parameters.AddWithValue("@CargaId", item.CargaId);
                command.Parameters.AddWithValue("@VeiculoId", item.VeiculoId);
                command.Parameters.AddWithValue("@TransportadoraId", item.TransportadoraId);
                command.Parameters.AddWithValue("@ValorFrete", item.ValorFrete.HasValue ? item.ValorFrete.Value : DBNull.Value);
                command.Parameters.AddWithValue("@DistanciaKM", item.DistanciaKM.HasValue ? item.DistanciaKM.Value : DBNull.Value);
                command.Parameters.AddWithValue("@Rota", item.Rota.HasValue ? item.Rota.Value : DBNull.Value);
                command.Parameters.AddWithValue("@PrevisaoEntrega", item.PrevisaoEntrega.HasValue ? item.PrevisaoEntrega.Value : DBNull.Value);
                command.Parameters.AddWithValue("@Responsavel", item.Responsavel == null ? DBNull.Value : item.Responsavel);
                command.Parameters.AddWithValue("@Prioridade", item.Prioridade);
                command.Parameters.AddWithValue("@Observacoes", item.Observacoes == null ? DBNull.Value : item.Observacoes);
                command.Parameters.AddWithValue("@DataCriacao", item.DataCriacao ?? DateTime.Now);
                command.Parameters.AddWithValue("@DataAtualizacao", item.DataAtualizacao ?? DateTime.Now);
                command.Parameters.AddWithValue("@DataCancelamento", item.DataCancelamento == null ? DBNull.Value : item.DataCancelamento);

                command.ExecuteNonQuery();
            }
        }

        #endregion        
    }
}