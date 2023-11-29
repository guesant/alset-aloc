using alset_aloc.Database;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Windows;
using alset_aloc.Helpers;

namespace alset_aloc.Models
{
  internal class FuncionarioDAO
    {

        private Conexao conn;

        public FuncionarioDAO()
        {
            conn = new Conexao();
        }

        public static Funcionario ParseReader(MySqlDataReader dtReader)
        {
            Funcionario funcionario = new Funcionario();

            funcionario.Id = dtReader.GetInt32("id_func");
            funcionario.Nome = dtReader.GetString("nome_func");
            funcionario.DataNascimento = dtReader.GetDateTime("data_nascimento_func");
            funcionario.Cpf = dtReader.GetString("cpf_func");
            funcionario.Rg = dtReader.GetString("rg_func");
            funcionario.Email = dtReader.GetString("email_func");
            funcionario.Telefone = dtReader.GetString("telefone_func");
            funcionario.Genero = dtReader.GetString("genero_func");

            funcionario.Cargo = dtReader.GetString("cargo_func");
            funcionario.CNH = dtReader.GetString("cnh_func");

            var enderecoIdRaw = dtReader.GetOrdinal("id_end_fk");

            if (!dtReader.IsDBNull(enderecoIdRaw))
            {
                funcionario.EnderecoID = dtReader.GetInt64(enderecoIdRaw);
            }
            else
            {
                funcionario.EnderecoID = null;
            }

            return funcionario;
        }

        public static void BindQuery(Funcionario t, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@nome", t.Nome);
            query.Parameters.AddWithValue("@dataNascimento", t.DataNascimento);
            query.Parameters.AddWithValue("@cpf", Mascaras.LimparCPF(t.Cpf));
            query.Parameters.AddWithValue("@rg", t.Rg);
            query.Parameters.AddWithValue("@email", t.Email);
            query.Parameters.AddWithValue("@telefone", t.Telefone);
            query.Parameters.AddWithValue("@genero", t.Genero);
            query.Parameters.AddWithValue("@cnh", t.CNH);
            query.Parameters.AddWithValue("@cargo", t.Cargo);

            query.Parameters.AddWithValue("@enderecoId", t.EnderecoID);
        }

        public static void BindQueryId(long id, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@idFunc", id);
        }

        public void Delete(Funcionario t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                  DELETE FROM usuario 
                  WHERE (id_func_fk = @idFunc);
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
            catch (Exception e)
            {
                throw e;
            }
            finally 
            {    
                conn.Close();
            }
        
        }

        public Funcionario GetById(int id)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT id_func, nome_func, data_nascimento_func, cpf_func, rg_func, email_func, telefone_func, genero_func, cnh_func, cargo_func, id_end_fk
                    FROM funcionario
                    WHERE (id_func = @idFunc);
                ";

                BindQueryId(id, query);

                MySqlDataReader dtReader = query.ExecuteReader();

                Funcionario funcionario;

                while (dtReader.Read())
                {
                    funcionario = ParseReader(dtReader);
                    return funcionario;
                }

                throw new Exception("Não foi possível encontrar o funcionário com o id fornecido. Verifique e tente novamente.");
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

        public void Insert(Funcionario t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    INSERT INTO 
                        funcionario 
                        (nome_func, data_nascimento_func, cpf_func, rg_func, email_func, telefone_func, genero_func, cnh_func, cargo_func, id_end_fk)
                    VALUES 
                        (@nome, @dataNascimento, @cpf, @rg, @email, @telefone, @genero, @cnh, @cargo, @enderecoId);
                ";

                BindQuery(t, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O funcionário não foi cadastrado. Verifique e tente novamente.");
                }

                t.Id = query.LastInsertedId;
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
                    SELECT id_func, nome_func, data_nascimento_func, cpf_func, rg_func, email_func, telefone_func, genero_func, cnh_func, cargo_func, id_end_fk
                    FROM funcionario;";


                MySqlDataReader dtReader = query.ExecuteReader();

                List<Funcionario> listaDeRetorno = new List<Funcionario>();

                while (dtReader.Read())
                {
                    Funcionario funcionario = ParseReader(dtReader);
                    listaDeRetorno.Add(funcionario);
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
            MessageBox.Show(t.ToString());
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    UPDATE funcionario
                    SET
                        nome_func = @nome,
                        data_nascimento_func = @dataNascimento,
                        cpf_func = @cpf,
                        rg_func = @rg,
                        email_func = @email,
                        telefone_func = @telefone,
                        genero_func = @genero,
                        cnh_func = @cnh,
                        cargo_func = @cargo,
                        id_end_fk = @enderecoId
                    WHERE (id_func = @idFunc);
                ";
                
                BindQuery(t, query);
                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O funcionário não foi alterado. Verifique e tente novamente.");
                }
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

    }
}

  
    
