// Copyright (c) 2025, Rafael Melo
// Todos os direitos reservados.
// All rights reserved.
//
// Este código é licenciado sob a licença BSD 3-Clause.
// This code is licensed under the BSD 3-Clause License.
//
// Consulte o arquivo LICENSE para obter mais informações.
// See the LICENSE file for more information.

using Bogus;
using rame.study.sqlperformance.Internal.Models;

namespace rame.study.sqlperformance.Internal
{
    internal class DataMocker
    {
        public static List<Despacho> GenerateMockData(int numberOfRecords)
        {
            int numberOfClientes = (int)(numberOfRecords * 0.58);
            int numberOfCargas = (int)(numberOfRecords * 0.87);
            int numberOfMotoristas = (int)(numberOfRecords * 0.0512);
            int numberOfTransportadoras = (int)(numberOfRecords * 0.0018);
            int numberOfVeiculos = (int)(numberOfRecords * 0.05);

            var clienteFaker = new Faker<Cliente>("pt_BR")
               .RuleFor(c => c.Id, f => f.IndexFaker + 1)
               .RuleFor(c => c.Nome, f => f.Person.FullName)
               .RuleFor(c => c.Telefone, f => f.Phone.PhoneNumber())
               .RuleFor(c => c.Email, f => f.Internet.Email());

            var clientes = clienteFaker.Generate(numberOfClientes);

            var cargaFaker = new Faker<Carga>("pt_BR")
               .RuleFor(c => c.Id, f => f.IndexFaker + 1)
               .RuleFor(c => c.Tipo, f => f.PickRandom("Eletrônicos", "Alimentos", "Vestuário", "Móveis"))
               .RuleFor(c => c.Peso, f => f.Random.Decimal(1, 1000))
               .RuleFor(c => c.Dimensoes, f => $"{f.Random.Int(1, 10)}x{f.Random.Int(1, 10)}x{f.Random.Int(1, 10)}m")
               .RuleFor(c => c.Valor, f => f.Random.Decimal(100, 10000))
               .RuleFor(c => c.Seguro, f => f.Random.Bool())
               .RuleFor(c => c.Descricao, f => f.Lorem.Sentence(10))
               .RuleFor(c => c.Altura, f => f.Random.Decimal(0.5m, 5m))
               .RuleFor(c => c.Largura, f => f.Random.Decimal(0.5m, 5m))
               .RuleFor(c => c.Profundidade, f => f.Random.Decimal(0.5m, 5m))
               .RuleFor(c => c.DataCriacao, f => f.Date.Past(2))
               .RuleFor(c => c.Observacoes, f => f.Lorem.Sentence(5));

            var cargas = cargaFaker.Generate(numberOfCargas);

            var motoristaFaker = new Faker<Motorista>("pt_BR")
              .RuleFor(m => m.Id, f => f.IndexFaker + 1)
              .RuleFor(m => m.Nome, f => f.Person.FullName)
              .RuleFor(m => m.Telefone, f => f.Phone.PhoneNumber())
              .RuleFor(m => m.Cnh, f => f.Random.Replace("###########"))
              .RuleFor(m => m.DataNascimento, f => f.Date.Past(50, DateTime.Now.AddYears(-18)))
              .RuleFor(m => m.Endereco, f => f.Address.StreetAddress())
              .RuleFor(m => m.Salario, f => f.Random.Decimal(1500, 10000))
              .RuleFor(m => m.Observacoes, f => f.Lorem.Sentence(5));

            var motoristas = motoristaFaker.Generate(numberOfMotoristas);

            var transportadoraFaker = new Faker<Transportadora>("pt_BR")
              .RuleFor(t => t.Id, f => f.IndexFaker + 1)
              .RuleFor(t => t.Nome, f => f.Company.CompanyName())
              .RuleFor(t => t.Telefone, f => f.Phone.PhoneNumber())
              .RuleFor(t => t.Endereco, f => f.Address.StreetAddress())
              .RuleFor(t => t.Cnpj, f => f.Random.Replace("##.###.###/####-##"))
              .RuleFor(t => t.InscricaoEstadual, f => f.Random.Replace("#########"))
              .RuleFor(t => t.DataCadastro, f => f.Date.Past(5))
              .RuleFor(t => t.Observacoes, f => f.Lorem.Sentence(5));

            var transportadoras = transportadoraFaker.Generate(numberOfTransportadoras);

            var veiculoFaker = new Faker<Veiculo>("pt_BR")
               .RuleFor(v => v.Id, f => f.IndexFaker + 1)
               .RuleFor(v => v.Placa, f => f.Random.Replace("???-####"))
               .RuleFor(v => v.Modelo, f => f.Vehicle.Model())
               .RuleFor(v => v.Cor, f => f.Commerce.Color())
               .RuleFor(v => v.IdMotorista, f => f.PickRandom(motoristas).Id)
               .RuleFor(v => v.AnoFabricacao, f => f.Random.Int(2000, DateTime.Now.Year))
               .RuleFor(v => v.CapacidadeCarga, f => f.Random.Decimal(1000, 20000))
               .RuleFor(v => v.DataRevisao, f => f.Date.Past(1))
               .RuleFor(v => v.Observacoes, f => f.Lorem.Sentence(5));

            var veiculos = veiculoFaker.Generate(numberOfVeiculos);

            foreach (var veiculo in veiculos)
            {
                veiculo.Motorista = motoristas.FirstOrDefault(x => x.Id == veiculo.IdMotorista) ?? motoristas.First();
            }

            var despachoFaker = new Faker<Despacho>("pt_BR")
                .RuleFor(d => d.Id, f => f.IndexFaker + 1)
                .RuleFor(d => d.DataPartida, f => f.Date.Future(2))
                .RuleFor(d => d.DataProcessamento, f => f.Date.Past(1))
                .RuleFor(d => d.DataEntrega, (f, d) => f.Date.Future(3, d.DataPartida))
                .RuleFor(d => d.EnderecoOrigem, f => f.Address.StreetAddress())
                .RuleFor(d => d.EnderecoDestino, f => f.Address.StreetAddress())
                .RuleFor(d => d.CoordenadasOrigem, f => $"{f.Address.Latitude()}, {f.Address.Longitude()}")
                .RuleFor(d => d.CoordenadasDestino, f => $"{f.Address.Latitude()}, {f.Address.Longitude()}")
                .RuleFor(d => d.Status, f => f.PickRandom("Pendente", "Em Trânsito", "Entregue", "Cancelado", "Aguardando Carga"))
                .RuleFor(d => d.Log, f => f.Lorem.Sentences(3))
                .RuleFor(d => d.ClienteId, f => f.PickRandom(clientes).Id)
                .RuleFor(d => d.CargaId, f => f.PickRandom(cargas).Id)
                .RuleFor(d => d.VeiculoId, f => f.PickRandom(veiculos).Id)
                .RuleFor(d => d.TransportadoraId, f => f.PickRandom(transportadoras).Id)
                .RuleFor(d => d.ValorFrete, f => f.Random.Decimal(50, 5000))
                .RuleFor(d => d.DistanciaKM, f => f.Random.Decimal(10, 2000))
                .RuleFor(d => d.Rota, f => f.Random.Int(1, 10))
                .RuleFor(d => d.PrevisaoEntrega, f => f.Date.Future(4))
                .RuleFor(d => d.Responsavel, f => f.Person.FullName)
                .RuleFor(d => d.Prioridade, f => f.Random.Int(1, 5))
                .RuleFor(d => d.Observacoes, f => f.Lorem.Sentence(5))
                .RuleFor(d => d.DataCriacao, f => f.Date.Past(2))
                .RuleFor(d => d.DataAtualizacao, f => f.Date.Past(1))
                .RuleFor(d => d.DataCancelamento, f => f.Date.Past(1).OrNull(f, 0.12f));

            var despachos = despachoFaker.Generate(numberOfRecords);

            foreach (var despacho in despachos)
            {
                despacho.Cliente = clientes.FirstOrDefault(x => x.Id == despacho.ClienteId) ?? clientes.First();
                despacho.Carga = cargas.FirstOrDefault(x => x.Id == despacho.CargaId) ?? cargas.First();
                despacho.Veiculo = veiculos.FirstOrDefault(x => x.Id == despacho.VeiculoId) ?? veiculos.First();
                despacho.Transportadora = transportadoras.FirstOrDefault(x => x.Id == despacho.TransportadoraId) ?? transportadoras.First();
            }

            return despachos;
        }
    }
}