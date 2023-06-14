using alset_aloc.Database;
using System;
using MySql.Data.MySqlClient;
using alset_aloc.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alset_aloc.Models
{
    internal class FuncionarioDAO
    {

        private Conexao conn;

        public FuncionarioDAO()
        {
            conn = new Conexao();
        }

        public void Delete(Funcionario t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                  DELETE FROM funcionario
                  WHERE (id_func = @idFunc);
                ";

                query.Parameters.AddWithValue("@idFunc", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O funcionário não foi encontrado. Verifique as informações.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { conn.Close(); }
        
        }

        public Funcionario GetById(int id)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT (id_func, nome_func, data_nascimento_func, cpf_func, rg_func, email_func, telefone_func, genero_func)
                    FROM funcionario
                    WHERE (id_func = @idFunc);
                ";

                query.Parameters.AddWithValue("@idFunc", id);

                MySqlDataReader reader = query.ExecuteReader();

                var func = new Funcionario();

                while (reader.Read())
                {
                    func.Id = Convert.ToInt32(reader["id_func"].ToString());
                    func.Nome = reader["nome_func"].ToString()!;
                    func.DataNascimento = Convert.ToDateTime(reader["data_nascimento_func"].ToString());
                    func.Cpf = reader["cpf_func"].ToString()!;
                    func.Rg = reader["rg_func"].ToString()!;
                    func.Email = reader["email_func"].ToString()!;
                    func.Telefone = reader["telefone_func"].ToString()!;
                    func.Genero = reader["genero_func"].ToString()!;

                    var idEndCrua = reader["id_end_fk"].ToString();

                    if (idEndCrua != null && !string.IsNullOrEmpty(idEndCrua))
                    {
                        func.EnderecoID = Convert.ToInt32(idEndCrua);
                    }
                    else
                    {
                        func.EnderecoID = null;
                    }

                }

                return func;
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível encontrar o endereço com o id fornecido. Verifique e tente novamente.");
            }
            finally
            {
                conn.Close();
            }
        }

        public void Insert(Funcionario t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                 
                    INSERT INTO 
                    funcionario 
                        (nome_func, data_nascimento_func, cpf_func, rg_func, email_func, telefone_func, genero_func)
                    VALUES 
                        (@nome, @data_nascimento, @cpf, @rg, @email, @telefone, @genero);
                    
                ";

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@data_nascimento", t.DataNascimento);
                query.Parameters.AddWithValue("@cpf", t.Cpf);
                query.Parameters.AddWithValue("@rg", t.Rg);
                query.Parameters.AddWithValue("@email", t.Email);
                query.Parameters.AddWithValue("@telefone", t.Telefone);
                query.Parameters.AddWithValue("@genero", t.Genero);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O funcionário não foi cadastrado. Verifique e tente novamente.");
                }

                long enderecoId = query.LastInsertedId;

                t.Id = enderecoId;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
            
        }

        public List<Funcionario> List()
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                         SELECT (id_func, nome_func, data_nascimento_func, cpf_func, rg_func, email_func, telefone_func, genero_func)
                    FROM funcionario
                    WHERE (id_func = @idFunc);
                ";


                MySqlDataReader reader = query.ExecuteReader();

                List<Funcionario> listaDeRetorno = new List<Funcionario>();

                while (reader.Read())
                {
                    Funcionario func = new Funcionario();

                    func.Id = Convert.ToInt32(reader["id_func"].ToString());
                    func.Nome = reader["nome_func"].ToString()!;
                    func.DataNascimento = Convert.ToDateTime(reader["data_nascimento_func"].ToString());
                    func.Cpf = reader["cpf_func"].ToString()!;
                    func.Rg = reader["rg_func"].ToString()!;
                    func.Email = reader["email_func"].ToString()!;
                    func.Telefone = reader["telefone_func"].ToString()!;
                    func.Genero = reader["genero_func"].ToString()!;

                    var rawEnderecoId = reader["id_end_fk"].ToString();

                    if (rawEnderecoId != null && !String.IsNullOrEmpty(rawEnderecoId))
                    {
                        func.EnderecoID = Convert.ToInt64(rawEnderecoId);
                    }
                    else
                    {
                        func.EnderecoID = null;
                    }

                    listaDeRetorno.Add(func);
                }

                return listaDeRetorno;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Update(Funcionario t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"UPDATE funcionario SET
                    nome_func = @nome,
                    data_nascimento_func = @dataNascimento,
                    cpf_func = @cpf,
                    rg_func = @rg,
                    email_func = @email,
                    telefone_func = @telefone,
                    genero_func = @genero,
                    id_end_fk = @idEndFK
                   WHERE (id_func = @idFunc);
                ";
                
                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@data_nascimento", t.DataNascimento);
                query.Parameters.AddWithValue("@cpf", t.Cpf);
                query.Parameters.AddWithValue("@rg", t.Rg);
                query.Parameters.AddWithValue("@email", t.Email);
                query.Parameters.AddWithValue("@telefone", t.Telefone);
                query.Parameters.AddWithValue("@genero", t.Genero);

                query.Parameters.AddWithValue("@idEndFk", t.EnderecoID);
                query.Parameters.AddWithValue("@idFunc", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O funcionário não foi alterado. Verifique e tente novamente.");
                }
            }
            catch 
            { 
            
            }
            finally
            {
                conn.Close();
            }
        }

    }
}

  
    
