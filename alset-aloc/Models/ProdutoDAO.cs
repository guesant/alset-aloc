using alset_aloc.Database;
using alset_aloc.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace alset_aloc.Models
{
  class ProdutoDAO : IDAO<Produto>
    {
        private Conexao conn;

        public ProdutoDAO()
        {
            conn = new Conexao();
        }

        static Produto ParseQuery(MySqlDataReader dtReader)
        {
            Produto produto = new Produto();

            produto.Id = dtReader.GetInt64("id_prod");

            produto.Nome = dtReader.GetString("nome_prod");
            produto.Preco = dtReader.GetDouble("preco_prod");
            produto.Estoque = dtReader.GetDouble("estoque_prod");

            return produto;
        }

        static void BindQuery(Produto t, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@nome", t.Nome);
            query.Parameters.AddWithValue("@preco", t.Preco);
            query.Parameters.AddWithValue("@estoque", t.Estoque);
        }

        static void BindQueryId(long id, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@idProd", id);
        }

        public void Delete(Produto t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    DELETE FROM produto
                    WHERE (id_prod = @idProd)
                ";

                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O produto não foi encontrado. Verifique e tente novamente.");
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

        public Produto GetById(int id)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT id_prod, nome_prod, preco_prod, estoque_prod
                    FROM produto
                    WHERE (id_prod = @idProd);
                ";

                BindQueryId(id, query);

                MySqlDataReader dtReader = query.ExecuteReader();

                while (dtReader.Read())
                {
                    Produto produto = ParseQuery(dtReader);                    
                    return produto;
                }

                throw new Exception("Não foi possível encontrar o produto com o id fornecido. Verifique e tente novamente.");
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

        public void Insert(Produto t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    INSERT INTO 
                        produto (nome_prod, preco_prod, estoque_prod)
                    VALUES
                        (@nome, @preco, @estoque)
                ";

                BindQuery(t, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O produto não foi cadastrado. Verifique e tente novamente.");
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

        public List<Produto> List()
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT id_prod, nome_prod, preco_prod, estoque_prod
                    FROM produto
                    ;
                "
                ;

                MySqlDataReader dtReader = query.ExecuteReader();

                List<Produto> listaDeRetorno = new List<Produto>();

                while (dtReader.Read())
                {
                    Produto produto = ParseQuery(dtReader);
                    listaDeRetorno.Add(produto);
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

        public void Update(Produto t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    UPDATE produto
                    SET
                        nome_prod = @nome,
                        preco_prod = @preco,
                        estoque_prod = @estoque
                    WHERE (id_prod = @idProd);
                ";

                BindQuery(t, query);
                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O produto não foi alterado. Verifique e tente novamente.");
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
