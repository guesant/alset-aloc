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
    class ProdutoDAO : IDAO<Produto>
    {
        private Conexao conn;

        public ProdutoDAO()
        {
            conn = new Conexao();
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

                query.Parameters.AddWithValue("@idProd", t.Id);

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
                    SELECT (id_prod, nome_prod, preco_prod, estoque_prod)
                    FROM produto
                    WHERE (id_prod = @idProd);
                ";

                query.Parameters.AddWithValue("@idProd", id);

                MySqlDataReader dtReader = query.ExecuteReader();

                Produto produto = new Produto();

                while (dtReader.Read())
                {

                    produto.Id = Convert.ToInt64(dtReader["id_prod"].ToString());

                    produto.Nome = dtReader["nome_prod"].ToString()!;
                    produto.Preco = Convert.ToDouble(dtReader["preco_prod"].ToString()!);
                    produto.Estoque = Convert.ToDouble(dtReader["estoque_prod"].ToString()!);
                    
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

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@preco", t.Preco);
                query.Parameters.AddWithValue("@estoque", t.Estoque);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O produto não foi cadastrado. Verifique e tente novamente.");
                }

                long produtoId = query.LastInsertedId;

                t.Id = produtoId;
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
                    SELECT (id_prod, nome_prod, preco_prod, estoque_prod)
                    FROM produto
                    ;
                "
                ;

                MySqlDataReader dtReader = query.ExecuteReader();

                List<Produto> listaDeRetorno = new List<Produto>();//Crie uma lista de Cliente

                while (dtReader.Read())
                {
                    Produto produto = new Produto();


                    produto.Id = Convert.ToInt64(dtReader["id_prod"].ToString());

                    produto.Nome = dtReader["nome_prod"].ToString()!;
                    produto.Preco = Convert.ToDouble(dtReader["preco_prod"].ToString()!);
                    produto.Estoque = Convert.ToDouble(dtReader["estoque_prod"].ToString()!);

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

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@preco", t.Preco);
                query.Parameters.AddWithValue("@estoque", t.Estoque);

                query.Parameters.AddWithValue("@idProd", t.Id);

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
