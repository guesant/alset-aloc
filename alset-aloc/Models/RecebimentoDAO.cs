using alset_aloc.Database;
using alset_aloc.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace alset_aloc.Models
{
  internal class RecebimentoDAO : IDAO<Recebimento>
    {
        private Conexao conn;

        public RecebimentoDAO()
        {
            conn = new Conexao();
        }

        static Recebimento ParseReader(MySqlDataReader dtReader)
        {
            var recebimento = new Recebimento();

            recebimento.Id = dtReader.GetInt64("id_rec");

            recebimento.Descricao = dtReader.GetString("descricao_rec");

            var rawValor = dtReader.GetOrdinal("valor_rec");
            recebimento.Valor = dtReader.IsDBNull(rawValor) ? null : dtReader.GetDouble(rawValor);

            var rawDataVencimento = dtReader.GetOrdinal("data_vencimento_rec");
            recebimento.DataVencimento = dtReader.IsDBNull(rawDataVencimento) ? null : dtReader.GetDateTime(rawDataVencimento);

            var rawDataCredenciamento = dtReader.GetOrdinal("data_credenciamento_rec");
            recebimento.DataCredenciamento = dtReader.IsDBNull(rawDataCredenciamento) ? null : dtReader.GetDateTime(rawDataCredenciamento);

            var rawPagador = dtReader.GetOrdinal("pagador_rec");
            recebimento.Pagador = dtReader.IsDBNull(rawPagador) ? null : dtReader.GetString(rawPagador);

            var rawParcelas = dtReader.GetOrdinal("parcelas_rec");
            recebimento.Parcelas = dtReader.IsDBNull(rawParcelas) ? null : dtReader.GetInt32(rawParcelas);

            var rawLocacaoId = dtReader.GetOrdinal("id_loc_fk");
            recebimento.LocacaoId = dtReader.IsDBNull(rawLocacaoId) ? null : dtReader.GetInt64(rawLocacaoId);

            return recebimento;
        }

        static void BindQuery(Recebimento t, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@descricao", t.Descricao);
            query.Parameters.AddWithValue("@valor", t.Valor);
            query.Parameters.AddWithValue("@dataVencimento", t.DataVencimento);
            query.Parameters.AddWithValue("@dataCredenciamento", t.DataCredenciamento);
            query.Parameters.AddWithValue("@pagador", t.Pagador);
            query.Parameters.AddWithValue("@parcelas", t.Parcelas);

            query.Parameters.AddWithValue("@locacaoId", t.LocacaoId);
        }

        static void BindQueryId(long id, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@idRec", id);
        }

        public void Delete(Recebimento t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    DELETE FROM recebimento
                    WHERE (id_rec = @idRec)
                ";

                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O recebimento não foi encontrado. Verifique e tente novamente.");
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

        public Recebimento GetById(int id)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT id_rec, descricao_rec, valor_rec, data_vencimento_rec, data_credenciamento_rec, pagador_rec, parcelas_rec, id_loc_fk
                    FROM recebimento
                    WHERE (id_rec = @idRec)
                    ;
                ";

                BindQueryId(id, query);

                MySqlDataReader dtReader = query.ExecuteReader();

                while (dtReader.Read())
                {
                    var recebimento = ParseReader(dtReader);
                    return recebimento;
                }

                throw new Exception("Não foi possível encontrar o recebimento com o id fornecido. Verifique e tente novamente.");
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

        public void Insert(Recebimento t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    INSERT INTO 
                        recebimento (descricao_rec, valor_rec, data_vencimento_rec, data_credenciamento_rec, pagador_rec, parcelas_rec, id_loc_fk)
                    VALUES
                        (@descricao, @valor, @dataVencimento, @dataCredenciamento, @pagador, @parcelas, @locacaoId)
                ";

                BindQuery(t, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O recebimento não foi cadastrado. Verifique e tente novamente.");
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

        public List<Recebimento> List()
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT id_rec, descricao_rec, valor_rec, data_vencimento_rec, data_credenciamento_rec, pagador_rec, parcelas_rec, id_loc_fk
                    FROM recebimento
                    ;
                ";


                MySqlDataReader dtReader = query.ExecuteReader();

                List<Recebimento> listaDeRetorno = new List<Recebimento>();

                while (dtReader.Read())
                {
                    var recebimento = ParseReader(dtReader);
                    listaDeRetorno.Add(recebimento);
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

        public void Update(Recebimento t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    UPDATE recebimento
                    SET
                        descricao_rec = @descricao,
                        valor_rec = @valor,
                        data_vencimento_rec = @dataVencimento,
                        data_credenciamento_rec = @dataCredenciamento,
                        pagador_rec = @pagador,
                        parcelas_rec = @parcelas,
                        id_loc_fk = @locacaoId
                    WHERE (id_rec = @idRec);
                ";

                BindQuery(t, query);
                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O recebimento não foi alterado. Verifique e tente novamente.");
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
