using System;
using System.Collections.Generic;
using System.Globalization;

// Classe abstrata base para todos os funcionários
public abstract class Funcionario
{
    // Propriedades comuns a todos os funcionários
    public string Nome { get; set; }
    public double SalarioBase { get; set; }

    // Construtor para inicializar as propriedades
    public Funcionario(string nome, double salarioBase)
    {
        Nome = nome;
        SalarioBase = salarioBase;
    }

    // Método abstrato que será implementado de forma diferente em cada classe filha
    public abstract double CalcularSalario();

    // Método concreto para exibir os dados. Ele pode ser usado por todas as classes filhas.
    public void ExibirDados()
    {
        // Calcula o salário final chamando o método implementado pela classe filha
        double salarioFinal = CalcularSalario();

        // Exibe as informações formatadas no console
        Console.WriteLine($"Nome: {Nome}");
        // GetType().Name retorna o nome da classe (ex: "Administrativo")
        Console.WriteLine($"Tipo: {this.GetType().Name}");
        // Formata o salário final como moeda local (R$)
        Console.WriteLine($"Salário Final: {salarioFinal.ToString("C", CultureInfo.CurrentCulture)}");
        Console.WriteLine("-----------------------------------");
    }
}

// Classe filha para funcionários Administrativos
public class Administrativo : Funcionario
{
    // Construtor que chama o construtor da classe base (Funcionario)
    public Administrativo(string nome, double salarioBase) : base(nome, salarioBase) { }

    // Implementação específica do cálculo de salário para Administrativo
    public override double CalcularSalario()
    {
        // Salário base + 10% de bônus
        return SalarioBase * 1.10;
    }
}

// Classe filha para funcionários Técnicos
public class Tecnico : Funcionario
{
    // Construtor que chama o construtor da classe base
    public Tecnico(string nome, double salarioBase) : base(nome, salarioBase) { }

    // Implementação específica do cálculo de salário para Técnico
    public override double CalcularSalario()
    {
        // Salário base + 20% de adicional técnico
        return SalarioBase * 1.20;
    }
}

// Classe filha para Estagiários
public class Estagiario : Funcionario
{
    // Construtor que chama o construtor da classe base
    public Estagiario(string nome, double salarioBase) : base(nome, salarioBase) { }

    // Implementação específica do cálculo de salário para Estagiário
    public override double CalcularSalario()
    {
        // Metade do salário base
        return SalarioBase * 0.5;
    }
}

// Classe principal do programa
class Program
{
    static void Main(string[] args)
    {
        // Define a cultura para garantir que o formato da moeda (R$) seja exibido corretamente
        CultureInfo.CurrentCulture = new CultureInfo("pt-BR", false);

        // Cria uma lista para armazenar todos os objetos de funcionário
        List<Funcionario> funcionarios = new List<Funcionario>();
        string continuar = "S";

        // Loop para cadastrar vários funcionários
        while (continuar.ToUpper() == "S")
        {
            Console.Clear(); // Limpa o console para cada novo cadastro
            Console.WriteLine("--- Cadastro de Novo Funcionário ---");

            // Solicita o tipo de funcionário
            Console.Write("Digite o tipo de funcionário (1-Administrativo, 2-Técnico, 3-Estagiário): ");
            int tipo = int.Parse(Console.ReadLine());

            // Solicita o nome
            Console.Write("Digite o nome: ");
            string nome = Console.ReadLine();

            // Solicita o salário base
            Console.Write("Digite o salário base: R$ ");
            double salarioBase = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            Funcionario novoFuncionario = null;

            // Cria o objeto do tipo correto com base na escolha do usuário
            switch (tipo)
            {
                case 1:
                    novoFuncionario = new Administrativo(nome, salarioBase);
                    break;
                case 2:
                    novoFuncionario = new Tecnico(nome, salarioBase);
                    break;
                case 3:
                    novoFuncionario = new Estagiario(nome, salarioBase);
                    break;
                default:
                    Console.WriteLine("Tipo inválido! O funcionário não será cadastrado.");
                    break;
            }

            // Se um funcionário válido foi criado, adiciona-o à lista
            if (novoFuncionario != null)
            {
                funcionarios.Add(novoFuncionario);
                Console.WriteLine("Funcionário cadastrado com sucesso!");
            }

            // Pergunta ao usuário se deseja continuar cadastrando
            Console.Write("\nDeseja cadastrar outro funcionário? (S/N): ");
            continuar = Console.ReadLine();
        }

        // Exibição do relatório final
        Console.Clear();
        Console.WriteLine("\n--- Relatório Completo de Salários ---");

        if (funcionarios.Count == 0)
        {
            Console.WriteLine("Nenhum funcionário cadastrado.");
        }
        else
        {
            // Itera sobre a lista de funcionários e chama o método para exibir os dados de cada um
            foreach (Funcionario func in funcionarios)
            {
                func.ExibirDados();
            }
        }

        Console.WriteLine("Pressione qualquer tecla para sair...");
        Console.ReadKey();
    }
}