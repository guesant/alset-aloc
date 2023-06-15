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
    internal class PagamentoDAO : IDAO<Pagamento>
    {
        private Conexao conn;

        public PagamentoDAO()
        {
            conn = new Conexao();
        }

        static Pagamento ParseReader(MySqlDataReader dtReader)
        {
            var pagamento = new Pagamento();

            pagamento.Id = dtReader.GetInt64("id_pag");

            pagamento.Descricao = dtReader.GetString("descricao_pag");
            
            var rawValor = dtReader.GetOrdinal("valor_pag");
            pagamento.Valor = dtReader.IsDBNull(rawValor) ? null : dtReader.GetDouble(rawValor);

            var rawDataVencimento = dtReader.GetOrdinal("data_vencimento_pag");
            pagamento.DataVencimento = dtReader.IsDBNull(rawDataVencimento) ? null : dtReader.GetDateTime(rawDataVencimento);
            
            var rawDataCredenciamento = dtReader.GetOrdinal("data_credenciamento_pag");
            pagamento.DataCredenciamento = dtReader.IsDBNull(rawDataCredenciamento) ? null : dtReader.GetDateTime(rawDataCredenciamento);

            var rawCredor = dtReader.GetOrdinal("credor_pag");
            pagamento.Credor = dtReader.IsDBNull(rawCredor) ? null : dtReader.GetString(rawCredor);

            var rawParcelas = dtReader.GetOrdinal("parcelas_pag");
            pagamento.Parcelas = dtReader.IsDBNull(rawParcelas) ? null : dtReader.GetInt32(rawParcelas);

            var rawCompraId = dtReader.GetOrdinal("id_com_fk");
            pagamento.CompraId = dtReader.IsDBNull(rawCompraId) ? null : dtReader.GetInt64(rawCompraId);

            return pagamento;
        }

        static void BindQuery(Pagamento t, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@descricao", t.Descricao);
            query.Parameters.AddWithValue("@valor", t.Valor);
            query.Parameters.AddWithValue("@dataVencimento", t.DataVencimento);
            query.Parameters.AddWithValue("@dataCredenciamento", t.DataCredenciamento);
            query.Parameters.AddWithValue("@credor", t.Credor);
            query.Parameters.AddWithValue("@parcelas", t.Parcelas);
            
            query.Parameters.AddWithValue("@compraId", t.CompraId);
        }

        static void BindQueryId(long id, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@idPag", id);
        }

        public void Delete(Pagamento t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    DELETE FROM pagamento
                    WHERE (id_pag = @idPag)
                ";

                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O pagamento não foi encontrado. Verifique e tente novamente.");
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

        public Pagamento GetById(int id)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT (id_pag, descricao_pag, valor_pag, data_vencimento_pag, data_credenciamento_pag, credor_pag, parcelas_pag, id_com_fk)
                    FROM pagamento
                    WHERE (id_pag = @idPag)
                    ;
                ";

                BindQueryId(id, query);

                MySqlDataReader dtReader = query.ExecuteReader();

                while (dtReader.Read())
                {
                    var pagamento = ParseReader(dtReader);
                    return pagamento;
                }

                throw new Exception("Não foi possível encontrar o pagamento com o id fornecido. Verifique e tente novamente.");
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

        public void Insert(Pagamento t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    INSERT INTO 
                        pagamento (descricao_pag, valor_pag, data_vencimento_pag, data_credenciamento_pag, credor_pag, parcelas_pag, id_com_fk)
                    VALUES
                        (@descricao, @valor, @dataVencimento, @dataCredenciamento, @credor, @parcelas, @compraId)
                ";

                BindQuery(t, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O pagamento não foi cadastrado. Verifique e tente novamente.");
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

        public List<Pagamento> List()
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT (id_pag, descricao_pag, valor_pag, data_vencimento_pag, data_credenciamento_pag, credor_pag, parcelas_pag, id_com_fk)
                    FROM pagamento
                    ;
                ";


                MySqlDataReader dtReader = query.ExecuteReader();

                List<Pagamento> listaDeRetorno = new List<Pagamento>();

                while (dtReader.Read())
                {
                    var pagamento = ParseReader(dtReader);
                    listaDeRetorno.Add(pagamento);
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

        public void Update(Pagamento t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    UPDATE pagamento
                    SET
                        descricao_pag = @descricao,
                        valor_pag = @valor,
                        data_vencimento_pag = @dataVencimento,
                        data_credenciamento_pag = @dataCredenciamento,
                        credor_pag = @credor,
                        parcelas_pag = @parcelas,
                        id_com_fk = @compraId,
                    WHERE (id_pag = @idPag);
                ";

                BindQuery(t, query);
                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O pagamento não foi alterado. Verifique e tente novamente.");
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
