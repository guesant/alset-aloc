using alset_aloc.Database;
using alset_aloc.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace alset_aloc.Models
{
    class EnderecoDAO : IDAO<Endereco>
    {
        private Conexao conn;

        public EnderecoDAO()
        {
            conn = new Conexao();
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

                query.Parameters.AddWithValue("@idEnd", t.Id);

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
                    SELECT (id_end, pais_end, codigo_postal_end, uf_end, cidade_end, rua_end, numero_end, bairro_end, complemento_end)
                    FROM endereco
                    WHERE (id_end = @idEnd);
                ";

                query.Parameters.AddWithValue("@idEnd", id);

                MySqlDataReader dtReader = query.ExecuteReader();

                Endereco endereco = new Endereco();

                while (dtReader.Read())
                {

                    endereco.Id = Convert.ToInt64(dtReader["id_end"].ToString());

                    endereco.Pais = dtReader["pais_end"].ToString()!;
                    endereco.CodigoPostal = dtReader["codigo_postal_end"].ToString()!;
                    endereco.UF = dtReader["uf_end"].ToString()!;
                    endereco.Cidade = dtReader["cidade_end"].ToString()!;
                    endereco.Rua = dtReader["rua_end"].ToString()!;
                    endereco.Numero = Convert.ToInt32(dtReader["numero_end"].ToString()!);
                    endereco.Bairro = dtReader["bairro_end"].ToString()!;
                    endereco.Complemento = dtReader["complemento_end"].ToString()!;

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

                query.Parameters.AddWithValue("@pais", t.Pais);
                query.Parameters.AddWithValue("@codigoPostal", t.CodigoPostal);
                query.Parameters.AddWithValue("@uf", t.UF);
                query.Parameters.AddWithValue("@cidade", t.Cidade);
                query.Parameters.AddWithValue("@rua", t.Rua);
                query.Parameters.AddWithValue("@numero", t.Numero);
                query.Parameters.AddWithValue("@bairro", t.Bairro);
                query.Parameters.AddWithValue("@complemento", t.Complemento);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O endereço não foi cadastrado. Verifique e tente novamente.");
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

        public List<Endereco> List()
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT (id_end, pais_end, codigo_postal_end, uf_end, cidade_end, rua_end, numero_end, bairro_end, complemento_end)
                    FROM endereco
                    ;
                "
                ;

                MySqlDataReader dtReader = query.ExecuteReader();

                List<Endereco> listaDeRetorno = new List<Endereco>();//Crie uma lista de Cliente
               
                while (dtReader.Read())
                {
                    Endereco endereco = new Endereco();

                    endereco.Id = Convert.ToInt64(dtReader["id_end"].ToString());
                    endereco.Pais = dtReader["pais_end"].ToString()!;
                    endereco.CodigoPostal = dtReader["codigo_postal_end"].ToString()!;
                    endereco.UF = dtReader["uf_end"].ToString()!;
                    endereco.Cidade = dtReader["cidade_end"].ToString()!;
                    endereco.Rua = dtReader["rua_end"].ToString()!;
                    endereco.Numero = Convert.ToInt32(dtReader["numero_end"].ToString()!);
                    endereco.Bairro = dtReader["bairro_end"].ToString()!;
                    endereco.Complemento = dtReader["complemento_end"].ToString()!;

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

                query.Parameters.AddWithValue("@pais", t.Pais);
                query.Parameters.AddWithValue("@codigoPostal", t.CodigoPostal);
                query.Parameters.AddWithValue("@uf", t.UF);
                query.Parameters.AddWithValue("@cidade", t.Cidade);
                query.Parameters.AddWithValue("@rua", t.Rua);
                query.Parameters.AddWithValue("@numero", t.Numero);
                query.Parameters.AddWithValue("@bairro", t.Bairro);
                query.Parameters.AddWithValue("@complemento", t.Complemento);

                query.Parameters.AddWithValue("@idEnd", t.Id);

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
