using Senai.Inlock.WebApi.Domains;
using Senai.Inlock.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Inlock.WebApi.Repositories
{
    public class JogoRepository
    {
        private string StringConexao = "Data Source=.\\SqlExpress; initial catalog=M_InLock; User Id=sa;Pwd=132";

        /// <summary>
        /// Método para listar os estúdios
        /// </summary>
        /// <returns>Lista de estúdios</returns>
        public List<Jogos> Listar()
        {
            using (InLockContext ctx = new InLockContext())
            {
                return ctx.Jogos.ToList();
            }

        }

        public List<Jogos> ListarPorPreco()
        {
            List<Jogos> Jogos = new List<Jogos>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "SELECT J.JogoId , J.NomeJogo , J.Descricao , J.DataLancamento , J.Valor FROM Jogos J ORDER BY J.Valor DESC";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Jogos jogo = new Jogos
                        {
                            JogoId = Convert.ToInt32(rdr["JogoId"]),
                            NomeJogo = rdr["NomeJogo"].ToString(),
                            Descricao = rdr["Descricao"].ToString(),
                            DataLancamento = Convert.ToDateTime(rdr["DataLancamento"]),
                            Valor = Convert.ToDouble(rdr["Valor"])
                        };
                        Jogos.Add(jogo);
                    };
                }
            }
            return Jogos.Take(5).ToList();
            

        }

        public List<Jogos> ListarPorData()
        {
            List<Jogos> Jogos = new List<Jogos>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "SELECT J.JogoId , J.NomeJogo , J.Descricao , J.DataLancamento , J.Valor FROM Jogos J ORDER BY J.DataLancamento DESC";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Jogos jogo = new Jogos
                        {
                            JogoId = Convert.ToInt32(rdr["JogoId"]),
                            NomeJogo = rdr["NomeJogo"].ToString(),
                            Descricao = rdr["Descricao"].ToString(),
                            DataLancamento = Convert.ToDateTime(rdr["DataLancamento"]),
                            Valor = Convert.ToDouble(rdr["Valor"])
                        };
                        Jogos.Add(jogo);
                    };
                }
            }
            return Jogos;


        }

        public void Cadastrar(Jogos jogo)
        {
            using (InLockContext ctx = new InLockContext())
            {
                ctx.Jogos.Add(jogo);
                ctx.SaveChanges();
            }
        }

        public Jogos BuscarPorId(int id)
        {
            using (InLockContext ctx = new InLockContext())
            {
                return ctx.Jogos.FirstOrDefault(x => x.JogoId == id);
            }
        }

        public Jogos BuscarPorNome(string nome)
        {
            using (InLockContext ctx = new InLockContext())
            {
                return ctx.Jogos.FirstOrDefault(x => x.NomeJogo == nome);
            }
        }

        public void Atualizar(Jogos jogo)
        {
            using (InLockContext ctx = new InLockContext())
            {
                Jogos JogoBuscado = ctx.Jogos.FirstOrDefault(x => x.JogoId == jogo.JogoId);
                JogoBuscado.NomeJogo = jogo.NomeJogo;
                ctx.Jogos.Update(JogoBuscado);
                ctx.SaveChanges();
            }
        }

        public void Deletar(int id)
        {
            using (InLockContext ctx = new InLockContext())
            {
                Jogos JogoBuscado = ctx.Jogos.Find(id);
                ctx.Jogos.Remove(JogoBuscado);
                ctx.SaveChanges();
            }
        }

        public List<Jogos> JogoEstudio()
        {
            List<Jogos> Jogos = new List<Jogos>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "SELECT J.JogoId , J.NomeJogo , J.Descricao , J.DataLancamento , J.Valor , E.EstudioId , E.NomeEstudio , E.PaisOrigem , E.DataCriacao , E.UsuarioId FROM Jogos J INNER JOIN Estudios E ON J.EstudioId = E.EstudioId";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Jogos jogo = new Jogos
                        {
                            JogoId = Convert.ToInt32(rdr["JogoId"]),
                            NomeJogo = rdr["NomeJogo"].ToString(),
                            Descricao = rdr["Descricao"].ToString(),
                            DataLancamento = Convert.ToDateTime(rdr["DataLancamento"]),
                            Valor = Convert.ToDouble(rdr["Valor"]),
                            Estudio = new Estudios
                            {
                                EstudioId = Convert.ToInt32(rdr["EstudioId"]),
                                NomeEstudio = rdr["NomeEstudio"].ToString(),
                                PaisOrigem = rdr["PaisOrigem"].ToString(),
                                DataCriacao = Convert.ToDateTime(rdr["DataCriacao"]),
                                UsuarioId = Convert.ToInt32(rdr["UsuarioId"])
                            },

                        };
                        Jogos.Add(jogo);
                    };
                }
            }
            return Jogos;
        }

        public List<Jogos> BuscarPorEstudio(int id)
        {
            List<Jogos> Jogos = new List<Jogos>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "SELECT J.JogoId , J.NomeJogo , J.Descricao , J.DataLancamento , J.Valor , E.EstudioId , E.NomeEstudio , E.PaisOrigem , E.DataCriacao , E.UsuarioId FROM Jogos J INNER JOIN Estudios E ON J.EstudioId = E.EstudioId WHERE E.EstudioId = @id";


                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Jogos jogo = new Jogos
                        {
                            JogoId = Convert.ToInt32(rdr["JogoId"]),
                            NomeJogo = rdr["NomeJogo"].ToString(),
                            Descricao = rdr["Descricao"].ToString(),
                            DataLancamento = Convert.ToDateTime(rdr["DataLancamento"]),
                            Valor = Convert.ToDouble(rdr["Valor"]),
                            Estudio = new Estudios
                            {
                                EstudioId = Convert.ToInt32(rdr["EstudioId"]),
                                NomeEstudio = rdr["NomeEstudio"].ToString(),
                                PaisOrigem = rdr["PaisOrigem"].ToString(),
                                DataCriacao = Convert.ToDateTime(rdr["DataCriacao"]),
                                UsuarioId = Convert.ToInt32(rdr["UsuarioId"])
                            },

                        };
                        Jogos.Add(jogo);
                    };
                }
            }
            return Jogos;
        }

        public List<LancamentoViewModel> TempoParaLancamento()
        {
            List<LancamentoViewModel> Lancamentos = new List<LancamentoViewModel>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "SELECT J.JogoId , J.NomeJogo , J.DataLancamento FROM Jogos J";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        LancamentoViewModel lancamento = new LancamentoViewModel
                        {
                            JogoId = Convert.ToInt32(rdr["JogoId"]),
                            NomeJogo = rdr["NomeJogo"].ToString(),
                            TempoRestante = DateTime.Parse(rdr["DataLancamento"].ToString())
                        };
                        Lancamentos.Add(lancamento);
                    };
                }
            }

            foreach(var items in Lancamentos)
            {

            }











            return Lancamentos;
        }
    }
}
