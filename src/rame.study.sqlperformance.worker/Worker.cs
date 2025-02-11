// Copyright (c) 2025, Rafael Melo
// Todos os direitos reservados.
// All rights reserved.
//
// Este código é licenciado sob a licença BSD 3-Clause.
// This code is licensed under the BSD 3-Clause License.
//
// Consulte o arquivo LICENSE para obter mais informações.
// See the LICENSE file for more information.

using Microsoft.Extensions.Options;

namespace rame.study.sqlperformance.worker
{
    public class Worker(
        ILogger<Worker> logger,
        IServiceScopeFactory serviceScopeFactory,
        IOptions<Configs> configs,
        IHostApplicationLifetime lifetime) : BackgroundService
    {
        private readonly ILogger<Worker> logger = logger;
        private readonly IServiceScopeFactory serviceScopeFactory = serviceScopeFactory;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!lifetime.ApplicationStarted.IsCancellationRequested)
            {
                await Task.Delay(100, stoppingToken);
            }

            Console.WriteLine();

            Console.WriteLine(">>> Iniciando Worker");

            Console.WriteLine();

            int count = 0;

            while (!stoppingToken.IsCancellationRequested)
            {
                count++;

                long miliseconds;

                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<SqlPerformanceMethods>();

                    miliseconds = service.WriteOriginToTargetBulk();
                }

                if (logger.IsEnabled(LogLevel.Information))
                {
                    Console.WriteLine($">>> [{count}] - | Processo executado {configs.Value.CountToCopy} registros copiados em {miliseconds} milisegundos");
                    Console.WriteLine($">>> Próxima execução em {configs.Value.WaitSeconds} segundos.");
                    Console.WriteLine();
                }
                
                await Task.Delay(TimeSpan.FromSeconds(configs.Value.WaitSeconds), stoppingToken);
            }
        }
    }
}