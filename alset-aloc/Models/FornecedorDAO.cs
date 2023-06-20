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
    class FornecedorDAO : IDAO<Fornecedor>
    {
        private Conexao conn;

        public FornecedorDAO()
        {
            conn = new Conexao();
        }

        static Fornecedor ParseReader(MySqlDataReader dtReader)
        {
            Fornecedor fornecedor = new Fornecedor();

            fornecedor.Id = dtReader.GetInt64("id_forn");

            var rawCNPJ = dtReader.GetOrdinal("cnpj_forn");
            fornecedor.CNPJ = dtReader.IsDBNull(rawCNPJ) ? null : dtReader.GetString(rawCNPJ);

            var rawRazaoSocial = dtReader.GetOrdinal("razao_social_forn");
            fornecedor.RazaoSocial = dtReader.IsDBNull(rawRazaoSocial) ? null : dtReader.GetString(rawRazaoSocial);

            var rawNomeFantasia = dtReader.GetOrdinal("nome_fantasia_forn");
            fornecedor.NomeFantasia = dtReader.IsDBNull(rawNomeFantasia) ? null : dtReader.GetString(rawNomeFantasia);

            var rawEmail = dtReader.GetOrdinal("email_forn");
            fornecedor.Email = dtReader.IsDBNull(rawEmail) ? null : dtReader.GetString(rawEmail);

            var rawTelefone = dtReader.GetOrdinal("telefone_forn");
            fornecedor.Telefone = dtReader.IsDBNull(rawTelefone) ? null : dtReader.GetString(rawTelefone);

            var rawEnderecoId = dtReader.GetOrdinal("id_end_fk");
            fornecedor.EnderecoId = dtReader.IsDBNull(rawEnderecoId) ? null : dtReader.GetInt64(rawEnderecoId);

            return fornecedor;
        }

        static void BindQuery(Fornecedor t, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@cnpj", t.CNPJ);
            query.Parameters.AddWithValue("@razaoSocial", t.RazaoSocial);
            query.Parameters.AddWithValue("@nomeFantasia", t.NomeFantasia);
            query.Parameters.AddWithValue("@email", t.Email);
            query.Parameters.AddWithValue("@telefone", t.Telefone);
            
            query.Parameters.AddWithValue("@enderecoId", t.EnderecoId);
        }

        static void BindQueryId(long id, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@idForn", id);
        }

        public void Delete(Fornecedor t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    DELETE FROM fornecedor
                    WHERE (id_forn = @idForn)
                ";

                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O fornecedor não foi encontrado. Verifique e tente novamente.");
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

        public Fornecedor GetById(int id)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT (id_forn, cnpj_forn, razao_social_forn, nome_fantasia_forn, email_forn, telefone_forn, id_end_fk)
                    FROM fornecedor
                    WHERE (id_forn = @idForn)
                    ;
                ";

                BindQueryId(id, query);

                MySqlDataReader dtReader = query.ExecuteReader();

                while (dtReader.Read())
                {
                    Fornecedor fornecedor = ParseReader(dtReader);
                    return fornecedor;
                }

                throw new Exception("Não foi possível encontrar o fornecedor com o id fornecido. Verifique e tente novamente.");
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

        public void Insert(Fornecedor t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    INSERT INTO 
                        fornecedor
                        (cnpj_forn, razao_social_forn, nome_fantasia_forn, email_forn, telefone_forn, id_end_fk)
                    VALUES
                        (@cnpj, @razaoSocial, @nomeFantasia, @email, @telefone, @enderecoId)
                ";

                BindQuery(t, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O fornecedor não foi cadastrado. Verifique e tente novamente.");
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

        public List<Fornecedor> List()
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT id_forn, cnpj_forn, razao_social_forn, nome_fantasia_forn, email_forn, telefone_forn, id_end_fk
                    FROM fornecedor;";

                MySqlDataReader dtReader = query.ExecuteReader();

                List<Fornecedor> listaDeRetorno = new List<Fornecedor>();

                while (dtReader.Read())
                {
                    Fornecedor fornecedor = ParseReader(dtReader);
                    listaDeRetorno.Add(fornecedor);
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

        public void Update(Fornecedor t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    UPDATE fornecedor
                    SET
                        cnpj_forn = @cnpj,
                        razao_social_forn = @razaoSocial,
                        nome_fantasia_forn = @cpf,
                        email_forn = @rg,
                        telefone_forn = @cnh,
                        id_end_fk = @email
                    WHERE (id_forn = @idForn);
                ";

                BindQuery(t, query);
                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O fornecedor não foi alterado. Verifique e tente novamente.");
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
