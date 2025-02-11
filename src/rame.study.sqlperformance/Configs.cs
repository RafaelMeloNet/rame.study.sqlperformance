// Copyright (c) 2025, Rafael Melo
// Todos os direitos reservados.
// All rights reserved.
//
// Este código é licenciado sob a licença BSD 3-Clause.
// This code is licensed under the BSD 3-Clause License.
//
// Consulte o arquivo LICENSE para obter mais informações.
// See the LICENSE file for more information.

using System.ComponentModel.DataAnnotations;

namespace rame.study.sqlperformance
{
    public class Configs
    {
        public string ConnStrOrigem { get; set; } = string.Empty;
        public string ConnStrDestino { get; set; } = string.Empty;
        public int CountToCopy { get; set; }
        public int WaitSeconds { get; set; }
    }
}