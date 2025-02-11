-- COUNTs

use [rame.study.mock]

select Count(Id) as "Despacho" from Despacho

select Count(Id) as "Cliente" from Cliente

select Count(Id) as "Carga" from Carga

select Count(Id) as "Transportadora" from Transportadora

select Count(Id) as "Motorista" from Motorista

select Count(Id) as "Veiculo" from Veiculo

use [rame.study.mock]

select Count(Id) as "Despacho" from Despacho

use [rame.study.mockCopy]

select Count(DespachoId) as "Desnormalizado" from DespachoDesnormalizado

-- SELECTs

use [rame.study.mock]

select * from Despacho where EnderecoOrigem like '%rua%'

select * from Cliente where Nome like '%xavier%'

select * from Carga where Descricao like '%eos%'

select * from Transportadora where Observacoes like '%non%'

select * from Motorista where Nome like '%souza%'

select * from Veiculo where AnoFabricacao like '%2008%'

use [rame.study.mockCopy]

SELECT * from DespachoDesnormalizado

---- DELETEs

--use [rame.study.mock]

--delete from Despacho

--delete from Carga
--delete from Cliente
--delete from Transportadora
--delete from Veiculo
--delete from Motorista

use [rame.study.mockCopy]

delete from DespachoDesnormalizado

---- DROPs

use [rame.study.mock]

drop table Despacho

drop table Carga
drop table Cliente
drop table Veiculo
drop table Motorista
drop table Transportadora

use [rame.study.mockCopy]

drop table DespachoDesnormalizado