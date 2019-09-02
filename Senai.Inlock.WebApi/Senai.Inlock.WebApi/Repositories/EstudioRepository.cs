using Senai.Inlock.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Inlock.WebApi.Repositories
{
    public class EstudioRepository
    {

        /// <summary>
        /// Método para listar os estúdios
        /// </summary>
        /// <returns>Lista de estúdios</returns>
        public List<Estudios> Listar()
        {
            using (InLockContext ctx = new InLockContext())
            {
                return ctx.Estudios.ToList();
            }

        }

        public void Cadastrar(Estudios estudio)
        {
            using (InLockContext ctx = new InLockContext())
            {
                ctx.Estudios.Add(estudio);
                ctx.SaveChanges();
            }
        }

        public void Atualizar(Estudios estudio)
        {
            using (InLockContext ctx = new InLockContext())
            {
                Estudios EstudioBuscado = ctx.Estudios.FirstOrDefault(x => x.EstudioId == estudio.EstudioId);
                EstudioBuscado.NomeEstudio = estudio.NomeEstudio;
                ctx.Estudios.Update(EstudioBuscado);
                ctx.SaveChanges();
            }
        }

        public void Deletar(Estudios estudio)
        {
            using (InLockContext ctx = new InLockContext())
            {
                Estudios EstudioBuscado = ctx.Estudios.Find(estudio.EstudioId);
                ctx.Estudios.Remove(EstudioBuscado);
                ctx.SaveChanges();
            }
        }
    }
}
