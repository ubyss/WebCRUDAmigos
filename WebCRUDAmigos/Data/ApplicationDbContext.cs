using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Web.Http.Description;
using System.Web.Http;
using WebCRUDAmigos.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebCRUDAmigos.Data
{
    public class ApplicationDbContext: DbContext
    {
        private readonly string _connectionString = "Server=tcp:crudamigos.database.windows.net,1433;Initial Catalog=crudamigos;Persist Security Info=False;User ID=thiagoadmin;Password=Aulainfnet123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        public DbSet<Amigo> Amigos { get; set; }

        public string PostAmigo(Amigo amigo)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("CriarAmigo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Nome", amigo.Nome);
                        command.Parameters.AddWithValue("@Sobrenome", amigo.Sobrenome);
                        command.Parameters.AddWithValue("@Email", amigo.Email);
                        command.Parameters.AddWithValue("@Telefone", amigo.Telefone);
                        command.Parameters.AddWithValue("@DataAniversario", amigo.DataAniversario);
                        command.ExecuteNonQuery();
                        return "Amigo registrado com sucesso";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public IEnumerable<Amigo> GetAmigos()
        {
            var amigos = new List<Amigo>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("LerTodosAmigos", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                amigos.Add(new Amigo
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Nome = reader["Nome"].ToString(),
                                    Sobrenome = reader["Sobrenome"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Telefone = reader["Telefone"].ToString(),
                                    DataAniversario = Convert.ToDateTime(reader["DataAniversario"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return amigos;
        }

        public Amigo? PutAmigo(int id, Amigo amigo)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("AtualizarAmigo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Nome", amigo.Nome);
                        command.Parameters.AddWithValue("@Sobrenome", amigo.Sobrenome);
                        command.Parameters.AddWithValue("@Email", amigo.Email);
                        command.Parameters.AddWithValue("@Telefone", amigo.Telefone);
                        command.Parameters.AddWithValue("@DataAniversario", amigo.DataAniversario);
                        command.ExecuteNonQuery();
                        return amigo;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string DeletarAmigo(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DeletarAmigo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                        return "Amigo deletado com sucesso";
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
