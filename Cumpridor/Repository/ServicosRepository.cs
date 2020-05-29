using Cumpridor.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cumpridor.Repository
{
    public class ServicosRepository : IServicosRepository
    {
        IConfiguration _configuration;

        public ServicosRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("LocalConnection").Value;
            return connection;
        }

        public int AddList(List<ServicosEnt> servicosFila)
        {
            var connectionString = this.GetConnection();
            int count = 0;
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    List<ServicosEnt> servicosExistentes = GetServicos();


                    TipoRepository tRep = new TipoRepository(_configuration);


                    foreach (var servico in servicosFila)
                    {
                        servico.TipoId = servico.Tipo.Id;

                        var existe = servicosExistentes.Where(s => s.Id == servico.Id).FirstOrDefault();
                        if (existe == null)
                        {

                            var tipo = tRep.GetTipos().Where(t => t.Id == servico.TipoId).FirstOrDefault();

                            if (tipo == null)
                            {
                                tRep.Add(servico.Tipo);
                            }

                            var query = "INSERT INTO Servicos(Id, Cliente, DataAgendamento, Observacao, Aceitar, Concluido, TipoId) VALUES(@Id, @Cliente, @DataAgendamento, @Observacao, @Aceitar, @Concluido, @TipoId);";
                            con.Query(query, servico);
                        }
                    }
                    count = 1;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return count;
            }
        }

        public List<ServicosEnt> GetServicos()
        {
            var connectionString = this.GetConnection();
            List<ServicosEnt> servicos = new List<ServicosEnt>();
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT S.*, T.* FROM Servicos S INNER JOIN Tipo T on T.Id = S.TipoId";
                    var resultado = con.Query<ServicosEnt, TipoEnt, ServicosEnt>(query, (s, t) => { s.Tipo = t; return s; }, splitOn: "TipoId");
                    servicos = resultado.ToList();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return servicos;
            }
        }

        public void AceitarServico(Guid Id)
        {
            var connectionString = this.GetConnection();
            
            using (var con = new SqlConnection(connectionString))
            {
                try 
                {
                    con.Open();
                    var query = "UPDATE Servicos SET Aceitar = 1 Where Id = '" + Id.ToString() + "'";
                    con.Query(query);
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void ConcluirServico(Guid Id)
        {
            var connectionString = this.GetConnection();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "UPDATE Servicos SET Concluido = 1 Where Id = '" + Id.ToString() + "'";
                    con.Query(query);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
