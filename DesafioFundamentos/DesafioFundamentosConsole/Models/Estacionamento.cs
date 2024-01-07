using System.Text.RegularExpressions;

namespace DesafioFundamentosConsole.Models
{
    public class Estacionamento
    {
        private decimal _precoInicial = 0;
        private decimal _precoPorHora = 0;
        private List<Carro> _veiculos = new List<Carro>();
        private int _quantidadeVagas=0;
        private int _quantidadeVagasEspeciais=0;
        private const char comum = 'C';
        private const char especial = 'E';
        private Dictionary<String,DateTime> _registroHorarios = new Dictionary<String, DateTime>();
        public decimal PrecoPorHora { get => _precoPorHora; set => _precoInicial = value; }
        public decimal PrecoInicial { get =>_precoInicial; set=> _precoInicial = value; }
        public int QuantidadeVagas { get=> _quantidadeVagas; }
        public int QuantidadeVagasEpeciais { get=> _quantidadeVagasEspeciais; }
        public Estacionamento(decimal precoInicial, decimal precoPorHora, int quantidadeVagas, int quantidadeVagasEspeciais)
        {
            this._precoInicial = precoInicial;
            this._precoPorHora = precoPorHora;
            this._quantidadeVagas = quantidadeVagas;
            this._quantidadeVagasEspeciais = quantidadeVagasEspeciais;
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
                if (!_veiculos.Any(x => x.Placa.ToUpper() == placa.ToUpper()))
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
                    if(_quantidadeVagasEspeciais > 0) 
                    {
                        _quantidadeVagasEspeciais--;
                        _veiculos.Add(new Carro(placa,especial));
                        _registroHorarios.Add(placa,DateTime.Now);
                    }
                    else
                    {
                        ExibirMensagem("não existem vagas do tipo Especial disponiveis","warning");
                    }
                    op = false;
                    break;
               }
                else if(opcao.ToUpper().Equals("N")){

                    if(_quantidadeVagas > 0) 
                    {
                        _quantidadeVagas--;
                        _veiculos.Add(new Carro(placa,comum));
                        _registroHorarios.Add(placa, DateTime.Now);
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
            DateTime horarioEntrada = new DateTime();
            placa = Console.ReadLine();

            // Verifica se o veículo existe
            if (_veiculos.Any(x => x.Placa.ToUpper() == placa.ToUpper()))
            {
                // o metodo recebe o horario atual do sistema para saida, horario do registro da placa
                // preco inicial e preco por hora
                decimal valorTotal = CalcularTicketEstacionamento(_registroHorarios[placa], DateTime.Now,
                _precoInicial, _precoPorHora);

                // TODO: Remover a placa digitada da lista de veículos
                for(int i=0; i < _veiculos.Count; i++)
                {
                    if (_veiculos[i].Placa.Equals(placa))
                    {
                       if (_veiculos[i].TipoCarro.Equals(comum))
                        {
                            _quantidadeVagas++;
                        }
                       else
                        {
                            _quantidadeVagasEspeciais++;
                            valorTotal *= 0.8m; // desconto pela mobilidade do usuário
                        }
                        horarioEntrada = _registroHorarios[_veiculos[i].Placa];
                        _registroHorarios.Remove(_veiculos[i].Placa);                    
                        _veiculos.RemoveAt(i);
                       
                    }
                }
                ExibirMensagem($"O veículo {placa} foi removido e o preço total foi de {valorTotal:C}","success");
                ExibirMensagem($"Horário Entrada veículo: {horarioEntrada}\nHorário saída : {DateTime.Now}", "success");
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
            if (_veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                // TODO: Realizar um laço de repetição, exibindo os veículos estacionados
                foreach (var carro in _veiculos)
                {
                   Console.WriteLine("==================================");
                   Console.WriteLine(carro.ToString());
                   Console.WriteLine($"Horário Entrada: {_registroHorarios[carro.Placa]}");
                   Console.WriteLine("==================================");
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
            int vagasRestantes = _quantidadeVagas;
            int vagasRestantesEspecial = _quantidadeVagasEspeciais;

            Console.WriteLine("Vagas comuns disponiveis: "+ vagasRestantes + "\nVagas Especiais: "+ vagasRestantesEspecial);
            Console.WriteLine($"Preço inicial: {_precoInicial:C}\nPreço por Hora: {_precoPorHora:C}");   
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
                            sw.WriteLine(_quantidadeVagas+"|"+_quantidadeVagasEspeciais);
                            // grava os dados dos preços
                            sw.WriteLine(_precoInicial+"|"+_precoPorHora);
                            // grava os dados dos carros
                            foreach (var carro in _veiculos)
                            {
                                DateTime horario = _registroHorarios[carro.Placa];
                                sw.WriteLine(carro.Placa + "|" + carro.TipoCarro + "|" + horario); 
                            }     
                            sw.Close();
                            ExibirMensagem("Processo de gravação finalizado.","success");
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("Exception: " + e.Message);
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
                    DateTime horario = Convert.ToDateTime(auxiliar[2]);

                    _registroHorarios.Add(placa,horario);
                    _veiculos.Add(new Carro(placa, tipoCarro));
                }
                
                //carrega a primeira linha do arquivo com as vagas
                string[] vagas = array[0].Split('|');

                _quantidadeVagas = Convert.ToInt32(vagas[0]);
                _quantidadeVagasEspeciais = Convert.ToInt32(vagas[1]);

                // carrega segunda linha para os preços
                string[] precos = array[1].Split('|');

                _precoInicial = Convert.ToInt32(precos[0]);
                _precoPorHora = Convert.ToInt32(precos[1]);

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception" + e.Message);
            }
        }
        public bool ValidarPadraoPlaca(String placa)
        {
            //remove o caracter '-' e os espaços 
            placa = Regex.Replace(placa, @"[-\s]", "");
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
        // metodo que calcula o valor devido a partir do tempo decorrido
        public decimal CalcularTicketEstacionamento(DateTime horarioInicial, DateTime horarioFinal,
         decimal _precoInicial, decimal precoHora) 
        {
            double horas = (horarioFinal - horarioInicial).TotalHours; 
            decimal result = _precoInicial + precoHora*(decimal)horas; 
            return result;
        }
    }
}
