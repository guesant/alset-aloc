using alset_aloc.Database;
using alset_aloc.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace alset_aloc.Models
{
  class EnderecoDAO : IDAO<Endereco>
    {
        private Conexao conn;

        public EnderecoDAO()
        {
            conn = new Conexao();
        }

        static Endereco ParseReader(MySqlDataReader dtReader)
        {
            Endereco endereco = new Endereco();

            endereco.Id = dtReader.GetInt64("id_end");

            endereco.Pais = dtReader.GetString("pais_end");
            endereco.CodigoPostal = dtReader.GetString("codigo_postal_end");
            endereco.UF = dtReader.GetString("uf_end");
            endereco.Cidade = dtReader.GetString("cidade_end");
            endereco.Rua = dtReader.GetString("rua_end");
            endereco.Numero = dtReader.GetInt32("numero_end");
            endereco.Bairro = dtReader.GetString("bairro_end");
            endereco.Complemento = dtReader.GetString("complemento_end");

            return endereco;
        }

        static void BindQuery(Endereco t, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@pais", t.Pais);
            query.Parameters.AddWithValue("@codigoPostal", t.CodigoPostal);
            query.Parameters.AddWithValue("@uf", t.UF);
            query.Parameters.AddWithValue("@cidade", t.Cidade);
            query.Parameters.AddWithValue("@rua", t.Rua);
            query.Parameters.AddWithValue("@numero", t.Numero);
            query.Parameters.AddWithValue("@bairro", t.Bairro);
            query.Parameters.AddWithValue("@complemento", t.Complemento);
        }

        public static void BindQueryId(long id, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@idEnd", id);
        }

        public void Delete(Endereco t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    DELETE FROM endereco
                    WHERE (id_end = @idEnd)
                ";

                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O endereço não foi encontrado. Verifique e tente novamente.");
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

        public Endereco GetById(int id)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT id_end, pais_end, codigo_postal_end, uf_end, cidade_end, rua_end, numero_end, bairro_end, complemento_end
                    FROM endereco
                    WHERE (id_end = @idEnd);
                ";

                BindQueryId(id, query);

                MySqlDataReader dtReader = query.ExecuteReader();


                while (dtReader.Read())
                {
                    Endereco endereco = ParseReader(dtReader);
                    return endereco;
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

        public void Insert(Endereco t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    INSERT INTO 
                        endereco (pais_end, codigo_postal_end, uf_end, cidade_end, rua_end, numero_end, bairro_end, complemento_end)
                    VALUES
                        (@pais, @codigoPostal, @uf, @cidade, @rua, @numero, @bairro, @complemento)
                ";

                BindQuery(t, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O endereço não foi cadastrado. Verifique e tente novamente.");
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

        public List<Endereco> List()
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT id_end, pais_end, codigo_postal_end, uf_end, cidade_end, rua_end, numero_end, bairro_end, complemento_end
                    FROM endereco
                    ;
                "
                ;

                MySqlDataReader dtReader = query.ExecuteReader();

                List<Endereco> listaDeRetorno = new List<Endereco>();

                while (dtReader.Read())
                {
                    Endereco endereco = ParseReader(dtReader);
                    listaDeRetorno.Add(endereco);
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

        public void Update(Endereco t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    UPDATE endereco
                    SET
                        pais_end = @pais,
                        codigo_postal_end = @codigoPostal,
                        uf_end = @uf,
                        cidade_end = @cidade,
                        rua_end = @rua,
                        numero_end = @numero,
                        bairro_end = @bairro,
                        complemento_end = @complemento
                    WHERE (id_end = @idEnd);
                ";

                BindQuery(t, query);
                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O endereço não foi alterado. Verifique e tente novamente.");
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
