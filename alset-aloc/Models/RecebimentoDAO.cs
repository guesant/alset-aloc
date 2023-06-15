using alset_aloc.Database;
using alset_aloc.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace alset_aloc.Models
{
     class RecebimentoDAO : IDAO<Recebimento>
        {
        private Conexao conn;

        public RecebimentoDAO()
            {
            conn = new Conexao();
            }

        static Recebimento ParseQuery(MySqlDataReader dtReader)
            {
            Recebimento recebimento = new Recebimento();

            recebimento.Id = dtReader.GetInt64("id_prod");

            recebimento.Descricao = dtReader.GetString("descricao_rec");
            recebimento.Valor = dtReader.GetDouble("valor_rec");
            recebimento.Data_Vencimento = dtReader.GetDateTime("data_vencimento_rec");
            recebimento.Data_Credenciamento = dtReader.GetDateTime("data_credenciamento_rec");
            recebimento.Pagador = dtReader.GetString("pagador_rec");
            recebimento.Parcelas = dtReader.GetInt64("parcelas_rec");
            recebimento.LocacaoId = dtReader.GetInt64("id_loc_fk");

            return recebimento;
            }

        static void BindQuery(Recebimento t , MySqlCommand query)
            {
            query.Parameters.AddWithValue("@descricaoRec" , t.Descricao);
            query.Parameters.AddWithValue("@valor" , t.Valor);
            query.Parameters.AddWithValue("@dataVencimento" , t.Data_Vencimento);
            query.Parameters.AddWithValue("@dataCredenciado" , t.Data_Credenciamento);
            query.Parameters.AddWithValue("@pagador" , t.Pagador);
            query.Parameters.AddWithValue("@parcelas" , t.Pagador);
            query.Parameters.AddWithValue("@locacaoId" , t.LocacaoId);
            }

        static void BindQueryId(long id , MySqlCommand query)
            {
            query.Parameters.AddWithValue("@idRec" , id);
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

                BindQueryId(t.Id , query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                    {
                    throw new Exception("O Recebimento não foi encontrado. Verifique e tente novamente.");
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
                    SELECT (id_rec,descricao_rec, valor_rec, data_vencimento_rec,data_credenciamento_rec,pagador_rec,id_loc_fk)
                    FROM recebimento
                    WHERE (id_rec = @idRec);
                ";

                BindQueryId(id , query);

                MySqlDataReader dtReader = query.ExecuteReader();

                while (dtReader.Read())
                    {
                    Recebimento recebimento = ParseQuery(dtReader);
                    return recebimento;
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

        public void Insert(Recebimento t)
            {
            try
                {
                var query = conn.Query();

                
                query.CommandText = @"
                    INSERT INTO 
                        produto (descricao_rec ,valor_rec,data_vencimento_rec,data_credenciamento_rec,pagador_rec,parcelas_rec,id_loc_fk)
                    VALUES
                        (@descricaoRec, @valor, @dataVencimento,@dataCredenciado,@pagador,@parcelas,@locacaoId)
                ";

                BindQuery(t , query);

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
                    SELECT (id_rec, descricao_rec, valor_rec, data_vencimento_rec , data_credenciamento_rec , pagador_rec, parcelas_rec, id_loc_fk)
                    FROM produto
                    ;
                "
                ;

                MySqlDataReader dtReader = query.ExecuteReader();

                List<Recebimento> listaDeRetorno = new List<Recebimento>();

                while (dtReader.Read())
                    {
                    Recebimento recebimento = ParseQuery(dtReader);
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

                /*descricao_rec
                valor_rec
                data_vencimento_rec
                data_credenciamento_rec
                pagador_rec
                parcelas_rec
                id_loc_fk

                @descricaoRec
                @valor 
                @dataVencimento" 
                @dataCredenciado" 
                @pagador" 
                @locacaoId" */

                query.CommandText = @"
                    UPDATE recebimento
                    SET
                        descricao_rec = @descricaoRec,
                        valor_rec = @valor,
                        data_vencimento_rec = @dataVencimento
                        data_credenciamento_rec = @dataCredenciado
                        pagador_rec = @pagador
                        parcelas_rec = @parcelas
                        id_loc_fk =@locacaoId

                    WHERE (id_prod = @idProd);
                ";

                BindQuery(t , query);
                BindQueryId(t.Id , query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                    {
                    throw new Exception("O Recebimento não foi alterado. Verifique e tente novamente.");
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

 

        

