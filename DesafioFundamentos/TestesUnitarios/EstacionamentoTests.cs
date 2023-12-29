namespace TestesUnitarios;
using DesafioFundamentosConsole.Models;
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
}