using alset_aloc.Database;
using alset_aloc.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace alset_aloc.Models
{
  class ClienteDAO : IDAO<Cliente>
    {
        private Conexao conn;

        public ClienteDAO()
        {
            conn = new Conexao();
        }

        static Cliente ParseReader(MySqlDataReader dtReader)
        {
            Cliente cliente = new Cliente();

            cliente.Id = dtReader.GetInt64("id_cli");

            cliente.Nome = dtReader.GetString("nome_cli");
            cliente.DataNascimento = dtReader.GetDateTime("data_nascimento_cli");
            cliente.CPF = dtReader.GetString("cpf_cli");
            cliente.RG = dtReader.GetString("rg_cli");
            cliente.CNH = dtReader.GetString("cnh_cli");
            cliente.Email = dtReader.GetString("email_cli");
            cliente.Telefone = dtReader.GetString("telefone_cli");
            cliente.Genero = dtReader.GetString("genero_cli");

            var rawEnderecoId = dtReader.GetOrdinal("id_end_fk");

            if (!dtReader.IsDBNull(rawEnderecoId))
            {
                cliente.EnderecoId = dtReader.GetInt64(rawEnderecoId);
            }
            else
            {
                cliente.EnderecoId = null;
            }

            return cliente;
        }

        static void BindQuery(Cliente t, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@nome", t.Nome);
            query.Parameters.AddWithValue("@dataNascimento", t.DataNascimento);
            query.Parameters.AddWithValue("@cpf", t.CPF);
            query.Parameters.AddWithValue("@rg", t.RG);
            query.Parameters.AddWithValue("@cnh", t.CNH);
            query.Parameters.AddWithValue("@email", t.Email);
            query.Parameters.AddWithValue("@telefone", t.Telefone);
            query.Parameters.AddWithValue("@genero", t.Genero);

            query.Parameters.AddWithValue("@enderecoId", t.EnderecoId);
        }

        static void BindQueryId(long id, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@idCli", id);
        }

        public void Delete(Cliente t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    DELETE FROM cliente
                    WHERE (id_cli = @idCli)
                ";

                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O cliente não foi encontrado. Verifique e tente novamente.");
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

        public Cliente GetById(int id)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT id_cli, nome_cli, data_nascimento_cli, cpf_cli, rg_cli, cnh_cli, email_cli, telefone_cli, genero_cli, id_end_fk
                    FROM cliente
                    WHERE (id_cli = @idCli)
                    ;
                ";

                BindQueryId(id, query);

                MySqlDataReader dtReader = query.ExecuteReader();


                while (dtReader.Read())
                {
                    Cliente cliente = ParseReader(dtReader);
                    return cliente;
                }

                throw new Exception("Não foi possível encontrar o endereço com o id fornecido. Verifique e tente novamente.");
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

        public void Insert(Cliente t)
        {
            try
            {
                var query = conn.Query();
                
                query.CommandText = @"
                    INSERT INTO 
                        cliente (nome_cli, data_nascimento_cli, cpf_cli, rg_cli, cnh_cli, email_cli, telefone_cli, genero_cli, id_end_fk)
                    VALUES
                        (@nome, @dataNascimento, @cpf, @rg, @cnh, @email, @telefone, @genero, @enderecoId);
                ";

                BindQuery(t, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O cliente não foi cadastrado. Verifique e tente novamente.");
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

        public List<Cliente> List()
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT id_cli, nome_cli, data_nascimento_cli, cpf_cli, rg_cli, cnh_cli, email_cli, telefone_cli, genero_cli, id_end_fk
                    FROM cliente
                    ;
                ";


                MySqlDataReader dtReader = query.ExecuteReader();

                List<Cliente> listaDeRetorno = new List<Cliente>();

                while (dtReader.Read())
                {
                    Cliente cliente = ParseReader(dtReader);
                    listaDeRetorno.Add(cliente);
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

        public void Update(Cliente t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    UPDATE cliente
                    SET
                        nome_cli = @nome,
                        data_nascimento_cli = @dataNascimento,
                        cpf_cli = @cpf,
                        rg_cli = @rg,
                        cnh_cli = @cnh,
                        email_cli = @email,
                        telefone_cli = @telefone,
                        genero_cli = @genero,
                        id_end_fk = @enderecoId
                    WHERE (id_cli = @idCli);
                ";

                BindQuery(t, query);
                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O cliente não foi alterado. Verifique e tente novamente.");
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
