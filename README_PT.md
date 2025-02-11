# Projeto .NET de Teste de Performance com Bulk Copy no SQL Server

Este projeto demonstra e testa a performance da operação de Bulk Copy no SQL Server utilizando .NET e C#. Ele é estruturado em três projetos principais:

*   **Business Layer (Camada de Negócios):** Contém a lógica central e as funcionalidades de acesso a dados, incluindo a implementação do Bulk Copy.
*   **Console Application:** Permite a execução de testes de Bulk Copy via linha de comando.
*   **Worker Service:** Executa testes de Bulk Copy em segundo plano, podendo ser configurado para rodar em intervalos regulares.

## Pré-requisitos

*   **.NET SDK:** Certifique-se de ter o .NET SDK (versão 8.0 ou superior) instalado.
*   **SQL Server:** É necessário ter uma instância do SQL Server acessível para a aplicação. Pode ser uma instância local ou um servidor remoto.
*   **SQL Server Management Studio (SSMS) (Opcional):** Recomendado para executar os scripts SQL e inspecionar o banco de dados.
*   **Visual Studio 2022 Community.**

## Configuração

1.  **Criar o Banco de Dados:**
    *   Execute os scripts SQL localizados na pasta `scripts/` no seu SQL Server. 
    *   Exemplo de uso no SSMS: Conecte-se ao seu servidor SQL Server no SSMS, abra os scripts e execute-os.
2.  **Configurar a String de Conexão:**
    *   A string de conexão com o banco de dados deve ser configurada no arquivo `appsettings.json` do projeto `worker`.
    *   Exemplo de `appsettings.json`:

    ```json
    {
	  "Logging": {
		"LogLevel": {
		  "Default": "Information",
		  "Microsoft.Hosting.Lifetime": "Information"
		}
	  },
	  "DbOrigem": "Server=(LocalDB)\\MSSQLLocalDB;Database=rame.study.mock;Trusted_Connection=True;",
	  "DbDestino": "Server=(LocalDB)\\MSSQLLocalDB;Database=rame.study.mockcopy;Trusted_Connection=True;"
	}
    ```

    *   **Importante:** Substitua `a connectionstring`, com os valores corretos para sua instância do SQL Server.

3.  **Referências de Projeto:**
    *   Verifique se os projetos `console` e `worker` possuem uma referência ao projeto `sqlperformance`.

## Utilização

### Aplicação Console

1.  **Selecione o projeto console e marque como startup project:**
2.  **Execute a aplicação: F5**
    *   As opções do menu direcionam as execuções desejadas.

### Worker Service

1.  **Selecione o projeto worker e marque como startup project:**
    **Execute a aplicação: F5**

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou enviar pull requests com melhorias, correções de bugs ou novas funcionalidades.