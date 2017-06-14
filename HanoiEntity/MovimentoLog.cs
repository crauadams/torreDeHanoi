using System;

namespace HanoiEntity
{
    public class MovimentoLog : BaseEntity
    {
        public Movimento Movimento { get; set; }
        public DateTime Data { get; set; }
    }
}
