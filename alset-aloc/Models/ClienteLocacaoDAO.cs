using alset_aloc.Database;
using alset_aloc.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alset_aloc.Models
{
    class ClienteLocacaoDAO : IDAO<ClienteLocacao>
    {
        private Conexao conn;

        public ClienteLocacaoDAO()
        {
            conn = new Conexao();
        }

        static ClienteLocacao ParseReader(MySqlDataReader dtReader)
        {
            var clienteLocacao = new ClienteLocacao();

            clienteLocacao.Id = dtReader.GetInt64("id_cli_loc");

            clienteLocacao.ClienteId = dtReader.GetInt64("id_cli_fk");
            clienteLocacao.LocacaoId = dtReader.GetInt64("id_loc_fk");

            return clienteLocacao;
        }

        static void BindQuery(ClienteLocacao t, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@clienteId", t.ClienteId);
            query.Parameters.AddWithValue("@locacaoId", t.LocacaoId);
        }

        public static void BindQueryId(long id, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@idCliLoc", id);
        }

        public void Delete(ClienteLocacao t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    DELETE FROM cliente_locacao
                    WHERE (id_cli_loc = @idCliLoc)
                ";

                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("A associação de cliente à locação não foi encontrada. Verifique e tente novamente.");
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

        public ClienteLocacao GetById(int id)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT (id_cli_loc, id_cli_fk, id_loc_fk)
                    FROM cliente_locacao
                    WHERE (id_cli_loc = @idCliLoc);
                ";

                BindQueryId(id, query);

                MySqlDataReader dtReader = query.ExecuteReader();


                while (dtReader.Read())
                {
                    var clienteLocacao = ParseReader(dtReader);
                    return clienteLocacao;
                }

                throw new Exception("Não foi possível encontrar a associação de cliente à locação com o id fornecido. Verifique e tente novamente.");
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

        public void Insert(ClienteLocacao t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    INSERT INTO 
                        cliente_locacao (id_cli_fk, id_loc_fk)
                    VALUES
                        (@clienteId, @locacaoId)
                ";

                BindQuery(t, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("A associação de cliente à locação não foi cadastrada. Verifique e tente novamente.");
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

        public List<ClienteLocacao> List()
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT (id_cli_loc, id_cli_fk, id_loc_fk)
                    FROM cliente_locacao
                    ;
                "
                ;

                MySqlDataReader dtReader = query.ExecuteReader();

                var listaDeRetorno = new List<ClienteLocacao>();

                while (dtReader.Read())
                {
                    var clienteLocacao = ParseReader(dtReader);
                    listaDeRetorno.Add(clienteLocacao);
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

        public void Update(ClienteLocacao t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    UPDATE cliente_locacao
                    SET
                        id_cli_fk = @clienteId,
                        id_loc_fk = @locacaoId
                    WHERE (id_cli_loc = @idCliLoc);
                ";

                BindQuery(t, query);
                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("A associação de cliente à locação não foi alterada. Verifique e tente novamente.");
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
