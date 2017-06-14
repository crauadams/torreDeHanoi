using System;

namespace HanoiEntity
{
    public class HistoricoLog : BaseEntity
    {
        public Game Game { get; set; }
        public DateTime DataHoraChamada { get; set; }
        public DateTime DataHoraFinalizacao { get; set; }
    }
}
