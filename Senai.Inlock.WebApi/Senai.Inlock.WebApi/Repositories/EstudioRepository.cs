using Microsoft.EntityFrameworkCore;
using Senai.Inlock.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Inlock.WebApi.Repositories
{

    public class EstudioRepository
    {
        private string StringConexao = "Data Source=.\\SqlExpress; initial catalog=M_InLock; User Id=sa;Pwd=132";

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

        public List<Estudios> BuscarTodos()
        {
            using (InLockContext ctx = new InLockContext())
            {
                return ctx.Estudios.Include(x => x.Jogos).ToList();
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

        public Estudios BuscarPorId(int id)
        {
            using (InLockContext ctx = new InLockContext())
            {
                return ctx.Estudios.FirstOrDefault(x => x.EstudioId == id);
            }
        }

        public List<Estudios> BuscarPorUsuario(int id)
        {
            List<Estudios> Estudios = new List<Estudios>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "SELECT E.EstudioId , E.NomeEstudio , E.PaisOrigem , E.DataCriacao , E.UsuarioId FROM Estudios E WHERE E.UsuarioId = @Id";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {

                    cmd.Parameters.AddWithValue("@Id", id);

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        Estudios Estudio = new Estudios
                        {
                            EstudioId = Convert.ToInt32(rdr["EstudioId"]),
                            NomeEstudio = rdr["NomeEstudio"].ToString(),
                            PaisOrigem = rdr["PaisOrigem"].ToString(),
                            DataCriacao = Convert.ToDateTime(rdr["DataCriacao"]),
                            UsuarioId = Convert.ToInt32(rdr["UsuarioId"])

                        };
                        Estudios.Add(Estudio);
                    };
                }
            }
            return Estudios;
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

        public void Deletar(int id)
        {
            using (InLockContext ctx = new InLockContext())
            {
                Estudios EstudioBuscado = ctx.Estudios.Find(id);
                ctx.Estudios.Remove(EstudioBuscado);
                ctx.SaveChanges();
            }
        }

        public List<Estudios> BuscarPorPais(string pais)
        {
            List<Estudios> Estudios = new List<Estudios>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "SELECT E.EstudioId , E.NomeEstudio , E.PaisOrigem , E.DataCriacao , E.UsuarioId FROM Estudios E WHERE E.PaisOrigem = @Pais";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {

                    cmd.Parameters.AddWithValue("@Pais", pais);

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        Estudios Estudio = new Estudios
                            {
                                EstudioId = Convert.ToInt32(rdr["EstudioId"]),
                                NomeEstudio = rdr["NomeEstudio"].ToString(),
                                PaisOrigem = rdr["PaisOrigem"].ToString(),
                                DataCriacao = Convert.ToDateTime(rdr["DataCriacao"]),
                                UsuarioId = Convert.ToInt32(rdr["UsuarioId"])

                        };
                        Estudios.Add(Estudio);
                    };
                }
            }
            return Estudios;
        }

        public Estudios BuscarPorNome(string nome)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "SELECT E.EstudioId , E.NomeEstudio , E.PaisOrigem , E.DataCriacao , E.UsuarioId FROM Estudios E WHERE E.NomeEstudio = @Nome";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {

                    cmd.Parameters.AddWithValue("@Nome", nome);

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        Estudios Estudio = new Estudios
                        {
                            EstudioId = Convert.ToInt32(rdr["EstudioId"]),
                            NomeEstudio = rdr["NomeEstudio"].ToString(),
                            PaisOrigem = rdr["PaisOrigem"].ToString(),
                            DataCriacao = Convert.ToDateTime(rdr["DataCriacao"]),
                            UsuarioId = Convert.ToInt32(rdr["UsuarioId"])

                        };
                        return Estudio;
                    };
                }
            }
            return null;
        }

        public List<Jogos> JogosPorNomeEstudio(string nome)
        {
            EstudioRepository estudioRepository = new EstudioRepository();
            JogoRepository jogoRepository = new JogoRepository();

            Estudios EstudioBuscado = estudioRepository.BuscarPorNome(nome);

            List<Jogos> JogosBuscados = jogoRepository.BuscarPorEstudio(EstudioBuscado.EstudioId);

            return JogosBuscados;
        }

        public List<Estudios> BuscarTodosPorPais(string pais)
        {
            using (InLockContext ctx = new InLockContext())
            {
                return ctx.Estudios.Include(x => x.Jogos).Where(x => x.PaisOrigem == pais).ToList();
            }

        }

        public List<Estudios> BuscarRecentes()
        {
            DateTime data = DateTime.Now;
            data = data.AddDays(-10);

            List<Estudios> Estudios = new List<Estudios>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "SELECT E.EstudioId , E.NomeEstudio , E.PaisOrigem , E.DataCriacao , E.UsuarioId FROM Estudios E WHERE E.DataCriacao >= @Data";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {

                    cmd.Parameters.AddWithValue("@Data", data.ToShortDateString());

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        Estudios Estudio = new Estudios
                        {
                            EstudioId = Convert.ToInt32(rdr["EstudioId"]),
                            NomeEstudio = rdr["NomeEstudio"].ToString(),
                            PaisOrigem = rdr["PaisOrigem"].ToString(),
                            DataCriacao = Convert.ToDateTime(rdr["DataCriacao"]),
                            UsuarioId = Convert.ToInt32(rdr["UsuarioId"])

                        };
                        Estudios.Add(Estudio);
                    };
                }
            }
            return Estudios;
        }

    }
}
