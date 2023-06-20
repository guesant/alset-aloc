using alset_aloc.Database;
using alset_aloc.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace alset_aloc.Models
{
  internal class ParcelaDAO : IDAO<Parcela>
    {
        private Conexao conn;

        public ParcelaDAO()
        {
            conn = new Conexao();
        }

        static Parcela ParseReader(MySqlDataReader dtReader)
        {
            var parcela = new Parcela();

            parcela.Id = dtReader.GetInt64("id_par");

            parcela.DataVencimento = dtReader.GetDateTime("data_vencimento_par");

            parcela.Valor = dtReader.GetDouble("valor_par");

            var rawFormaPagamento = dtReader.GetOrdinal("forma_pagamento_par");
            parcela.FormaPagamento = dtReader.IsDBNull(rawFormaPagamento) ? null : dtReader.GetString(rawFormaPagamento);

            var rawNumeroParcela = dtReader.GetOrdinal("numero_parcela_par");
            parcela.Numero = dtReader.IsDBNull(rawNumeroParcela) ? null : dtReader.GetInt32(rawNumeroParcela);

            var rawTipoParcela = dtReader.GetOrdinal("tipo_parcela_par");
            parcela.Tipo = dtReader.IsDBNull(rawTipoParcela) ? null : dtReader.GetString(rawTipoParcela);
            
            var rawStatus = dtReader.GetOrdinal("status_par");
            parcela.Status = dtReader.IsDBNull(rawStatus) ? null : dtReader.GetBoolean(rawStatus);

            var rawRecebimentoId = dtReader.GetOrdinal("id_rec_fk");
            parcela.RecebimentoId = dtReader.IsDBNull(rawRecebimentoId) ? null : dtReader.GetInt64(rawRecebimentoId);

            var rawPagamentoId = dtReader.GetOrdinal("id_rec_fk");
            parcela.PagamentoId = dtReader.IsDBNull(rawPagamentoId) ? null : dtReader.GetInt64(rawPagamentoId);

            return parcela;
        }

        static void BindQuery(Parcela t, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@dataVencimento", t.DataVencimento);
            query.Parameters.AddWithValue("@valor", t.Valor);
            query.Parameters.AddWithValue("@formaPagamento", t.FormaPagamento);
            query.Parameters.AddWithValue("@numero", t.Numero);
            query.Parameters.AddWithValue("@tipo", t.Tipo);
            query.Parameters.AddWithValue("@status", t.Status);

            query.Parameters.AddWithValue("@recebimentoId", t.RecebimentoId);
            query.Parameters.AddWithValue("@pagamentoId", t.PagamentoId);
        }

        static void BindQueryId(long id, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@idPar", id);
        }

        public void Delete(Parcela t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    DELETE FROM parcela
                    WHERE (id_par = @idPar)
                ";

                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("A parcela não foi encontrada. Verifique e tente novamente.");
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

        public Parcela GetById(int id)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT id_par, data_vencimento_par, valor_par, forma_pagamento_par, numero_parcela_par, tipo_parcela_par, status_par, id_rec_fk, id_pag_fk
                    FROM parcela
                    WHERE (id_par = @idPar)
                    ;
                ";

                BindQueryId(id, query);

                MySqlDataReader dtReader = query.ExecuteReader();

                while (dtReader.Read())
                {
                    var parcela = ParseReader(dtReader);
                    return parcela;
                }

                throw new Exception("Não foi possível encontrar a parcela com o id fornecido. Verifique e tente novamente.");
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

        public void Insert(Parcela t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    INSERT INTO 
                        parcela (data_vencimento_par, valor_par, forma_pagamento_par, numero_parcela_par, tipo_parcela_par, status_par, id_rec_fk, id_pag_fk)
                    VALUES
                        (@dataVencimento, @valor, @formaPagamento, @numero, @tipo, @status, @recebimentoId, @pagamentoId)
                ";

                BindQuery(t, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("A parcela não foi cadastrada. Verifique e tente novamente.");
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

        public List<Parcela> List()
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT id_par, data_vencimento_par, valor_par, forma_pagamento_par, numero_parcela_par, tipo_parcela_par, status_par, id_rec_fk, id_pag_fk
                    FROM parcela
                    ;
                ";


                MySqlDataReader dtReader = query.ExecuteReader();

                var listaDeRetorno = new List<Parcela>();

                while (dtReader.Read())
                {
                    var parcela = ParseReader(dtReader);
                    listaDeRetorno.Add(parcela);
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

        public void Update(Parcela t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    UPDATE parcela
                    SET
                        data_vencimento_par = @dataVencimento,
                        valor_par = @valor,
                        forma_pagamento_par = @formaPagamento,
                        numero_parcela_par = @numero,
                        tipo_parcela_par = @tipo,
                        status_par = @status,
                        id_rec_fk = @recebimentoId,
                        id_pag_fk = @pagamentoId,
                    WHERE (id_par = @idPar);
                ";

                BindQuery(t, query);
                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("A parcela não foi alterada. Verifique e tente novamente.");
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
