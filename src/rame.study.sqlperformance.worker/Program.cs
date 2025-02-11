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
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.Configure<Configs>(builder.Configuration);

            builder.Services.AddScoped(provider =>
            {
                var configs = provider.GetRequiredService<IOptions<Configs>>().Value;

                return new SqlPerformanceMethods(configs);
            });

            builder.Services.AddHostedService<Worker>();

            var host = builder.Build();

            host.Run();
        }
    }
}