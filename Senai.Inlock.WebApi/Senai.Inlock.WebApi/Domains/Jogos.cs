using System;
using System.Collections.Generic;

namespace Senai.Inlock.WebApi.Domains
{
    public partial class Jogos
    {
        public int JogoId { get; set; }
        public string NomeJogo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataLancamento { get; set; }
        public double Valor { get; set; }
        public int EstudioId { get; set; }

        public Estudios Estudio { get; set; }
    }
}
