namespace HanoiEntity
{
    public class HistoricoDTO
    {
        public string NomeJogo { get; set; }
        public int QuantidadeDeDiscos { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }

        public static implicit operator HistoricoDTO(HistoricoLog log)
        {
            if (log == null)
                return null;

            return new HistoricoDTO
            {
                NomeJogo = log.Game.Nome,
                QuantidadeDeDiscos = log.Game.QuantidadeDeDiscos,
                DataInicio = log.DataHoraChamada.ToString("dd/MM/yyyy HH:mm:ss"),
                DataFim = log.DataHoraFinalizacao.ToString("dd/MM/yyyy HH:mm:ss")
            };
        }

        public override string ToString()
        {
            return $"{NomeJogo} | Quantidade de discos: {QuantidadeDeDiscos} | Data Inicio: {DataInicio} | Data Final: {DataFim}";
        }
    }
}
