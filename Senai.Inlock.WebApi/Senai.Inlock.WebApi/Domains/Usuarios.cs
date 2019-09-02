using System;
using System.Collections.Generic;

namespace Senai.Inlock.WebApi.Domains
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            Estudios = new HashSet<Estudios>();
        }

        public int UsuarioId { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string PermissaoUsuario { get; set; }

        public ICollection<Estudios> Estudios { get; set; }
    }
}
