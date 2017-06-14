using System;
using System.Collections.Generic;

namespace HanoiEntity
{
    public class Game : BaseEntity
    {
        public string Nome { get; set; }
        public int QuantidadeDeDiscos { get; set; }
        public int QuantidadeDeMovimentosNecessarios => ((int)Math.Pow(2.0, QuantidadeDeDiscos)) - 1;
        public int QuantidadeDeMovimentosExecutados { get; set; }
        public List<Torre> Torres { get; set; }

        public Game(string nome, int quantidadeDeDiscos)
        {
            Nome = nome;
            QuantidadeDeDiscos = quantidadeDeDiscos;

            var primeiraTorre = new Torre { Id = 1, Nome = "Torra A" };

            for (int i = 0; i < quantidadeDeDiscos; i++)
            {
                primeiraTorre.Discos.Add(new Disco
                {
                    Id = i + 1
                });
            }

            Torres = new List<Torre>
            {
                primeiraTorre,
                new Torre { Id = 2, Nome = "Torre B"},
                new Torre { Id = 3, Nome = "Torre C"}
            };
        }

        public bool IsFinalDoJogo()
        {
            return QuantidadeDeMovimentosExecutados == QuantidadeDeMovimentosNecessarios;
        }
    }
}
