use [rame.study.mock]

SET STATISTICS TIME ON

SELECT TOP 1000
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
INNER JOIN Cliente c ON d.ClienteId = c.Id --AND c.Nome LIKE '%maria%'
INNER JOIN Carga cg ON d.CargaId = cg.Id
INNER JOIN Veiculo v ON d.VeiculoId = v.Id
INNER JOIN Motorista m ON v.IdMotorista = m.Id
INNER JOIN Transportadora t ON d.TransportadoraId = t.Id
ORDER BY d.Id DESC

SET STATISTICS TIME OFF