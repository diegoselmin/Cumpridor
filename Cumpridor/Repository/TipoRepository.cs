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
    public class TipoRepository : ITipoRepository
    {
        IConfiguration _configuration;

        public TipoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("LocalConnection").Value;
            return connection;
        }
        public int Add(TipoEnt tipo)
        {
            var connectionString = this.GetConnection();
            int count = 0;
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "INSERT INTO Tipo(Id,Descricao, Valor) VALUES(@Id, @Descricao, @Valor); SELECT CAST(SCOPE_IDENTITY() as INT); ";
                    count = con.Execute(query, tipo);
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

        public List<TipoEnt> GetTipos()
        {
            var connectionString = this.GetConnection();
            List<TipoEnt> tipos = new List<TipoEnt>();
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM Tipo";
                    tipos = con.Query<TipoEnt>(query).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return tipos;
            }
        }
    }
}
