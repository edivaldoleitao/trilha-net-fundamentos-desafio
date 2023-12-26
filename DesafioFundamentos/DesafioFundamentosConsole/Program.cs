using DesafioFundamentos.Models;
// adicioar limite de vagas, criar classe carro com placa e tipo idoso, e limitar as vagas especiais
// Coloca o encoding para UTF8 para exibir acentuação
Console.OutputEncoding = System.Text.Encoding.UTF8;

decimal precoInicial = 0;
decimal precoPorHora = 0;
int limiteVagas = 0;
int limiteVagasEspeciais = 0;
Estacionamento es;

Console.WriteLine("Deseja carregar a última sessão ?\nS/N");

if (Console.ReadLine().ToUpper().Equals("N"))
{
    
    Console.WriteLine("Seja bem vindo ao sistema de estacionamento!\n" +
                    "Digite o preço inicial:");

    precoInicial = ConverterDecimal();

    Console.WriteLine("Agora digite o preço por hora:");

    precoPorHora = ConverterDecimal();

    Console.WriteLine("Agora digite o limite de vagas comuns:");

    limiteVagas = ConverterInteiro();

    Console.WriteLine("Agora digite a quantidade de vagas especiais:");

    limiteVagasEspeciais = ConverterInteiro();

    // Instancia a classe Estacionamento, já com os valores obtidos anteriormente
    es = new Estacionamento(precoInicial, precoPorHora, limiteVagas, limiteVagasEspeciais);
}    
else {
    // inicia vazio e preenche através do arquivo
    es = new Estacionamento();
    es.CarregarDados();
}


bool exibirMenu = true;
// Realiza o loop do menu
while (exibirMenu)
{
    Console.Clear();
    Console.WriteLine("Digite a sua opção:");
    Console.WriteLine("1 - Cadastrar veículo");
    Console.WriteLine("2 - Remover veículo");
    Console.WriteLine("3 - Listar veículos");
    Console.WriteLine("4 - Encerrar");
    Console.WriteLine("5 - Mostrar vagas e Preços");

    String opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            es.AdicionarVeiculo();
            break;

        case "2":
            es.RemoverVeiculo();
            break;

        case "3":
            es.ListarVeiculos();
            break;

        case "4":
            es.GravarDados();
            exibirMenu = false;
            break;

        case "5":
            es.MostrarVagasEPrecos();
            break;
        default:
            Console.WriteLine("Opção inválida");
            break;
    }

    Console.WriteLine("Pressione uma tecla para continuar");
    Console.ReadLine();
}

Console.WriteLine("O programa se encerrou");

//tenta capturar um valor válido do decimal, enquanto não for, fica me loop
static decimal ConverterDecimal() {

    decimal valor;
    while(true) {
        if(!Decimal.TryParse(Console.ReadLine(),out valor) || valor <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("digite valor válido de preço ");
            Console.ResetColor();
        }
        else {
            break;
        };
    }
    return valor;
}

//tenta capturar um valor válido do inteiro, enquanto não for, fica me loop
static int ConverterInteiro() 
{
    int valor;
    while(true)
    {
        if (!int.TryParse(Console.ReadLine(),out valor) || valor <= 0)
        {   
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("digite um valor válido para o limite de vagas");
            Console.ResetColor();
        }
        else {
            break;
        }
    }
    return valor;
};