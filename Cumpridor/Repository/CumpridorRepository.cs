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
    public class CumpridorRepository : ICumpridorRepository
    {
        IConfiguration _configuration;

        public CumpridorRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("LocalConnection").Value;
            return connection;
        }

        public int Add(CumpridorEnt cumpridor)
        {
            var connectionString = this.GetConnection();
            int count = 0;
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "INSERT INTO Cumpridor(Nome, Cpf, Email, Senha) VALUES(@Nome, @Cpf, @Email, @Senha); SELECT CAST(SCOPE_IDENTITY() as INT); ";
                    count = con.Execute(query, cumpridor);
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

        public List<CumpridorEnt> GetCumpridores()
        {
            var connectionString = this.GetConnection();
            List<CumpridorEnt> cumpridores = new List<CumpridorEnt>();
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM Cumpridor";
                    cumpridores = con.Query<CumpridorEnt>(query).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return cumpridores;
            }
        }

        public CumpridorEnt LogOn(CumpridorEnt login)
        {
            var connectionString = this.GetConnection();
            CumpridorEnt cumpridor = new CumpridorEnt();
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM Cumpridor where Email = @Email and Senha = @Senha";
                    cumpridor = con.Query<CumpridorEnt>(query, new { login.Email, login.Senha }).First();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return cumpridor;
            }
        }

        public CumpridorEnt Get(int id)
        {
            throw new NotImplementedException();
        }

        public int Edit(CumpridorEnt cumpridor)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
