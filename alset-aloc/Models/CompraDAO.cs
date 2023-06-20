using alset_aloc.Database;
using alset_aloc.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace alset_aloc.Models
{
  class CompraDAO : IDAO<Compra>
    {
        private Conexao conn;

        public CompraDAO()
        {
            conn = new Conexao();
        }

        static Compra ParseQuery(MySqlDataReader dtReader)
        {
            var compra = new Compra();

            compra.Id = dtReader.GetInt64("id_com");

            var rawDataCompra = dtReader.GetOrdinal("data_compra_com");
            compra.DataCompra = dtReader.IsDBNull(rawDataCompra) ? null : dtReader.GetDateTime(rawDataCompra);

            var rawNumeroNota = dtReader.GetOrdinal("numero_nota_com");
            compra.NumeroNota = dtReader.IsDBNull(rawNumeroNota) ? null : dtReader.GetString(rawNumeroNota);

            var rawProdutoId = dtReader.GetOrdinal("id_prod_fk");
            compra.ProdutoId = dtReader.IsDBNull(rawProdutoId) ? null : dtReader.GetInt64(rawProdutoId);

            var rawFornecedorId = dtReader.GetOrdinal("id_forn_fk");
            compra.FornecedorId = dtReader.IsDBNull(rawFornecedorId) ? null : dtReader.GetInt64(rawFornecedorId);

            return compra;
        }

        static void BindQuery(Compra t, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@dataCompra", t.DataCompra);
            query.Parameters.AddWithValue("@numeroNota", t.NumeroNota);

            query.Parameters.AddWithValue("@produtoId", t.ProdutoId);
            query.Parameters.AddWithValue("@fornecedorId", t.FornecedorId);
        }

        static void BindQueryId(long id, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@idCom", id);
        }

        public void Delete(Compra t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    DELETE FROM compra
                    WHERE (id_com = @idCom)
                ";

                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("A compra não foi encontrada. Verifique e tente novamente.");
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

        public Compra GetById(int id)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT id_com, data_compra_com, numero_nota_com, id_prod_fk, id_forn_fk
                    FROM compra
                    WHERE (id_com = @idCom);
                ";

                BindQueryId(id, query);

                MySqlDataReader dtReader = query.ExecuteReader();

                while (dtReader.Read())
                {
                    var compra = ParseQuery(dtReader);
                    return compra;
                }

                throw new Exception("Não foi possível encontrar a compra com o id fornecido. Verifique e tente novamente.");
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

        public void Insert(Compra t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    INSERT INTO 
                        compra (data_compra_com, numero_nota_com, id_prod_fk, id_forn_fk)
                    VALUES
                        (@dataCompra, @numeroNota, @produtoId, @fornecedorId)
                ";

                BindQuery(t, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("A compra não foi cadastrada. Verifique e tente novamente.");
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

        public List<Compra> List()
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT
                        (id_com, data_compra_com, numero_nota_com, id_prod_fk, id_forn_fk)
                    FROM compra
                    ;
                "
                ;

                MySqlDataReader dtReader = query.ExecuteReader();

                var listaDeRetorno = new List<Compra>();

                while (dtReader.Read())
                {
                    var compra = ParseQuery(dtReader);
                    listaDeRetorno.Add(compra);
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

        public void Update(Compra t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    UPDATE compra
                    SET
                        data_compra_com = @dataCompra,
                        numero_nota_com = @numeroNota,
                        id_prod_fk = @produtoId,
                        id_forn_fk = @fornecedorId
                    WHERE (id_com = @idCom);
                ";

                BindQuery(t, query);
                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("A compra não foi alterada. Verifique e tente novamente.");
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
