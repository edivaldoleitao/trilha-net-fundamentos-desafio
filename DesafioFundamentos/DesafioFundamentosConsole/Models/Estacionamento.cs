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
                    int vagasRestantes = limiteVagas - veiculos.Where(carro => carro.TipoCarro.Equals(especial)).Count();

                    if(vagasRestantes >0) 
                    {
                        veiculos.Add(new Carro(placa,especial));
                    }
                    else
                    {
                        Console.WriteLine("não existem vagas do tipo Especial disponiveis");
                    }
                    op = false;
                    break;

                case "N":
                    int vagasRestantesEspecial = limiteVagasEspeciais - veiculos.Where(carro => carro.TipoCarro.Equals(comum)).Count();

                    if(vagasRestantesEspecial > 0) 
                    {
                        veiculos.Add(new Carro(placa,comum));
                    }
                    else
                    {
                        Console.WriteLine("não existem vagas do tipo comum disponiveis");
                    }
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
                   string tipo = carro.TipoCarro.Equals(comum) ? "comum" : "especial";
                   Console.WriteLine($" PLACA: {carro.Placa} - TIPO: {tipo}");
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
        //implementação mostrar numero de vagas restantes

        public void MostrarVagas()
        {
            int vagasRestantes=0;
            int vagasRestantesEspecial=0;

            vagasRestantes = limiteVagas - veiculos.Where(carro => carro.TipoCarro.Equals(comum)).Count();

            vagasRestantesEspecial = limiteVagasEspeciais - veiculos.Where(carro => carro.TipoCarro.Equals(especial)).Count();

            Console.WriteLine("Vagas comuns disponiveis: "+ vagasRestantes + "\nVagas Especiais: "+ vagasRestantesEspecial);   
        }

        public void GravarDados()
        {
            try
            {
                StreamWriter sw = new StreamWriter("Arquivos//carros.txt");
                
                foreach (var carro in veiculos)
                {
                   sw.WriteLine(carro.Placa + "|" + carro.TipoCarro); 
                }     
                sw.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Processo de gravação finalizado.");
            }
        }
    }
}
