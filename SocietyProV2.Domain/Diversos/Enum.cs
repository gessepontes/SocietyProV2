using System.ComponentModel.DataAnnotations;

namespace SocietyProV2.Domain.Diversos
{
    public enum Posicao : int
    {
        [Display(Name = "Goleiro")]
        Goleiro = 0,
        [Display(Name = "Zagueiro")]
        Zagueiro = 1,
        [Display(Name = "Volante")]
        Volante = 2,
        [Display(Name = "Meio-Campo")]
        MeioCampo = 3,
        [Display(Name = "Atacante")]
        Atacante = 4,
        [Display(Name = "Chupa-Sangue")]
        ChupaSangue = 5,
        [Display(Name = "Matador")]
        Matador = 6,
        [Display(Name = "Lateral")]
        Lateral = 7
    }

    public enum TipoTime : int
    {
        [Display(Name = "Society")]
        Society = 0,
        [Display(Name = "Campo de 11")]
        Campo = 1
    }

    public enum TipoHorario : int
    {
        [Display(Name = "Padrão")]
        Padrao = 1,
        [Display(Name = "Extra")]
        Extra = 2
    }

    public enum TipoAgendamento : int
    {
        [Display(Name = "Avulso")]
        Avulso = 1,
        [Display(Name = "Mensal")]
        Mensal = 2
    }

    public enum TipoCampeonato : int
    {
        Grupos = 1,
        [Display(Name = "Mata-Mata")]
        MataMata = 2,
        [Display(Name = "Pontos Corridos")]
        PontosCorridos = 3
    }


    public enum TipoArbitragem : int
    {
        [Display(Name = "Society")]
        Society = 1,
        [Display(Name = "Campo de 11")]
        Campo = 2
    }

    public enum DiaSemana : int
    {
        [Display(Name = "Segunda-feira")]
        Segunda = 1,
        [Display(Name = "Terça-feira")]
        Terça = 2,
        [Display(Name = "Quarta-feira")]
        Quarta = 3,
        [Display(Name = "Quinta-feira")]
        Quinta = 4,
        [Display(Name = "Sexta-feira")]
        Sexta = 5,
        [Display(Name = "Sabado")]
        Sabado = 6,
        [Display(Name = "Domingo")]
        Domingo = 0
    }

    public enum TipoCartao : int
    {
        [Display(Name = "Nenhum")]
        Nenhum = 0,
        [Display(Name = "Amarelo")]
        Amarelo = 1,
        [Display(Name = "Vermelho")]
        Vermelho = 2,
        [Display(Name = "Segundo amarelo seguido de vermelho")]
        SegundoAmareloVermelho = 3,
        [Display(Name = "Vermelho depois de um amarelo")]
        VermelhoAmarelo = 4
    }

    public enum IDGrupo : int
    {
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        E = 5,
        F = 6,
        G = 7,
        H = 8,
        I = 9,
        J = 10,
        L = 11,
        M = 12
    }

    public enum StatusP : int
    {
        [Display(Name = "Pendente")]
        Pendente = 0,
        [Display(Name = "Aprovado")]
        Aprovado = 1,
        [Display(Name = "Cancelado")]
        Cancelado = 2
    }

    public enum TipoPerfil : int
    {
        [Display(Name = "Administrador")]
        Administrador = 0,
        [Display(Name = "Usuário")]
        Usuario = 1,
        [Display(Name = "Administrador do campeonato")]
        AdministradorCampeonato = 2,
        [Display(Name = "Arbitro")]
        Arbitro = 3,
        [Display(Name = "Jogador")]
        Jogador = 4,
        [Display(Name = "Administrador do campo")]
        AdministradorCampo = 5
    }
}