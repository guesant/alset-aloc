using alset_aloc.Database;
using alset_aloc.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace alset_aloc.Models
{
  class VeiculoDAO : IDAO<Veiculo>
    {
        private Conexao conn;

        public VeiculoDAO()
        {
            conn = new Conexao();
        }

        static Veiculo ParseQuery(MySqlDataReader dtReader)
        {
            Veiculo veiculo = new Veiculo();

            veiculo.Id = dtReader.GetInt64("id_vei");

            veiculo.Modelo = dtReader.GetString("modelo_vei");
            veiculo.Marca = dtReader.GetString("marca_vei");
            veiculo.Ano = dtReader.GetInt32("ano_vei");
            veiculo.Placa = dtReader.GetString("placa_vei");
            veiculo.NumeroChassi = dtReader.GetString("numero_chassi_vei");
            veiculo.Cor = dtReader.GetString("cor_vei");
            veiculo.DataCompra = dtReader.GetDateTime("data_compra_vei");
            veiculo.Descricao = dtReader.GetString("descricao_vei");

            return veiculo;
        }

        static void BindQuery(Veiculo t, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@modelo", t.Modelo);
            query.Parameters.AddWithValue("@marca", t.Marca);
            query.Parameters.AddWithValue("@ano", t.Ano);
            query.Parameters.AddWithValue("@placa", t.Placa);
            query.Parameters.AddWithValue("@numeroChassi", t.NumeroChassi);
            query.Parameters.AddWithValue("@cor", t.Cor);
            query.Parameters.AddWithValue("@dataCompra", t.DataCompra);
            query.Parameters.AddWithValue("@descricao", t.Descricao);
        }

        static void BindQueryId(long id, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@idVei", id);
        }

        public void Delete(Veiculo t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    DELETE FROM veiculo
                    WHERE (id_vei = @idVei)
                ";

                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O veiculo não foi encontrado. Verifique e tente novamente.");
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

        public Veiculo GetById(int id)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT (id_vei, modelo_vei, marca_vei, ano_vei, placa_vei, numero_chassi_vei, cor_vei, data_compra_vei, descricao_vei)
                    FROM veiculo
                    WHERE (id_vei = @idVei)
                    ;
                ";

                BindQueryId(id, query);

                MySqlDataReader dtReader = query.ExecuteReader();

                while (dtReader.Read())
                {
                    Veiculo veiculo = ParseQuery(dtReader);
                    return veiculo;
                }

                throw new Exception("Não foi possível encontrar o veículo com o id fornecido. Verifique e tente novamente.");
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

        public void Insert(Veiculo t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    INSERT INTO 
                        veiculo (modelo_vei, marca_vei, ano_vei, placa_vei, numero_chassi_vei, cor_vei, data_compra_vei, descricao_vei)
                    VALUES
                        (@modelo, @marca, @ano, @placa, @numeroChassi, @cor, @dataCompra, @descricao)
                ";

                BindQuery(t, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O veículo não foi cadastrado. Verifique e tente novamente.");
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

        public List<Veiculo> List()
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT (id_vei, modelo_vei, marca_vei, ano_vei, placa_vei, numero_chassi_vei, cor_vei, data_compra_vei, descricao_vei)
                    FROM veiculo
                    ;
                "
                ;

                MySqlDataReader dtReader = query.ExecuteReader();

                List<Veiculo> listaDeRetorno = new List<Veiculo>();

                while (dtReader.Read())
                {
                    Veiculo veiculo = ParseQuery(dtReader);
                    listaDeRetorno.Add(veiculo);
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

        public void Update(Veiculo t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    UPDATE veiculo
                    SET
                        modelo_vei = @modelo,
                        marca_vei = @marca,
                        ano_vei = @ano,
                        placa_vei = @placa,
                        numero_chassi_vei = @numeroChassi,
                        cor_vei = @cor,
                        data_compra_vei = @dataCompra,
                        descricao_vei = @descricao
                    WHERE 
                        (id_vei = @idVei);
                ";

                BindQuery(t, query);
                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O veículo não foi alterado. Verifique e tente novamente.");
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
