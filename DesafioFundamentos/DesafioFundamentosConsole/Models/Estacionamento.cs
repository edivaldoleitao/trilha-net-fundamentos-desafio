using System.Reflection.Metadata.Ecma335;
using DesafioFundamentosConsole.Models;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<Carro> veiculos = new List<Carro>();
        private int limiteVagas;
        private int limiteVagasEspeciais;
        private const char comum = 'C';
        private const char especial = 'E';

        public int LimiteVagas { get=> limiteVagas; }

        public int LimiteVagasEpeciais { get=> limiteVagasEspeciais; }
        public Estacionamento(decimal precoInicial, decimal precoPorHora, int limiteVagas, int limiteVagasEspeciais)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
            this.limiteVagas = limiteVagas;
            this.limiteVagasEspeciais = limiteVagasEspeciais;
        }

        public void AdicionarVeiculo()
        {
            // TODO: Pedir para o usuário digitar uma placa (ReadLine) e adicionar na lista "veiculos"

            bool op = true;
            string placa;
            while (true)
            {
                Console.WriteLine("Digite a placa do veículo para estacionar:");
                placa = Console.ReadLine();
                if (!veiculos.Any(x => x.Placa.ToUpper() == placa.ToUpper()))
                {
                    break;
                }
                else{
                    Console.WriteLine("A placa já foi cadastrada!");
                }
            }



            while(op)
            {
                Console.WriteLine("O carro tem direito a vaga especial ?\ny/n");
                string opcao = Console.ReadLine();

               switch (opcao.ToUpper())
               {
                case "Y":
                    veiculos.Add(new Carro(placa,especial));
                    op = false;
                    break;

                case "N":
                    veiculos.Add(new Carro(placa,comum));
                    op = false;
                    break;
                default:
                    Console.WriteLine("digite uma opção válida");
                break;
               }
            }
            
            
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");

            // Pedir para o usuário digitar a placa e armazenar na variável placa
            string placa = "";
            placa = Console.ReadLine();

            // Verifica se o veículo existe
            if (veiculos.Any(x => x.Placa.ToUpper() == placa.ToUpper()))
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                // TODO: Pedir para o usuário digitar a quantidade de horas que o veículo permaneceu estacionado,
                // TODO: Realizar o seguinte cálculo: "precoInicial + precoPorHora * horas" para a variável valorTotal                
                int horas = 0;
                decimal valorTotal = 0; 
                horas = Convert.ToInt32(Console.ReadLine());
                valorTotal = horas*precoPorHora + precoInicial;

                // TODO: Remover a placa digitada da lista de veículos
                for(int i=0; i < veiculos.Count; i++)
                {
                    if (veiculos[i].Placa.Equals(placa))
                    {
                       veiculos.RemoveAt(i); 
                    }
                }
                

                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                // TODO: Realizar um laço de repetição, exibindo os veículos estacionados
                foreach (var carro in veiculos)
                {
                    Console.WriteLine(carro.Placa);
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
    }
}
