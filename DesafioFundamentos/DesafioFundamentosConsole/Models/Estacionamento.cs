using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using DesafioFundamentosConsole.Models;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<Carro> veiculos = new List<Carro>();
        private int quantidadeVagas=0;
        private int quantidadeVagasEspeciais=0;
        private const char comum = 'C'; 
        private const char especial = 'E';

        public int QuantidadeVagas { get=> quantidadeVagas; }

        public int QuantidadeVagasEpeciais { get=> quantidadeVagasEspeciais; }
        public Estacionamento(decimal precoInicial, decimal precoPorHora, int quantidadeVagas, int quantidadeVagasEspeciais)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
            this.quantidadeVagas = quantidadeVagas;
            this.quantidadeVagasEspeciais = quantidadeVagasEspeciais;
        }
        public Estacionamento(){

        }
        public void AdicionarVeiculo()
        {
            bool op = true;
            string placa;
            while (true)
            {
                Console.WriteLine("Digite a placa do veículo para estacionar:");
                placa = Console.ReadLine();
                //verifica se existe a mesma placa cadastrada no sistema
                if (!veiculos.Any(x => x.Placa.ToUpper() == placa.ToUpper()))
                {
                    if(ValidarPadraoPlaca(placa))
                    {
                        break;
                    }
                    else{
                        ExibirMensagem("Padrão inválido para placa !","error");
                    }  
                }
                else{
                    ExibirMensagem("A placa já foi cadastrada!","error");
                }
            }

            while(op)
            {
                Console.WriteLine("O carro tem direito a vaga especial ?\nS/N");
                string opcao = Console.ReadLine();
               if (opcao.ToUpper().Equals("S"))
               {
                    if(quantidadeVagasEspeciais > 0) 
                    {
                        quantidadeVagasEspeciais--;
                        veiculos.Add(new Carro(placa,especial));
                    }
                    else
                    {
                        ExibirMensagem("não existem vagas do tipo Especial disponiveis","warning");
                    }
                    op = false;
                    break;
               }
                else if(opcao.ToUpper().Equals("N")){

                    if(quantidadeVagas > 0) 
                    {
                        quantidadeVagas--;
                        veiculos.Add(new Carro(placa,comum));
                    }
                    else
                    {
                        ExibirMensagem("não existem vagas do tipo comum disponiveis","warning");
                    }
                    op = false;
                    break;
                }
                else{
                    ExibirMensagem("digite uma opção válida","warning");
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
                       if (veiculos[i].TipoCarro.Equals(comum))
                        {
                            quantidadeVagas++;
                        }
                       else
                        {
                            quantidadeVagasEspeciais++;
                        }                    
                       veiculos.RemoveAt(i); 
                    }
                }
                ExibirMensagem($"O veículo {placa} foi removido e o preço total foi de {valorTotal:C}","success");
            }
            else
            {   
                ExibirMensagem("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente",
                "warning");
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
                   Console.WriteLine(carro.ToString());
                }
            }
            else
            {   
                ExibirMensagem("Não há veículos estacionados.","error");
            }
        }
        //implementação mostrar numero de vagas restantes
        public void MostrarVagasEPrecos()
        {
            int vagasRestantes = quantidadeVagas;
            int vagasRestantesEspecial = quantidadeVagasEspeciais;

            Console.WriteLine("Vagas comuns disponiveis: "+ vagasRestantes + "\nVagas Especiais: "+ vagasRestantesEspecial);
            Console.WriteLine($"Preço inicial: {precoInicial}\nPreço por Hora: {precoPorHora}");   
        }

        public void GravarDados()
        {
            bool opcao = true;

            while (opcao)
            {
                Console.WriteLine("Deseja salvar a sessão ?\nS/N");
                switch(Console.ReadLine().ToUpper()) 
                {
                    case "S":
                        try
                        {
                            StreamWriter sw = new StreamWriter("Arquivos//estacionamento.txt");
                            
                            //grava os dados sobre as vagas
                            sw.WriteLine(quantidadeVagas+"|"+quantidadeVagasEspeciais);
                            // grava os dados dos preços
                            sw.WriteLine(precoInicial+"|"+precoPorHora);
                            // grava os dados dos carros
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
                            ExibirMensagem("Processo de gravação finalizado.","success");
                        }
                        opcao = false;
                        break;

                    case "N":
                        opcao = false;
                        break;
                    default:
                        ExibirMensagem("Opção inválida !","warning");
                        break;                    
                }
              
            }
        }
        public void CarregarDados()
        {   
            try
            {
                string[] array = File.ReadAllLines("Arquivos//estacionamento.txt");

                 for (int i = 2; i < array.Length; i++)
                {
                    string[] auxiliar = array[i].Split('|');
                    string placa = auxiliar[0];
                    char tipoCarro = auxiliar[1][0];
                    veiculos.Add(new Carro(placa, tipoCarro));
                }
                
                //carrega a primeira linha do arquivo com as vagas
                string[] vagas = array[0].Split('|');

                quantidadeVagas = Convert.ToInt32(vagas[0]);
                quantidadeVagasEspeciais = Convert.ToInt32(vagas[1]);

                // carrega segunda linha para os preços
                string[] precos = array[1].Split('|');

                precoInicial = Convert.ToInt32(precos[0]);
                precoPorHora = Convert.ToInt32(precos[1]);

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception" + e.Message);
            }
        }
        public bool ValidarPadraoPlaca(String placa)
        {
            //remove o caracter '-' e os espaços 
            placa = placa.Replace("-", "").Trim();
            // verifica se a placa está vazia
            if (String.IsNullOrEmpty(placa))
            {
                return false;
            }

            String padraoPlacaBrasil = @"^[a-zA-Z]{3}[0-9]{4}$";
            String padraoPlacaMercosul = @"^[a-zA-Z]{3}[0-9]{1}[a-zA-Z]{1}[0-9]{2}$";

            // retorna verdadeiro caso qualquer um dos dois padrões seja atendido
            return Regex.IsMatch(placa,padraoPlacaBrasil) || Regex.IsMatch(placa,padraoPlacaMercosul);
        }

        private void ExibirMensagem(String mensagem, string tipoMensagem) {
            switch (tipoMensagem)
            {
                case "warning":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(mensagem);
                    Console.ResetColor();
                    break;
                case "error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(mensagem);
                    Console.ResetColor();
                    break;
                case "success":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(mensagem);
                    Console.ResetColor();
                    break;
                default:
                    break;
            }


        }
    }
}
