namespace SocietyProV2.Domain.Entities
{
    public class JogadorPartida
    {
        public JogadorPartida()
        {
        }

        public int ID { get; set; }
        public int IDJOGADOR { get; set; }
        public int IDPARTIDA { get; set; }

        public virtual Jogador Jogador { get; set; }
        public virtual Partida Partida { get; set; }
    }
}
