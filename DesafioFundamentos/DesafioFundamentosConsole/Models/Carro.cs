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

        public override String ToString() {

           String tipo = this.TipoCarro.Equals('C') ? "comum" : "especial";
           String s = "Placa: "+this.placa + "\nTipo Carro: "+tipo;
           return s;
        }
    }
}