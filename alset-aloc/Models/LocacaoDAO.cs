using alset_aloc.Database;
using alset_aloc.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace alset_aloc.Models
{
  class LocacaoDAO : IDAO<Locacao>
    {
        private Conexao conn;

        public LocacaoDAO()
        {
            conn = new Conexao();
        }

        public void Delete(Locacao t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    DELETE FROM locacao
                    WHERE (id_loc = @idLoc)
                ";

                query.Parameters.AddWithValue("@idLoc", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("A locação não foi encontrada. Verifique e tente novamente.");
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

        public Locacao GetById(int id)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT (id_loc, data_locacao_loc, data_devolucao_prevista, data_devolucao_efetivada, status_loc)
                    FROM locacao
                    WHERE (id_loc = @idLoc);
                ";

                query.Parameters.AddWithValue("@idLoc", id);

                MySqlDataReader dtReader = query.ExecuteReader();

                Locacao locacao = new Locacao();

                while (dtReader.Read())
                {
                    locacao.Id = dtReader.GetInt64("id_loc");

                    locacao.DataLocacao = dtReader.GetDateTime("data_locacao_loc");
                    locacao.DataDevolucaoPrevista = dtReader.GetDateTime("data_devolucao_prevista");

                    var rawDataDevolucaoEfetivada = dtReader.GetOrdinal("data_devolucao_efetivada");

                    if (!dtReader.IsDBNull(rawDataDevolucaoEfetivada))
                    {
                        locacao.DataDevolucaoEfetivada = dtReader.GetDateTime(rawDataDevolucaoEfetivada);
                    }
                    else
                    {
                        locacao.DataDevolucaoEfetivada = null;
                    }

                    locacao.Status = dtReader.GetBoolean("status_loc");

                    var rawVeiculoId = dtReader.GetOrdinal("id_vei_fk");

                    if (!dtReader.IsDBNull(rawVeiculoId))
                    {
                        locacao.VeiculoId = dtReader.GetInt64(rawVeiculoId);
                    }
                    else
                    {
                        locacao.VeiculoId = null;
                    }

                    var rawFuncionarioId = dtReader.GetOrdinal("id_fun_fk");

                    if (!dtReader.IsDBNull(rawFuncionarioId))
                    {
                        locacao.FuncionarioId = dtReader.GetInt64(rawFuncionarioId);
                    }
                    else
                    {
                        locacao.FuncionarioId = null;
                    }

                    return locacao;
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

        public void Insert(Locacao t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    INSERT INTO 
                        locacao (data_locacao_loc, data_devolucao_prevista, data_devolucao_efetivada, status_loc, id_vei_fk, id_fun_fk)
                    VALUES
                        (@dataLocacao, @dataDevolucaoPrevista, @dataDevolucaoEfetivada, @status, @veiculoId, @funcionarioId)
                ";

                query.Parameters.AddWithValue("@dataLocacao", t.DataLocacao);
                query.Parameters.AddWithValue("@dataDevolucaoPrevista", t.DataDevolucaoPrevista);
                query.Parameters.AddWithValue("@dataDevolucaoEfetivada", t.DataDevolucaoEfetivada);
                query.Parameters.AddWithValue("@status", t.Status);
                query.Parameters.AddWithValue("@veiculoId", t.VeiculoId);
                query.Parameters.AddWithValue("@funcionarioId", t.FuncionarioId);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("A locação não foi cadastrada. Verifique e tente novamente.");
                }

                long locacaoId = query.LastInsertedId;

                t.Id = locacaoId;
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

        public List<Locacao> List()
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT (id_loc, data_locacao_loc, data_devolucao_prevista, data_devolucao_efetivada, status_loc)
                    FROM locacao
                    ;
                "
                ;

                MySqlDataReader dtReader = query.ExecuteReader();

                List<Locacao> listaDeRetorno = new List<Locacao>();

                while (dtReader.Read())
                {
                    Locacao locacao = new Locacao();

                    locacao.Id = dtReader.GetInt64("id_loc");

                    locacao.DataLocacao = dtReader.GetDateTime("data_locacao_loc");
                    locacao.DataDevolucaoPrevista = dtReader.GetDateTime("data_devolucao_prevista");

                    var rawDataDevolucaoEfetivada = dtReader.GetOrdinal("data_devolucao_efetivada");

                    if (!dtReader.IsDBNull(rawDataDevolucaoEfetivada))
                    {
                        locacao.DataDevolucaoEfetivada = dtReader.GetDateTime(rawDataDevolucaoEfetivada);
                    }
                    else
                    {
                        locacao.DataDevolucaoEfetivada = null;
                    }

                    locacao.Status = dtReader.GetBoolean("status_loc");

                    var rawVeiculoId = dtReader.GetOrdinal("id_vei_fk");

                    if (!dtReader.IsDBNull(rawVeiculoId))
                    {
                        locacao.VeiculoId = dtReader.GetInt64(rawVeiculoId);
                    }
                    else
                    {
                        locacao.VeiculoId = null;
                    }

                    var rawFuncionarioId = dtReader.GetOrdinal("id_fun_fk");

                    if (!dtReader.IsDBNull(rawFuncionarioId))
                    {
                        locacao.FuncionarioId = dtReader.GetInt64(rawFuncionarioId);
                    }
                    else
                    {
                        locacao.FuncionarioId = null;
                    }

                    listaDeRetorno.Add(locacao);
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

        public void Update(Locacao t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    UPDATE locacao
                    SET
                        data_locacao_loc = @dataLocacao,
                        data_devolucao_prevista = @dataDevolucaoPrevista,
                        data_devolucao_efetivada = @dataDevolucaoEfetivada,
                        status_loc = @status,
                        id_vei_fk = @veiculoId,
                        id_fun_fk = @funcionarioId
                    WHERE (id_loc = @idLoc);
                ";

                query.Parameters.AddWithValue("@dataLocacao", t.DataLocacao);
                query.Parameters.AddWithValue("@dataDevolucaoPrevista", t.DataDevolucaoPrevista);
                query.Parameters.AddWithValue("@dataDevolucaoEfetivada", t.DataDevolucaoEfetivada);
                query.Parameters.AddWithValue("@status", t.Status);
                query.Parameters.AddWithValue("@veiculoId", t.VeiculoId);
                query.Parameters.AddWithValue("@funcionarioId", t.FuncionarioId);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("A locação não foi alterada. Verifique e tente novamente.");
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
