using alset_aloc.Database;
using alset_aloc.Interfaces;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace alset_aloc.Models
{
    class ClienteDAO : IDAO<Cliente>
    {
        private Conexao conn;

        public ClienteDAO()
        {
            conn = new Conexao();
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

                query.Parameters.AddWithValue("@idCli", t.Id);

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
                    SELECT (id_cli, nome_cli, data_nascimento_cli, cpf_cli, rg_cli, cnh_cli, email_cli, telefone_cli, genero_cli, id_end_fk)
                    FROM cliente
                    WHERE (id_cli = @idCli)
                    ;
                ";

                query.Parameters.AddWithValue("@idCli", id);

                MySqlDataReader dtReader = query.ExecuteReader();

                Cliente cliente = new Cliente();

                while (dtReader.Read())
                {
                    cliente.Id = Convert.ToInt64(dtReader["id_cli"].ToString());

                    cliente.Nome = dtReader["nome_cli"].ToString()!;
                    cliente.DataNascimento = Convert.ToDateTime(dtReader["data_nascimento_cli"].ToString());
                    cliente.CPF = dtReader["cpf_cli"].ToString()!;
                    cliente.RG = dtReader["rg_cli"].ToString()!;
                    cliente.CNH = dtReader["cnh_cli"].ToString()!;
                    cliente.Email = dtReader["email_cli"].ToString()!;
                    cliente.Telefone = dtReader["telefone_cli"].ToString()!;
                    cliente.Genero = dtReader["genero_cli"].ToString()!;

                    var rawEnderecoId = dtReader["id_end_fk"].ToString();

                    if(rawEnderecoId != null && !String.IsNullOrEmpty(rawEnderecoId))
                    {
                        cliente.EnderecoId = Convert.ToInt64(rawEnderecoId);
                    } else
                    {
                        cliente.EnderecoId = null;
                    }

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
                        (@nome, @dataNascimento, @cpf, @rg, @cnh, @email, @telefone, @genero, @idEnd)
                ";

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@dataNascimento", t.DataNascimento);
                query.Parameters.AddWithValue("@cpf", t.CPF);
                query.Parameters.AddWithValue("@rg", t.RG);
                query.Parameters.AddWithValue("@cnh", t.CNH);
                query.Parameters.AddWithValue("@email", t.Email);
                query.Parameters.AddWithValue("@telefone", t.Telefone);
                query.Parameters.AddWithValue("@genero", t.Genero);

                if(enderecoId != null)
                {
                    query.Parameters.AddWithValue("@id_end_fk", enderecoId);
                }

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O cliente não foi cadastrado. Verifique e tente novamente.");
                }

                long clienteId = query.LastInsertedId;

                t.Id = clienteId;
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
                    SELECT (id_cli, nome_cli, data_nascimento_cli, cpf_cli, rg_cli, cnh_cli, email_cli, telefone_cli, genero_cli, id_end_fk)
                    FROM cliente
                    ;
                ";


                MySqlDataReader dtReader = query.ExecuteReader();

                List<Cliente> listaDeRetorno = new List<Cliente>();//Crie uma lista de Cliente

                while (dtReader.Read())
                {
                    Cliente cliente = new Cliente();

                    cliente.Id = Convert.ToInt64(dtReader["id_cli"].ToString());

                    cliente.Nome = dtReader["nome_cli"].ToString()!;
                    cliente.DataNascimento = Convert.ToDateTime(dtReader["data_nascimento_cli"].ToString());
                    cliente.CPF = dtReader["cpf_cli"].ToString()!;
                    cliente.RG = dtReader["rg_cli"].ToString()!;
                    cliente.CNH = dtReader["cnh_cli"].ToString()!;
                    cliente.Email = dtReader["email_cli"].ToString()!;
                    cliente.Telefone = dtReader["telefone_cli"].ToString()!;
                    cliente.Genero = dtReader["genero_cli"].ToString()!;

                    var rawEnderecoId = dtReader["id_end_fk"].ToString();

                    if (rawEnderecoId != null && !String.IsNullOrEmpty(rawEnderecoId))
                    {
                        cliente.EnderecoId = Convert.ToInt64(rawEnderecoId);
                    }
                    else
                    {
                        cliente.EnderecoId = null;
                    }

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
                        id_end_fk = @idEndFk
                    WHERE (id_cli = @idCli);
                ";

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@dataNascimento", t.DataNascimento);
                query.Parameters.AddWithValue("@cpf", t.CPF);
                query.Parameters.AddWithValue("@rg", t.RG);
                query.Parameters.AddWithValue("@cnh", t.CNH);
                query.Parameters.AddWithValue("@email", t.Email);
                query.Parameters.AddWithValue("@telefone", t.Telefone);
                query.Parameters.AddWithValue("@genero", t.Genero);

                query.Parameters.AddWithValue("@idEndFk", t.EnderecoId);

                query.Parameters.AddWithValue("@idCli", t.Id);

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
