using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Inlock.WebApi.ViewModels
{
    public class LancamentoViewModel
    {
        public int JogoId { get; set; }
        public string NomeJogo { get; set; }
        public DateTime TempoRestante { get; set; }      
    }
}
