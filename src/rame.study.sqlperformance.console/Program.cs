// Copyright (c) 2025, Rafael Melo
// Todos os direitos reservados.
// All rights reserved.
//
// Este código é licenciado sob a licença BSD 3-Clause.
// This code is licensed under the BSD 3-Clause License.
//
// Consulte o arquivo LICENSE para obter mais informações.
// See the LICENSE file for more information.

using System.ComponentModel;
using System.Diagnostics;

namespace rame.study.sqlperformance.console
{
    internal class Program
    {
        private static readonly Configs configs = new()
        {
            ConnStrOrigem = @"Server=(LocalDB)\MSSQLLocalDB;Database=rame.study.mock;Trusted_Connection=True;",
            ConnStrDestino = @"Server=(LocalDB)\MSSQLLocalDB;Database=rame.study.mockcopy;Trusted_Connection=True;"
        };

        private readonly static SqlPerformanceMethods sqlPerformanceMethods = new(configs);

        public static void Main(string[] args)
        {
            bool showMenu = true;

            while (showMenu)
            {
                Console.Clear();
                Console.WriteLine("1 - ORIGEM - Popular Banco / Populate Database");
                Console.WriteLine("2 - ORIGEM - Executar / Execute Query/View - benchmark");
                Console.WriteLine(string.Join(string.Empty, [
                    $"3 - Processar - Executa Query/View ORIGEM e Insert na tabela banco DESTINO{Environment.NewLine}",
                    "3 - Process   - Executes SOURCE query/view and inserts into DESTINATION bank table." ]));
                Console.WriteLine("0 - Sair / Exit");
                Console.WriteLine("Selecione uma opção: ");

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        SubmenuOpcaoSelecionada(key.KeyChar);
                        break;
                    case '2':
                        SubmenuOpcaoSelecionada(key.KeyChar);
                        break;
                    case '3':
                        SubmenuOpcaoSelecionada(key.KeyChar);
                        break;
                    case '0':
                        showMenu = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida.Invalid option. Press any key to try again.");
                        Console.ReadKey(true);
                        break;
                }
            }

            Console.WriteLine("Programa finalizado.");
        }

        static void SubmenuOpcaoSelecionada(char keychar)
        {
            bool showSubMenu = true;

            while (showSubMenu)
            {
                Console.Clear();
                Console.WriteLine($"===== Submenu Opção {keychar} =====");
                Console.WriteLine("1 - Executar");
                Console.WriteLine("2 - Voltar");
                Console.WriteLine("Selecione uma opção: ");

                ConsoleKeyInfo keySubMenu = Console.ReadKey(true);

                long milliseconds = 0;

                switch (keySubMenu.KeyChar)
                {
                    case '1':
                        
                        switch (keychar)
                        {
                            case '1':

                                for (int i = 0; i < 100; i++)
                                {
                                    milliseconds += sqlPerformanceMethods.PopularBancoOrigem(1000);
                                }

                                Write($"PopularBancoOrigem", milliseconds);

                                WaitKey(); 
                                
                                break;

                            case '2':

                                milliseconds = sqlPerformanceMethods.ExecutarQueryOrigem();

                                Write($"ExecutarQueryOrigem", milliseconds);

                                WaitKey();

                                break;

                            case '3':

                                milliseconds = sqlPerformanceMethods.WriteOriginToTargetBulk();

                                Write($"WriteOriginToTargetBulk", milliseconds);

                                WaitKey();

                                break;
                        }

                        break;

                    case '2':

                        showSubMenu = false;

                        break;

                    default:

                        Console.WriteLine("Opção inválida.Invalid option. Press any key to try again.");
                        Console.ReadKey(true);

                        break;
                }
            }
        }

        private static void WaitKey()
        {
            Console.WriteLine("Precione qualquer tecla para continuar... Press any key to continue...");
            Console.ReadKey();
        }

        private static void Write(string text, long ms)
        {
            Console.Write($"{text} : {ms} milleseconds");
        }
    }
}
