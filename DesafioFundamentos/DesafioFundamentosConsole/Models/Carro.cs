using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioFundamentosConsole.Models
{
    public class Carro
    {
        private string placa;
        private char tipoCarro;
        public string Placa { get=>placa; }
        public char TipoCarro { get=>tipoCarro;}
        public Carro(string placa, char tipoCarro) {
            this.placa = placa;
            this.tipoCarro = tipoCarro;
        }

        public void toString() {

          Console.WriteLine("Placa: "+this.placa + "\nTipo Carro: "+this.tipoCarro);
        }
    }
}