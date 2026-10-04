using System;
using System.Globalization;

namespace CalculoJuros;

public class Program
{
    public static void Main()
    {
        CultureInfo cultura = new CultureInfo("pt-BR");

        Console.Clear();
        Console.WriteLine("=== CALCULADORA DE JUROS / MULTA POR ATRASO ===");
        Console.WriteLine("Regra: Multa/Juros de 2,5% ao dia de atraso.\n");

        // 1. Leitura do Valor Original (com repetição até digitar correto)
        decimal valorOriginal = 0;
        while (true)
        {
            Console.Write("Digite o valor original da conta (R$): ");
            string? entradaValor = Console.ReadLine();

            if (decimal.TryParse(entradaValor, NumberStyles.Any, cultura, out valorOriginal) && valorOriginal > 0)
            {
                break; // Entrada válida, sai do laço
            }

            Console.WriteLine("[ERRO]: Valor inválido! Digite um número positivo (exemplo: 150,00).\n");
        }

        // 2. Leitura da Data de Vencimento (com repetição até digitar correto)
        DateTime dataVencimento;
        while (true)
        {
            Console.Write("Digite a data de vencimento (dd/MM/yyyy): ");
            string? entradaData = Console.ReadLine();

            if (DateTime.TryParseExact(entradaData, "dd/MM/yyyy", cultura, DateTimeStyles.None, out dataVencimento))
            {
                break; // Data válida, sai do laço
            }

            Console.WriteLine("[ERRO]: Data inválida! Use o formato dd/MM/yyyy (exemplo: 15/10/2026).\n");
        }

        // 3. Obter a Data Atual
        DateTime dataHoje = DateTime.Today;

        Console.WriteLine($"\nData de hoje considerada: {dataHoje:dd/MM/yyyy}");
        Console.WriteLine($"Data de vencimento       : {dataVencimento:dd/MM/yyyy}");

        // 4. Cálculo dos Dias de Atraso
        int diasAtraso = (dataHoje - dataVencimento).Days;

        // 5. Exibição do Resultado
        if (diasAtraso <= 0)
        {
            Console.WriteLine("\n==================================================");
            Console.WriteLine("A conta está em dia ou vence hoje!");
            Console.WriteLine($"Valor a pagar: {valorOriginal.ToString("C", cultura)}");
            Console.WriteLine("==================================================");
        }
        else
        {
            decimal taxaDiaria = 0.025m; // 2,5% ao dia
            decimal percentualJurosTotal = diasAtraso * taxaDiaria;
            decimal valorJuros = valorOriginal * percentualJurosTotal;
            decimal valorTotalPagar = valorOriginal + valorJuros;

            Console.WriteLine("\n==================================================");
            Console.WriteLine("              DETALHAMENTO DO CÁLCULO             ");
            Console.WriteLine("==================================================");
            Console.WriteLine($"Dias em atraso       : {diasAtraso} dia(s)");
            Console.WriteLine($"Taxa de juros diária : 2,5%");
            Console.WriteLine($"Juros acumulados     : {percentualJurosTotal * 100:N1}%");
            Console.WriteLine($"--------------------------------------------------");
            Console.WriteLine($"Valor Original       : {valorOriginal.ToString("C", cultura)}");
            Console.WriteLine($"Valor dos Juros/Multa: {valorJuros.ToString("C", cultura)}");
            Console.WriteLine($"--------------------------------------------------");
            Console.WriteLine($">>> VALOR TOTAL A PAGAR: {valorTotalPagar.ToString("C", cultura)} <<<");
            Console.WriteLine("==================================================");
        }

        Console.WriteLine("\nPressione qualquer tecla para encerrar...");
        Console.ReadKey();
    }
}