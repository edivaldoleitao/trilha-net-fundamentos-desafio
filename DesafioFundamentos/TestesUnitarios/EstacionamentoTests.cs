namespace TestesUnitarios;
using DesafioFundamentosConsole.Models;
using System;
public class EstacionamentoTests
{
    private Estacionamento es = new Estacionamento();

    [Theory]
    [InlineData("ASD")]
    [InlineData("")]
    [InlineData("ASD-12344")]
    [InlineData("S")]
    public void TestaPlacasComValorForaDoPadraoERetornaFalse(string placa)
    {
        bool result;
        result = es.ValidarPadraoPlaca(placa);
        Assert.False(result);
    }

    [Theory]
    [InlineData("BRA2E19")]
    [InlineData("BNM-1212")]
    [InlineData("ASD-1234")]
    [InlineData("BRA7S19")]
    public void TestaPlacasComValorDentroDoPadraoERetornaTrue(string placa)
    {
        bool result;
        result = es.ValidarPadraoPlaca(placa);
        Assert.True(result);
    }

    [Fact]
    public void CalcularPrecoDe1HoraComRetornoDeIgualA2()
    {
        DateTime horarioInicial = new DateTime(2023,01,01,11,00,00);
        DateTime horarioFinal = new DateTime(2023,01,01,12,00,00);
        decimal precoInicial = 1m;
        decimal precoPorHora = 1m;

        decimal result = es.CalcularTicketEstacionamento(horarioInicial, horarioFinal, precoInicial, precoPorHora);

        Assert.Equal(2m,result);
    }

}