using System.Collections.Generic;

namespace HanoiEntity
{
    public class Torre : BaseEntity
    {
        public Torre()
        {
            Discos = new List<Disco>();
        }

        public string Nome { get; set; }
        public List<Disco> Discos { get; set; }
    }
}
