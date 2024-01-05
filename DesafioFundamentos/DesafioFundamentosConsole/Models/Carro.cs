using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioFundamentosConsole.Models
{
    public class Carro
    {
        private string _placa;
        private char _tipoCarro;
        public string Placa { get=>_placa; }
        public char TipoCarro { get=>_tipoCarro;}
        public Carro(string _placa, char _tipoCarro) {
            this._placa = _placa;
            this._tipoCarro = _tipoCarro;
        }

        public override String ToString() {

           String tipo = this.TipoCarro.Equals('C') ? "comum" : "especial";
           String s = "Placa: "+this._placa + "\nTipo Carro: "+tipo;
           return s;
        }
    }
}