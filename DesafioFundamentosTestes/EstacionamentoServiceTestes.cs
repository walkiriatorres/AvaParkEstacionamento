using DesafioFundamentos.Exceptions;
using DesafioFundamentos.Models;
using DesafioFundamentos.Services;
namespace DesafioFundamentosTestes;

public class EstacionamentoServiceTestes
{
    private Estacionamento _estacionamento;
    private EstacionamentoService _estacionamentoService;

    public EstacionamentoServiceTestes(){
        _estacionamento = new Estacionamento(10, 5, 2);
        _estacionamentoService = new EstacionamentoService(_estacionamento);
    }

    [Fact]
    public void DeveExibirOEstacionamento()
    {
        Estacionamento _estacionamento = new Estacionamento(10, 5, 5);
        EstacionamentoService _estacionamentoService = new EstacionamentoService(_estacionamento);
        
        Estacionamento resultado = _estacionamentoService.ExibirEstacionamento();
        var resultadoEsperado = new Estacionamento(10, 5, 5);
        
        Assert.Equal(resultadoEsperado, resultado);
    }

    [Fact]
    public void DeveExibir1QuandoForAdicionadoUmVeiculoComPlacaABC1234()
    {
        string placa = "ABC1234";
        Veiculo veiculo = new Veiculo(placa);
        _estacionamentoService.AdicionarVeiculo(veiculo);

        int resultadoEsperado = 1;
        var resultado = _estacionamento.GetVagasOcupadas().Count;

        Assert.Equal(resultadoEsperado, resultado);
    }

    [Fact]
    public void DeveExibirVeiculoNaoPodeSerNuloQuandoForAdicionadoUmVeiculoNulo()
    {
        string resultadoEsperado = "O veiculo não pode ser nulo.";        
        var exception = Assert.Throws<VeiculoInvalidoException>(() => _estacionamentoService.AdicionarVeiculo(null));        
        
        Assert.Equal(resultadoEsperado, exception.Message);
    }

    [Fact]
    public void DeveExibirPlacaNulaOuVaziaQuandoForAdicionadoUmVeiculoComPlacaVazia()
    {
        string placa = "";
        Veiculo veiculo = new Veiculo(placa);

        string resultadoEsperado = "Placa nula ou vazia não pode ser estacionada.";
        var exception = Assert.Throws<VeiculoInvalidoException>(() => _estacionamentoService.AdicionarVeiculo(veiculo));        

        Assert.Equal(resultadoEsperado, exception.Message);
    }

    [Fact]
    public void DeveExibirPlacaNulaOuVaziaQuandoForAdicionadoUmVeiculoComPlacaNula()
    {
        string placa = null;
        Veiculo veiculo = new Veiculo(placa);

        string resultadoEsperado = "Placa nula ou vazia não pode ser estacionada.";
        var exception = Assert.Throws<VeiculoInvalidoException>(() => _estacionamentoService.AdicionarVeiculo(veiculo)); 

        Assert.Equal(resultadoEsperado, exception.Message);
    }

    [Fact]
    public void DeveExibirPlacaInvalidaQuandoForAdicionadoUmVeiculoComPlacaABC()
    {
        string placa = "ABC";
        Veiculo veiculo = new Veiculo(placa);

        string resultadoEsperado = "Placa inválida.";
        var exception = Assert.Throws<VeiculoInvalidoException>(() => _estacionamentoService.AdicionarVeiculo(veiculo));        

        Assert.Equal(resultadoEsperado, exception.Message);
    }

    [Fact]
    public void DeveExibirEstacionamentoLotadoQuandoAdicionar3VeiculosNoEstacionamentoCom2Vagas()
    {
        string placa1 = "ABC1234";        
        Veiculo veiculo1 = new Veiculo(placa1);
        _estacionamentoService.AdicionarVeiculo(veiculo1);

        string placa2 = "bcd1234";        
        Veiculo veiculo2 = new Veiculo(placa2);
         _estacionamentoService.AdicionarVeiculo(veiculo2);

        string placa3 = "bcd1234";        
        Veiculo veiculo3 = new Veiculo(placa3);

        string resultadoEsperado = "Estacionamento está lotado.";

        var exception = Assert.Throws<VeiculoInvalidoException>(() => _estacionamentoService.AdicionarVeiculo(veiculo3));
        
        Assert.Equal(resultadoEsperado, exception.Message);
    }

    [Fact]
    public void DeveExibirVeiculoJaEstaEstacionadoQuandoAdicionarVeiculoPreviamenteAdicionado()
    {
        string placa1 = "ABC1234";        
        Veiculo veiculo1 = new Veiculo(placa1);
        _estacionamentoService.AdicionarVeiculo(veiculo1);

        string placa2 = "ABC1234";        
        Veiculo veiculo2 = new Veiculo(placa2);

        string resultadoEsperado = "Veiculo já está estacionado.";

        var exception = Assert.Throws<VeiculoInvalidoException>(() => _estacionamentoService.AdicionarVeiculo(veiculo2));
        
        Assert.Equal(resultadoEsperado, exception.Message);
    }

    [Fact]
    public void DeveExibir15SeOveiculoFicarAcimaDe60MinutosEstacionado()
    {
        string placa = "ABC1234";        
        Veiculo veiculo = new Veiculo(placa);
        _estacionamentoService.AdicionarVeiculo(veiculo);

        DateTime dataHoraSimuladaSaida = DateTime.Now.AddMinutes(60);
        veiculo.SetSaida(dataHoraSimuladaSaida);

        decimal resultadoEsperado = 15;        
        var resultado = _estacionamentoService.ConsultarValorPagamento(veiculo); 
        
        Assert.Equal(resultadoEsperado, resultado);
    }

    [Fact]
    public void DeveExibirVeiculoNaoPodeSerNuloQuandoConsultarValorPagamentoDeVeiculoNulo()
    {
        Veiculo veiculo = null;

        string resultadoEsperado = "O veiculo não pode ser nulo.";        
        var exception = Assert.Throws<VeiculoInvalidoException>(() => _estacionamentoService.ConsultarValorPagamento(veiculo)); 
        
        Assert.Equal(resultadoEsperado, exception.Message);
    }

    [Fact]
    public void DeveExibirPlacaNulaOuVaziaNaoPodeSerEstacionadaQuandoConsultarValorPagamentoDeVeiculoComPlacaVazia()
    {
        string placa = "";
        Veiculo veiculo = new Veiculo(placa);

        string resultadoEsperado = "Placa nula ou vazia não pode ser estacionada.";        
        var exception = Assert.Throws<PlacaInvalidaException>(() => _estacionamentoService.ConsultarValorPagamento(veiculo)); 
        
        Assert.Equal(resultadoEsperado, exception.Message);
    }

    [Fact]
    public void DeveExibirPlacaNulaOuVaziaNaoPodeSerEstacionadaQuandoConsultarValorPagamentoDeVeiculoComPlacaNula()
    {
        string placa = null;
        Veiculo veiculo = new Veiculo(placa);

        string resultadoEsperado = "Placa nula ou vazia não pode ser estacionada.";        
        var exception = Assert.Throws<PlacaInvalidaException>(() => _estacionamentoService.ConsultarValorPagamento(veiculo));    

        Assert.Equal(resultadoEsperado, exception.Message);
    }

    [Fact]
    public void DeveExibirPlacaInvalidaQuandoConsultarValorPagamentoDeVeiculoComPlacaInvalida()
    {
        string placa = "a1";
        Veiculo veiculo = new Veiculo(placa);

        string resultadoEsperado = "Placa inválida.";        
        var exception = Assert.Throws<PlacaInvalidaException>(() => _estacionamentoService.ConsultarValorPagamento(veiculo));    

        Assert.Equal(resultadoEsperado, exception.Message);
    }

    [Fact]
    public void DeveExibirVeiculoNaoEstaEstacionadoQuandoConsultarValorPagamentoDeVeiculoNaoEstacionado()
    {
        string placa = "abc1234";
        Veiculo veiculo = new Veiculo(placa);

        string resultadoEsperado = "Veiculo não está estacionado.";        
        var exception = Assert.Throws<VeiculoInvalidoException>(() => _estacionamentoService.ConsultarValorPagamento(veiculo));    

        Assert.Equal(resultadoEsperado, exception.Message);
    }

    /*
    // -- INICIO WIP -- //
    // FAZER TESTES PARA REMOVER VEICULO APÓS VALIDAR METODO //
    // Devo implementar o método PodeRemoverVeiculo para validar parametros do método RemoverVeiculo? //
    

    [Fact]
    public void NaoRemoveVeiculoNulo()
    {
        string placa = "abc1234";
        Veiculo veiculo = new Veiculo(placa);
        _estacionamentoService.AdicionarVeiculo(veiculo);

        _estacionamentoService.RemoverVeiculo(null, "1", 20m);

        int resultadoEsperado = 0;
        var resultado = _estacionamento.GetVagasOcupadas().Count;

        Assert.Equal(resultadoEsperado, resultado);
    }

    [Fact]
    public void DeveExibirVeiculoNaoPodeSerNuloQuandoUmVeiculoNuloForRemovidoDoEstacionamento()
    {
        var resultadoEsperado = "O veiculo não pode ser nulo.";
        var exception = Assert.Throws<VeiculoInvalidoException>(() => _estacionamentoService.RemoverVeiculo(null, "Cartao", 20m));    

        Assert.Equal(resultadoEsperado, exception.Message);        
    }


    // -- FIM WIP -- //
    */
    [Fact]
    public void DeveExibir0QuandoUmVeiculoForRemovidoDoEstacionamento()
    {
        string placa = "abc1234";
        Veiculo veiculo = new Veiculo(placa);
        _estacionamentoService.AdicionarVeiculo(veiculo);

        _estacionamentoService.RemoverVeiculo(veiculo, (FormaPagamento)1, 20m);

        int resultadoEsperado = 0;
        var resultado = _estacionamento.GetVagasOcupadas().Count;

        Assert.Equal(resultadoEsperado, resultado);
    }

    [Fact]
    public void DeveExibirOVeiculoPlacaABC1234QuandoConsultarSeUmVeiculoDePlacaABC1234EstaEstacionado()
    {
        string placa = "abc1234";
        Veiculo veiculo = new Veiculo(placa);
        _estacionamentoService.AdicionarVeiculo(veiculo);

        var resultadoEsperado = veiculo;
        var resultado = _estacionamentoService.ConsultarVeiculoEstacionado(veiculo);

        Assert.Equal(resultadoEsperado, resultado);
    }

    [Fact]
    public void DeveExibirVeiculoNaoPodeSerNuloQuandoConsultarSeUmVeiculoNuloEstaEstacionado()
    {
        Veiculo veiculo = null;

        var resultadoEsperado = "O veiculo não pode ser nulo.";
        var exception = Assert.Throws<VeiculoInvalidoException>(() => _estacionamentoService.ConsultarVeiculoEstacionado(veiculo));    

        Assert.Equal(resultadoEsperado, exception.Message);        
    }

    [Fact]
    public void DeveExibirEstacionamentoNaoPodeSerNuloQuandoConsultarSeUmVeiculoEstaEstacionadoNoEstacionamentoNull()
    {
        Estacionamento _estacionamento = null;
        EstacionamentoService _estacionamentoService = new EstacionamentoService(_estacionamento);

        string placa = "abc1234";
        Veiculo veiculo = new Veiculo(placa);

        var resultadoEsperado = "O estacionamento não pode ser nulo.";
        var exception = Assert.Throws<EstacionamentoInvalidoException>(() => _estacionamentoService.ConsultarVeiculoEstacionado(veiculo));    

        Assert.Equal(resultadoEsperado, exception.Message);        
    }

    [Fact]
    public void DeveExibirPlacaInvalidaQuandoConsultarSeUmVeiculoDePlacaInvalidaEstaEstacionado()
    {
        string placa = "a1";
        Veiculo veiculo = new Veiculo(placa);

        var resultadoEsperado = "Placa inválida.";
        var exception = Assert.Throws<PlacaInvalidaException>(() => _estacionamentoService.ConsultarVeiculoEstacionado(veiculo));    

        Assert.Equal(resultadoEsperado, exception.Message);        
    }

    [Fact]
    public void DeveExibirPlacaNulaOuVaziaQuandoConsultarSeUmVeiculoDePlacaVaziaEstaEstacionado()
    {
        string placa = "";
        Veiculo veiculo = new Veiculo(placa);

        var resultadoEsperado = "Placa nula ou vazia não pode ser estacionada.";
        var exception = Assert.Throws<PlacaInvalidaException>(() => _estacionamentoService.ConsultarVeiculoEstacionado(veiculo));    

        Assert.Equal(resultadoEsperado, exception.Message);        
    }

    [Fact]
    public void DeveExibirPlacaNulaOuVaziaQuandoConsultarSeUmVeiculoDePlacaNulaEstaEstacionado()
    {
        string placa = null;
        Veiculo veiculo = new Veiculo(placa);

        var resultadoEsperado = "Placa nula ou vazia não pode ser estacionada.";
        var exception = Assert.Throws<PlacaInvalidaException>(() => _estacionamentoService.ConsultarVeiculoEstacionado(veiculo));    

        Assert.Equal(resultadoEsperado, exception.Message);        
    }

    [Fact]
    public void DeveExibirVeiculoNaoEstaEstacionadoQuandoVeiculoNaoEstiverEstacionado()
    {
        string placa = "abc1234";
        Veiculo veiculo = new Veiculo(placa);

        var resultadoEsperado = "Veiculo não está estacionado.";
        var exception = Assert.Throws<VeiculoInvalidoException>(() => _estacionamentoService.ConsultarVeiculoEstacionado(veiculo));    

        Assert.Equal(resultadoEsperado, exception.Message);        
    }

    [Fact]
    public void DeveExibirUmaListaQueContemVeiculo1EVeiculo2QuandoEstesVeiculosForemEstacionados()
    {
        string placa1 = "abc1234";
        Veiculo veiculo1 = new Veiculo(placa1);
        string placa2 = "bcd2345";
        Veiculo veiculo2 = new Veiculo(placa2);

        List<Veiculo> VeiculosEstacionados = new List<Veiculo> {veiculo1, veiculo2};

        _estacionamentoService.AdicionarVeiculo(veiculo1);
        _estacionamentoService.AdicionarVeiculo(veiculo2);

        var resultado = _estacionamentoService.ListarTodos();

        Assert.Equal(VeiculosEstacionados, resultado);        
    }

    [Fact]
    public void DeveExibir20QuandoTempoEstacionadoFor2horas30minutos()
    {
        TimeSpan intervaloDeTempo = new TimeSpan(2, 30, 0);

        var resultadoEsperado = 20;
        var resultado = _estacionamentoService.CalcularValorPagamentoMinutos(intervaloDeTempo);

        Assert.Equal(resultadoEsperado, resultado);               
    }
}