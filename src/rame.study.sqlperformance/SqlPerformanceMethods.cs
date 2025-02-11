// Copyright (c) 2025, Rafael Melo
// Todos os direitos reservados.
// All rights reserved.
//
// Este código é licenciado sob a licença BSD 3-Clause.
// This code is licensed under the BSD 3-Clause License.
//
// Consulte o arquivo LICENSE para obter mais informações.
// See the LICENSE file for more information.

using rame.study.sqlperformance.Internal;
using System.Diagnostics;

namespace rame.study.sqlperformance
{
    /// <summary>
    /// É a única classe public deste projeto/assembly (.dll).
    /// Dependendo do projeto, podem exixtir mais de uma classe public no assembly.
    /// </summary>
    public class SqlPerformanceMethods(Configs configs)
    {
        Stopwatch stopwatch = new Stopwatch();

        public long PopularBancoOrigem(int itemCount)
        {
            stopwatch.Restart();

            var data = DataMocker.GenerateMockData(itemCount);

            new DataWriter(configs).WriteDataToDbOrigem(data);

            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds;
        }

        public long ExecutarQueryOrigem()
        {
            stopwatch.Restart();

            new DataReader(configs.ConnStrOrigem).GetAllData();

            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds;
        }

        public long WriteOriginToTargetBulk()
        {
            stopwatch.Restart();

            new  DataWriter(configs).WriteOriginToTargetBulk();

            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds;
        }
    }
}
