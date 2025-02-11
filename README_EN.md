# .NET Performance Test Project with SQL Server Bulk Copy

This project demonstrates and tests the performance of the Bulk Copy operation in SQL Server using .NET and C#. It is structured into three main projects:

*   **Business Layer:** Contains the core logic and data access functionalities, including the Bulk Copy implementation.
*   **Console Application:** Allows running Bulk Copy tests via the command line.
*   **Worker Service:** Executes Bulk Copy tests in the background, configurable to run at regular intervals.

## Prerequisites

*   **.NET SDK:** Ensure you have the .NET SDK (version 8.0 or higher) installed.
*   **SQL Server:** An accessible SQL Server instance is required for the application. This can be a local instance or a remote server.
*   **SQL Server Management Studio (SSMS) (Optional):** Recommended for executing SQL scripts and inspecting the database.
*   **Visual Studio 2022 Community.**

## Configuration

1.  **Create the Database:**
    *   Execute the SQL scripts located in the `scripts/` folder on your SQL Server.
    *   Example usage in SSMS: Connect to your SQL Server in SSMS, open the scripts, and execute them.
2.  **Configure the Connection String:**
    *   The database connection string must be configured in the `appsettings.json` file of the `worker` project.
    *   Example `appsettings.json`:

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

    *   **Important:** Replace `a connectionstring` with the correct values for your SQL Server instance. (Note: I replaced "a connectionstring" which seemed like a placeholder).

3.  **Project References:**
    *   Verify that the `console` and `worker` projects have a reference to the `sqlperformance` project.

## Usage

### Console Application

1.  **Select the console project and set it as the startup project.**
2.  **Run the application: F5**
    *   The menu options guide the desired executions.

### Worker Service

1.  **Select the worker project and set it as the startup project.**
2.  **Run the application: F5**

## Contribution

Contributions are welcome! Feel free to open issues or submit pull requests with improvements, bug fixes, or new features.